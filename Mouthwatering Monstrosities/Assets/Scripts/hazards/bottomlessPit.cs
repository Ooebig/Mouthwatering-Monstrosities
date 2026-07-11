using System.Collections;
using UnityEngine;

public class bottomlessPit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        gamemanager.instance.playerFallingFlash.SetActive(true);
        yield return new WaitForSeconds(1f);
        gamemanager.instance.playerScript.changePlayerPos();
        gamemanager.instance.playerFallingFlash.SetActive(false);
        gamemanager.instance.playerScript.takeDamage(20f);
    }
}
