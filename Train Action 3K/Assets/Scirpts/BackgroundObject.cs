using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObject : MonoBehaviour
{
    public float size;



    // Update is called once per frame
    void Update()
    {
        // Move with the Speed of the Train
        float xPosition = transform.position.x + Train.trainSpeed * Time.deltaTime * -1;
        transform.position = new Vector3(xPosition, transform.position.y);

        // Destroy this Object once it leaves the Screen
        // TODO Change -15 to a variable that is reacting to the Screen Size
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
}
