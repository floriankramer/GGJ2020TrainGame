﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float Health
    {
        get
        {
            return this.health;
        }
        set
        {
            if (value < 75 && (int)(this.health / 25) > (int)(value / 25)) {
                particleSystem.Play();
            }
            // TODO: Check if health is 0.0f and start a fancy explosion animation!
            this.health = value;
            int partIndex = (int)(this.health / 25);
            spriteRenderer.sprite = partSprites[partIndex];
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
}
