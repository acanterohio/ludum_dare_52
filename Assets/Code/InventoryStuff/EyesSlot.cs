using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesSlot : InventorySlot
{
    public override void OnStart()
    {
        slot = -1;
    }

    public override void UpdateSlot()
    {
        Item item = Inventory.Instance.currentEyes;
        UpdateSlotWithItem(item);
    }
    
    public override void DropItem()
    {
        manager.DropEyes();
    }

    public override void ItemDropped(int originalSlot)
    {
        if (hovering && Inventory.Instance.GetItem(originalSlot) is Eyes)
        {
            Inventory.Instance.EquipEyes(originalSlot);
        }
    }
}
