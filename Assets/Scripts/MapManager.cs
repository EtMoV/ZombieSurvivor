using System.Collections.Generic;
using Firebase.Analytics;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public TileMapGroundWall currentMap;

    public List<TileMapGroundWall> tilemaps;

    public GameObject lvlObj_1;
    public GameObject lvlObj_2;

    public void Awake()
    {
        SetMap();
    }
    
    public void SetMap()
    {
        SaveData data = SaveSystem.GetData();

        switch (data.currentMap)
        {
            case 0:
                FirebaseAnalytics.LogEvent("map_" + 0);
                setMapByIndex(0);
                lvlObj_1.SetActive(true);
                break;
            case 1:
                FirebaseAnalytics.LogEvent("map_" + 1);
                setMapByIndex(1);
                lvlObj_1.SetActive(false);
                lvlObj_2.SetActive(true);
                break;
        }
    }

    private void setMapByIndex(int indexChoose)
    {
        if (currentMap != null && currentMap.ground != null && currentMap.wall != null)
        {
            currentMap.ground.gameObject.SetActive(false);
            currentMap.wall.gameObject.SetActive(false);
        }

        currentMap = tilemaps[indexChoose];

        currentMap.ground.gameObject.SetActive(true);
        currentMap.wall.gameObject.SetActive(true);
    }
}
