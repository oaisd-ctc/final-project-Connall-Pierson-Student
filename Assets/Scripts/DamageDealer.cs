using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damage = 25f;
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.GetComponent<Health>() != null && other.gameObject.layer != gameObject.layer)
        {
            other.gameObject.GetComponent<Health>().Damage(damage, gameObject.GetComponent<ProjectileBehavior>().projectileOwner);
            if(GetComponent<ProjectileBehavior>() != null)
            {
                Destroy(gameObject);
            }
        }
    }  
}
