using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [Header("Tilemaps")]
    public Tilemap groundTilemap;
    public Tilemap wallTilemap;

    [Header("Node settings")]
    public float nodeRadius = 0.5f;
    public LayerMask obstacleMask;

    [HideInInspector]
    public Node[,] grid;
    public int gridSizeX;
    public int gridSizeY;
    private Vector3 worldBottomLeft;

    public GameObject mapManagerGo;
    private MapManager _mapManager;

    public bool isHub;
    public void GenerateGrid()
    {
        if (!isHub)
        {
            _mapManager = mapManagerGo.GetComponent<MapManager>();
            groundTilemap = _mapManager.currentMap.ground;
            wallTilemap = _mapManager.currentMap.wall;
        }
        
        if (groundTilemap == null)
        {
            Debug.LogError("Ground Tilemap manquante !");
            return;
        }

        BoundsInt bounds = groundTilemap.cellBounds;
        gridSizeX = bounds.size.x;
        gridSizeY = bounds.size.y;
        grid = new Node[gridSizeX, gridSizeY];
        worldBottomLeft = groundTilemap.CellToWorld(bounds.min) + Vector3.one * nodeRadius;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3Int cellPos = new Vector3Int(bounds.xMin + x, bounds.yMin + y, 0);
                Vector3 worldPoint = groundTilemap.CellToWorld(cellPos) + Vector3.one * nodeRadius;

                bool walkable = groundTilemap.HasTile(cellPos);
                if (wallTilemap != null && wallTilemap.HasTile(cellPos)) walkable = false;
                if (Physics2D.OverlapCircle(worldPoint, nodeRadius * 0.5f, obstacleMask)) walkable = false;

                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPos)
    {
        if (grid == null) return null;

        float percentX = Mathf.InverseLerp(worldBottomLeft.x, worldBottomLeft.x + gridSizeX, worldPos.x);
        float percentY = Mathf.InverseLerp(worldBottomLeft.y, worldBottomLeft.y + gridSizeY, worldPos.y);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        // Mouvements orthogonaux
        int[,] dirs = new int[,] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

        for (int i = 0; i < dirs.GetLength(0); i++)
        {
            int checkX = node.gridX + dirs[i, 0];
            int checkY = node.gridY + dirs[i, 1];

            if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
            {
                Node neighbor = grid[checkX, checkY];
                if (neighbor.walkable) neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    public bool IsNearObstacle(Node node, int buffer = 2)
    {
        for (int x = -buffer; x <= buffer; x++)
        {
            for (int y = -buffer; y <= buffer; y++)
            {
                if (x == 0 && y == 0) continue;
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    if (!grid[checkX, checkY].walkable) return true;
                }
            }
        }
        return false;
    }


    public class Node
    {
        public bool walkable;
        public Vector3 worldPosition;
        public int gridX;
        public int gridY;

        public int gCost;
        public int hCost;
        public Node parent;
        public int fCost => gCost + hCost;

        public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
        {
            walkable = _walkable;
            worldPosition = _worldPos;
            gridX = _gridX;
            gridY = _gridY;
        }
    }

    public bool IsNearObstacle(Node node)
    {
        foreach (var n in GetNeighbors(node))
            if (!n.walkable)
                return true;
        return false;
    }
}
