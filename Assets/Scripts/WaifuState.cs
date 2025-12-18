[System.Serializable]
public class WaifuState
{
    public int id;
    public string title;
    public string description;
    public string spriteName;

    public WaifuState(int id,  string title, string description, string spriteName)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.spriteName = spriteName;
    }

    public WaifuState Clone()
    {
        return new WaifuState(id, title, description, spriteName);
    }
}
