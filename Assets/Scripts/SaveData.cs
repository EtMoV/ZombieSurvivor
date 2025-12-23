using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int currentMapV2 = 0;
    public List<ItemState> items = new List<ItemState>();
    public int loot = 0;
}
