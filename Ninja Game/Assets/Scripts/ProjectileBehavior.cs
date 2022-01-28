using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines behavior for projectiles (ninja stars)
/// </summary>
public class ProjectileBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Ninja stars will automatically disappear after 2 seconds
        Destroy(this.gameObject, 2);        
    }

    // Update is called once per frame
    void Update()
    {
    }

}
