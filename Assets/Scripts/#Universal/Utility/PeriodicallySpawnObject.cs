using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicallySpawnObject : MonoBehaviour
{
    public GameObject objectToSpawn;
    public bool toSpawn;

    [Space]
    public float spawnDelay;
    public float randomDelayDegree;

    [Space]
    public float randomSpawnLocationDegree;

    [Space]
    public bool parentToThis;
    public bool parentToParent;

    [Space]
    public int objectsUntilDelete = 0;

    float originalSpawnDelay;
    float spawnTimer = 0;

    int objectsSpawned = 0;

    void Start()
    {
        originalSpawnDelay = spawnDelay;
    }

    void Update()
    {
        if (toSpawn)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnDelay)
            {
                spawnTimer -= spawnDelay;
                SpawnObject();
            }
        }
    }

    void SpawnObject()
    {
        spawnDelay = originalSpawnDelay + Random.Range(-randomDelayDegree, randomDelayDegree);

        Transform spawned = Instantiate(objectToSpawn, (Vector2)(transform.position + Random.insideUnitSphere * randomSpawnLocationDegree), transform.rotation).transform;

        if (parentToThis) spawned.SetParent(transform);
        else if (parentToParent) spawned.SetParent(transform.parent);

        if (objectsUntilDelete > 0)
        {
            objectsSpawned++;
            if (objectsSpawned >= objectsUntilDelete) Destroy(gameObject);
        }
    }
}
