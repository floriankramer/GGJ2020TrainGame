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
        this.offset.x += Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.transform.position + offset;
        targetPosition.x -= Camera.main.orthographicSize * Camera.main.aspect;
        this.transform.position = (drag * this.transform.position) + ((1.0f - drag) * targetPosition);

        Train t = target.GetComponent<Train>();
        if (t != null)  {
            float length = Train.EngineLength + t.numCars * Train.CarLength;
            float padding = 0.5f * length;
            Camera.main.orthographicSize = (2 * padding + length) / Camera.main.aspect / 2;
        }
    }
}
