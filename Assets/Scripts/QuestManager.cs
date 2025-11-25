using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public static class QuestManager
{
    static List<QuestState> questsInit = new List<QuestState>
    {
        new QuestState("1", false, "Kill 100 zombies", "Get 5 loot", "Unlock Parking Zone", "shotgun"),
        new QuestState("2", false, "Kill 200 zombies", "Get 5 loot", "Unlock Forest Zone", "bat"),
        new QuestState("3", false, "Kill 300 zombies", "Get 5 loot", "Unlock Public dump Zone", "assaultRifle"),
        new QuestState("4", false, "Kill 400 zombies", "Get 5 loot", "Unlock Parking Zone", "grenade"),
        new QuestState("5", false, "Kill 500 zombies", "Get 5 loot", "Unlock City Zone", "ak"),
        new QuestState("6", false, "Kill 600 zombies", "Get 5 loot", "Unlock Forest Zone", "sniper"),
        new QuestState("7", false, "Kill 700 zombies", "Get 5 loot", "Unlock Public dump Zone", "rocketLauncher"),
        new QuestState("8", false, "Kill 800 zombies", "Get 10 loot", "Unlock City Zone", "gatling"),
        new QuestState("9", false, "Kill 1000 zombies", "Get 100 loot", "Unlock City Zone", "spas"),
        new QuestState("10", false, "No more quest", "Stay tunned :)", "", ""),
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

}
