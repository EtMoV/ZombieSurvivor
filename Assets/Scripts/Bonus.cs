public class Bonus
{
    public string type;

    public Bonus(string type)
    {
        this.type = type;
    }

    public Bonus Clone()
    {
        Bonus newInstance = new Bonus(type);
        return newInstance;
    }
}
