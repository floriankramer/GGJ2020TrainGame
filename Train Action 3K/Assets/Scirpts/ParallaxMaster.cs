using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    public Sprite sprite;
    public float movementFactor = 1;

    public float yPos = 0;

    public float overlap = 0.1f;
    public float yStretch = 1;
}


public class ParallaxMaster : MonoBehaviour
{

    private Dictionary<ParallaxLayer, float> lastPositions = new Dictionary<ParallaxLayer, float>();

    public List<ParallaxLayer> layers = new List<ParallaxLayer>();

    // Start is called before the first frame update
    void Start()
    {
        float cpos = Camera.main.transform.position.x;
        float screenwidth = 2 * Camera.main.orthographicSize * Camera.main.aspect;
        float rightEdge = cpos + screenwidth / 2;

        // spawn the initial parallax objects
        for (int i = 0; i < layers.Count; ++i)
        {
            ParallaxLayer p = layers[i];
            lastPositions[p] = 1;

            float spriteWidth = p.sprite.bounds.max.x - p.sprite.bounds.min.x - p.overlap;
            int numSprites = (int)Mathf.Ceil(screenwidth / spriteWidth);
            for (int j = 0; j < numSprites + 2; ++j)
            {
                SpawnLayerAt(rightEdge + spriteWidth * 1.5f - j * spriteWidth, i, p);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float cpos = Camera.main.transform.position.x;
        float screenwidth = 2 * Camera.main.orthographicSize * Camera.main.aspect;
        float rightEdge = cpos + screenwidth / 2;

        for (int i = 0; i < layers.Count; ++i)
        {
            ParallaxLayer p = layers[i];

            float spriteWidth = p.sprite.bounds.max.x - p.sprite.bounds.min.x - p.overlap;
            float absPos = p.movementFactor * cpos;
            float offset = absPos % spriteWidth;
            offset -= spriteWidth / 2;
            if (lastPositions[p] > 0 && offset < 0)
            {
                SpawnLayerAt(rightEdge + spriteWidth * 1.5f, i, p);
            }
            lastPositions[p] = offset;
        }
    }

    void SpawnLayerAt(float x, int index, ParallaxLayer p)
    {
        // Spawn a new sprite
        GameObject newObject = new GameObject("ParalaxLayer");
        newObject.transform.SetParent(gameObject.transform);
        newObject.transform.position = new Vector3(x, p.yPos, 0);
        newObject.transform.localScale = new Vector3(1, p.yStretch, 1);
        newObject.transform.parent = transform;

        newObject.AddComponent<SpriteRenderer>();
        SpriteRenderer renderer = newObject.GetComponent<SpriteRenderer>();
        renderer.sprite = p.sprite;
        renderer.sortingOrder = -41 - (layers.Count - index);

        newObject.AddComponent<ParallaxObject>();
        ParallaxObject po = newObject.GetComponent<ParallaxObject>();
        po.movementFactor = p.movementFactor;
        po.sprite = p.sprite;
    }
}
