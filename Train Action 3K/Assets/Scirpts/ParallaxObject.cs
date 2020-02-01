using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxObject : MonoBehaviour
{
    public float movementFactor = 1;


    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float motion = (1 - movementFactor) * Train.trainSpeed;
        transform.Translate(motion * Time.deltaTime, 0, 0);
        
        float cpos = Camera.main.transform.position.x;
        float screenwidth = 2 * Camera.main.orthographicSize * Camera.main.aspect;
        float spriteWidth = sprite.bounds.max.x - sprite.bounds.min.x;
        if (cpos - screenwidth / 2 - transform.position.x > spriteWidth / 2) {
            Destroy(gameObject);
        }
    }
}
