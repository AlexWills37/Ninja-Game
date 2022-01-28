using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Specifies how fruit should be automatically created in the game
/// </summary>
public class FruitGeneratorBehavior : MonoBehaviour
{
    // GameObject containing the different fruit objects as children
    public GameObject allFruits;

    // Fruit will randomly spawn anywhere in this cube
    public GameObject spawnCube;

    // This is where the children of allFruits will be placed
    public List<GameObject> fruitList;

    // True if fruit should be generated, false otherwise.
    public bool spawningActive = true;

    // Time between fruits
    public float timeBetweenSpawn = 1f;

    // The next value to spawn a fruit at
    private float timeToSpawn;

    // The boundaries of the cube to spawn from
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;
    private float spawnY;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSpawnLocations();
        RegisterFruit();
        timeToSpawn = Time.time + timeBetweenSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        // When ready to spawn, generate a fruit
        if(spawningActive && Time.time >= timeToSpawn)
        {
            SpawnFruit();
            timeToSpawn = Time.time + timeBetweenSpawn;
        }
    }

    /// <summary>
    /// Generate a fruit from the fruit list at a random position between the min and max coordinates
    /// </summary>
    void SpawnFruit()
    {
        // Random vector between minX, minZ and maxX, maxZ, at spawnY
        Vector3 position = new Vector3(Random.Range(minX, maxX), spawnY, Random.Range(minZ, maxZ));

        // Random fruit in the fruitlist
        GameObject randomFruit = fruitList[Random.Range(0, fruitList.Count)];

        Instantiate(randomFruit, position, Quaternion.identity);
    }

    /// <summary>
    /// Uses the spawnCube game object to update the min and max coordinates
    /// </summary>
    private void UpdateSpawnLocations()
    {
        // Set min and max values to the boundaries of the transform object
        Transform spawnTransform = spawnCube.transform;
        Vector3 middle = spawnTransform.position;
        float xScale = spawnTransform.localScale.x;
        float zScale = spawnTransform.localScale.z;

        minX = middle.x - xScale / 2;
        maxX = minX + xScale;
        minZ = middle.z - zScale / 2;
        maxZ = minZ + zScale;

        spawnY = middle.y;
    }

    /// <summary>
    /// Adds all the fruit to the list of fruits to randomly choose from
    /// </summary>
    private void RegisterFruit()
    {
        foreach(Transform child in allFruits.transform)
        {
            fruitList.Add(child.gameObject);
        }
    }
}
