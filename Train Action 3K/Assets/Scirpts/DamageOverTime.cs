using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Part))]
public class DamageOverTime : MonoBehaviour
{

    private Part part;

    [Range(0.0f, 100.0f)]
    public float damagePerSecond;

    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<Part>();
    }

    // Update is called once per frame
    void Update()
    {
        Tracker.totalDamage += Time.deltaTime * damagePerSecond;
        part.Health -= Time.deltaTime * damagePerSecond;
    }
}
