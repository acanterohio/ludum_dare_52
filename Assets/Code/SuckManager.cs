using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class SuckManager : MonoBehaviour
{
    [SerializeField] private GameObject brainPrefab, eyesPrefab, lungsPrefab, heartPrefab, bonePrefab;
    [SerializeField] private Transform playerSuckPoint;
    [SerializeField] private float suckRadius;
    [SerializeField] private UnityEvent firstSuck;
    [SerializeField] private Transform pickUpBones;
    [SerializeField] private Transform takeOrgans;
    [SerializeField] private AudioSource vacStart, vacRunning, vacEnd;
    private bool canSuck;
    private bool firstSuckDone;
    private bool sucking = false;

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1) && canSuck)
        {
            sucking = true;
            vacEnd.Stop();
            vacRunning.Stop();
            vacStart.Play();
            StartCoroutine(SuckSounds());
        }
        if (Input.GetMouseButtonUp(1) && sucking)
        {
            sucking = false;
            vacStart.Stop();
            vacRunning.Stop();
            vacEnd.Play();
        }
        if (Input.GetMouseButton(1) && canSuck)
        {
            if (!firstSuckDone)
            {
                firstSuck.Invoke();
                firstSuckDone = true;
            }
            Collider[] hitColliders = Physics.OverlapSphere(playerSuckPoint.position, suckRadius);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                ISuckable suckable = hitColliders[i].gameObject.GetComponent<ISuckable>();
                if (suckable != null )
                {
                    Item sucked = suckable.suck(transform);
                    if (sucked is NormalAmmo ammo)
                    {
                        pickUpBones.GetChild(Random.Range(0, pickUpBones.childCount)).GetComponent<AudioSource>().Play();
                        GameObject prefab = bonePrefab;
                        GameObject instance = Instantiate(prefab, hitColliders[i].transform.position, hitColliders[i].transform.rotation);
                        playerSuckPoint.rotation = Random.rotation;
                        instance.GetComponent<AnimateWithTransforms>().SetTargets(new List<Transform>() { instance.transform, playerSuckPoint });
                        instance.GetComponent<AnimateWithTransforms>().StartAnimation();
                        NavMeshAgent agent = hitColliders[i].GetComponentInChildren<NavMeshAgent>();
                        if (agent != null) agent.velocity = Vector3.zero;
                    }
                    if (sucked is Organ organ)
                    {
                        takeOrgans.GetChild(Random.Range(0, takeOrgans.childCount)).GetComponent<AudioSource>().Play();
                        print("Congrats you got: " + sucked.GetType().Name);
                        GameObject prefab;
                        if (organ is Brain) prefab = brainPrefab;
                        else if (organ is Eyes) prefab = eyesPrefab;
                        else if (organ is Heart) prefab = heartPrefab;
                        else prefab = lungsPrefab;
                        GameObject instance = Instantiate(prefab, hitColliders[i].transform.position, hitColliders[i].transform.rotation);
                        playerSuckPoint.rotation = Random.rotation;
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

    public void EnableSuck()
    {
        canSuck = true;
    }

    private IEnumerator SuckSounds()
    {
        while (vacStart.isPlaying && sucking)
        {
            yield return null;
        }
        while (sucking)
        {
            if (!vacRunning.isPlaying) vacRunning.Play();
            yield return null;
        }
    }
}
