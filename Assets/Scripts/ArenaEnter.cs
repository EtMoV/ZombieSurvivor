using System.Collections;
using UnityEngine;

public class ArenaEnter : MonoBehaviour
{
    public GameObject ArenaGo;
    private Arena _arena;
    public GameObject doorArenaGo;

    public GameObject fadeBlackGo;

    public GameObject arenaManagerGo;
    private ArenaManager _arenaManager;

    public void Awake()
    {
        _arena = ArenaGo.GetComponent<Arena>();
        _arenaManager = arenaManagerGo.GetComponent<ArenaManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fadeBlackGo.GetComponent<FadeBlack>().ShowBlack(0.5f); // Fondu en 1 seconde
            StartCoroutine(CoroutineCollision(collision.gameObject));
        }
    }

    private IEnumerator CoroutineCollision(GameObject playerGO)
    {
        yield return new WaitForSeconds(0.6f);

        // Suppression des zombies existants
        playerGO.GetComponent<PlayerZombieSpawn>().isActive = false;

        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");

        foreach (GameObject z in zombies)
        {
            z.GetComponent<Zombie>().autoKillArena();
        }

        GameObject[] arenaObj = GameObject.FindGameObjectsWithTag("ArenaObject");

        foreach (GameObject ao in arenaObj)
        {
            ao.SetActive(false);
            _arenaManager.arenaObjects.Add(ao); // On ajoute les references des objets d'arène à la liste pour les reafficher plus tard
        }

        doorArenaGo.SetActive(false);
        _arena.enterArena();
        Destroy(gameObject);
    }
}
