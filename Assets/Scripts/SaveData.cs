using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<QuestState> quests = new List<QuestState>();
    public List<LootState> loots = new List<LootState>();
    public List<SchemaState> schemas = new List<SchemaState>();
    public int prestige;
    public List<PowerUpState> powerUps = new List<PowerUpState>();

    public List<WeaponUnlockState> weaponUnlocks = new List<WeaponUnlockState>();

    public List<EpisodeState> episodes = new List<EpisodeState>();

    public List<ItemState> items = new List<ItemState>();

    public EquipmentState equipment = new EquipmentState();

    public bool isTutoHubDone;

    public bool isTutoDone;

    public bool mapOneDone;
    public bool mapTwoDone;
    public bool mapThreeDone;
    public bool mapFourDone;
    public bool mapFiveDone;
    public bool mapSixDone;
    public bool mapSevenDone;
    public bool mapEightDone;
    public bool mapNineDone;
}
