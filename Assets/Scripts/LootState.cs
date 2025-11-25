[System.Serializable]
public class LootState
{
    public string id;
    public string title;
    public string description;
    public int quantity;

    public LootState(string id, string title, string description, int quantity)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.quantity = quantity;
    }
}
