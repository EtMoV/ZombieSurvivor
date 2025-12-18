using UnityEngine;

[System.Serializable]
public class Waifu : MonoBehaviour
{
    public int id;
    public string title;
    public string description;
    public string spriteName;

    public Waifu(int id,  string title, string description, string spriteName)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.spriteName = spriteName;
    }
}
