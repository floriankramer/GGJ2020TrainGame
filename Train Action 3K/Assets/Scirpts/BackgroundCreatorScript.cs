using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCreatorScript : MonoBehaviour
{
    

    public GameObject[] spawnableObjects;

    private float nextSpawnDistance;
    private float spawnDistance;

    // Minimal and Maximal Distance that can between two Background Objects 
    private float minObjectDistance = 0.5f;
    private float maxObjectDistance = 6f;

    // Values for the Spawning on the Y-Axis
    private float minObjectYValue = 2;
    private float maxObjectYValue = 4;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnDistance += Train.trainSpeed * Time.deltaTime;
        if (nextSpawnDistance < spawnDistance)
        {
            SpawnSomething();
            spawnDistance = 0;
        }
    }

    void SpawnSomething()
    {
        // TODO add Script that selects a fitting Background Object from the List
        GameObject spawnThis = spawnableObjects[0];

        // TODO add Script that adds a spawnObjectY infront of the train
        float spawnObjectY = Random.Range(minObjectYValue, maxObjectYValue);

        // TODO Change 15 to value based on the Screen Size
        Instantiate(spawnThis, new Vector3(15, spawnObjectY), Quaternion.identity);

        float size = spawnThis.GetComponent<SpriteRenderer>().sprite.rect.size.x/100;
        float minSpawnDistance = size + minObjectDistance;
        float maxSpawnDistance = minSpawnDistance + maxObjectDistance;
        nextSpawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
    }
}
