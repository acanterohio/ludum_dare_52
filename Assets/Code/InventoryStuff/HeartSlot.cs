using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSlot : InventorySlot
{
    public override void OnStart()
    {
        slot = -1;
    }

    public override void UpdateSlot()
    {
        Item item = Inventory.Instance.currentHeart;
        UpdateSlotWithItem(item);
    }
    
    public override void DropItem()
    {
        manager.DropHeart();
    }

    public override void ItemDropped(int originalSlot)
    {
        if (hovering && Inventory.Instance.GetItem(originalSlot) is Heart)
        {
            Inventory.Instance.EquipHeart(originalSlot);
        }
    }
}
