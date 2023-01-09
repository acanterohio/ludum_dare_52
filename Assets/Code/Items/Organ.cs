using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Organ : Item
{
    private float decayRate = 1f / 60f;
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
        quality = Random.value;
        
    }

    public Organ(float qualityModifier)
    {
        quality = Random.value * qualityModifier;
    }


    public void Update(float mult = 1f)
    {
        quality -= Time.deltaTime * decayRate * mult;
        quality = Mathf.Max(0, quality);
    }
    
}