using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public MapManager mapManager;
    public Transform player;
    public Image fadeImage;
    private float duration = 3f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SaveData data = SaveSystem.GetData();
            data.currentMap += 1;
            SaveSystem.Save(data);
            StartCoroutine(FadeInCoroutine());
            mapManager.setMapByIndex(data.currentMap);
            player.GetComponent<PlayerInventory>().SetMoneyToZero();
            player.localPosition = StoreData.SPAWN_POINT_PLAYER;
        }
    }

    IEnumerator FadeInCoroutine()
    {
        Color color = fadeImage.color;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / duration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
    }
}
