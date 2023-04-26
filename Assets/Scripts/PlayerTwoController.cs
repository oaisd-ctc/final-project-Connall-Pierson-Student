using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTwoController : MonoBehaviour
{
    [Header("Tank Parts")]
    [SerializeField] GameObject tankGun;
    [SerializeField] GameObject tankBody;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject tankGunFront;
    Vector2 rawInputVector;
    Rigidbody2D rb;

    [Header("Tank Stats")]
    [SerializeField] float tankAccelerationSpeed = 5f;
    [SerializeField] float tankRotationSpeed = 5f;
    [SerializeField] float tankSpeedThreshold = 5f;
    [SerializeField] float health = 100f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= Mathf.Epsilon)
        {
            Destroy(gameObject);
        }
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
            if (Keyboard.current.uKey.isPressed && !Keyboard.current.oKey.isPressed)
            {
                tankGun.transform.RotateAround(transform.position, Vector3.forward, tankRotationSpeed * Time.deltaTime * 30);
            }
            else if (Keyboard.current.oKey.isPressed && !Keyboard.current.uKey.isPressed)
            {
                tankGun.transform.RotateAround(transform.position, Vector3.forward, -tankRotationSpeed * Time.deltaTime * 30);
            }
    }
    // void RotateBody()
    // {
    //     rawInputVectorAngle = Mathf.Abs(Mathf.Rad2Deg * Mathf.Atan(rawInputVector.y / rawInputVector.x));

    //     currentFacingAngle = Mathf.Rad2Deg * Mathf.Atan(
    //         Vector3.Distance(tankFront.transform.position, new Vector3(tankFront.transform.position.x, tankBody.transform.position.y, 0)) /
    //         Vector3.Distance(tankBody.transform.position, new Vector3(tankFront.transform.position.x, tankBody.transform.position.y, 0)));

    // }

    void OnMovePlayerTwo(InputValue value)
    {
        rawInputVector = value.Get<Vector2>();
    }

    void OnFirePlayerTwo(InputValue value)
    {
        GameObject projectileCreated = Instantiate(projectile, tankGunFront.transform.position, tankGun.transform.rotation);
        projectileCreated.layer = gameObject.layer;
        if (projectileCreated.GetComponent<Rigidbody2D>() != null)
        {
            var projectileRigidBody = projectileCreated.GetComponent<Rigidbody2D>();
            projectileRigidBody.velocity = new Vector2(
                (tankGunFront.transform.position.x - tankBody.transform.position.x) * projectileCreated.GetComponent<ProjectileBehavior>().projectileSpeed,
                (tankGunFront.transform.position.y - tankBody.transform.position.y) * projectileCreated.GetComponent<ProjectileBehavior>().projectileSpeed);

        }
    }
}
