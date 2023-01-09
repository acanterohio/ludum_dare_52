using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour, ISuckable
{
    private int itemsLeft = 3;
    private float suckCooldown = 3f;
    private bool onCooldown = false;

    [SerializeField] private GameObject trashBagOne;
    [SerializeField] private GameObject trashBagTwo;
    [SerializeField] private GameObject trashBagThree;

    private bool cycleStarted = false;
    private float restoreRate = 60f;


    public Item suck(Transform transform)
    {
        if (!cycleStarted)
        {
            StartCoroutine(TrashCycle());
            cycleStarted = true;
        }
        if (!onCooldown)
        {
            onCooldown = true;
            StartCoroutine(StartCooldown());
            if (itemsLeft > 0)
            {
                itemsLeft--;
                setTrashCount(itemsLeft);
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

    private IEnumerator TrashCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(restoreRate);
            if (itemsLeft < 3)
            {
                itemsLeft++;
                setTrashCount(itemsLeft);
            }
        }
        
    }

    private void setTrashCount(int trashLeft)
    {
        switch (itemsLeft)
        {
            case 3:
                trashBagOne.SetActive(true);
                trashBagTwo.SetActive(true);
                trashBagThree.SetActive(true);
                break;
            case 2:
                trashBagOne.SetActive(false);
                trashBagTwo.SetActive(true);
                trashBagThree.SetActive(true);
                break;
            case 1:
                trashBagOne.SetActive(false);
                trashBagTwo.SetActive(false);
                trashBagThree.SetActive(true);
                break;
            case 0:
                trashBagOne.SetActive(false);
                trashBagTwo.SetActive(false);
                trashBagThree.SetActive(false);
                break;
        }
    }
}
