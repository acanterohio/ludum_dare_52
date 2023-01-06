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
                    Item sucked = suckable.suck();
                    if (sucked is NormalAmmo ammo)
                    {
                        print("POW POW NORMAL AMMO");
                    }
                    if (sucked is null)
                    {
                        print("EMPTYYYYY");
                    }
                }
            }
        }
    }
}
