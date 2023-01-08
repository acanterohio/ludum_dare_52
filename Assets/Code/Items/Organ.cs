using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Organ : Item
{
    private float quality;
    public float Quality
    {
        get
        {
            return quality;
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
    
}