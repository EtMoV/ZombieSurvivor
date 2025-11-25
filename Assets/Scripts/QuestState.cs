[System.Serializable]
public class QuestState
{
    public string id;
    public bool completed;
    public string descriptionOne;
    public string descriptionTwo;
    public string descriptionThree;
    public string schemaReward;

    public QuestState(string id, bool completed, string descriptionOne, string descriptionTwo, string descriptionThree, string schemaReward)
    {
        this.id = id;
        this.completed = completed;
        this.descriptionOne = descriptionOne;
        this.descriptionTwo = descriptionTwo;
        this.descriptionThree = descriptionThree;
        this.schemaReward = schemaReward;
    }

    public QuestState Clone()
    {
        return new QuestState(id, completed, descriptionOne, descriptionTwo, descriptionThree, schemaReward);
    }
}
