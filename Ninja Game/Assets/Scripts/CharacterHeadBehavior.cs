using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The character head is responsible for rotating vertically about the x-axis
/// </summary>
public class CharacterHeadBehavior : MonoBehaviour
{
    public float minVertAngle = -50f;
    public float maxVertAngle = 50f;

    // Behavior script for the whole character, where movement and rotation speed are located
    public CharacterBehavior characterBehavior;

    private float verticalRotation;

    // Start is called before the first frame update
    void Start()
    {
        characterBehavior = this.GetComponentInParent<CharacterBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        verticalRotation = Input.GetAxis("Mouse Y") * characterBehavior.rotationSpeed;
    }

    private void FixedUpdate()
    {
        // The head is in charge of vertical rotation
        // Because the head is part of the Player, its movement is kinematic
        this.transform.Rotate(verticalRotation * Time.deltaTime, 0, 0);

        // Ensure that the rotation does not go past the minimum and maximum angle
        Vector3 rotation = this.transform.rotation.eulerAngles;
        float rotX = rotation.x;

        // If rotation is outside of the minimum, maximum range, snap it to the right direction
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

            this.transform.rotation = Quaternion.Euler(rotX, rotation.y, rotation.z);
        }
    }
}
