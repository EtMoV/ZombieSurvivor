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
    public GameObject openContainerMapThree_1;
    public GameObject openContainerMapThree_2;
    public GameObject openContainerMapThree_3;
    public GameObject openContainerMapThree_4;
    public GameObject openContainerMapThree_5;
    public GameObject openContainerMapThree_6;
    public GameObject openContainerMapFour_1;
    public GameObject openContainerMapFour_2;
    public GameObject openContainerMapFour_3;
    public GameObject openContainerMapFour_4;
    public GameObject openContainerMapFour_5;
    public GameObject openContainerMapFour_6;
    public GameObject openContainerMapFour_7;
    public GameObject openContainerMapFive_1;
    public GameObject openContainerMapFive_2;
    public GameObject openContainerMapFive_3;
    public GameObject openContainerMapFive_4;
    public GameObject openContainerMapFive_5;
    public GameObject openContainerMapFive_6;
    public GameObject EnterArenaPrefabMapFive_1;
    public GameObject DoorArenaPrefabMapFive_1;
    public GameObject EnterArenaPrefabMapFive_2;
    public GameObject DoorArenaPrefabMapFive_2;
    public GameObject EnterArenaPrefabMapFive_3;
    public GameObject DoorArenaPrefabMapFive_3;
    public GameObject EnterArenaPrefabMapFive_4;
    public GameObject DoorArenaPrefabMapFive_4;

    public GameObject openContainerMapSeven_1;
    public GameObject openContainerMapSeven_2;
    public GameObject openContainerMapSeven_3;
    public GameObject openContainerMapSeven_4;
    public GameObject openContainerMapSeven_5;
    public GameObject openContainerMapSeven_6;

    public GameObject openContainerMapEight_1;
    public GameObject openContainerMapEight_2;
    public GameObject openContainerMapEight_3;
    public GameObject openContainerMapEight_4;
    public GameObject openContainerMapEight_5;
    public GameObject openContainerMapEight_6;

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
            case "mapThree":
                setMapByIndex(3);
                openContainerMapThree_1.SetActive(true);
                openContainerMapThree_2.SetActive(true);
                openContainerMapThree_3.SetActive(true);
                openContainerMapThree_4.SetActive(true);
                openContainerMapThree_5.SetActive(true);
                openContainerMapThree_6.SetActive(true);
                break;
            case "mapFour":
                setMapByIndex(4);
                openContainerMapFour_1.SetActive(true);
                openContainerMapFour_2.SetActive(true);
                openContainerMapFour_3.SetActive(true);
                openContainerMapFour_4.SetActive(true);
                openContainerMapFour_5.SetActive(true);
                openContainerMapFour_6.SetActive(true);
                openContainerMapFour_7.SetActive(true);
                break;
            case "mapFive":
                setMapByIndex(5);
                openContainerMapFive_1.SetActive(true);
                openContainerMapFive_2.SetActive(true);
                openContainerMapFive_3.SetActive(true);
                openContainerMapFive_4.SetActive(true);
                openContainerMapFive_5.SetActive(true);
                openContainerMapFive_6.SetActive(true);
                break;
            case "mapSix":
                setMapByIndex(6);
                EnterArenaPrefabMapFive_1.SetActive(true);
                DoorArenaPrefabMapFive_1.SetActive(true);
                EnterArenaPrefabMapFive_2.SetActive(true);
                DoorArenaPrefabMapFive_2.SetActive(true);
                EnterArenaPrefabMapFive_3.SetActive(true);
                DoorArenaPrefabMapFive_3.SetActive(true);
                EnterArenaPrefabMapFive_4.SetActive(true);
                DoorArenaPrefabMapFive_4.SetActive(true);
                break;
            case "mapSeven":
                setMapByIndex(7);
                openContainerMapSeven_1.SetActive(true);
                openContainerMapSeven_2.SetActive(true);
                openContainerMapSeven_3.SetActive(true);
                openContainerMapSeven_4.SetActive(true);
                openContainerMapSeven_5.SetActive(true);
                openContainerMapSeven_6.SetActive(true);
                break;
            case "mapEight":
                setMapByIndex(8);
                openContainerMapEight_1.SetActive(true);
                openContainerMapEight_2.SetActive(true);
                openContainerMapEight_3.SetActive(true);
                openContainerMapEight_4.SetActive(true);
                openContainerMapEight_5.SetActive(true);
                openContainerMapEight_6.SetActive(true);
                break;
            default:
                // Tuto
                setMapByIndex(2);
                break;
        }
    }
}
