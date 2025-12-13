using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text;

public class TilemapToAscii : MonoBehaviour
{
    public Tilemap backgroundTilemap;
    public Tilemap wallTilemap;

    void Start()
    {
        int[,] matrix = GenerateMatrix();
        Debug.Log(MatrixToString(matrix));
    }

    int[,] GenerateMatrix()
    {
        BoundsInt bgBounds = backgroundTilemap.cellBounds;
        BoundsInt wallBounds = wallTilemap.cellBounds;

        int minX = Mathf.Min(bgBounds.xMin, wallBounds.xMin);
        int minY = Mathf.Min(bgBounds.yMin, wallBounds.yMin);
        int maxX = Mathf.Max(bgBounds.xMax, wallBounds.xMax);
        int maxY = Mathf.Max(bgBounds.yMax, wallBounds.yMax);

        int width = maxX - minX;
        int height = maxY - minY;

        int[,] matrix = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int cellPos = new Vector3Int(
                    minX + x,
                    minY + y,
                    0
                );

                if (wallTilemap.HasTile(cellPos))
                    matrix[x, y] = 1;
                else if (backgroundTilemap.HasTile(cellPos))
                    matrix[x, y] = 2;
            }
        }

        return matrix;
    }

    string MatrixToString(int[,] matrix)
    {
        StringBuilder sb = new StringBuilder();
        int width = matrix.GetLength(0);
        int height = matrix.GetLength(1);

        // Affichage top → bottom (plus lisible)
        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                sb.Append(matrix[x, y]);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
