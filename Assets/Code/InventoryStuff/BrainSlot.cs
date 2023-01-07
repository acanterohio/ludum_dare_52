using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainSlot : InventorySlot
{
    public override void OnStart()
    {
        slot = -1;
    }

    public override void UpdateSlot()
    {
        Item item = Inventory.Instance.currentBrain;
        UpdateSlotWithItem(item);
    }
    
    public override void DropItem()
    {
        manager.DropBrain();
    }

    public override void ItemDropped(int originalSlot)
    {
        if (hovering && Inventory.Instance.GetItem(originalSlot) is Brain)
        {
            Inventory.Instance.EquipBrain(originalSlot);
        }
    }
}
