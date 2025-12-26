using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int currentMap = 0;
    public List<ItemState> items = new List<ItemState>();
    public int loot = 0;
}
