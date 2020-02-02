using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float drag = 0.99f;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        this.offset = this.transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.transform.position + offset;
        Vector3 motion = new Vector3(0, 0, 0);
        // this.transform.position = (drag * this.transform.position) + ((1.0f - drag) * targetPosition);
        // this.transform.position = Vector3.SmoothDamp(this.transform.position, target.transform.position + offset, ref motion, drag, float.MaxValue, Time.deltaTime);
        this.transform.position = target.transform.position + offset;
       
    }
}
