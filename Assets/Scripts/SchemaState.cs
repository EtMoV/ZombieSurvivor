[System.Serializable]
public class SchemaState
{
    public string id;

    public SchemaState(string id)
    {
        this.id = id;
    }

    public SchemaState Clone()
    {
        return new SchemaState(id);
    }
}
