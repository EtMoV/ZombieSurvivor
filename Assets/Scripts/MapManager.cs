using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public TileMapGroundWall currentMap;

    public List<TileMapGroundWall> tilemaps;

    public GameObject EnterArenaPrefabMapOne;
    public GameObject DoorArenaPrefabMapOne;

    public GameObject EnterArenaPrefabMapOne_1;
    public GameObject DoorArenaPrefabMapOne_1;

    public GameObject EnterArenaPrefabMapOne_2;
    public GameObject DoorArenaPrefabMapOne_2;

    public GameObject EnterArenaPrefabMapOne_3;
    public GameObject DoorArenaPrefabMapOne_3;

    public void setMapByIndex(int indexChoose)
    {
        currentMap = tilemaps[indexChoose];
        
        currentMap.ground.gameObject.SetActive(true);
        currentMap.wall.gameObject.SetActive(true);
    }

    public void Awake()
    {
        switch (StoreDataScene.currentMap)
        {
            case "mapOne":
                setMapByIndex(0);
                EnterArenaPrefabMapOne.SetActive(true);
                DoorArenaPrefabMapOne.SetActive(true);
                EnterArenaPrefabMapOne_1.SetActive(true);
                DoorArenaPrefabMapOne_1.SetActive(true);
                EnterArenaPrefabMapOne_2.SetActive(true);
                DoorArenaPrefabMapOne_2.SetActive(true);
                EnterArenaPrefabMapOne_3.SetActive(true);
                DoorArenaPrefabMapOne_3.SetActive(true);
                break;
            case "mapTwo":
                setMapByIndex(1);
                break;
        }
    }
}
