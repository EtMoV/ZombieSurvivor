using System.Collections.Generic;

public static class QuestManager
{
    public static List<QuestState> questsInit = new List<QuestState>
    {
        new QuestState("1", false, "Kill 1000 zombies", ""),
        new QuestState("2", false, "Finish small sherif's town", ""),
        new QuestState("3", false, "Clean all building in small sheriff's town", ""),
        new QuestState("4", false, "Kill 2000 zombies", ""),
        new QuestState("5", false, "Finish the appartment", ""),
        new QuestState("6", false, "Stay tunned :)", ""),
    };

    public static bool IsQuestCompleted(string questId)
    {
        SaveData data = SaveSystem.GetData();
        foreach (var q in data.quests)
            if (q.id == questId)
                return q.completed;

        return false;
    }

    public static void CompleteQuest(string questId)
    {
        SaveData data = SaveSystem.GetData();
        foreach (var q in questsInit)
        {
            // On recherche la quete
            if (q.id == questId)
            {
                bool flagAlreadyExist = false;
                foreach (var qd in data.quests)
                {
                    if (q.id == qd.id)
                    {
                        flagAlreadyExist = true;
                        break;
                    }
                }

                if (!flagAlreadyExist)
                {
                    // Ajout que si n'existe pas deja dans les données physiques
                    q.completed = true;
                    data.quests.Add(q.Clone()); // Ajout de la quetes dans la sauvegarde physique
                    SaveSystem.Save(data);
                    return;
                }
            }
        }

        SaveSystem.Save(data);
    }
    public static QuestState getQuestById(string questId)
    {
        SaveData data = SaveSystem.GetData();

        foreach (var q in data.quests)
        {
            if (q.id == questId)
            {
                return q;
            }

        }

        return null;
    }

    public static QuestState getCurrentQuest()
    {
        SaveData data = SaveSystem.GetData();
        int id = 1; // 1er id

        foreach (var q in data.quests)
        {
            if (!q.completed)
            {
                break;
            }
            id++; // on passe a l'id suivant pour la prochaine quetes
        }

        return questsInit[id - 1];
    }

    public static QuestState getLastCompletedQuest()
    {
        SaveData data = SaveSystem.GetData();
        QuestState lastCompleted = null;
        foreach (var q in data.quests)
        {
            if (q.completed)
            {
                QuestState quest = questsInit[int.Parse(q.id) - 1];
                lastCompleted = quest;
            }

        }
        return lastCompleted;
    }

    public static List<QuestState> GetThreeQuestToDo()
    {

        List<QuestState> threeQuestFree = new List<QuestState>();

        for (int i = 0; i < questsInit.Count; i++)
        {
            bool questCompleted = IsQuestCompleted(questsInit[i].id);
            if (!questCompleted)
            {
                QuestState questState = getQuestById(questsInit[i].id);
                if (questState == null)
                    threeQuestFree.Add(questsInit[i]);
                else
                    threeQuestFree.Add(questState);
            }

            if (threeQuestFree.Count == 3)
            {
                break;
            }
        }

        return threeQuestFree;
    }
}
