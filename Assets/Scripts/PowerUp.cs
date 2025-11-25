public class PowerUp
{
    public string type;
    public int lvl;
    public string title;
    public string description;
    public bool isStat;

    public PowerUp(string type, int lvl, string title, string description, bool isStat)
    {
        this.type = type;
        this.lvl = lvl;
        this.title = title;
        this.description = description;
        this.isStat = isStat;
    }
    
    public PowerUp Clone()
    {
        PowerUp newInstance = new PowerUp(type, lvl, title, description, isStat);
        return newInstance;
    }
}
