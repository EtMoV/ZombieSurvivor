using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EpisodeManager : MonoBehaviour
{

    public GameObject imageEpisode;
    public GameObject titleEpisode;

    private List<EpisodeState> episodes;

    private int currentIndex = 0;

    public void Start()
    {
        // Fusionner les deux listes en donnant priorité à episodesUnlock
        episodes = MergeEpisodes(EpisodeManagerState.getListAllEpisodes(), EpisodeManagerState.getEpisodesUnlock());
        DefineDefaultEpisodeUi();
    }

    private List<EpisodeState> MergeEpisodes(List<EpisodeState> allEpisodes, List<EpisodeState> unlockedEpisodes)
    {
        List<EpisodeState> merged = new List<EpisodeState>();

        // Ajouter d'abord les épisodes débloqués
        foreach (var unlockedEpisode in unlockedEpisodes)
        {
            merged.Add(unlockedEpisode);
        }

        // Ajouter les épisodes non débloqués (qui ne sont pas dans unlockedEpisodes)
        foreach (var episode in allEpisodes)
        {
            bool exists = false;
            foreach (var unlockedEpisode in unlockedEpisodes)
            {
                if (episode.id == unlockedEpisode.id)
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                merged.Add(episode);
            }
        }

        return merged;
    }

    public void OnRightEpisode()
    {
        if (currentIndex + 1 < episodes.Count)
        {
            currentIndex++;
            DefineEpisodeUi(episodes[currentIndex]);
        }
    }

    public void OnLeftEpisode()
    {
        if (currentIndex - 1 >= 0)
        {
            currentIndex--;
            DefineEpisodeUi(episodes[currentIndex]);
        }
    }

    public void DefineDefaultEpisodeUi()
    {
        DefineEpisodeUi(episodes[currentIndex]);
    }

    private void DefineEpisodeUi(EpisodeState episode)
    {
        imageEpisode.GetComponent<Image>().sprite = Resources.Load<Sprite>("Episodes/" + episode.id);
        titleEpisode.GetComponent<TextMeshProUGUI>().text = episode.title;
    }
}
