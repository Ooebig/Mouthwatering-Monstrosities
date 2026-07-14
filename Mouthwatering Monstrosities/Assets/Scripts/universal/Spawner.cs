using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] objectToSpawn;
    [SerializeField] int amountToSpawn;
    [SerializeField] float spawnRate;
    [SerializeField] int spawnDist;

    float spawnTimer;
    List<GameObject> spawnList;
    bool startSpawning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startSpawning = true;
        spawnList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
       if (spawnList.Count >= 5)
        {
            startSpawning = false;
        }
       

        spawnTimer += Time.deltaTime;

        if (startSpawning)
        {
            if (spawnTimer > spawnRate)
            {
                spawn();
            }
        }
    }

    void spawn()
    {
        spawnTimer = 0;

        Vector3 randPos = Random.insideUnitSphere * spawnDist;
        randPos += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randPos, out hit, spawnDist, 1);
        int arrPos = Random.Range(0, objectToSpawn.Length);

       GameObject enemy = Instantiate(objectToSpawn[arrPos], hit.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
        spawnList.Add(enemy);
    }

    public void EnemyDied(GameObject enemy)
    {
        spawnTimer = 0;
        startSpawning = true;
        spawnList.Remove(enemy);
        GetComponent<LootBag>().InstantiateDrops(transform.position);
        Destroy(gameObject);
    }

}
