using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DonorController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform donorTransform;
    private Transform playerTransform;
    private int angerLevel = 0;

    private List<Vector3> destinations;

    private bool isFrozen = false;
    private float frozenPeriod = 3f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        donorTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFrozen)
        {
            agent.speed = 0f;
            return;
        }
        if (angerLevel == 5)
        {
            Destroy(GetComponent<NavMeshAgent>());
            donorTransform.rotation = Quaternion.Euler(90, 0, 0);
            angerLevel = 6;
        }
        float speed = 12f;
        switch (angerLevel)
        {

            case 0:
                break;
            case 1:
                speed = 14f;
                break;
            case 2:
                speed = 15f;
                break;
            case 3:
                speed = 20f;
                break;
            case 4:
                speed = 25f;
                break;
            case 6: //fallen
                return;
        }
        agent.speed = speed;
        if (destinations != null && angerLevel == 0 && agent.velocity.sqrMagnitude < .1f)
        {
            int random = (int) Random.Range(0, destinations.Count);
            agent.SetDestination(destinations[random]);
        }
        else if (playerTransform != null)
        {
            
            agent.SetDestination(playerTransform.position);
        }

    }

    public void updateAngerLevel(int anger)
    {
        angerLevel = anger;
    }
    public void setTarget(Transform transform)
    {
        if (angerLevel == 0)
        {
            angerLevel = 1;
        }
        playerTransform = transform;
    }

    public void setDestinations(List<Vector3> destinations)
    {
        this.destinations = destinations;
    }

    public void setFrozen()
    {
        isFrozen= true;
        StartCoroutine(StartCooldown());

    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(frozenPeriod);
        isFrozen = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IHittable hittable = collision.gameObject.GetComponent<IHittable>();
        if (hittable != null)
        {
            if (angerLevel != 0 && angerLevel < 5 && hittable.hit())
            {
                print("Player hit");
                setFrozen();
            }
        }
    }
}
