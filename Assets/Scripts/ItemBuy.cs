using UnityEngine;

public class ItemBuy : MonoBehaviour
{
    public int id;
    public int cost;
    public string title;
    public string description;
    public string spriteName;
    public string type;

    public ItemBuy(int id, int cost, string title, string description, string spriteName, string type)
    {
        this.id = id;
        this.cost = cost;
        this.title = title;
        this.description = description;
        this.spriteName = spriteName;
        this.type = type;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
