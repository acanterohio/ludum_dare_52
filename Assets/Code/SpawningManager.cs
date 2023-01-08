using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{

    int numberSpawned = 0;
    int maxSpawned = 20;
    float spawnInterval = 15f;

    [SerializeField] private GameObject donorPrefab;

    List<Vector3> spawnLocations = new List<Vector3>();
    Vector3 spawnCheck = new Vector3(100, 100, 100);
    LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform g in transform.GetChild(0).GetComponentsInChildren<Transform>())
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
            print(numberSpawned);
            while (numberSpawned < maxSpawned)
            {
                int random = Random.Range(0, spawnLocations.Count);
                Vector3 location = spawnLocations[random];
                if (!Physics.CheckBox(location, spawnCheck, Quaternion.identity, mask))
                {
                    GameObject donor = Instantiate(donorPrefab, location, Quaternion.identity, transform.GetChild(1));
                    donor.GetComponent<DonorController>().setDestinations(spawnLocations);
                    numberSpawned++;
                } 
                
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
