using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainVelocity : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Train.trainSpeed = rb.velocity.magnitude * 30.0f;
    }
}
