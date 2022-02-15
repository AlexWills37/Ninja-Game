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

    private bool canMove = true;

    private float forwardMovement;
    private float horizontalMovement;
    private float horizontalRotation;

    private float getUpTime;
    /// <summary>
    /// How long it takes the player to get up, after being knocked over by fruit
    /// </summary>
    private float recoveryTime = 1f;

    public AudioSource damageSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        damageSound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get user input values
        forwardMovement = Input.GetAxis("Vertical") * movementSpeed;
        horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed;
        horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;

        // When it is time to get up, get back up
        if(!canMove && Time.time >= getUpTime)
        {
            getUp();
        }
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


        // Allow the user to rotate, but not to move when they cannot move
        if (canMove)
        {
            // Translate to the new position in world space and move to there
            Vector3 newPosition = this.transform.TransformPoint(deltaPosition);
            rb.MovePosition(newPosition);

        }
        rb.MoveRotation(Quaternion.Euler(newRotation));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player hits a fruit, fall over
        if (collision.gameObject.name.Contains("Fruit"))
        {
            damageSound.time = 0.8f;
            damageSound.Play();

            fallOver();
        }
    }

    private void fallOver()
    {
        // To fall over, change the roll (rotate about the z-axis)
        rb.constraints = (int) RigidbodyConstraints.FreezeRotationY + RigidbodyConstraints.FreezeRotationX;
        rb.AddRelativeTorque(0, 0, 1.5f, ForceMode.Impulse);
        getUpTime = Time.time + recoveryTime;
        canMove = false;
        
    }


    private void getUp()
    {
        Vector3 orientation = this.transform.rotation.eulerAngles;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        Quaternion newRotation;

        // To smoothly get up, multiply the z rotation until it is close to 0 with repeated calls to this method
        if(orientation.z <= 0.5)
        {
            newRotation = Quaternion.Euler(orientation.x, orientation.y, 0);
            canMove = true;

        } else
        {
            newRotation = Quaternion.Euler(orientation.x, orientation.y, orientation.z * 0.9f);
        }

        rb.MoveRotation(newRotation);

    }
}
