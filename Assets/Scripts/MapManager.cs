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

    public GameObject containerMapTwo;
    public GameObject openContainerMapTwo;
    public GameObject containerMapTwo_1;
    public GameObject openContainerMapTwo_1;
    public GameObject containerMapTwo_2;
    public GameObject openContainerMapTwo_2;
    public GameObject containerMapTwo_3;
    public GameObject openContainerMapTwo_3;
    public GameObject containerMapTwo_4;
    public GameObject openContainerMapTwo_4;
    public GameObject containerMapTwo_5;
    public GameObject openContainerMapTwo_5;
    public GameObject containerMapTwo_6;
    public GameObject openContainerMapTwo_6;

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
                containerMapTwo.SetActive(true);
                openContainerMapTwo.SetActive(true);
                containerMapTwo_1.SetActive(true);
                openContainerMapTwo_1.SetActive(true);
                containerMapTwo_2.SetActive(true);
                openContainerMapTwo_2.SetActive(true);
                containerMapTwo_3.SetActive(true);
                openContainerMapTwo_3.SetActive(true);
                containerMapTwo_4.SetActive(true);
                openContainerMapTwo_4.SetActive(true);
                containerMapTwo_5.SetActive(true);
                openContainerMapTwo_5.SetActive(true);
                containerMapTwo_6.SetActive(true);
                openContainerMapTwo_6.SetActive(true);
                break;
        }
    }
}
