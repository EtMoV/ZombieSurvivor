[System.Serializable]
public class ItemState
{
    public int id;
    public int cost;
    public string title;
    public string description;
    public string spriteName;
    public string type;

    public ItemState(int id, int cost, string title, string description, string spriteName, string type)
    {
        this.id = id;
        this.cost = cost;
        this.title = title;
        this.description = description;
        this.spriteName = spriteName;
        this.type = type;
    }

    public ItemState Clone()
    {
        return new ItemState(id, cost, title, description, spriteName, type);
    }
}
