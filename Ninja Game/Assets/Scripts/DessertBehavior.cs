using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DessertBehavior : MonoBehaviour
{
    public Rigidbody rb;
    public GameBehavior gameBehavior;
    public ActiveFruitCounter fruitCounter;

    // Start is called before the first frame update
    void Start()
    {
        // Create fruit and add some spin to make it look more interesting
        this.rb = this.GetComponent<Rigidbody>();
        rb.AddTorque(1, 1, 1, ForceMode.Impulse);

        GameObject manager = GameObject.Find("Game Manager");
        this.gameBehavior = manager.GetComponent<GameBehavior>();
        this.fruitCounter = manager.GetComponent<ActiveFruitCounter>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        // If a dessert is hit by a shuriken, the dessert should be destroyed and the user should lose a point
        if (collision.gameObject.name == "Shuriken(Clone)")
        {
            Debug.Log("Hit!");
            Destroy(this.gameObject);
            gameBehavior.points--;
            fruitCounter.numActiveFruit -= 1;

        } else if (collision.gameObject.name == "Player")
        {
            // If the player hits the dessert, the player will eat the dessert
            Destroy(this.gameObject);
            gameBehavior.points += 2;
            fruitCounter.numActiveFruit -= 1;
        }
    }
}
