using UnityEngine;
using System.Collections;

public class UIBump : MonoBehaviour
{
    public float bumpScale = 1.2f;    // taille max du pop
    public float bumpDuration = 0.2f; // durée de l’animation

    private Vector3 originalScale;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void DoBump()
    {
        StopAllCoroutines();
        StartCoroutine(BumpCoroutine());
    }

    private IEnumerator BumpCoroutine()
    {
        float elapsed = 0f;

        // Aller à bumpScale
        while (elapsed < bumpDuration / 2f)
        {
            float t = elapsed / (bumpDuration / 2f);
            transform.localScale = Vector3.Lerp(originalScale, originalScale * bumpScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Retour à la taille normale
        elapsed = 0f;
        while (elapsed < bumpDuration / 2f)
        {
            float t = elapsed / (bumpDuration / 2f);
            transform.localScale = Vector3.Lerp(originalScale * bumpScale, originalScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }
}
