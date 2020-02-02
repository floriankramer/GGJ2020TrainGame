using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    public float constantSpeed = 0.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distTravelled = (Train.trainSpeed + constantSpeed) * Time.deltaTime;
        transform.Rotate(0, 0, -distTravelled / Mathf.PI * 180);
    }
}
