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

    private AudioSource audioSource;

    public AudioClip PickupSound;
    public AudioClip RepairingSound;

    private static bool HasItemInHand = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && following)
        {
            // Move it with the Mouse
            transform.position = GetWorldPositionFromMouse();

            // Find objects we might repair
            Collider2D[] hits = Physics2D.OverlapPointAll(GetWorldPositionFromMouse());
            // Ensure we always try to repair the closest object first.
            System.Array.Sort(hits, (a, b) => {
                float la = (a.gameObject.transform.position - gameObject.transform.position).magnitude;
                float lb = (a.gameObject.transform.position - gameObject.transform.position).magnitude;
                return  la == lb ? 0 : (la < lb ? -1 : 1);
            });

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

                        if (counter == maxCounter || !audioSource.isPlaying) {
                            audioSource.clip = RepairingSound;
                            audioSource.loop = true;
                            audioSource.Play();
                        }

                        OnStartRepair();
                        
                        counter -= Time.deltaTime;

                        progressBarBar.SetProgress(counter / maxCounter);

                        foundPart = true;
                        if (counter < 0)
                        {
                            colPart.Repair(itemType, 100);
                            OnStopRepair();
                            HasItemInHand = false;
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
        if (progressBar == null) {
            progressBar = GameObject.Instantiate(ProgressBarTemplate, new Vector3(0, 0, 0), new Quaternion());
            progressBar.transform.SetParent(GameObject.Find("Canvas").transform);
            progressBar.transform.position = new Vector3(0, 0, -5);
            progressBar.transform.localScale = new Vector3(0.2f, 0.2f, 1);
            progressBar.transform.rotation = new Quaternion();

            progressBarBar = progressBar.GetComponent<ProgressBar>();
            progressBarBar.target = gameObject;
        }

    }

    void OnStopRepair()
    {
        if (audioSource.clip == PickupSound) {
            audioSource.Stop();
        }
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
        if (HasItemInHand) {
            return;
        }
        HasItemInHand= true;
        following = true;

        audioSource.clip = PickupSound;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void OnMouseUpAll()
    {
        HasItemInHand = false;
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

        audioSource.clip = PickupSound;
        audioSource.loop = false;
        audioSource.Play();
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
