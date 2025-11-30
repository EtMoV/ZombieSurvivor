public class PowerUp
{
    public string type;
    public int lvl;
    public string title;
    public string description;

    public string descriptionEvol;
    public bool isStat;

    public bool isWeapon;

    public string titleHasEvol;
    public string descriptionHasEvol;
    public string typeEvol;

    public PowerUp(string type, int lvl, string title, string description, string descriptionEvol, bool isStat, bool isWeapon, string titleHasEvol, string descriptionHasEvol, string typeEvol)
    {
        this.type = type;
        this.lvl = lvl;
        this.title = title;
        this.description = description;
        this.descriptionEvol = descriptionEvol;
        this.isStat = isStat;
        this.isWeapon = isWeapon;
        this.titleHasEvol = titleHasEvol;
        this.descriptionHasEvol = descriptionHasEvol;
        this.typeEvol = typeEvol;
    }

    public PowerUp Clone()
    {
        PowerUp newInstance = new PowerUp(type, lvl, title, description, descriptionEvol, isStat, isWeapon, titleHasEvol, descriptionHasEvol, typeEvol);
        return newInstance;
    }
}
