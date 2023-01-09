using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuckManager : MonoBehaviour
{
    [SerializeField] private GameObject brainPrefab, eyesPrefab, lungsPrefab, heartPrefab;
    [SerializeField] private Transform playerSuckPoint;
    [SerializeField] private float suckRadius;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            Collider[] hitColliders = Physics.OverlapSphere(playerSuckPoint.position, suckRadius);
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
                        GameObject prefab;
                        if (organ is Brain) prefab = brainPrefab;
                        else if (organ is Eyes) prefab = eyesPrefab;
                        else if (organ is Heart) prefab = heartPrefab;
                        else prefab = lungsPrefab;
                        GameObject instance = Instantiate(prefab, hitColliders[i].transform.position, hitColliders[i].transform.rotation);
                        instance.GetComponent<AnimateWithTransforms>().SetTargets(new List<Transform>() { instance.transform, playerSuckPoint });
                        instance.GetComponent<AnimateWithTransforms>().StartAnimation();
                        foreach (ParticleSystem ps in hitColliders[i].GetComponentsInChildren<ParticleSystem>())
                        {
                            if (ps.name == "BloodSpurt") ps.Play();
                        }
                        NavMeshAgent agent = hitColliders[i].GetComponentInChildren<NavMeshAgent>();
                        if (agent != null) agent.velocity = Vector3.zero;
                    }
                    if (sucked is Empty)
                    {
                        print("It's all out :(");
                    }
                    Inventory.Instance.AddItem(sucked);
                    GameObject.Find("InventoryManager").GetComponent<InventoryManager>().UpdateAllSlots();
                }
            }
        }
    }
}
