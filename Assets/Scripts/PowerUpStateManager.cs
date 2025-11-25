using System.Collections.Generic;

public static class PowerUpStateManager
{
    public static void AddPowerUp(PowerUpState powerUpState)
    {
        SaveData data = SaveSystem.GetData();

        data.powerUps.Add(powerUpState.Clone());

        SaveSystem.Save(data);
    }

    public static List<PowerUpState> getPowerUps()
    {
        SaveData data = SaveSystem.GetData();

        return data.powerUps;
    }

    public static void clear()
    {
        SaveData data = SaveSystem.GetData();

        data.powerUps.Clear();

        SaveSystem.Save(data);
    }
}
