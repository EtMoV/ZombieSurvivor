using System.Collections.Generic;

public static class InventoryManagerState
{
    public static void AddItem(ItemBuy itemBuy)
    {
        SaveData data = SaveSystem.GetData();

        foreach (ItemState item in data.items)
        {
            if (item.id == itemBuy.id)
            {
                return; // L'item existe deja
            }
        }

        // Si l'item n'existe pas encore → on le crée
        data.items.Add(new ItemState(itemBuy.id, itemBuy.cost, itemBuy.title, itemBuy.description, itemBuy.spriteName, itemBuy.type));

        SaveSystem.Save(data);
    }


    public static List<ItemState> getItems()
    {
        SaveData data = SaveSystem.GetData();

        return data.items;
    }

    public static bool ExistItem(int id)
    {
        SaveData data = SaveSystem.GetData();

        foreach (ItemState item in data.items)
        {
            if (item.id == id)
            {
                return true; // L'item existe deja
            }
        }

        return false; // L'item n'existe pas
    }

    public static void AddWaifu(Waifu waifu)
    {
        SaveData data = SaveSystem.GetData();

        foreach (WaifuState w in data.waifus)
        {
            if (w.id == waifu.id)
            {
                return; // L'item existe deja
            }
        }
        
        // Si l'item n'existe pas encore → on le crée
        data.waifus.Add(new WaifuState(waifu.id, waifu.title, waifu.description, waifu.spriteName));
        
        SaveSystem.Save(data);
    }

    public static bool ExistWaifu(int id)
    {
        SaveData data = SaveSystem.GetData();

        foreach (WaifuState waifu in data.waifus)
        {
            if (waifu.id == id)
            {
                return true; // L'item existe deja
            }
        }

        return false; // L'item n'existe pas
    }

}
