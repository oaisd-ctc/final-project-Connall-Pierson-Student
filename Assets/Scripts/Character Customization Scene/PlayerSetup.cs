using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetup : MonoBehaviour
{
    public PlayerController playerOne;
    public PlayerController playerTwo;

    public Sprite playerOneProjectileSprite;
    public Sprite playerTwoProjectileSprite;

    Coroutine FiringCoroutinePlayerOne;
    Coroutine FiringCoroutinePlayerTwo;
    bool IsFiringPlayerOne;
    bool IsFiringPlayerTwo;
    float playerOneFiringCooldown = -1f;
    float playerTwoFiringCooldown = -1f;

    void Update()
    {
        Move();
        Fire();
        TickCooldowns();
    }

    void TickCooldowns()
    {
        playerOneFiringCooldown -= Time.deltaTime;
        playerTwoFiringCooldown -= Time.deltaTime;
    }

    void Move()
    {
        playerOne.rawInputVector = new Vector2(0f, 0f);
        playerTwo.rawInputVector = new Vector2(0f, 0f);
        //Player One
        if(Keyboard.current.wKey.isPressed && Keyboard.current.dKey.isPressed)
            playerOne.rawInputVector = new Vector2(0.71f, 0.71f);
        else if(Keyboard.current.wKey.isPressed && Keyboard.current.aKey.isPressed)
            playerOne.rawInputVector = new Vector2(-0.71f, 0.71f);
        else if(Keyboard.current.sKey.isPressed && Keyboard.current.aKey.isPressed)
            playerOne.rawInputVector = new Vector2(-0.71f, -0.71f);
        else if(Keyboard.current.sKey.isPressed && Keyboard.current.dKey.isPressed)
            playerOne.rawInputVector = new Vector2(0.71f, -0.71f);
        else if(Keyboard.current.wKey.isPressed)
            playerOne.rawInputVector = new Vector2(0f, 1f);
        else if(Keyboard.current.aKey.isPressed)
            playerOne.rawInputVector = new Vector2(-1f, 0f);
        else if(Keyboard.current.sKey.isPressed)
            playerOne.rawInputVector = new Vector2(0f, -1f);
        else if(Keyboard.current.dKey.isPressed)
            playerOne.rawInputVector = new Vector2(1f, 0f);

            //Player Two (Keyboard)
        if(Keyboard.current.iKey.isPressed && Keyboard.current.lKey.isPressed)
            playerTwo.rawInputVector = new Vector2(0.71f, 0.71f);
        else if(Keyboard.current.iKey.isPressed && Keyboard.current.jKey.isPressed)
            playerTwo.rawInputVector = new Vector2(-0.71f, 0.71f);
        else if(Keyboard.current.kKey.isPressed && Keyboard.current.jKey.isPressed)
            playerTwo.rawInputVector = new Vector2(-0.71f, -0.71f);
        else if(Keyboard.current.kKey.isPressed && Keyboard.current.lKey.isPressed)
            playerTwo.rawInputVector = new Vector2(0.71f, -0.71f);
        else if(Keyboard.current.iKey.isPressed)
            playerTwo.rawInputVector = new Vector2(0f, 1f);
        else if(Keyboard.current.jKey.isPressed)
            playerTwo.rawInputVector = new Vector2(-1f, 0f);
        else if(Keyboard.current.kKey.isPressed)
            playerTwo.rawInputVector = new Vector2(0f, -1f);
        else if(Keyboard.current.lKey.isPressed)
            playerTwo.rawInputVector = new Vector2(1f, 0f);

        //Player Two (Controller)
        if(Gamepad.current != null)
            playerTwo.rawInputVector = Gamepad.current.leftStick.ReadValue();
    }
    // void OnMove(InputValue value)
    // {
    //     playerOne.rawInputVector = value.Get<Vector2>();
    // }
    // void OnMovePlayerTwo(InputValue value)
    // {
    //     playerTwo.rawInputVector = value.Get<Vector2>();
    // }

    void Fire()
    {
        //Player one
        if ((Keyboard.current.fKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame) && FiringCoroutinePlayerOne == null && playerOneFiringCooldown <= 0 && playerOne.isAlive)
        {
            IsFiringPlayerOne = true;
            playerOneFiringCooldown = playerOne.tankFireRate;
            FiringCoroutinePlayerOne = StartCoroutine(FireContinuouslyPlayerOne());
        }
        else if (Keyboard.current.fKey.wasReleasedThisFrame || Mouse.current.leftButton.wasReleasedThisFrame)
        {
            StopCoroutine(FireContinuouslyPlayerOne());
            FiringCoroutinePlayerOne = null;
            IsFiringPlayerOne = false;
        }

        //Player two
        if ((Keyboard.current.semicolonKey.wasPressedThisFrame || (Gamepad.current != null && Gamepad.current.rightTrigger.wasPressedThisFrame)) && FiringCoroutinePlayerTwo == null && playerTwoFiringCooldown <= 0 && playerTwo.isAlive)
        {
            IsFiringPlayerTwo = true;
            playerTwoFiringCooldown = playerTwo.tankFireRate;
            FiringCoroutinePlayerTwo = StartCoroutine(FireContinuouslyPlayerTwo());
        }
        else if (Keyboard.current.semicolonKey.wasReleasedThisFrame || (Gamepad.current != null && Gamepad.current.rightTrigger.wasReleasedThisFrame))
        {
            StopCoroutine(FireContinuouslyPlayerTwo());
            FiringCoroutinePlayerTwo = null;
            IsFiringPlayerTwo = false;
        }
    }

    IEnumerator FireContinuouslyPlayerOne()
    {
        while (IsFiringPlayerOne && playerOne.isAlive)
        {
            GameObject projectileCreated = Instantiate(playerOne.projectile, playerOne.tankGunFront.transform.position, playerOne.tankGun.transform.rotation);
            projectileCreated.GetComponent<SpriteRenderer>().sprite = playerOneProjectileSprite;
            projectileCreated.GetComponent<ProjectileBehavior>().projectileOwner = playerOne.GetComponent<PlayerController>();
            projectileCreated.GetComponent<ProjectileBehavior>().projectileSpeed = projectileCreated.GetComponent<ProjectileBehavior>().projectileOwner.tankProjectileSpeed;
            projectileCreated.layer = playerOne.gameObject.layer + 1;
            Destroy(projectileCreated, playerOne.tankProjectileLifeTime);
            if (projectileCreated.GetComponent<Rigidbody2D>() != null)
            {
                var projectileRigidBody = projectileCreated.GetComponent<Rigidbody2D>();
                projectileRigidBody.velocity = new Vector2(
                    (playerOne.tankGunFront.transform.position.x - playerOne.transform.position.x) * projectileCreated.GetComponent<ProjectileBehavior>().projectileSpeed,
                    (playerOne.tankGunFront.transform.position.y - playerOne.transform.position.y) * projectileCreated.GetComponent<ProjectileBehavior>().projectileSpeed);
            }
            yield return new WaitForSeconds(playerOne.tankFireRate);
        }
    }

    IEnumerator FireContinuouslyPlayerTwo()
    {
        while (IsFiringPlayerTwo && playerTwo.isAlive)
        {
            GameObject projectileCreated = Instantiate(playerTwo.projectile, playerTwo.tankGunFront.transform.position, playerTwo.tankGun.transform.rotation);
            projectileCreated.GetComponent<SpriteRenderer>().sprite = playerTwoProjectileSprite;
            projectileCreated.GetComponent<ProjectileBehavior>().projectileOwner = playerTwo.GetComponent<PlayerController>();
            projectileCreated.GetComponent<ProjectileBehavior>().projectileSpeed = projectileCreated.GetComponent<ProjectileBehavior>().projectileOwner.tankProjectileSpeed;
            projectileCreated.layer = playerTwo.gameObject.layer + 1;
            Destroy(projectileCreated, playerTwo.tankProjectileLifeTime);
            if (projectileCreated.GetComponent<Rigidbody2D>() != null)
            {
                var projectileRigidBody = projectileCreated.GetComponent<Rigidbody2D>();
                projectileRigidBody.velocity = new Vector2(
                    (playerTwo.tankGunFront.transform.position.x - playerTwo.transform.position.x) * projectileCreated.GetComponent<ProjectileBehavior>().projectileSpeed,
                    (playerTwo.tankGunFront.transform.position.y - playerTwo.transform.position.y) * projectileCreated.GetComponent<ProjectileBehavior>().projectileSpeed);

            }
            yield return new WaitForSeconds(playerTwo.tankFireRate);
        }
    }
}
