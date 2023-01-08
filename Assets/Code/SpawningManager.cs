using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{

    int numberSpawned = 0;
    int maxSpawned = 20;
    float spawnInterval = 5f;

    [SerializeField] private GameObject donorPrefab;
   // [SerializeField] private GameObject spawnPrefab;

    List<Vector3> spawnLocations = new List<Vector3>();
    Vector3 spawnCheck = new Vector3(25, 25, 25);
    LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        //for (float x = -225; x <= 225; x += 150)
        //{
        //    for (float z = -225; z <= 225; z += 150)
        //    {
        //        Instantiate(spawnPrefab, new Vector3(x, 1, z), Quaternion.identity, transform.GetChild(0));
        //    }
        //}
        foreach (Transform g in transform.GetChild(0).GetComponentsInChildren<Transform>().Skip(1))
        {
            spawnLocations.Add(g.position);
        }
        
        mask = LayerMask.GetMask("Player");
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            numberSpawned = transform.GetChild(1).childCount;
            while (numberSpawned < maxSpawned)
            {
                int random = Random.Range(0, spawnLocations.Count);
                Vector3 location = spawnLocations[random];
                if (!Physics.CheckBox(location, spawnCheck, Quaternion.identity, mask))
                {
                    GameObject donor = Instantiate(donorPrefab, location, Quaternion.identity, transform.GetChild(1));
                    donor.GetComponentInChildren<DonorController>().setDestinations(spawnLocations);
                    numberSpawned++;
                }
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
