using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerPickup : MonoBehaviour
{
    public float powerTime = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FlameThrower[] fts = GameObject.Find("Train").GetComponentsInChildren<FlameThrower>();

            foreach (FlameThrower ft in fts)
            {
                ft.poweredFor += powerTime;
            }
            Destroy(gameObject);
        }
    }
}
