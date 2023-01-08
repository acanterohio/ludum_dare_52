using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPoint : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    private float originalHeight;

    void Start()
    {
        originalHeight = transform.position.y;
        StartCoroutine(StayOnGround());
    }

    private IEnumerator StayOnGround()
    {
        while (true)
        {
            Vector3 point;
            Vector3 origin = transform.position;
            origin.y = originalHeight;

            if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, maxDistance, 7))
            {
                point = hit.point;
            }
            else
            {
                point = transform.position;
                point.y = originalHeight - maxDistance;
            }
            transform.position = point;
            yield return null;
        }
    }
}
