using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    public float constantSpeed = 0.0f;
    private bool isAlive = true;
    public bool killOnDestroy = false;

    void Start()
    {
        Part part = gameObject.GetComponent<Part>();
        part.onDeathEvent.AddListener(onDeath);
    }

    void onDeath()
    {
        isAlive = false;
        if(killOnDestroy)
            gameObject.GetComponent<Collider2D>().transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            float distTravelled = (Train.trainSpeed + constantSpeed) * Time.deltaTime;
            transform.Rotate(0, 0, -distTravelled / Mathf.PI * 180);
        }
    }
}
