public static class LootManagerState
{
    public static void AddLoot(int amount)
    {
        SaveData data = SaveSystem.GetData();
        data.loot += amount;
        SaveSystem.Save(data);
    }

    public static void SubLoots(int amount)
    {
        SaveData data = SaveSystem.GetData();
        data.loot -= amount;
        SaveSystem.Save(data);
    }

    public static int GetLoot()
    {
        SaveData data = SaveSystem.GetData();
        return data.loot;
    }
}
