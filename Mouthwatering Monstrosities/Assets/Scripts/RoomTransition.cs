using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] private Transform destinationPoint;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private bool transitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || transitioning) return;

        transitioning = true;
        StartCoroutine(Transition(other.transform));
    }

    private IEnumerator Transition(Transform player)
    {
        playerController pc = player.GetComponent<playerController>();
        CharacterController cc = player.GetComponent<CharacterController>();

        if (pc != null)
            pc.enabled = false;

        yield return StartCoroutine(FadeScreen(1));

        if (destinationPoint != null)
        {
            Vector3 targetPosition = destinationPoint.position;

            gamemanager.instance.playerSpawnPos.transform.position = targetPosition;

            if (cc != null)
                cc.enabled = false;

            player.position = targetPosition;

            if (pc != null)
                pc.playerVel = Vector3.zero;

            Physics.SyncTransforms();

            if (cc != null)
                cc.enabled = true;
        }

        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(FadeScreen(0));

        if (pc != null)
            pc.enabled = true;

        yield return new WaitForSeconds(0.2f);

        transitioning = false;
    }

    private IEnumerator FadeScreen(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float timer = 0f;

        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            fadeImage.color = color;

            yield return null;
        }

        color.a = targetAlpha;
        fadeImage.color = color;
    }
}