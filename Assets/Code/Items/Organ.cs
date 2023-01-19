using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Organ : Item
{
    private float decayRate = 1f / 120f;
    private float quality;
    public float Quality
    {
        get
        {
            return quality;
        }
        set
        {
            quality = Mathf.Clamp(value, 0f, 1f);
        }
    }
    private float originalValue = 100;
    public float Value
    {
        get
        {
            return originalValue * quality * quality;
        }
    }

    public Organ()
    {
        quality = .1f + .9f * Random.value;
        
    }

    public Organ(float qualityModifier)
    {
        quality = .1f + .9f * Random.value;
        // quality = Random.value * qualityModifier;
    }


    public void Update(float mult = 1f)
    {
        quality -= Time.deltaTime * decayRate * mult;
        quality = Mathf.Max(0, quality);
    }
    
}