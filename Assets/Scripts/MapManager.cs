using System.Collections.Generic;
using Firebase.Analytics;
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

        switch (data.currentMap)
        {
            case 0:
                FirebaseAnalytics.LogEvent("map_" + 0);
                setMapByIndex(0);
                break;
            case 1:
                FirebaseAnalytics.LogEvent("map_" + 1);
                setMapByIndex(1);
                break;
            default:
                setMapByIndex(0);
                break;
        }
    }
}
