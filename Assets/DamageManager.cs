using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageManager : MonoBehaviour, IHittable
{
    private bool isHittable = true;
    [SerializeField] InventoryManager inventoryManager;
    public float test = 0f;
    private float invincibilityPeriod = 2f;
    public bool hit()
    {
        if (isHittable)
        {
            // List<Organ> organs = inventoryManager.getInventory().getCurrentOrgans();
            // if (organs != null)
            // {
            //     int random = (int)Random.Range(0f, organs.Count);
            //     organs[random].Quality -= .05f;
            // }
            // isHittable = false;
            // StartCoroutine(StartCooldown());
            // return true;
        }
        return false;
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(invincibilityPeriod);
        isHittable = true;
    }
}
