using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckManager : MonoBehaviour

{
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + gameObject.transform.forward * 2, new Vector3(1, 1, 1), Quaternion.identity);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                ISuckable suckable =  hitColliders[i].gameObject.GetComponent<ISuckable>();
                if (suckable != null )
                {
                    suckable.suck();
                }
            }
        }
    }
}
