using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonorAttack : MonoBehaviour
{
    private DonorController donorController;
    private void Start()
    {
        donorController= GetComponent<DonorController>();
    }
    
}
