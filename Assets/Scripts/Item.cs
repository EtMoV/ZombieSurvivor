using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public int id;
    public int price;
    public string nameItem;
    public string title;
    public string description;
    public string spriteName;
    public string category;

    public Item(int id, int price, string nameItem, string title, string description, string spriteName, string category)
    {
        this.id = id;
        this.price = price;
        this.nameItem = nameItem;
        this.title = title;
        this.description = description;
        this.spriteName = spriteName;
        this.category = category;
    }
}
