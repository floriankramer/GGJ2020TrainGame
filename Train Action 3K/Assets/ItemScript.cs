using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public RescourceType rescourceType = RescourceType.Wood;
    public float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
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

    }


    public enum RescourceType {
        Wood,
        Steel
    }
}
