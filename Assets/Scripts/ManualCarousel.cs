using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class NetflixCarousel : MonoBehaviour
{
    [System.Serializable]
    public class EpisodeData
    {
        public string titre;
        public Sprite image;
    }

    public EpisodeData[] episodes;
    public GameObject episodePrefab;
    public RectTransform content;
    public float spacing = 400f; // distance entre centres des épisodes
    public float snapSpeed = 10f;

    private int currentIndex = 0;
    private List<RectTransform> items = new List<RectTransform>();
    private bool isDragging = false;
    private ScrollRect scrollRect;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        GenerateEpisodes();
        CenterOnEpisode(currentIndex, instant:true);
    }

    void Update()
    {
        if (!isDragging)
        {
            // Smoothly move to the current episode
            Vector2 targetPos = new Vector2(items[currentIndex].anchoredPosition.x, content.anchoredPosition.y);
            content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPos, Time.deltaTime * snapSpeed);
        }
    }

    public void OnBeginDrag() => isDragging = true;
    public void OnEndDrag()
    {
        isDragging = false;
        // Determine nearest episode
        float closestDistance = Mathf.Infinity;
        int closestIndex = currentIndex;
        for (int i=0; i<items.Count; i++)
        {
            float dist = Mathf.Abs(items[i].anchoredPosition.x - content.anchoredPosition.x);
            if(dist < closestDistance)
            {
                closestDistance = dist;
                closestIndex = i;
            }
        }
        currentIndex = closestIndex;
    }

    void GenerateEpisodes()
    {
        float xPos = 0;
        foreach (var episode in episodes)
        {
            GameObject obj = Instantiate(episodePrefab, content);
            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.localScale = Vector3.one;

            rt.anchoredPosition = new Vector2(xPos, 0);
            xPos += spacing;

            obj.transform.Find("EpisodeImage").GetComponent<Image>().sprite = episode.image;
            obj.transform.Find("EpisodeTitle").GetComponent<TMP_Text>().text = episode.titre;

            items.Add(rt);
        }

        // Adjust content width
        content.sizeDelta = new Vector2(spacing * episodes.Length, content.sizeDelta.y);
    }

    public void NextEpisode()
    {
        currentIndex = Mathf.Min(currentIndex + 1, items.Count - 1);
    }

    public void PreviousEpisode()
    {
        currentIndex = Mathf.Max(currentIndex - 1, 0);
    }

    void CenterOnEpisode(int index, bool instant=false)
    {
        Vector2 targetPos = new Vector2(items[index].anchoredPosition.x, content.anchoredPosition.y);
        if(instant)
            content.anchoredPosition = targetPos;
    }
}
