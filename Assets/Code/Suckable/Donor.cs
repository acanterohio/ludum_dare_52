using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donor : MonoBehaviour, ISuckable
{
    private List<Item> organs = new List<Item>()
    {
        new Brain(),
        new Eyes(),
        new Lungs(),
        new Heart()
    };

    private float suckCooldown = 3f;
    private bool onCooldown = false;
    public Item suck()
    {
        if (!onCooldown)
        {
            onCooldown = true;
            StartCoroutine(StartCooldown());
            if (organs.Count > 0)
            {
                int index = (int)Random.Range(0f, organs.Count);
                Item item = organs[index];
                organs.RemoveAt(index);
                return item;
            }
            
            return new Empty();
        }
        return null;
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(suckCooldown);
        onCooldown = false;
    }
}
