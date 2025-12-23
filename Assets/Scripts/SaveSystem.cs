using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/saveV2.json";
    private static SaveData _data;

    public static SaveData GetData()
    {
        if (_data == null)
        {
            _data = Load();
        }
        return _data;
    }

    public static void CreateFile()
    {
        SaveData data = new SaveData();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("Save file created at: " + path);
    }

    public static void Save(SaveData data)
    {
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        _data = data; // Mise à jour de l'instance partagée
    }

    public static SaveData Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            _data = JsonUtility.FromJson<SaveData>(json);
            return _data;
        }

        // Si le fichier n'existe pas, on le crée
        CreateFile();
        _data = new SaveData();
        return _data;
    }

    
}
