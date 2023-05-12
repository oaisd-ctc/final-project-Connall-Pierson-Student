using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float projectileSpeed = 30f;
    Rigidbody2D rb;
    [HideInInspector] public PlayerController projectileOwner;
    Vector2 currentVelocity;
    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update() 
    {
        currentVelocity = rb.velocity;
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag != "Fence")
            Destroy(gameObject);
        else
        {
            Vector2 inDirection = currentVelocity;
            Vector2 inNormal = other.contacts[0].normal;
            Vector2 newVelocity;
            if(other.contacts[0].normal.x < -Mathf.Epsilon)
            {
                //Debug.Log("Hit Side");
                transform.rotation = Quaternion.Inverse(transform.rotation);
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                //Debug.Log("Hit Top/Bottom");
                transform.rotation = Quaternion.Inverse(transform.rotation);
                transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            }
            newVelocity = Vector2.Reflect(inDirection, inNormal);
            rb.velocity = newVelocity;
        }
    }
}
