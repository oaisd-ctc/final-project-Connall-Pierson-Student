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
        health -= damage;
        StartCoroutine("PlayerHitVisualEffect");
        healthSlider.value = health / 100f;
        if (health < Mathf.Epsilon)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            healthSlider.value = health;
            gameManager.WinRound(dealer);
            gameObject.SetActive(false);
            GetComponent<PlayerController>().isAlive = false;
        }
    }

    IEnumerator PlayerHitVisualEffect()
    {
        Debug.Log("visual effect");
        if(GetComponentInChildren<SpriteRenderer>() != null)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.2f);
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }
}
