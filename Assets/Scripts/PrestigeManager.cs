public static class PrestigeManager
{
    public static int getPrestige()
    {
        SaveData data = SaveSystem.GetData();
        return data.prestige;
    }
    
    public static void increasePrestige()
    {
        SaveData data = SaveSystem.GetData();
        data.prestige++;
        SaveSystem.Save(data);
    }
}
