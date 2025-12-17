using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System.Collections.Generic;
using UnityEditor;

public class TileCodeJsonManager
{
    private string filePath;
    private TileCodeJson data;

    private Dictionary<string, string> lookup = new Dictionary<string, string>();

    public TileCodeJsonManager(string fileName)
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        Load();
    }

    void Load()
    {
        if (File.Exists(filePath))
        {
            data = JsonUtility.FromJson<TileCodeJson>(File.ReadAllText(filePath));
        }
        else
        {
            data = new TileCodeJson();
        }

        lookup.Clear();
        foreach (var entry in data.tiles)
        {
            lookup[entry.guid] = entry.code;
        }
    }

    void Save()
    {
        File.WriteAllText(filePath, JsonUtility.ToJson(data, true));
    }

    string GetTileGuid(TileBase tile)
    {
        string path = AssetDatabase.GetAssetPath(tile);
        return AssetDatabase.AssetPathToGUID(path);
    }

    public string GetOrCreateCode(TileBase tile, string prefix)
    {
        if (tile == null)
            return "..";

        string guid = GetTileGuid(tile);

        if (lookup.TryGetValue(guid, out var code))
            return code;

        string newCode = prefix == "W"
            ? $"W{data.nextWall++}"
            : $"G{data.nextGround++}";

        data.tiles.Add(new TileCodeEntry
        {
            guid = guid,
            code = newCode
        });

        lookup[guid] = newCode;
        Save();

        return newCode;
    }
}
