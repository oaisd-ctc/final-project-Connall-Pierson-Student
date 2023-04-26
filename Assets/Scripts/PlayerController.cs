using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        //RotateBody();
    }

    void Move()
    {
        if (rb.velocity.magnitude < tankSpeedThreshold)
            rb.velocity += rawInputVector * Time.deltaTime * tankAccelerationSpeed;
        else
        {
            rb.velocity = new Vector3(tankSpeedThreshold, tankSpeedThreshold);
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
