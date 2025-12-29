using System.IO;
using UnityEngine;

public static class SaveSystemV2
{
    private static string path = Application.persistentDataPath + "/saveV2.json";
    private static SaveDataV2 _data;

    public static SaveDataV2 GetData()
    {
        if (_data == null)
        {
            _data = Load();
        }
        return _data;
    }

    public static void CreateFile()
    {
        SaveDataV2 data = new SaveDataV2();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("Save file created at: " + path);
    }

    public static void Save(SaveDataV2 data)
    {
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        _data = data; // Mise à jour de l'instance partagée
    }

    public static SaveDataV2 Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            _data = JsonUtility.FromJson<SaveDataV2>(json);
            return _data;
        }

        // Si le fichier n'existe pas, on le crée
        CreateFile();
        _data = new SaveDataV2();
        return _data;
    }

    
}
