using System.Collections.Generic;

public static class ItemManagerState
{
    public static void AddItem(Item itemInput)
    {
        SaveData data = SaveSystem.GetData();

        foreach (ItemState item in data.items)
        {
            if (item.id == itemInput.id)
            {
                return; // L'item existe deja
            }
        }

        // Si l'item n'existe pas encore → on le crée
        data.items.Add(new ItemState(itemInput.id, itemInput.price, itemInput.title, itemInput.description, itemInput.spriteName, itemInput.category));

        SaveSystem.Save(data);
    }

    public static List<ItemState> getItems()
    {
        SaveData data = SaveSystem.GetData();

        return data.items;
    }

    public static bool existItem(Item itemInput)
    {
        SaveData data = SaveSystem.GetData();
        foreach (ItemState item in data.items)
        {
            if (item.id == itemInput.id)
            {
                return true;
            }
        }

        return false;
    }

    public static ItemState getLastWeapon()
    {
        SaveData data = SaveSystem.GetData();
        ItemState lastWeapon = null;
        foreach (ItemState item in data.items)
        {
            if (item.category == "weapon")
            {
                lastWeapon = item;
            }
        }
        return lastWeapon;
    }

    public static ItemState getLastArmor()
    {
        SaveData data = SaveSystem.GetData();
        ItemState lastArmor = null;
        foreach (ItemState item in data.items)
        {
            if (item.category == "armor")
            {
                lastArmor = item;
            }
        }
        return lastArmor;
    }
}
