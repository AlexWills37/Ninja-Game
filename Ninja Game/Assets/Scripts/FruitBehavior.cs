using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehavior : MonoBehaviour
{

    public Rigidbody rb;
    public GameBehavior gameBehavior;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
        rb.AddTorque(1, 1, 1, ForceMode.Impulse);
        this.gameBehavior = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Shuriken(Clone)")
        {
            Debug.Log("Hit!");
            Destroy(this.gameObject);
            gameBehavior.points++;
        }
    }
}
