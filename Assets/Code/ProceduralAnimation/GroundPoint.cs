using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundPoint : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private float forwardDistance = .5f;
    private float originalHeight;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private Vector3 originalLocalPos;

    void Start()
    {
        originalLocalPos = transform.localPosition;
        originalHeight = -1;
        agent = GetComponentInParent<NavMeshAgent>();
        rb = GetComponentInParent<Rigidbody>();
        StartCoroutine(StayOnGround());
    }

    private IEnumerator StayOnGround()
    {
        while (true)
        {
            Vector3 point;
            Vector3 origin = transform.position;
            origin.y = transform.parent.position.y + originalHeight;

            // if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, maxDistance, 7))
            // {
            //     point = hit.point;
            // }
            // else
            // {
                point = transform.position;
                point.y = transform.parent.position.y + originalHeight - maxDistance;
            // }

            transform.position = point;

            if (agent != null && agent.velocity.magnitude != 0) 
            {
                Vector3 localPos = new Vector3(transform.localPosition.x, transform.localPosition.y, originalLocalPos.z);
                localPos.z = forwardDistance;
                transform.localPosition = localPos;
            }
            else if (rb != null && rb.velocity.magnitude != 0)
            {
                Vector3 localPos = new Vector3(transform.localPosition.x, transform.localPosition.y, originalLocalPos.z);
                localPos.z = forwardDistance;
                transform.localPosition = localPos;
            }
            else
            {
                Vector3 localPos = new Vector3(transform.localPosition.x, transform.localPosition.y, originalLocalPos.z);
                localPos.z = 0;
                transform.localPosition = localPos;
            }
            yield return null;
        }
    }
}
