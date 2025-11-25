using System.Collections.Generic;

public static class EpisodeManagerState
{
    static List<EpisodeState> episodesStates = new List<EpisodeState>
    {
        new EpisodeState("episode_1", "Episode 1", false),
    };


    public static void AddEpisode(EpisodeState episode)
    {
        SaveData data = SaveSystem.GetData();

        foreach (var l in data.episodes)
        {
            if (l.id == episode.id)
            {
                return; // L'episode est deja unlock
            }
        }

        // Si l'episode n'est pas encore, on l'ajoute
        episode.unlock = true;
        data.episodes.Add(episode.Clone());

        SaveSystem.Save(data);
    }

    public static List<EpisodeState> getEpisodesUnlock()
    {
        SaveData data = SaveSystem.GetData();
        return data.episodes;
    }

    public static List<EpisodeState> getListAllEpisodes()
    {
        return episodesStates;
    }

}
