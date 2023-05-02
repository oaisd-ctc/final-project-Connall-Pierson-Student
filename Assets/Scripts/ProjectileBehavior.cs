using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float projectileSpeed = 30f;
    Rigidbody2D rb;
    [HideInInspector] public PlayerController projectileOwner;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag != "Fence")
            Destroy(gameObject);
    }
}
