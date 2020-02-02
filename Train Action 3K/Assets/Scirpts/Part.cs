using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Part : MonoBehaviour
{
    public PartType partType;
    public List<ItemType> repairedBy = new List<ItemType>();


    public Sprite[] partSprites = new Sprite[4];

    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float health = 100.0f;

    private SpriteRenderer spriteRenderer;
    private ParticleSystem damageParticleSystem;

    public UnityEvent onDeathEvent;
    public UnityEvent onHealthChangedEvent;

    private GameObject frameGameObject;

    public Sprite BoxSprite;

    public float Health
    {
        get
        {
            return this.health;
        }
        set
        {
            bool died = health > 0 && value <= 0;
            value = Mathf.Min(100, Mathf.Max(value, 0));

            int oldPartIndex = Mathf.Min(3, (int)Mathf.Ceil(this.health /  (100 / 3)));
            // TODO: Check if health is 0.0f and start a fancy explosion animation!
            this.health = value;
            if(this.onHealthChangedEvent != null)
                this.onHealthChangedEvent.Invoke();

            int partIndex = Mathf.Min(3, (int)Mathf.Ceil(this.health /  (100 / 3)));
            if (partIndex < oldPartIndex) {
                damageParticleSystem.Play();
                PlayBreakingSound();
            }
            spriteRenderer.sprite = partSprites[partIndex];
            if (died) {
                onDeathEvent.Invoke();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Health > 0) {
            spriteRenderer.color = Color.HSVToRGB(0.3f * (health / 100.0f), 1, 1);
        } else {
            spriteRenderer.color = Color.white;
        }
    }


    void OnRenderObject() {

    }

    public bool Repair(ItemType type, float amount) {
        if (Health > 0 && repairedBy.Contains(type)) {
            Health += amount;
            return true;
        }
        return false;
    }

    public bool CanRepair(ItemType type)
    {
        if (Health > 0 && repairedBy.Contains(type) && Health < 100)
        {
            return true;
        }
        return false;
    }

    private void PlayBreakingSound()
    {
        this.GetComponent<AudioSource>().Play();
    }

    private void BuildFrame() {
        frameGameObject = new GameObject();
        frameGameObject.transform.parent = gameObject.transform;
        frameGameObject.transform.localPosition = new Vector3(0, 0, 0);
        frameGameObject.transform.localScale = new Vector3(1, 1, 1);
        frameGameObject.transform.localRotation = new Quaternion();

        // frameGameObject.AddComponent<LineRenderer>();
        // LineRenderer lr = frameGameObject.GetComponent<LineRenderer>();
        // lr.useWorldSpace = false;

        Collider2D collider = GetComponent<Collider2D>();
        float width = collider.bounds.max.x - collider.bounds.min.x;
        float height = collider.bounds.max.y - collider.bounds.min.y;


        frameGameObject.AddComponent<SpriteRenderer>();
        SpriteRenderer renderer = frameGameObject.GetComponent<SpriteRenderer>();

        renderer.sprite = BoxSprite;
        renderer.sortingLayerName = "UI";
        renderer.size = new Vector2(width, height);

        // lr.positionCount = 5;
        // lr.SetPosition(0, new Vector3(-w2, -h2, 0));
        // lr.SetPosition(1, new Vector3(w2, -h2, 0));
        // lr.SetPosition(2, new Vector3(w2, h2, 0));
        // lr.SetPosition(3, new Vector3(-w2, h2, 0));
        // lr.SetPosition(4, new Vector3(-w2, -h2, 0));

        // lr.widthMultiplier = 0.03f;

        // lr.startColor = Color.white;
        // lr.endColor = Color.white;

        // lr.material = new Material(Shader.Find("Unlit/Color"));
        // lr.material.color = Color.white;

        // lr.sortingLayerName = "UI";

        // lr.enabled = false;
    }

    private void ShowFrame() {
        SpriteRenderer renderer = frameGameObject.GetComponent<SpriteRenderer>();
        renderer.enabled = true;
    }

    private void HideFrame() {
        SpriteRenderer renderer = frameGameObject.GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }
}
