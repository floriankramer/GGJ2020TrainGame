using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityHealth : MonoBehaviour
{
    public Part target;
    public float exponent;

    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        this.renderer = GetComponent<SpriteRenderer>();
        this.target.onHealthChangedEvent.AddListener(UpdateOpacity);
    }

    void UpdateOpacity()
    {
        float opacity = Mathf.Pow(1.0f - (target.Health * 0.01f), exponent);
        this.renderer.color = new Color(this.renderer.color.r, this.renderer.color.g, this.renderer.color.b, opacity);
        Debug.Log("Health changed");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
