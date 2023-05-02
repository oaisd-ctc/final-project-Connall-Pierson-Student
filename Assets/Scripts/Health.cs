using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    public float health = 100f;

    void Start()
    {
        healthSlider.value = health;
    }

    public void ResetHealth()
    {
        health = 100f;
        healthSlider.value = health;
    }
    public void Damage(float damage, PlayerController dealer)
    {
        Debug.Log(dealer);
        health -= damage;
        healthSlider.value = health / 100f;
        if (health < Mathf.Epsilon)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            healthSlider.value = health;
            gameManager.WinRound(dealer);
            gameObject.SetActive(false);
        }
    }
}
