using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonorHealth : MonoBehaviour, IDamageable
{
    private float health = 25f;
    private Donor donor;
    private DonorController donorController;
    private void Start()
    {
        donorController= GetComponent<DonorController>();
        donor= GetComponent<Donor>();
    }
    public bool damage(float damage, Transform transform)
    {
        donorController.setTarget(transform);
        health -= damage;
        if (health <= 0)
        {
            donor.kill();
            return true;
        }
        return false;
    }
}
