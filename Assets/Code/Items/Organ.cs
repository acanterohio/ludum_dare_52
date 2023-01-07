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

    public Organ()
    {
        quality = Random.value;
    }
    
}