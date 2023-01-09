using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Cinemachine;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] GameObject bonePrefab;
    [SerializeField] Transform boneParentTransform;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private Transform shootSounds;
    [SerializeField] private GameObject inventory;
    private Transform playerTransform;
    private float boneSpeed = 100f;
    private bool canShoot;

    private void Start()
    {
        playerTransform = GetComponent<Transform>();
        Inventory.Instance.ammoCount += 5;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot && !inventory.activeSelf)
        {
            if (Inventory.Instance.ammoCount > 0)
            {
                GetComponent<CinemachineImpulseSource>().GenerateImpulse();
                shootSounds.GetChild(Random.Range(0, shootSounds.childCount)).GetComponent<AudioSource>().Play();
                Inventory.Instance.ammoCount--;
                GameObject bone = Instantiate(bonePrefab, shootPosition.position, Quaternion.identity, boneParentTransform);
                Bone boneScript = bone.GetComponent<Bone>();
                boneScript.setPlayer(transform);
                bone.GetComponent<Rigidbody>().velocity = playerTransform.forward * boneSpeed;
            }
        }
    }

    public void EnableShoot()
    {
        canShoot = true;
    }
}
