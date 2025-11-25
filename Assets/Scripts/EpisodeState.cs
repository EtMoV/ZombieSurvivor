[System.Serializable]
public class EpisodeState
{
    public string id;
    public string title;
    public bool unlock;

    public EpisodeState(string id, string title, bool unlock)
    {
        this.id = id;
        this.title = title;
        this.unlock = unlock;
    }

    public EpisodeState Clone()
    {
        return new EpisodeState(id, title, unlock);
    }
}
