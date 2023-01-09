using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donor : MonoBehaviour, ISuckable
{
    private List<Item> organs;
    private DonorController donorController;
    [SerializeField] GameObject hat;
    [SerializeField] GameObject stripe;
    [SerializeField] GameObject brain;
    [SerializeField] GameObject eyeOne;
    [SerializeField] GameObject eyeTwo;
    [SerializeField] GameObject lungOne;
    [SerializeField] GameObject lungTwo;
    [SerializeField] GameObject heart;
    private void Start()
    {
        organs = new List<Item>()
        {
            new Brain(),
            new Eyes(),
            new Lungs(),
            new Heart()
        };
        donorController = GetComponent<DonorController>();
    }

    private float suckCooldown = 3f;
    private bool onCooldown = false;
    private bool isDead = false;
    private bool bodyHarvested = false;

    private void LateUpdate()
    {
        if (bodyHarvested)
        {
            Destroy(gameObject);
        }
    }
    public Item suck(Transform transform)
    {
        if (!isDead) {
            if (!onCooldown)
            {
                onCooldown = true;
                StartCoroutine(StartCooldown());
                donorController.setTarget(transform);
                if (organs.Count > 0)
                {
                    int index = (int)Random.Range(0f, organs.Count);
                    Item item = organs[index];
                    organs.RemoveAt(index);
                    donorController.updateAngerLevel(4 - organs.Count);
                    if (item is Brain)
                    {
                        hat.SetActive(false);
                        brain.SetActive(false);
                    } else if (item is Eyes)
                    {
                        eyeOne.SetActive(false);
                        eyeTwo.SetActive(false);
                    } else if (item is Lungs)
                    {
                        stripe.SetActive(false);
                        lungOne.SetActive(false);
                        lungTwo.SetActive(false);
                    } else if (item is Heart)
                    {
                        stripe.SetActive(false);
                        heart.SetActive(false);
                    }
                    return item;
                }

                return new Empty();
            }
            return null;
        }
        if (!bodyHarvested)
        {
            bodyHarvested = true;
            return new NormalAmmo();
        }
        return null;
    }

    public void kill()
    {
        if (!isDead)
        {
            isDead = true;
            donorController.updateAngerLevel(5);
        }
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(suckCooldown);
        onCooldown = false;
    }
}
