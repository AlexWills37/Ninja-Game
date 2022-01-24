using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraBehavior : MonoBehaviour
{

    public GameObject player;
    public Vector3 camOffset = new Vector3(0, 0, 0);


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");    
    }

    // Update the camera view
    private void LateUpdate()
    {
        Vector3 newPosition = player.transform.TransformPoint(camOffset);
        this.transform.position = newPosition;
        this.transform.rotation = player.transform.rotation;
    }
}
