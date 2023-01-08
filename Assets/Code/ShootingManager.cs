using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] GameObject bonePrefab;
    [SerializeField] Transform boneParentTransform;
    [SerializeField] private Transform shootPosition;
    private Transform playerTransform;
    private float boneSpeed = 100f;

    private void Start()
    {
        playerTransform = GetComponent<Transform>();
        Inventory.Instance.ammoCount += 5;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Inventory.Instance.ammoCount > 0)
            {
                Inventory.Instance.ammoCount--;
                GameObject bone = Instantiate(bonePrefab, shootPosition.position, Quaternion.identity, boneParentTransform);
                Bone boneScript = bone.GetComponent<Bone>();
                boneScript.setPlayer(transform);
                bone.GetComponent<Rigidbody>().velocity = playerTransform.forward * boneSpeed;
            }
        }
    }
}
