using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Healtbar : MonoBehaviour
{
    // Currently displayed health
    private float health = 0.0f;

    // List of all tracked parts (cached at Start())
    private List<Part> trackedParts = new List<Part>();

    // Reference to the ScaleRoot, which is used to scale the inner health bar object
    public GameObject scaleRoot;

    // Reference to the target of this health bar (Wagon or Part)
    public GameObject target;

    // The PartType this health bar tracks
    public PartType trackedPartType;

    // Event that gets invoked if the health bar is empty.
    public UnityEvent onHealthEmpty = new UnityEvent();

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
            // Cache all childs with a "Part" Script and store them into trackedParts (filtered by trackedPartType)
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
        RefreshDisplayedHealth();
    }

    /// <summary>
    /// Refreshes the displayed health by summing up all the tracked part healths.
    /// </summary>
    void RefreshDisplayedHealth()
    {
        this.health = 0.0f;
        foreach (Part part in this.trackedParts)
        {
            if(part != null)
                this.health += part.Health;
        }
        this.health /= trackedParts.Count;

        scaleRoot.transform.localScale = new Vector3(this.health / 100.0f,
            scaleRoot.transform.localScale.y,
            scaleRoot.transform.localScale.z);

        if(this.health <= 0.01f)
        {
            if (onHealthEmpty != null)
                onHealthEmpty.Invoke();

            Destroy(this.gameObject);
        }
    }
}
