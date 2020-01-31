using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healtbar : MonoBehaviour
{
    private float health = 0.0f;
    private List<Part> trackedParts = new List<Part>();

    public GameObject scaleRoot;
    public GameObject target;

    public PartType trackedPartType;

    public float Health
    {
        get
        {
            return health;
        }
    }

    // Start is called before the first frame update
    void Start()
    {


        if (target == null)
            Debug.LogError("ERROR: Health bar initialized without a target game object.");
        else
        {
            // Cache all Parts in target into trackedParts, filtered by trackedPartType

            Part[] parts = target.GetComponentsInChildren<Part>();
            foreach(Part part in parts)
            {
                if(part.partType == this.trackedPartType)
                {
                    this.trackedParts.Add(part);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RefreshHealth();
    }

    void RefreshHealth()
    {
        this.health = 0.0f;
        foreach (Part part in this.trackedParts)
        {
            this.health += part.health;
        }
        this.health /= trackedParts.Count;

        scaleRoot.transform.localScale = new Vector3(this.health / 100.0f,
            scaleRoot.transform.localScale.y,
            scaleRoot.transform.localScale.z);
    }
}
