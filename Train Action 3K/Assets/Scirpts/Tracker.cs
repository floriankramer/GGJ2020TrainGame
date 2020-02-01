using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public static float totalDamage;
    public static float wheeleHealthPercentage;
    public static float strutHealthPercentage;
    public static float windowHealthPercentage;
    public static float ventHealthPercentage;

    private List<Part> wheeles = new List<Part>();
    private List<Part> struts = new List<Part>();
    private List<Part> windows = new List<Part>();
    private List<Part> vents = new List<Part>();

    // Start is called before the first frame update
    void Start()
    {
        // Cache all childs with a "Part" Script and store them into their Lists
        Part[] parts = GetComponentsInChildren<Part>();
        foreach (Part part in parts)
        {
            if (part.partType == PartType.Wheel)
            {
                wheeles.Add(part);
            }
            if (part.partType == PartType.Strut)
            {
                struts.Add(part);
            }
            if (part.partType == PartType.Window)
            {
                windows.Add(part);
            }
            if (part.partType == PartType.Vent)
            {
                vents.Add(part);
            }
        }
        Debug.Log(vents.Count);
    }

    // Update is called once per frame
    void Update()
    {
        // Get Health Percentage of Wheeles
        float health = 0.0f;
        foreach (Part part in wheeles)
        {
            if (part != null)
                health += part.Health;
        }
        wheeleHealthPercentage = (health / wheeles.Count)/100f;

        // Get Health Percentage of struts
        health = 0.0f;
        foreach (Part part in struts)
        {
            if (part != null)
                health += part.Health;
        }
        strutHealthPercentage = (health / struts.Count) / 100f;

        // Get Health Percentage of Windows
        health = 0.0f;
        foreach (Part part in windows)
        {
            if (part != null)
                health += part.Health;
        }
        windowHealthPercentage = (health / windows.Count) / 100f;

        // Get Health Percentage of struts
        health = 0.0f;
        foreach (Part part in vents)
        {
            if (part != null)
                health += part.Health;
        }
        ventHealthPercentage = (health / vents.Count) / 100.0f;

        Debug.Log(ventHealthPercentage);

    }

}
