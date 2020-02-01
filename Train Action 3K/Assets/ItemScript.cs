using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public RescourceType rescourceType = RescourceType.Wood;
    public float health = 100;

    private BoxCollider2D boxCollider;
    private Vector2 originPosition;
    private bool foundStorage = false;

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
        originPosition = GetWorldPositionFromMouse();
    }

    void OnMouseDrag()
    {
        transform.position = GetWorldPositionFromMouse();
    }

    void OnMouseUp()
    {
        Collider2D[] hits = Physics2D.OverlapPointAll(GetWorldPositionFromMouse());
        Debug.unityLogger.LogWarning("Item", hits.Length);

        foreach(Collider2D hit in hits )
        {
            if("StorageRoom" == hit.gameObject.tag)
            {
                foundStorage = true;
                transform.SetParent(hit.gameObject.transform);
                transform.localPosition = new Vector3(0,0,0);
                break;
            }
            else if("Part" == hit.gameObject.tag)
            {
                // TODO: Call repair method from part
                hit.gameObject.GetComponent<Part>().Health = 100;
                Consume(health);
            }
            else
            {
                transform.SetPositionAndRotation(originPosition,new Quaternion());
            }
        }

        if(!foundStorage)
        {
            // TODO: Where do we parent to?
            //transform.SetParent(transform.parent.parent);
            //Consume(health);
        }
    }

    private Vector2 GetWorldPositionFromMouse()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;
        return worldPoint;
    }


    public enum RescourceType {
        Wood,
        Steel
    }
}
