using UnityEngine;
using System.Collections;

public class checkpoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (spawnPoint != null) { gamemanager.instance.playerSpawnPos.transform.position = spawnPoint.position; }
    }
}
