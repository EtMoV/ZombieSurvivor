using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

public class SpawnPoint : MonoBehaviour
{
    [Header("Références")]
    public GameObject gameManagerGo; // GameObject du GameManager
    public GameObject roundManagerGo; // GameObject du RoundManager

    [Header("Paramètres de spawn")]
    public float spawnInterval = 0.2f; // Temps entre chaque spawn
    public float detectionRadius = 20f; // Distance de détection du joueur

    public bool isActive = false;

    private List<GameObject> activeZombies;
    private ZombieFactory _zombieFactory;
    private RoundManager _roundManager;

    public GameObject arenaManagerGo;
    private ArenaManager _arenaManager;

    private bool launchSpawn = true;

    private bool endArena = false;

    public bool isScenario = false;

    public int nbZombieScenario = 0;

    public bool isBossScenario = false;

    public int pvZombieScenario = 0;

    public bool hasSpawnScenario = false;

    public bool zombieSpawnHasDie = false;

    public bool launchSpawnScenario = false;

    private GameObject lastZombie = null;

    public void RelaunchSpawn(bool relaunchSpawn)
    {
        if (relaunchSpawn)
        {
            _zombieFactory = gameManagerGo.GetComponent<ZombieFactory>();
            _roundManager = roundManagerGo.GetComponent<RoundManager>();
            _arenaManager = arenaManagerGo.GetComponent<ArenaManager>();
            activeZombies = new List<GameObject>();
            launchSpawn = true;
            StartCoroutine(SpawnZombies(_arenaManager.getZombieCount(), _arenaManager.getIsBoss()));
        }
    }

    void FixedUpdate()
    {
        if (isScenario && isActive && !hasSpawnScenario)
        {
            hasSpawnScenario = true;
            _zombieFactory = gameManagerGo.GetComponent<ZombieFactory>();
            activeZombies = new List<GameObject>();
            launchSpawnScenario = true;
            StartCoroutine(SpawnZombies(nbZombieScenario, isBossScenario));
        }

        if (isScenario)
        {
            if (hasSpawnScenario && !launchSpawnScenario)
            {
                zombieSpawnHasDie = activeZombies.Count == 0;
            }
            return;
        }

        if (!launchSpawn && activeZombies.Count == 0)
        {
            launchSpawn = true;
            endArena = true;
            _arenaManager.endArena(lastZombie);
        }
        else if (!endArena && launchSpawn && activeZombies.Count == 0)
        {
            // On relance le spawn car il y a eu une interruption de la coroutines
            RelaunchSpawn(true);
        }
    }

    IEnumerator SpawnZombies(int nbZombie, bool isBoss)
    {
        // ✅ Spawn des zombies
        for (int i = 0; i < nbZombie; i++)
        {
            GameObject newZombie = null;

            if (isScenario)
            {
                if (isBoss)
                {
                    newZombie = _zombieFactory.InstantiateZombie(transform.position, pvZombieScenario, true, true).gameObject;
                    isBoss = false; // Spawn qu'un seul boss
                }
                else
                {
                    newZombie = _zombieFactory.InstantiateZombie(transform.position, pvZombieScenario, false, true).gameObject;
                }
            }
            else
            {

                if (isBoss)
                {
                    _roundManager.currentRound.bossHasSpawn = true;
                    newZombie = _zombieFactory.InstantiateZombie(transform.position, _roundManager.currentRound.pvZombie, true, true).gameObject;
                    isBoss = false; // Spawn qu'un seul boss
                }
                else
                {
                    newZombie = _zombieFactory.InstantiateZombie(transform.position, _roundManager.currentRound.pvZombie, false, true).gameObject;
                }
            }

            // Stocke la référence pour suivi
            activeZombies.Add(newZombie);

            // Supprime de la liste à la mort
            Zombie zombieScript = newZombie.GetComponent<Zombie>();
            zombieScript.OnDeath += () => {activeZombies.Remove(newZombie); lastZombie = newZombie;};
            yield return new WaitForSeconds(spawnInterval);

            if (i == nbZombie - 1)
            {
                launchSpawn = false; // Tout le spawn a eu lieu
                launchSpawnScenario = false;
            }
        }
    }

    // 🧩 Visualisation dans la scène
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
