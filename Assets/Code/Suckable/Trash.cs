using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour, ISuckable
{
    private int itemsLeft = 3;
    private float suckCooldown = .3f;
    private bool onCooldown = false;
    public Item suck(Transform transform)
    {
        if (!onCooldown)
        {
            onCooldown = true;
            StartCoroutine(StartCooldown());
            if (itemsLeft > 0)
            {
                itemsLeft--;
                int rng = Random.Range(0, 40);
                switch (rng)
                {
                    case 0:
                        return new Brain();
                    case 1:
                        return new Eyes();
                    case 2:
                        return new Lungs();
                    case 3:
                        return new Heart();
                    default:
                        return new NormalAmmo();

                }
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
