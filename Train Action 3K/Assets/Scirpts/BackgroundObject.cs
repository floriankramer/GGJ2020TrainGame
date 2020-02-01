using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObject : MonoBehaviour
{
    public float size;



    // Update is called once per frame
    void Update()
    {

        // Destroy this Object once it leaves the Screen
        // TODO Change -15 to a variable that is reacting to the Screen Size
        if (transform.position.x < Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect * 1.2f)
        {
            Destroy(gameObject);
        }
    }
}
