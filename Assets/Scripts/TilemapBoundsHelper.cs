using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapBoundsHelper : MonoBehaviour
{
    public Tilemap tilemap;

    void Start()
    {
        BoundsInt bounds = tilemap.cellBounds;
        int width = bounds.size.x;
        int height = bounds.size.y;
        //Debug.Log($"Grid World Size: Width={width}, Height={height}");
    }
}
