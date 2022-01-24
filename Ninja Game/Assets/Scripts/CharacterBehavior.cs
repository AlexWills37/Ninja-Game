using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    public float movementSpeed = 2f;
    public float rotationSpeed = 80f;
    public float minVertAngle = -50f;
    public float maxVertAngle = 50f;

    public Rigidbody rb;


    private float forwardMovement;
    private float horizontalMovement;
    private float verticalRotation;
    private float horizontalRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get user input values
        forwardMovement = Input.GetAxis("Vertical") * movementSpeed;
        horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed;
        verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
        horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
    }

    private void FixedUpdate()
    {
        // Get the change in position
        float deltaHorizontalMovement = horizontalMovement * Time.deltaTime;
        float deltaForwardMovement = forwardMovement * Time.deltaTime;
        Vector3 deltaPosition = new Vector3(deltaHorizontalMovement, 0, deltaForwardMovement);

        Vector3 deltaRotation = new Vector3(verticalRotation * Time.deltaTime, horizontalRotation * Time.deltaTime, 0);
        Vector3 newRotation = rb.rotation.eulerAngles + deltaRotation;
        
        // Ensure x axis is between 50 and 0 (looking down) or 0 and -50 (360 and 310)
        if( newRotation.x > maxVertAngle && newRotation.x < 360 + minVertAngle)
        {
            // Looking down
            if(newRotation.x < 180)
            {
                newRotation.x = maxVertAngle;

            // Looking up
            } else
            {
                newRotation.x = minVertAngle;
            }
        }


        // Translate to the new position in world space and move to there
        Vector3 newPosition = this.transform.TransformPoint(deltaPosition);
        rb.MovePosition(newPosition);


        rb.MoveRotation(Quaternion.Euler(newRotation));
    }
}
