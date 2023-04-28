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
    public float fireRate = 4f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

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
            if (Keyboard.current.qKey.isPressed && !Keyboard.current.eKey.isPressed)
            {
                tankGun.transform.RotateAround(transform.position, Vector3.forward, tankRotationSpeed * Time.deltaTime * 30);
            }
            else if (Keyboard.current.eKey.isPressed && !Keyboard.current.qKey.isPressed)
            {
                tankGun.transform.RotateAround(transform.position, Vector3.forward, -tankRotationSpeed * Time.deltaTime * 30);
            }
        }
        //player two controls
        else
        {
            if (Keyboard.current.uKey.isPressed && !Keyboard.current.oKey.isPressed)
            {
                tankGun.transform.RotateAround(transform.position, Vector3.forward, tankRotationSpeed * Time.deltaTime * 30);
            }
            else if (Keyboard.current.oKey.isPressed && !Keyboard.current.uKey.isPressed)
            {
                tankGun.transform.RotateAround(transform.position, Vector3.forward, -tankRotationSpeed * Time.deltaTime * 30);
            }
        }
    }
}