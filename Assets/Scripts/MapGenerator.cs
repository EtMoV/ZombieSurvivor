using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic; // Pour List<T>

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemapWalls;
    public Tilemap tilemapGround;

    public Tile tileWall;
    public Tile tileWallUp;
    public Tile tileWallDown;
    public Tile tileWallLeft;
    public Tile tileWallRight;
    public Tile tileObject;

    [Header("Ground Tiles")]
    public List<Tile> tileGroundList;  // <- Liste de tuiles sol
    private Tile selectedGroundTile;   // <- Tuile sol choisie aléatoirement

    public string jsonFilePath = "Assets/Maps/map.json";

    void Start()
    {
        // Choisir un sol aléatoire une seule fois
        if (tileGroundList == null || tileGroundList.Count == 0)
        {
            Debug.LogError("Aucune tile dans tileGroundList !");
            return;
        }

        selectedGroundTile = tileGroundList[Random.Range(0, tileGroundList.Count)];
        Debug.Log("Tile sol sélectionnée : " + selectedGroundTile.name);

        GenerateMap();
    }

    void GenerateMap()
    {
        // Lire le JSON
        string jsonText = File.ReadAllText(jsonFilePath);
        JObject mapData = JObject.Parse(jsonText);
        JArray mapArray = (JArray)mapData["map"];

        int height = mapArray.Count;
        int width = mapArray[0].ToString().Length;

        // Génération
        for (int y = 0; y < height; y++)
        {
            string line = mapArray[y].ToString();

            for (int x = 0; x < width; x++)
            {
                char c = line[x];
                Vector3Int pos = new Vector3Int(x, -y, 0);

                switch (c)
                {
                    case 'M':
                        tilemapWalls.SetTile(pos, tileWall);
                        break;

                    case 'G':
                        tilemapGround.SetTile(pos, selectedGroundTile);  // <- utilisation de la tuile choisie
                        break;

                    case 'L':
                        tilemapWalls.SetTile(pos, tileWallLeft);
                        break;

                    case 'R':
                        tilemapWalls.SetTile(pos, tileWallRight);
                        break;

                    case 'U':
                        tilemapWalls.SetTile(pos, tileWallUp);
                        break;

                    case 'D':
                        tilemapWalls.SetTile(pos, tileWallDown);
                        break;

                    case 'O':
                        tilemapWalls.SetTile(pos, tileObject);
                        break;
                }
            }
        }
    }
}
