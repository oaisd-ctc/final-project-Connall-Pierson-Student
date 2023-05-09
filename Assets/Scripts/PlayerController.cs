using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public int playerNum = 1;

    [Header("Tank Parts")]
    public GameObject tankGun;
    public GameObject tankBody;
    public GameObject projectile;
    public GameObject tankGunFront;

    [HideInInspector] public Vector2 rawInputVector;
    Rigidbody2D rb;
    bool isMovingAtMaxSpeedX;
    bool isMovingAtMaxSpeedY;

    [Header("Tank Stats")]
    [SerializeField] float tankAccelerationSpeed = 5f;
    [SerializeField] float tankRotationSpeed = 5f;
    [SerializeField] float tankSpeedThreshold = 5f;
    [SerializeField] float tankSize = 1f;
    public float tankProjectileSpeed = 6f;
    public float tankFireRate = 4f;
    public PlayerStats playerStats;
    Vector3 prevMousePosition;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        prevMousePosition = new Vector3(0, 0, 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if(playerNum == 1)
            playerStats = FindObjectsOfType<PlayerStats>()[0];
        else if(playerNum == 2)
            playerStats = FindObjectsOfType<PlayerStats>()[1];
        else
            Debug.Log("Something went wrong.");
        
        if(playerStats.playerSprite != null)
            spriteRenderer.sprite = playerStats.playerSprite.sprite;

        tankAccelerationSpeed = 5f + (playerStats.playerSpeedSkillPointsSpent * 5f);
        tankSize = 1f - (playerStats.playerSizeSkillPointsSpent/14);
        tankProjectileSpeed = (2f * playerStats.projectileSpeedSkillPointsSpent) + 5f;
        tankFireRate = 2f + (playerStats.fireRateSkillPointsSpent); 

        //Apply any variables that need to be applied
        gameObject.transform.localScale = new Vector3(tankSize, tankSize, 1f);
    }
    void Update()
    {
        Move();
        RotateGun();
    }

    void Move()
    {
        if (rb.velocity.x < tankSpeedThreshold && rb.velocity.x > -tankSpeedThreshold)
        {
            isMovingAtMaxSpeedX = false;
            rb.velocity += rawInputVector * Time.deltaTime * tankAccelerationSpeed;
        }
        else if (!isMovingAtMaxSpeedX)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * tankSpeedThreshold, rb.velocity.y);
            isMovingAtMaxSpeedX = true;
        }

        if (rb.velocity.y < tankSpeedThreshold && rb.velocity.y > -tankSpeedThreshold)
        {
            isMovingAtMaxSpeedY = false;
            rb.velocity += rawInputVector * Time.deltaTime * tankAccelerationSpeed;
        }
        else if (!isMovingAtMaxSpeedY)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * tankSpeedThreshold);
            isMovingAtMaxSpeedY = true;
        }
    }

    void RotateGun()
    {
        //player one controls
        if (playerNum == 1)
        {
            if ((Keyboard.current.qKey.isPressed && !Keyboard.current.eKey.isPressed) || (Keyboard.current.leftShiftKey.isPressed && !Keyboard.current.spaceKey.isPressed))
            {
                tankGun.transform.RotateAround(transform.position, Vector3.forward, tankRotationSpeed * Time.deltaTime * 30);
            }
            else if ((Keyboard.current.eKey.isPressed && !Keyboard.current.qKey.isPressed) || (Keyboard.current.spaceKey.isPressed && !Keyboard.current.leftShiftKey.isPressed))
            {
                tankGun.transform.RotateAround(transform.position, Vector3.forward, -tankRotationSpeed * Time.deltaTime * 30);
            }
        }
        //player two controls
        else
        {
            if ((Keyboard.current.uKey.isPressed && !Keyboard.current.oKey.isPressed) || (Gamepad.current != null && Gamepad.current.leftShoulder.isPressed && !Gamepad.current.rightShoulder.isPressed))
            {
                tankGun.transform.RotateAround(transform.position, Vector3.forward, tankRotationSpeed * Time.deltaTime * 30);
            }
            else if (Keyboard.current.oKey.isPressed && !Keyboard.current.uKey.isPressed || (Gamepad.current != null && Gamepad.current.rightShoulder.isPressed && !Gamepad.current.leftShoulder.isPressed))
            {
                tankGun.transform.RotateAround(transform.position, Vector3.forward, -tankRotationSpeed * Time.deltaTime * 30);
            }
        }
    }

    public void Respawn(Vector3 pos)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = pos;
        Debug.Log(pos);
        GetComponent<Health>().ResetHealth();
    }
}
