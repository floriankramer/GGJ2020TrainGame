using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade_Physical : MonoBehaviour
{
    public float damage = 5.0f;
    public float damagePropagationProbability = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.GetComponent<Part>() != null || collision.gameObject.GetComponent<Train>() != null)
        {
            Part[] parts = FindObjectsOfType<Part>();

            if(parts.Length > 0)
            {
                System.Array.Sort(parts, CompareObjDistance);
                for(int i = 0; i < parts.Length; i++)
                {
                    if(Random.Range(0.0f, 100.0f) < damagePropagationProbability)
                    {
                        parts[i].Health -= damage;
                        break;
                    }
                }
            }

            //int randomIndex = Mathf.RoundToInt(Random.Range(0.0f, parts.Length - 1.0f));
        }
        


    }

    int CompareObjDistance(Part x, Part y)
    {
        return y.transform.position.x < x.transform.position.x ? -1 : 1;
    }
}
