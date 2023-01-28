using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject SlimePrefab;
    [SerializeField] private float SpawnRate = 10f;
    [SerializeField] private float NextSpawnTime = 0;
    [SerializeField] private int SpawnLimit = 3;
    [SerializeField] public int Spawned = 0;

    private void Update()
    {
        if (Time.time >= NextSpawnTime && Spawned <= SpawnLimit)
        {
            Vector3 RandomSpawnPosition = new Vector3(Random.Range(-19, 20), Random.Range(-15, 7), 0);
            Instantiate(SlimePrefab, RandomSpawnPosition, Quaternion.identity);
            Spawned++;
            NextSpawnTime = Time.time + SpawnRate;
        }
    }
}
