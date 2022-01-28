using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines character movement and control. To be attached to the player object
/// </summary>
public class CharacterBehavior : MonoBehaviour
{
    public float movementSpeed = 2f;
    
    // How quickly to rotate the player when you move the mouse
    public float rotationSpeed = 80f;
    
    // Ridigbody attached to the player
    public Rigidbody rb;


    private float forwardMovement;
    private float horizontalMovement;
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
        horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
    }

    private void FixedUpdate()
    {
        // Get the change in position
        float deltaHorizontalMovement = horizontalMovement * Time.deltaTime;
        float deltaForwardMovement = forwardMovement * Time.deltaTime;
        Vector3 deltaPosition = new Vector3(deltaHorizontalMovement, 0, deltaForwardMovement);

        // Get the change in horizontal rotation (vertical rotation is done with the PlayerHead
        Vector3 deltaRotation = new Vector3(0, horizontalRotation * Time.deltaTime, 0);
        Vector3 newRotation = rb.rotation.eulerAngles + deltaRotation;
        

        // Translate to the new position in world space and move to there
        Vector3 newPosition = this.transform.TransformPoint(deltaPosition);
        rb.MovePosition(newPosition);
        rb.MoveRotation(Quaternion.Euler(newRotation));
    }
}
