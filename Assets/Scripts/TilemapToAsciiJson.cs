using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text;
using System.IO;

public class TilemapToAsciiJson : MonoBehaviour
{
    public Tilemap groundTilemap;
    public Tilemap wallTilemap;
    public string outputFileName;
    private TileCodeJsonManager jsonDB;

    void Awake()
    {
        jsonDB = new TileCodeJsonManager("tile_codes.json");
        Generate();
    }

    public void Generate()
    {
        BoundsInt bounds = groundTilemap.cellBounds;
        StringBuilder sb = new StringBuilder();

        for (int y = bounds.yMax - 1; y >= bounds.yMin; y--)
        {
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);

                TileBase wall = wallTilemap.GetTile(pos);
                TileBase ground = groundTilemap.GetTile(pos);

                if (wall != null)
                    sb.Append(jsonDB.GetOrCreateCode(wall, "W"));
                else if (ground != null)
                    sb.Append(jsonDB.GetOrCreateCode(ground, "G"));
                else
                    sb.Append("..");

                sb.Append(" ");
            }

            sb.AppendLine();
        }

        WriteToFile(sb.ToString());
    }

    private void WriteToFile(string content)
    {
        string path = Path.Combine(Application.persistentDataPath, outputFileName);
        File.WriteAllText(path, content);

        Debug.Log($"ASCII map written to:\n{path}");
    }
}
