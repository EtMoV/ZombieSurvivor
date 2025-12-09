using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeBlack : MonoBehaviour
{
    public Image blackImage; // L'image noire à faire apparaître

    // Fonction publique pour lancer le fondu
    public void ShowBlack(float duration)
    {
        StartCoroutine(FadeIn(duration));
    }

    private IEnumerator FadeIn(float duration)
    {
        blackImage.gameObject.SetActive(true);

        Color color = blackImage.color;
        color.a = 0f;
        blackImage.color = color;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration);
            blackImage.color = color;
            yield return null;
        }

        color.a = 1f;
        blackImage.color = color;

        // On attend un instant (par exemple 1 seconde) puis on refait le fondu sortant
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOut(duration));
    }

    private IEnumerator FadeOut(float duration)
    {
        Color color = blackImage.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(elapsed / duration);
            blackImage.color = color;
            yield return null;
        }

        color.a = 0f;
        blackImage.color = color;
        blackImage.gameObject.SetActive(false);
    }
}
