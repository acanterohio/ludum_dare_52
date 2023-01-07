using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    void Update()
    {
        text.text = "x" + Inventory.Instance.ammoCount;
    }
}
