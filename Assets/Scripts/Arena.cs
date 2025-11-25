using UnityEngine;
using UnityEngine.Tilemaps;

public class Arena : MonoBehaviour
{
    public GameObject exitArenaGo;
    public GameObject exitArenaTextGo;

    public GameObject spawnPointGo;

    public GameObject arenaManagerGo;
    private ArenaManager _arenaManager;

    public GameObject gridManagerGo;
    private GridManager _gridManager;

    public Tilemap arenaGroundTilemap;
    public Tilemap arenaWallTilemap;
    public Tilemap worldGroundTilemap;
    public Tilemap worldWallTilemap;

    public GameObject playerGo;

    public GameObject shopGo;

    public void enterArena()
    {

        _arenaManager = arenaManagerGo.GetComponent<ArenaManager>();
        _gridManager = gridManagerGo.GetComponent<GridManager>();
        _arenaManager.posEnter = new Vector3(playerGo.transform.position.x, playerGo.transform.position.y, playerGo.transform.position.z);
        arenaGroundTilemap.gameObject.SetActive(true);
        arenaWallTilemap.gameObject.SetActive(true);
        worldGroundTilemap.gameObject.SetActive(false);
        worldWallTilemap.gameObject.SetActive(false);
        exitArenaGo.SetActive(false);
        exitArenaTextGo.SetActive(false);
        spawnPointGo.SetActive(true);
        shopGo.SetActive(false);
        _gridManager.groundTilemap = arenaGroundTilemap;
        _gridManager.wallTilemap = arenaWallTilemap;
        spawnPointGo.GetComponent<SpawnPoint>().RelaunchSpawn(true);
        _arenaManager.isActive = true;
        playerGo.transform.position = _arenaManager.posEnterArena; // On remet le joueur à l'entrée de l'arène
    }
}
