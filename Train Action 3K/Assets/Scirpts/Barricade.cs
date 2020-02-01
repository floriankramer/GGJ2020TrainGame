using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public float damage = 5.0f;
    public float damagePropagationProbability = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Catch Inputs direct at this Object
    void OnMouseOver()
    {
        // Check if a Click is happening above the Object
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.GetComponent<Part>() != null || collision.gameObject.GetComponentInParent<Train>() != null)
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
                        Destroy(gameObject);
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
