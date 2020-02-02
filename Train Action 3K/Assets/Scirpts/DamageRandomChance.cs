using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRandomChance : MonoBehaviour
{
    public float tickSeconds = 1.0f;
    public float probabilityPerTick = 1.0f;
    public float damageMin = 0.0f;
    public float damageMax = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        IEnumerator coroutine = CheckProbability(tickSeconds);
        StartCoroutine(coroutine);
    }

    private IEnumerator CheckProbability(float waitTime)
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);

            if (Random.Range(0.0f, 100.0f) <= probabilityPerTick)
            {
                Part randomPart = GetComponent<Part>();
                if(randomPart != null)
                {
                    float damage = Random.Range(damageMin, damageMax);
                    Tracker.totalDamage += Time.deltaTime * damage;
                    randomPart.Health -= damage;
                }
                    
            }
        }
    }

    private Part GetRandomPart()
    {
        Part[] parts = FindObjectsOfType<Part>();
        int randomIndex = Mathf.RoundToInt(Random.Range(0.0f, parts.Length - 1.0f));
        return parts[randomIndex];
    }
}