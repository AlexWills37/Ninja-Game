using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines how destroyable fruit should operate
/// </summary>
public class FruitBehavior : MonoBehaviour
{

    public Rigidbody rb;
    public GameBehavior gameBehavior;

    // Start is called before the first frame update
    void Start()
    {
        // Create fruit and add some spin to make it look more interesting
        this.rb = this.GetComponent<Rigidbody>();
        rb.AddTorque(1, 1, 1, ForceMode.Impulse);

        this.gameBehavior = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        // If a fruit is hit by a shuriken, the fruit should be destroyed and the player should gain a point
        if(collision.gameObject.name == "Shuriken(Clone)")
        {
            Debug.Log("Hit!");
            Destroy(this.gameObject);
            gameBehavior.points++;
        }
    }
}
