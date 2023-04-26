using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] PlayerController playerOne;
    [SerializeField] PlayerController playerTwo;

    Coroutine FiringCoroutinePlayerOne;
    Coroutine FiringCoroutinePlayerTwo;
    bool IsFiringPlayerOne;
    bool IsFiringPlayerTwo;

    void Update()
    {
        Fire();
    }
    void OnMove(InputValue value)
    {
        playerOne.rawInputVector = value.Get<Vector2>();
    }
    void OnMovePlayerTwo(InputValue value)
    {
        playerTwo.rawInputVector = value.Get<Vector2>();
    }

    void Fire()
    {
        //Player one
        if (Keyboard.current.fKey.wasPressedThisFrame && FiringCoroutinePlayerOne == null)
        {
            IsFiringPlayerOne = true;
            FiringCoroutinePlayerOne = StartCoroutine(FireContinuouslyPlayerOne());
        }
        else if (Keyboard.current.fKey.wasReleasedThisFrame)
        {
            StopCoroutine(FireContinuouslyPlayerOne());
            FiringCoroutinePlayerOne = null;
            IsFiringPlayerOne = false;
        }

        //Player two
        if (Keyboard.current.semicolonKey.wasPressedThisFrame && FiringCoroutinePlayerTwo == null)
        {
            IsFiringPlayerTwo = true;
            FiringCoroutinePlayerTwo = StartCoroutine(FireContinuouslyPlayerTwo());
        }
        else if (Keyboard.current.semicolonKey.wasReleasedThisFrame)
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
            Destroy(projectileCreated, 5f);
            projectileCreated.layer = gameObject.layer;
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
            projectileCreated.layer = gameObject.layer;
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
