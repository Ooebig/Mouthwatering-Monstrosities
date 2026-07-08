using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] objectToSpawn;
    [SerializeField] int amountToSpawn;
    [SerializeField] float spawnRate;
    [SerializeField] int spawnDist;
    [SerializeField] ParticleSystem spawnEffect;

    public int spawnCount;
    float spawnTimer;

    bool startSpawning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startSpawning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCount == 5)
        {
            startSpawning = false;
        }
        else if (spawnCount <= 5)
        {
            startSpawning = true;
        }

        if (startSpawning)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer > spawnRate)
            {
                spawn();
            }
        }
    }

    void spawn()
    {
        spawnTimer = 0;
        spawnCount++;

        Vector3 randPos = Random.insideUnitSphere * spawnDist;
        randPos += transform.position;

        NavMeshHit hit;

        NavMesh.SamplePosition(randPos, out hit, spawnDist, 1);

        int arrPos = Random.Range(0, objectToSpawn.Length);

        Instantiate(objectToSpawn[arrPos], hit.position, Quaternion.Euler(0, Random.Range(0, 360), 0));

        if (spawnEffect != null)
            Instantiate(spawnEffect, hit.position, Quaternion.identity);
    }

    public void DecrementCount()
    {
        --spawnCount;

        return;
    }
}
