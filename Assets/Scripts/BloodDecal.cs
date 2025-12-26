using UnityEngine;

public class BloodDecal : MonoBehaviour
{
    void Start()
    {
        // Rotation aléatoire
        transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        // Échelle aléatoire
        float scale = Random.Range(0.6f, 1.1f);
        transform.localScale = Vector3.one * scale;

        // Optionnel : destruction lente pour limiter le nombre de decals
        Destroy(gameObject, Random.Range(15f, 30f));
    }
}
