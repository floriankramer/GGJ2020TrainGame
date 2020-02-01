using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public RescourceType rescourceType = RescourceType.Wood;
    public float health = 100;

    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Consume(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            RemoveExistance();
        }
    }

    private void RemoveExistance()
    {
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        //transform.Translate(10,0,0);
    }

    void OnMouseDrag()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;
        transform.position = worldPoint;
    }

    void OnMouseUp()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;
        Collider2D[] hits = Physics2D.OverlapPointAll(worldPoint);
        Debug.unityLogger.LogWarning("Item", hits.Length);
        bool foundStorage = false;

        foreach(Collider2D hit in hits )
        {
            if("StorageRoom" == hit.gameObject.tag)
            {
                foundStorage = true;
                transform.SetParent(hit.gameObject.transform);
                transform.localPosition = new Vector3(0,0,0);
                break;
            }

            if("Part" == hit.gameObject.tag)
            {
                
                Consume(health);
            }
        }

        if(!foundStorage)
        {
            // TODO: Where do we parent to?
            transform.SetParent(transform.parent.parent);
        }
    }


    public enum RescourceType {
        Wood,
        Steel
    }
}
