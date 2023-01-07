using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LungsSlot : InventorySlot
{
    public override void OnStart()
    {
        slot = -1;
    }

    public override void UpdateSlot()
    {
        Item item = Inventory.Instance.currentLungs;
        UpdateSlotWithItem(item);
    }
    
    public override void DropItem()
    {
        manager.DropLungs();
    }

    public override void ItemDropped(int originalSlot)
    {
        if (hovering && Inventory.Instance.GetItem(originalSlot) is Lungs)
        {
            Inventory.Instance.EquipLungs(originalSlot);
        }
    }
}
