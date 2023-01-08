using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Phone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool hovering;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }

    public void ItemDropped(int slot)
    {
        if (hovering)
        {
            Inventory.Instance.RemoveItem(slot);
        }
    }
}
