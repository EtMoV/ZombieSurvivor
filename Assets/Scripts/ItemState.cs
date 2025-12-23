using UnityEngine;

[System.Serializable]
public class ItemState
{
    public int id;
    public int price;
    public string title;
    public string description;
    public string spriteName;
    public string category;

    public ItemState(int id, int price, string title, string description, string spriteName, string category)
    {
        this.id = id;
        this.price = price;
        this.title = title;
        this.description = description;
        this.spriteName = spriteName;
        this.category = category;
    }
}
