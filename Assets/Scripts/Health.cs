using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Slider healthSlider;
    public float health = 100f;

    void Awake()
    {
        healthSlider.value = health;
    }
    public void Damage(float damage)
    {
        health -= damage;
        healthSlider.value = health / 100f;
        if (health < Mathf.Epsilon)
        {
            healthSlider.value = health;
            Destroy(gameObject);
        }
    }
}
