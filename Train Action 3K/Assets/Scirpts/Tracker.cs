using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public static float timeSinceBreak;
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

    }

    public static PartType comparePartTypeHealth()
    {
        PartType compare1;
        float compare1Value;
        if (wheeleHealthPercentage < strutHealthPercentage)
        {
            compare1 = PartType.Wheel;
            compare1Value = wheeleHealthPercentage;
        }
        else
        {
            compare1 = PartType.Strut;
            compare1Value = strutHealthPercentage;
        }

        PartType compare2;
        float compare2Value;
        if (windowHealthPercentage < ventHealthPercentage)
        {
            compare2 = PartType.Wheel;
            compare2Value = windowHealthPercentage;
        }
        else
        {
            compare2 = PartType.Strut;
            compare2Value = ventHealthPercentage;
        }
        

        if (compare1Value < compare2Value)
        {
            return compare1;
        }
        else
        {
            return compare2;
        }

    }

}
