using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] GameObject bonePrefab;
    [SerializeField] Transform boneParentTransform;
    private Transform playerTransform;
    private float boneSpeed = 100f;
    private void Start()
    {
        playerTransform = GetComponent<Transform>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bone = Instantiate(bonePrefab, playerTransform.position + playerTransform.forward * 1, Quaternion.identity, boneParentTransform);
            Bone boneScript = bone.GetComponent<Bone>();
            boneScript.setPlayer(transform);
            bone.GetComponent<Rigidbody>().velocity = playerTransform.forward * boneSpeed;
        }
    }
}
