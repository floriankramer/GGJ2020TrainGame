using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Changeable Values for Game Experience
    private static float maxTimeSinceBreak = 60f;
    private static float reactionTime = 0.5f;
    private static float breakTime = 2.5f;
    private static float difficulty = 3f;
    
    // for items and materials
    private float timeTillObjectSpawn =2f;
    public GameObject[] items;
    public GameObject barikade;
    

    //
    // All Variables for the Background
    //

    public GameObject[] backgroundObjects;

    // Variables used to Spawn the Background
    private float nextSpawnDistanceBackground;
    private float spawnDistanceBackground;

    // Minimal and Maximal Distance that can between two Background Objects 
    private float minBackgroundObjectDistance = 0.5f;
    private float maxBackgroundObjectDistance = 6f;

    // Values for the Spawning on the Y-Axis
    private float minBackgroundObjectYValue = -1;
    private float maxBackgroundObjectYValue = 1.5f;

    
    // Update is called once per frame
    void Update()
    {
        Tracker.timeSinceBreak += Time.deltaTime;
        timeTillObjectSpawn -= Time.deltaTime;

        if (timeTillObjectSpawn < 0)
        {
            SpawnObject();
        }

        spawnDistanceBackground += Train.trainSpeed * Time.deltaTime;
        if (nextSpawnDistanceBackground < spawnDistanceBackground)
        {
            SpawnBackground();
            spawnDistanceBackground = 0;
        }
    }

    void SpawnBackground()
    {
        int n = (int)Random.Range(0, backgroundObjects.Length - 0.1f);
        GameObject spawnThis = backgroundObjects[n];

        float spawnObjectY = Random.Range(minBackgroundObjectYValue, maxBackgroundObjectYValue);

        // TODO Change 25 to value based on the Screen Size
        Instantiate(spawnThis, new Vector3(Camera.main.transform.position.x + 25, spawnObjectY), Quaternion.identity);

        float size = spawnThis.GetComponent<SpriteRenderer>().sprite.rect.size.x/100;
        float minSpawnDistance = size + minBackgroundObjectDistance;
        float maxSpawnDistance = minSpawnDistance + maxBackgroundObjectDistance;
        nextSpawnDistanceBackground = Random.Range(minSpawnDistance, maxSpawnDistance);
    }

    void SpawnObject()
    {

        // Spawn an item if there is to much unpreventable Damage
        /*if (Tracker.totalDamage > 700)
        {
            //Spawn a Item based on a part type
            SpawnNeededMaterial(Tracker.comparePartTypeHealth());
            timeTillObjectSpawn += reactionTime + (difficulty*0.2f);
            Tracker.totalDamage -= 100;
            return;
        }*/

        // Take a break if there wasn't a break for a long time
        if (Tracker.timeSinceBreak > maxTimeSinceBreak)
        {
            timeTillObjectSpawn += breakTime;
            Tracker.timeSinceBreak = 0;
            return;
        }

        // Choose a Object to be spawned based on pseudorandomness
        float randomValue = Random.value;
        // Add long Break
        if (randomValue < 0.05)
        {
            timeTillObjectSpawn = 2* breakTime;
            Tracker.timeSinceBreak = 0;
            return;
        }

        // Add short Break
        if (randomValue < 0.3)
        {
            timeTillObjectSpawn = breakTime;
            Tracker.timeSinceBreak = 0;
            return;
        }

        // Add wanted Item
        if (randomValue < 0.3+ (difficulty/100))
        {
            SpawnNeededMaterial(Tracker.comparePartTypeHealth());
            timeTillObjectSpawn = reactionTime + (difficulty * 0.1f);
            return;
        }

        // Add Barikade
        if (randomValue < 0.7 - (difficulty / 100))
        {
            Vector3 coordinates = new Vector3(Camera.main.transform.position.x +20, -3.85f);
            Instantiate(barikade, coordinates , Quaternion.identity);
            timeTillObjectSpawn = reactionTime + (difficulty * 0.1f);
            return;
        }

        // Add Random Item 
        SpawnRandomItem();
        timeTillObjectSpawn = reactionTime + (difficulty * 0.2f);

    }

    public void SpawnRandomItem()
    {
        int n = (int)Random.Range(0, items.Length - 0.1f);
        Instantiate(items[n], new Vector3(Camera.main.transform.position.x + Camera.main.orthographicSize*Camera.main.aspect*1.2f, GetItemYSpawnpoint()), Quaternion.identity);
    }

    public void SpawnNeededMaterial(PartType part)
    {
        SpawnRandomItem();
    }

    public float GetItemYSpawnpoint()
    {
        float value = Random.value * -4 - 0.5f;
        if (value < -2)
        {
            value -= 3.5f;
        }
        return value;
    }


}
