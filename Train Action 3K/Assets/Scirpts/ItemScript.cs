using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public RescourceType rescourceType = RescourceType.Wood;
    
    [SerializeField]
    [Range(0.0f, 100.0f)]
    public float health = 100.0f;

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
        originPosition = transform.position;
    }

    void OnMouseDrag()
    {
        transform.position = GetWorldPositionFromMouse();
    }

    void OnMouseUp()
    {
        Collider2D[] hits = Physics2D.OverlapPointAll(GetWorldPositionFromMouse());

        foreach(Collider2D hit in hits )
        {
            PerformItemInteraction(hit.gameObject);
        }
    }

    private Vector2 GetWorldPositionFromMouse()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;
        return worldPoint;
    }

    private void PerformItemInteraction(GameObject interactionObject)
    {
        switch (interactionObject.tag)
        {
            case "StorageRoom":
                StoreItem(interactionObject);
                break;
            case "Part":
                InteractWithPart(interactionObject);
                break;
            default:
                SnapItemBack();
                break;
        }

    }

    private void StoreItem(GameObject storageObject)
    {
        foundStorage = true;
        transform.SetParent(storageObject.transform);
        transform.localPosition = new Vector3(0,0,0);
    }

    private void InteractWithPart(GameObject partObject)
    {
        // TODO: Call repair method from part
        partObject.GetComponent<Part>().Health = 100;
        Consume(health);
    }
    private void SnapItemBack()
    {
        transform.SetPositionAndRotation(originPosition,new Quaternion());
    }


    public enum RescourceType {
        Wood,
        Steel
    }
}
