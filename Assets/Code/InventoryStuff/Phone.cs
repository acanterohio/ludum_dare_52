using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Phone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool hovering;
    [SerializeField] private TextMeshProUGUI phoneText, cornerText;
    [SerializeField] private AudioSource sellSound;

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
            Inventory.Instance.SellItem(slot);
            sellSound.Play();
            UpdateCashText();
        }
    }

    private void UpdateCashText()
    {
        string text = "$" + Mathf.Round(Inventory.Instance.Cash * 100) / 100f;
        phoneText.text = text;
        cornerText.text = text;
    }
}
