using System.Collections;
using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    [SerializeField] private GameObject webVisual;
    [SerializeField] private int escapeAmount = 30;
    [SerializeField] private int maxUses = 3;
    [SerializeField] private float respawnTime = 10f;
    [SerializeField] private float speedReduction = 2.6f;

    private int uses = 0;
    private bool active = true;

    private playerController trappedPlayer;
    private int escapeProgress;

    private void OnTriggerEnter(Collider other)
    {
        if (!active || !other.CompareTag("Player")) return;
        if (uses >= maxUses) return;
        playerController player = other.GetComponent<playerController>();
        if (player == null) return;
        if (player.activeWebs >= player.maxWebs) return;
        trappedPlayer = player;
        player.activeWebs++;
        uses++;
        escapeProgress = 0;
        active = false;
        webVisual.SetActive(false);
        trappedPlayer.speed -= speedReduction;
        StartCoroutine(RespawnWeb());
    }

    private void Update()
    {
        if (trappedPlayer == null) return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            escapeProgress++;
            if (escapeProgress >= escapeAmount) { BreakFree(); }
        }
    }

    private void BreakFree()
    {
        trappedPlayer.speed += speedReduction;
        trappedPlayer.activeWebs--;
        trappedPlayer = null;
        escapeProgress = 0;
    }

    private IEnumerator RespawnWeb()
    {
        yield return new WaitForSeconds(respawnTime);
        if (uses < maxUses)
        {
            webVisual.SetActive(true);
            active = true;
        }
    }
}