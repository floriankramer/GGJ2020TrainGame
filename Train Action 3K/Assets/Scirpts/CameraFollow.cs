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
        this.transform.position = (drag * this.transform.position) + ((1.0f - drag) * targetPosition);
    }
}
