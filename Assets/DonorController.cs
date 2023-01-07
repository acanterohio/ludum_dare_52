using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DonorController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] Transform playerTransform;
    private int angerLevel = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float speed = 0;
        switch (angerLevel)
        {
            
            case 0:
                return;
            case 1:
                speed = 2f;
                break;
            case 2:
                speed = 4f;
                break;
            case 3:
                speed = 7f;
                break;
            case 4:
                speed = 10f;
                break;
        }
        agent.speed = speed;
        agent.SetDestination(playerTransform.position);
    }

    public void updateAngerLevel(int anger)
    {
        angerLevel= anger;
    }
}
