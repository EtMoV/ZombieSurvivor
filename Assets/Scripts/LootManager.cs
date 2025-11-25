using System.Collections.Generic;

public static class LootManager
{
    static List<LootBasic> lootBasics = new List<LootBasic>
    {
        new LootBasic("1", "loot1", "desc1"),
    };

    private static LootBasic GetRandomLoot()
    {
        if (lootBasics.Count == 0)
            return null;

        int randomIndex = UnityEngine.Random.Range(0, lootBasics.Count);
        return lootBasics[randomIndex];
    }

    public static void AddLoot()
    {
        SaveData data = SaveSystem.GetData();
        // Generation d'un loot aléatoire
        LootBasic lootBasic = GetRandomLoot();

        foreach (var l in data.loots)
        {
            if (l.id == lootBasic.id)
            {
                l.quantity++;
                SaveSystem.Save(data);
                return; // Le loot existe deja
            }
        }

        // Si le loot n'existe pas encore → on le crée
        data.loots.Add(new LootState(lootBasic.id, lootBasic.title, lootBasic.description, 1));

        SaveSystem.Save(data);
    }

    // Methode deg pour aller vite
    public static void subLootFive()
    {
        SaveData data = SaveSystem.GetData();
        data.loots[0].quantity -= 5;
        if (data.loots[0].quantity < 0)
        {
            // Securite sur le bug du negatif
            data.loots[0].quantity = 0;
        }

        SaveSystem.Save(data);
    }

    public static void subLoot(int quantity)
    {
        SaveData data = SaveSystem.GetData();

        if (data.loots[0].quantity - quantity >= 0)
            data.loots[0].quantity -= quantity;

        SaveSystem.Save(data);
    }

    public static int getLoots()
    {
        SaveData data = SaveSystem.GetData();
        if (data.loots.Count > 0)
        {
            return data.loots[0].quantity;
        }
        return 0;
    }

}
