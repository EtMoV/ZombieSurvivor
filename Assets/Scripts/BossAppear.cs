using UnityEngine;
using TMPro;

public class BossAppear : MonoBehaviour
{
    public TextMeshProUGUI bossText;

    void Start()
    {
    }

    void Update()
    {
       
    }

    public void FlashBossText()
    {
        StartCoroutine(TextFlash());
    }

    private System.Collections.IEnumerator TextFlash()
    {
        float duration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            
            // Oscille l'alpha entre 0 et 1
            float alpha = Mathf.PingPong(elapsedTime * 4f, 1f);
            
            Color color = bossText.color;
            color.a = alpha;
            bossText.color = color;
            
            yield return null;
        }

        // Remet l'alpha à 1 à la fin
        Color finalColor = bossText.color;
        finalColor.a = 1f;
        bossText.color = finalColor;
        gameObject.SetActive(false);
    }
}
