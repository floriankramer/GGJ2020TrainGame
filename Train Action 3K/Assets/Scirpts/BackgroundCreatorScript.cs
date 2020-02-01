using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCreatorScript : MonoBehaviour
{
    // Changeable Values for Game Experience
    private static float maxTimeSinceBreak = 60f;
    private static float reactionTime = 2f;
    private static float breakTime = 2f;
    private static float difficulty = 5f;
    
    private float timeTillObjectSpawn;



    public GameObject[] items;
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
        Tracker.timeSinceBreak += Time.deltaTime;
        timeTillObjectSpawn -= Time.deltaTime;

        if (timeTillObjectSpawn < 0)
        {
            SpawnObject();
        }
















        spawnDistance += Train.trainSpeed * Time.deltaTime;
        if (nextSpawnDistance < spawnDistance)
        {
            SpawnBackground();
            spawnDistance = 0;
        }
    }

    void SpawnBackground()
    {
        // TODO add Script that selects a fitting Background Object from the List
        GameObject spawnThis = spawnableObjects[0];

        // TODO add Script that adds a spawnObjectY infront of the train
        float spawnObjectY = Random.Range(minObjectYValue, maxObjectYValue);

        // TODO Change 15 to value based on the Screen Size
        Instantiate(spawnThis, new Vector3(Camera.main.transform.position.x + 15, spawnObjectY), Quaternion.identity);

        float size = spawnThis.GetComponent<SpriteRenderer>().sprite.rect.size.x/100;
        float minSpawnDistance = size + minObjectDistance;
        float maxSpawnDistance = minSpawnDistance + maxObjectDistance;
        nextSpawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
    }

    void SpawnObject()
    {
        // Spawn an item if there is to much unpreventable Damage
        if (Tracker.totalDamage > 70)
        {
            //Spawn a Item based on a part type
            SpawnItem(Tracker.comparePartTypeHealth());
            timeTillObjectSpawn += reactionTime + (difficulty*0.2f);
            return;
        }

        // Take a break if there wasn't a break for a long time
        if (Tracker.timeSinceBreak > maxTimeSinceBreak)
        {
            timeTillObjectSpawn += breakTime * (1 +  difficulty /10);
            return;
        }

        // Choose a Object to be spawned based on pseudorandomness
        float randomValue = Random.value * 100;

        // Add long Break
        if (randomValue < 0.05)
        {
            timeTillObjectSpawn += 2* breakTime * (1 + difficulty / 10);
            return;
        }

        // Add short Break
        if (randomValue < 0.3)
        {
            timeTillObjectSpawn += breakTime * (1 + difficulty / 10);
            return;
        }

        // Add wanted Item
        if (randomValue < 0.3+ (difficulty/100))
        {
            SpawnItem(Tracker.comparePartTypeHealth());
            timeTillObjectSpawn += reactionTime + (difficulty * 0.2f);
            return;
        }

        // Add Barikade
        if (randomValue < 0.7 - (difficulty / 100))
        {
            SpawnBarikade();
            timeTillObjectSpawn += reactionTime + (difficulty * 0.2f);
            return;
        }

        // Add Random Item 
        // ToDo add better Random 
        int n = (int)(randomValue * 3.499);
        PartType part = PartType.Wheel;
        SpawnItem(part);

    }

    public void SpawnItem(PartType part)
    {
        // TODO implement
        // TODO add Script that adds a spawnObjectY infront of the train
        float spawnObjectY = Random.Range(minObjectYValue, maxObjectYValue);

        // TODO Change 15 to value based on the Screen Size
        Instantiate(items[0], new Vector3(Camera.main.transform.position.x + 15, spawnObjectY), Quaternion.identity);
    }

    public void SpawnBarikade()
    {
        /*/ TODO implement
        float spawnObjectY = Random.Range(minObjectYValue, maxObjectYValue);
        Instantiate(items[0], new Vector3(Camera.main.transform.position.x + 15, spawnObjectY), Quaternion.identity);*/
    }



}
