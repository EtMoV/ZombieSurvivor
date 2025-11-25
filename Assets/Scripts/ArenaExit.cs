using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ArenaExit : MonoBehaviour
{

    public GameObject exitArenaTextGo;

    public GameObject spawnPointGo;

    public GameObject gridManagerGo;
    private GridManager _gridManager;

    public Tilemap arenaGroundTilemap;
    public Tilemap arenaWallTilemap;

    public Tilemap worldGroundTilemap;
    public Tilemap worldWallTilemap;

    public GameObject PlayerGo;
    private PlayerZombieSpawn _playerZombieSpawn;

    public GameObject arenaManagerGo;
    private ArenaManager _arenaManager;

    public GameObject shopGo;

    public GameObject fadeBlackGo;

    public void Awake()
    {
        _gridManager = gridManagerGo.GetComponent<GridManager>();
        _playerZombieSpawn = PlayerGo.GetComponent<PlayerZombieSpawn>();
        _arenaManager = arenaManagerGo.GetComponent<ArenaManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fadeBlackGo.GetComponent<FadeBlack>().ShowBlack(0.5f); // Fondu en 1 seconde
            StartCoroutine(CoroutineCollision(collision));
        }
    }

    private IEnumerator CoroutineCollision(Collision2D collision)
    {
        yield return new WaitForSeconds(0.6f);

        arenaGroundTilemap.gameObject.SetActive(false);
        arenaWallTilemap.gameObject.SetActive(false);
        worldGroundTilemap.gameObject.SetActive(true);
        worldWallTilemap.gameObject.SetActive(true);
        exitArenaTextGo.SetActive(false);
        spawnPointGo.SetActive(false);
        shopGo.SetActive(false);
        _playerZombieSpawn.isActive = true;
        _arenaManager.isActive = false;
        _gridManager.groundTilemap = worldGroundTilemap;
        _gridManager.wallTilemap = worldWallTilemap;
        PlayerGo.transform.position = _arenaManager.posEnter; // On remet le joueur à l'entrée de l'arène

        // On reaffiche les element
        _arenaManager.arenaObjects.ForEach
        (ao =>
        {
            if (ao != null)
                ao.SetActive(true);
        });
        _arenaManager.arenaObjects.Clear();

       
        gameObject.SetActive(false);
    }
}
