[System.Serializable]
public class QuestState
{
    public string id;
    public bool completed;
    public string descriptionOne;
    public string schemaReward;

    public QuestState(string id, bool completed, string descriptionOne, string schemaReward)
    {
        this.id = id;
        this.completed = completed;
        this.descriptionOne = descriptionOne;
        this.schemaReward = schemaReward;
    }

    public QuestState Clone()
    {
        return new QuestState(id, completed, descriptionOne, schemaReward);
    }
}
