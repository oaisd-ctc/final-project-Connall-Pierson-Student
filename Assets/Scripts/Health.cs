using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;

    public void Damage(float damage)
    {
        health -= damage;
        if(health < Mathf.Epsilon)
        {
            Destroy(gameObject);
        }
    }
}
