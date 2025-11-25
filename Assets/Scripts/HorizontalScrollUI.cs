using UnityEngine;
using UnityEngine.UI;

public class HorizontalScrollUI : MonoBehaviour
{
    public float scrollSpeed = 0.2f; // vitesse du défilement horizontal
    private Material mat;

    void Start()
    {
        // Récupère l'Image UI
        Image img = GetComponent<Image>();
        if (img == null)
        {
            Debug.LogError("HorizontalScrollUI : aucun Image trouvé !");
            return;
        }

        // Clone le Material pour ne pas partager l'offset avec d'autres Images
        mat = Instantiate(img.material);
        img.material = mat;
    }

    void Update()
    {
        if (mat == null) return;

        // Décalage horizontal
        Vector2 offset = mat.mainTextureOffset;
        offset.x += scrollSpeed * Time.deltaTime;
        mat.mainTextureOffset = offset;
    }
}
