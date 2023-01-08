using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Start()
    {
        StartCoroutine(CopyTargetRotation());
    }

    private IEnumerator CopyTargetRotation()
    {
        while (true)
        {
            transform.rotation = target.rotation;
            yield return null;
        }
    }
}
