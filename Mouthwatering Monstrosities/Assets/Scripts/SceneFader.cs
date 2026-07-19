using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image image;
    public string sceneToLoad;
    public float fadeDuration = 1f;

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FadeAndLoad(sceneToLoad, fadeDuration);
        }
    }

    public void FadeAndLoad(string sceneName, float duration)
    {
        StartCoroutine(Fader(sceneName, duration));
    }

    IEnumerator Fader(string sceneName, float duration)
    {
        float t = 0f;
        Color c = image.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Clamp01(t / duration);
            image.color = c;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeOut()
    {
        float t = 0f;
        Color c = image.color;

        while (t < 1f)
        {
            t += Time.deltaTime;
            c.a = 1f - Mathf.Clamp01(t);
            image.color = c;
            yield return null;
        }
    }
}
