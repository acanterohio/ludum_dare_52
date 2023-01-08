using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    float startingVal = -225;
    float endingVal = 225;
    float incrementVal = 150;
    float yVal = 1;

    int numberSpawned = 0;
    int maxSpawnPerCheck = 5;
    int maxSpawned = 20;
    float spawnInterval = 15f;

    [SerializeField] private GameObject donorPrefab;
    [SerializeField] private GameObject spawnPrefab;

    List<Vector3> spawnLocations = new List<Vector3>();
    Vector3 spawnCheck = new Vector3(75, 75, 75);
    LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {

        foreach (Transform g in transform.GetComponentsInChildren<Transform>())
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
            if (numberSpawned < maxSpawned)
            {
                spawnLocations.Sort((a, b) => 1 - 2 * Random.Range(0, 1)); //shuffles list
                for (int i = 0; i < spawnLocations.Count; i++)
                {
                    int random = (int)Random.Range(0f, spawnLocations.Count);
                    Vector3 location = spawnLocations[random];
                    if (Physics.CheckBox(location, spawnCheck, Quaternion.identity, mask))
                    {
                        print("Player!!!");
                        continue;
                    }
                    Instantiate(donorPrefab, location, Quaternion.identity);
                    print("SPAWNING");
                    break;
                }
            }


            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
