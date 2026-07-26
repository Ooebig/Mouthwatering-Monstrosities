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

        StartCoroutine(Transition(other.transform));
    }

    private IEnumerator Transition(Transform player)
    {
        transitioning = true;

        yield return StartCoroutine(FadeScreen(1));

        if (destinationPoint != null)
        {
            Vector3 targetPosition = destinationPoint.position;

            gamemanager.instance.playerSpawnPos.transform.position = targetPosition;

            Rigidbody rb = player.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.position = targetPosition;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            else
            {
                player.position = targetPosition;
            }
        }

        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(FadeScreen(0));

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
