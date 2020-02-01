using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    // Start is called before the first frame update
    private Image progressBarImage;

    public GameObject target;

    void Start()
    {
        progressBarImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
    }

/// Progress is from 0 to 1
    public void SetProgress(float amount) {
        progressBarImage.fillAmount = 1 - amount;
    }
}
