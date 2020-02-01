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
    private ParticleSystem particleSystem;

    public UnityEvent onDeathEvent;

    public float Health
    {
        get
        {
            return this.health;
        }
        set
        {
            bool died = health > 0 && value <= 0;
            value = Mathf.Max(value, 0);

            int oldPartIndex = (int)Mathf.Ceil(this.health /  (100 / 3));
            // TODO: Check if health is 0.0f and start a fancy explosion animation!
            this.health = value;
            int partIndex = Mathf.Min(3, (int)Mathf.Ceil(this.health /  (100 / 3)));
            if (partIndex < oldPartIndex) {
                particleSystem.Play();
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
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Repair(ItemType type, float amount) {
        if (Health > 0 && repairedBy.Contains(type)) {
            Health += amount;
            return true;
        }
        return false;
    }

    private void PlayBreakingSound()
    {
        this.GetComponent<AudioSource>().Play();
    }
}
