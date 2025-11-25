[System.Serializable]
public class PowerUpState
{
    public string id;
    public string title;
    public string description;

    public PowerUpState(string id, string title, string description)
    {
        this.id = id;
        this.title = title;
        this.description = description;
    }

    public PowerUpState Clone()
    {
        return new PowerUpState(id, title, description);
    }
}
