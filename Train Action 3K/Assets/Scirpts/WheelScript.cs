using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distTravelled = Train.trainSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -distTravelled / Mathf.PI * 180);
    }
}
