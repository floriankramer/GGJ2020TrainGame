using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceCounterScript : MonoBehaviour
{
    private Text text;
    
    public float distanceTraveled;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceTraveled += Train.trainSpeed * Time.deltaTime;
        text.text = distanceTraveled.ToString("F1") + "m";
    }
}
