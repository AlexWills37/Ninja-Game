using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The character head is responsible for rotating vertically about the x-axis
/// </summary>
public class CharacterHeadBehavior : MonoBehaviour
{

    public Weapon weapon;

    public float spreadAngle = 60;

    // Lowest and highest values for the transform rotation value
    public float minVertAngle = -50f;
    public float maxVertAngle = 50f;

    // Speed at which to throw projectiles
    public float projectileSpeed = 10f;
    // Offset to spawn projectile, so that it is not created inside the player's colliders
    public float projectileOffset = 0.5f;

    // The projectile to throw
    public GameObject projectile;

    // Behavior script for the whole character, where movement and rotation speed are located
    public CharacterBehavior characterBehavior;


    public GameBehavior gameManager;
    public int tripleShurikenUnlock = 25;

    private float verticalRotation;

    // Start is called before the first frame update
    void Start()
    {
        characterBehavior = this.GetComponentInParent<CharacterBehavior>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        verticalRotation = Input.GetAxis("Mouse Y") * characterBehavior.rotationSpeed;

        // When the user clicks, use a weapon
        if (Input.GetMouseButtonDown(0))
        {
            // Check to see which weapon is unlocked
            if ( !(gameManager.points < tripleShurikenUnlock)) 
            {
                weapon = Weapon.TripleShuriken;
            }

            // Single throw
            if(weapon == Weapon.SingleShuriken)
            {
                throwSingleShuriken();

                // Triple throw
            } else if (weapon == Weapon.TripleShuriken)
            {
                throwTripleShuriken();
            }
            
            
        }
    }

    private void FixedUpdate()
    {
        // The head is in charge of vertical rotation
        // Because the head is part of the Player, its movement is kinematic
        this.transform.Rotate(verticalRotation * Time.deltaTime, 0, 0);

        // Ensure that the rotation does not go past the minimum and maximum angle
        Vector3 rotation = this.transform.rotation.eulerAngles;

        float rotY = this.transform.parent.rotation.eulerAngles.y;

        // Always look forward from the body's perspective
        float rotX = rotation.x;

        // Stop rotation from going past the minimum and maximum
        if(rotX > maxVertAngle && rotX < 360 + minVertAngle)
        {
            // Looking up
            if(rotX > 180)
            {
                rotX = (360 + minVertAngle) % 360;
            } else
            {
                // Looking down, so snap to max angle
                rotX = maxVertAngle;
            }

            this.transform.rotation = Quaternion.Euler(rotX, rotY, 0);
        }
    }

    /// <summary>
    /// Throw a single shuriken in front of the player
    /// </summary>
    private void throwSingleShuriken()
    {
        throwShurikenAngle(0);
    }

    /// <summary>
    /// Throw 3 shurikens at once, at a bit of a spread angle
    /// </summary>
    private void throwTripleShuriken()
    {

        // Spawn projectiles at an angle
        float halfAngle = spreadAngle / 2;

        // Throw a shuriken straight ahead of the player
        throwSingleShuriken();
        // And two at equal angles from the center
        throwShurikenAngle(-halfAngle);
        throwShurikenAngle(halfAngle);


    }

    /// <summary>
    /// Spawn a shuriken in front of the player at the relative angle from the player
    /// </summary>
    /// <param name="angle"> The angle from the player to throw the star. Positive angle is to the right
    /// of the player, and negative angle is to the left. E.g. 0 corresponds to right in front of the player,
    /// 90 for 90 degrees to the right of the player, and -90 for 90 degrees to the left of the player </param>
    private void throwShurikenAngle(float angle)
    {
        // Spawn at an offset so that shuriken is not inside of the player
        Vector3 spawnOffset = this.transform.forward;
        spawnOffset.y = 0;
        spawnOffset.Normalize();
        spawnOffset *= projectileOffset;

        // Copy the player's transform, rotate the suriken, and translate it by an offset
        Transform playerTransform = this.GetComponent<Transform>();
        Vector3 starRotation = playerTransform.rotation.eulerAngles;
        starRotation.y += angle;

        // Create shuriken, spawn it, add velocity, and destroy it after 2 seconds
        GameObject star = Instantiate(projectile, playerTransform.position + spawnOffset, Quaternion.Euler(starRotation)) as GameObject;
        Rigidbody projectileRB = star.GetComponent<Rigidbody>();
        projectileRB.velocity = star.transform.forward * projectileSpeed;

        Destroy(star, 2);
    }
}
