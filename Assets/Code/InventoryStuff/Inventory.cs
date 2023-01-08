using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private static Inventory instance;
    public static Inventory Instance
    {
        get
        {
            if (instance == null) instance = new Inventory();
            return instance;
        }
    }

    const int size = 24;

    private Organ[] inventoryItems = new Organ[size];
    public int ammoCount = 0;
    public Brain currentBrain;
    public Eyes currentEyes;
    public Lungs currentLungs;
    public Heart currentHeart;
    private float cash = 0;
    public float Cash
    {
        get
        {
            return cash;
        }
    }

    private Inventory() {}

    public bool AddItem(Item item)
    {
        bool success = false;

        if (currentBrain == null && item is Brain brain)
        {
            currentBrain = brain;
            success = true;
        }
        else if (currentEyes == null && item is Eyes eyes)
        {
            currentEyes = eyes;
            success = true;
        }
        else if (currentLungs == null && item is Lungs lungs)
        {
            currentLungs = lungs;
            success = true;
        }
        else if (currentHeart == null && item is Heart heart)
        {
            currentHeart = heart;
            success = true;
        }
        else if (item is Organ organ)
        {
            for (int i = 0; i < size; i++)
            {
                if (inventoryItems[i] == null)
                {
                    inventoryItems[i] = organ;
                    success = true;
                    break;
                }
            }
        }
        else if (item is Ammo)
        {
            ammoCount += 5; // idk
        }

        return success;
    }

    public void RemoveItem(int slot)
    {
        inventoryItems[slot] = null;
    }

    public void SellItem(int slot)
    {
        cash += inventoryItems[slot].Value;
        inventoryItems[slot] = null;
    }

    public Organ GetItem(int slot)
    {
        return inventoryItems[slot];
    }

    public int ItemCount()
    {
        int count = 0;
        foreach (Organ o in inventoryItems)
        {
            if (o != null) count++;
        }
        return count;
    }

    public void MoveItem(int originalSlot, int destinationSlot)
    {
        Organ temp = inventoryItems[destinationSlot];
        inventoryItems[destinationSlot] = inventoryItems[originalSlot];
        inventoryItems[originalSlot] = temp;
    }

    public void EquipBrain(int slot)
    {
        if (inventoryItems[slot] is Brain brain)
        {
            inventoryItems[slot] = currentBrain;
            currentBrain = brain;
        }
        else if (inventoryItems[slot] == null)
        {
            inventoryItems[slot] = currentBrain;
            currentBrain = null;
        }
    }

    public void EquipEyes(int slot)
    {
        if (inventoryItems[slot] is Eyes eyes)
        {
            inventoryItems[slot] = currentEyes;
            currentEyes = eyes;
        }
        else if (inventoryItems[slot] == null)
        {
            inventoryItems[slot] = currentEyes;
            currentEyes = null;
        }
    }

    public void EquipLungs(int slot)
    {
        if (inventoryItems[slot] is Lungs lungs)
        {
            inventoryItems[slot] = currentLungs;
            currentLungs = lungs;
        }
        else if (inventoryItems[slot] == null)
        {
            inventoryItems[slot] = currentLungs;
            currentLungs = null;
        }
    }

    public void EquipHeart(int slot)
    {
        if (inventoryItems[slot] is Heart heart)
        {
            inventoryItems[slot] = currentHeart;
            currentHeart = heart;
        }
        else if (inventoryItems[slot] == null)
        {
            inventoryItems[slot] = currentHeart;
            currentHeart = null;
        }
    }

}
