using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemScript : MonoBehaviour
{
    public ItemType itemType = ItemType.Wood;
    bool following = false;
    bool isCollected = false;
    float counter = 2f;
    float maxCounter = 2f;
    float repairValue;

    public GameObject ProgressBarTemplate;
    public GameObject CantRepairTemplate;

    private GameObject progressBar;
    private ProgressBar progressBarBar;

    private GameObject cantRepair = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && following)
        {
            // Move it with the Mouse
            transform.position = GetWorldPositionFromMouse();

            Collider2D[] hits = Physics2D.OverlapPointAll(GetWorldPositionFromMouse());

            bool foundPart = false;
            bool foundPartButCantRepair = false;
            foreach (Collider2D hit in hits)
            {
                Part colPart = hit.gameObject.GetComponent<Part>();
                if (colPart != null)
                {
                    foundPartButCantRepair = true;
                    if (colPart.CanRepair(itemType))
                    {
                        foundPartButCantRepair = false;
                        if (counter == maxCounter)
                        {
                            OnStartRepair();
                        }
                        counter -= Time.deltaTime;

                        progressBarBar.SetProgress(counter / maxCounter);

                        foundPart = true;
                        if (counter < 0)
                        {
                            colPart.Repair(itemType, 100);
                            OnStopRepair();
                            Destroy(gameObject);
                        }
                        break;
                    }
                }

            }
            if (!foundPart)
            {
                counter = maxCounter;
                OnStopRepair();
            }
            if (foundPartButCantRepair) {
                OnCantRepair();
            } else {
                OnStopCantRepair();
            }

        }
    }

    void OnStartRepair()
    {
        progressBar = GameObject.Instantiate(ProgressBarTemplate, new Vector3(0, 0, 0), new Quaternion());
        progressBar.transform.SetParent(GameObject.Find("Canvas").transform);
        progressBar.transform.position = new Vector3(0, 0, -5);
        progressBar.transform.localScale = new Vector3(0.2f, 0.2f, 1);
        progressBar.transform.rotation = new Quaternion();

        progressBarBar = progressBar.GetComponent<ProgressBar>();
        progressBarBar.target = gameObject;

    }

    void OnStopRepair()
    {
        if (progressBar != null)
        {
            Destroy(progressBar);
        }
        progressBar = null;
    }

    void OnCantRepair() {
        if (cantRepair == null) {
            cantRepair = GameObject.Instantiate(CantRepairTemplate, new Vector3(0, 0, 0), new Quaternion());
            cantRepair.transform.SetParent(GameObject.Find("Canvas").transform);
            cantRepair.transform.position = new Vector3(0, 0, -5);
            cantRepair.transform.localScale = new Vector3(0.2f, 0.2f, 1);
            cantRepair.transform.rotation = new Quaternion();

            ProgressBar pb  = cantRepair.GetComponent<ProgressBar>();
            pb.target = gameObject;
            pb.SetProgress(1);
        }
    }

    void OnStopCantRepair() {
        if (cantRepair != null) {
            Destroy(cantRepair);
        }
        cantRepair = null;
    }


    public void OnMouseDownAll()
    {
        following = true;
    }

    public void OnMouseUpAll()
    {
        following = false;

        Collider2D[] hits = Physics2D.OverlapPointAll(GetWorldPositionFromMouse());

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.tag == "StorageRoom")
            {
                if (hit.transform.childCount == 0)
                {
                    transform.parent = hit.transform;
                    isCollected = true;
                }
            }
        }
        if (isCollected)
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
        OnStopRepair();
        OnStopCantRepair();
    }

    // If the Mouse is above this Object
    void OnMouseOver()
    {

    }

    private Vector3 GetWorldPositionFromMouse()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = -1;
        return worldPoint;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        counter = maxCounter;
    }
}
