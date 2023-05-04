using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetup : MonoBehaviour
{
    public PlayerController playerOne;
    public PlayerController playerTwo;

    Coroutine FiringCoroutinePlayerOne;
    Coroutine FiringCoroutinePlayerTwo;
    bool IsFiringPlayerOne;
    bool IsFiringPlayerTwo;

    void Update()
    {
        Move();
        Fire();
        Debug.Log(playerOne.rawInputVector);
    }

    void Move()
    {
        playerOne.rawInputVector = new Vector2(0f, 0f);
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

        Debug.Log(playerTwo.rawInputVector);
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
        if ((Keyboard.current.fKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame) && FiringCoroutinePlayerOne == null)
        {
            IsFiringPlayerOne = true;
            FiringCoroutinePlayerOne = StartCoroutine(FireContinuouslyPlayerOne());
        }
        else if (Keyboard.current.fKey.wasReleasedThisFrame || Mouse.current.leftButton.wasReleasedThisFrame)
        {
            StopCoroutine(FireContinuouslyPlayerOne());
            FiringCoroutinePlayerOne = null;
            IsFiringPlayerOne = false;
        }

        //Player two
        if ((Keyboard.current.semicolonKey.wasPressedThisFrame || (Gamepad.current != null && Gamepad.current.rightTrigger.wasPressedThisFrame)) && FiringCoroutinePlayerTwo == null)
        {
            IsFiringPlayerTwo = true;
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
        while (IsFiringPlayerOne)
        {
            GameObject projectileCreated = Instantiate(playerOne.projectile, playerOne.tankGunFront.transform.position, playerOne.tankGun.transform.rotation);
            projectileCreated.GetComponent<ProjectileBehavior>().projectileOwner = playerOne.GetComponent<PlayerController>();
            Destroy(projectileCreated, 2f);
            projectileCreated.layer = playerOne.gameObject.layer;
            if (projectileCreated.GetComponent<Rigidbody2D>() != null)
            {
                var projectileRigidBody = projectileCreated.GetComponent<Rigidbody2D>();
                projectileRigidBody.velocity = new Vector2(
                    (playerOne.tankGunFront.transform.position.x - playerOne.transform.position.x) * projectileCreated.GetComponent<ProjectileBehavior>().projectileSpeed,
                    (playerOne.tankGunFront.transform.position.y - playerOne.transform.position.y) * projectileCreated.GetComponent<ProjectileBehavior>().projectileSpeed);
            }
            yield return new WaitForSeconds(1f / playerOne.fireRate);
        }
    }

    IEnumerator FireContinuouslyPlayerTwo()
    {
        while (IsFiringPlayerTwo)
        {
            GameObject projectileCreated = Instantiate(playerTwo.projectile, playerTwo.tankGunFront.transform.position, playerTwo.tankGun.transform.rotation);
            projectileCreated.GetComponent<ProjectileBehavior>().projectileOwner = playerTwo.GetComponent<PlayerController>();
            projectileCreated.layer = playerTwo.gameObject.layer;
            if (projectileCreated.GetComponent<Rigidbody2D>() != null)
            {
                var projectileRigidBody = projectileCreated.GetComponent<Rigidbody2D>();
                projectileRigidBody.velocity = new Vector2(
                    (playerTwo.tankGunFront.transform.position.x - playerTwo.transform.position.x) * projectileCreated.GetComponent<ProjectileBehavior>().projectileSpeed,
                    (playerTwo.tankGunFront.transform.position.y - playerTwo.transform.position.y) * projectileCreated.GetComponent<ProjectileBehavior>().projectileSpeed);

            }
            yield return new WaitForSeconds(1f / playerTwo.fireRate);
        }
    }
}
