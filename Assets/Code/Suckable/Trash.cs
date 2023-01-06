using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour, ISuckable
{
    private int itemsLeft = 3;
    public Item suck()
    {
        if (itemsLeft > 0)
        {
            itemsLeft--;
            return new NormalAmmo();
        }
        return null;
    }
}
