using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenEdge {
 LEFT,
 RIGHT,
 TOP,
 BOTTOM
}

public class ScaleToCamera : MonoBehaviour
{

    public ScreenEdge screenEdge;

    public SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float cheight = Camera.main.orthographicSize;
        float cwidth = cheight * Camera.main.aspect;
        float leftEdge = Camera.main.transform.position.x - cwidth;
        float rightEdge = Camera.main.transform.position.x + cwidth;

        float topEdge = Camera.main.transform.position.y + cheight;
        float bottomEdge = Camera.main.transform.position.y - cheight;

        float objectWidth = renderer.bounds.max.x - renderer.bounds.min.x;
        float objectHeight = renderer.bounds.max.y - renderer.bounds.min.y;

        switch (screenEdge) {
            case ScreenEdge.LEFT:
                // gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x, objectHeight / gameObject.transform.localScale.y / 2 / cwidth);
                gameObject.transform.position = new Vector3(leftEdge + objectWidth / 2, gameObject.transform.position.y, gameObject.transform.position.z);
                break;
            case ScreenEdge.RIGHT:
                // gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x, objectHeight / gameObject.transform.localScale.y / 2 / cwidth);
                gameObject.transform.position = new Vector3(rightEdge - objectWidth / 2, gameObject.transform.position.y, gameObject.transform.position.z);
                break;
            case ScreenEdge.TOP:
                // gameObject.transform.localScale = new Vector2(objectWidth / gameObject.transform.localScale.x / 2 / cheight, gameObject.transform.localScale.y);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, topEdge - objectHeight / 2, gameObject.transform.position.z);
                break;
            case ScreenEdge.BOTTOM:
                // gameObject.transform.localScale = new Vector2(objectWidth / gameObject.transform.localScale.x / 2 / cheight, gameObject.transform.localScale.y);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, bottomEdge + objectHeight / 2, gameObject.transform.position.z);
                break;
        }
    }
}
