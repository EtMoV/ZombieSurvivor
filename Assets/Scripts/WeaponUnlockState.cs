[System.Serializable]
public class WeaponUnlockState
{
    public string id;

    public int cost;
    public string type;

    public WeaponUnlockState(string id, int cost, string type)
    {
        this.id = id;
        this.cost = cost;
        this.type = type;
        
    }

    public WeaponUnlockState Clone()
    {
        return new WeaponUnlockState(id, cost, type);
    }
}
