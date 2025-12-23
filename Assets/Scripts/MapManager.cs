using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public TileMapGroundWall currentMap;

    public List<TileMapGroundWall> tilemaps;

    public void setMapByIndex(int indexChoose)
    {
        currentMap = tilemaps[indexChoose];

        currentMap.ground.gameObject.SetActive(true);
        currentMap.wall.gameObject.SetActive(true);
    }

    public void Awake()
    {
        SaveData data = SaveSystem.GetData();

        switch (data.currentMapV2)
        {
            case 0:
                setMapByIndex(0);
                break;
            default:
                setMapByIndex(0);
                break;
        }
    }
}
