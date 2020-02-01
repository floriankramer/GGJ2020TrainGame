using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public ItemType itemType = ItemType.Wood;

    [SerializeField]
    [Range(0.0f, 100.0f)]
    public float health = 100.0f;

    private BoxCollider2D boxCollider;
    public Vector2 originPosition;
    public Vector2 backgroundPosition;
    private bool isStored = false;
    private bool isInBackground = true;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        originPosition = transform.position;
        backgroundPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*float xPosition = backgroundPosition.x + Train.trainSpeed * Time.deltaTime * -1;
        backgroundPosition = new Vector3(xPosition, transform.position.y);

        if (isInBackground && !isStored)
        {
            SyncSpeedWithTrain(backgroundPosition);
        }*/

        if (transform.position.x < Camera.main.transform.position.x - 15)
        {
            Destroy(gameObject);
        }
    }

    private void SyncSpeedWithTrain(Vector2 backgroundPosition)
    {
        transform.position = backgroundPosition;
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
        isInBackground = false;
        originPosition = transform.position;
    }

    void OnMouseDrag()
    {
        transform.position = GetWorldPositionFromMouse();
    }

    void OnMouseUp()
    {
        Collider2D[] hits = Physics2D.OverlapPointAll(GetWorldPositionFromMouse());

        foreach (Collider2D hit in hits)
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
        isStored = true;
        transform.SetParent(storageObject.transform);
        transform.localPosition = new Vector3(0, 0, 0);
    }

    private void InteractWithPart(GameObject partObject)
    {
        // TODO: Call repair method from part
        bool isRepaired = partObject.GetComponent<Part>().Repair(this.itemType, health);
        Debug.unityLogger.LogWarning("Interact", isRepaired);
        if (isRepaired)
        {
            Consume(health);
        }
        else
        {
            SnapItemBack();
        }
    }
    private void SnapItemBack()
    {
        Vector2 positionToSnap;
        if (isStored){
            positionToSnap = originPosition;
        }
        else
        {
            positionToSnap = backgroundPosition;
            isInBackground = true;
        }
        transform.SetPositionAndRotation(positionToSnap, new Quaternion());
    }
}
