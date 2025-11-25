using System.Collections.Generic;

public static class SchemaManager
{
    public static void addSchema(string id)
    {
        SaveData data = SaveSystem.GetData();
        
        foreach (var s in data.schemas)
        {
            if (s.id == id)
            {
                return; // Le schema existe deja
            }
        }

        data.schemas.Add(new SchemaState(id)); // Ajout du schema dans la sauvegarde physique
        SaveSystem.Save(data);
    }

    public static bool SchemaExists(string id)
    {
        SaveData data = SaveSystem.GetData();
        
        foreach (var s in data.schemas)
        {
            if (s.id == id)
                return true;
        }
        return false;
    }

    public static List<SchemaState> getAllSchemas()
    {
        SaveData data = SaveSystem.GetData();
        return data.schemas;
    }

}
