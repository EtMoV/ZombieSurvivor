using System.Collections.Generic;

public static class WeaponUnlockManagerState
{

    static List<WeaponUnlockState> weaponsUnlocksPossible = new List<WeaponUnlockState>
    {
        new WeaponUnlockState("1", 10, "shotgun"),
        new WeaponUnlockState("2", 20, "bat"),
        new WeaponUnlockState("3", 30, "assaultRifle"),
        new WeaponUnlockState("4", 40, "grenade"),
        new WeaponUnlockState("5", 50, "ak"),
        new WeaponUnlockState("6", 60, "sniper"),
        new WeaponUnlockState("7", 70, "rocketLauncher"),
        new WeaponUnlockState("8", 80, "gatling"),
        new WeaponUnlockState("9", 100, "spas"),

    };

    public static void AddWeaponUnlock()
    {
        SaveData data = SaveSystem.GetData();
        WeaponUnlockState wUS = getNextWeapon();

        if (wUS == null)
            return; // Toutes les armes sont debloquees

        data.weaponUnlocks.Add(wUS.Clone());

        SaveSystem.Save(data);
    }

    public static WeaponUnlockState getNextWeapon()
    {
        SaveData data = SaveSystem.GetData();

        foreach (var weapon in weaponsUnlocksPossible)
        {
            bool exists = false;
            foreach (var unlockedWeapon in data.weaponUnlocks)
            {
                if (weapon.id == unlockedWeapon.id)
                {
                    exists = true;
                    break;
                }
            }

            // Si l'arme n'existe pas dans les débloquées, on la retourne
            if (!exists)
            {
                return weapon;
            }
        }

        return null; // Toutes les armes sont débloquées
    }

    public static List<WeaponUnlockState> getAllWeapons()
    {
        SaveData data = SaveSystem.GetData();
        return data.weaponUnlocks;
    }
}
