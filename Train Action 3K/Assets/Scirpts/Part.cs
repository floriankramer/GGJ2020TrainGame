using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    public PartType partType;


    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float health = 100.0f;

    public float Health
    {
        get
        {
            return this.health;
        }
        set
        {
            // TODO: Check if health is 0.0f and start a fancy explosion animation!
            this.health = value;
            if(health <= 0.0f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
