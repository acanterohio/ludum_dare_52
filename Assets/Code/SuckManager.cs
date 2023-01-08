using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckManager : MonoBehaviour

{
 //assuming on player
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + gameObject.transform.forward * 2, new Vector3(1, 1, 1), gameObject.transform.rotation);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                ISuckable suckable =  hitColliders[i].gameObject.GetComponent<ISuckable>();
                if (suckable != null )
                {
                    Item sucked = suckable.suck(transform);
                    if (sucked is NormalAmmo ammo)
                    {
                        print("Found some ammo!");
                    }
                    if (sucked is Organ organ)
                    {
                        print("Congrats you got: " + sucked.GetType().Name);
                    }
                    if (sucked is Empty)
                    {
                        print("It's all out :(");
                    }
                    Inventory.Instance.AddItem(sucked);
                }
            }
        }
    }
}
