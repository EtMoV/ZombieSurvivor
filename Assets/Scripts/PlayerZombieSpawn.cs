using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class PlayerZombieSpawn : MonoBehaviour
{
    [Header("Références")]
    public GameObject gameManagerGo;
    public GameObject roundManagerGo;

    [Header("Paramètres de spawn")]
    public float spawnInterval = 2f; // Temps entre chaque spawn
    public float spawnRadius = 12f; // Rayon de spawn en cercle autour du joueur
    public bool isActive = true;

    private List<GameObject> activeZombies;
    private ZombieFactory _zombieFactory;
    private RoundManager _roundManager;

    [Header("Tilemap sol")]
    public Tilemap backgroundTilemap; // sol

    public Tilemap wallTilemap; // murs

    public bool isScenario = false;

    void Awake()
    {
        _zombieFactory = gameManagerGo.GetComponent<ZombieFactory>();
        _roundManager = roundManagerGo.GetComponent<RoundManager>();
        activeZombies = new List<GameObject>();
    }

    void Start()
    {
        // Si le joueur n'est pas assigné, on le cherche automatiquement
        RelaunchSpawn();
    }

    bool IsValidSpawn(Vector2 position)
    {
        Vector3Int cellPos = backgroundTilemap.WorldToCell(position);

        // La tile de sol doit exister
        TileBase bgTile = backgroundTilemap.GetTile(cellPos);
        if (bgTile == null)
            return false;

        // Vérifie si on est dans un mur → interdit
        TileBase wallTile = wallTilemap.GetTile(cellPos);
        if (wallTile != null)
            return false;

        return true;
    }

    public void RelaunchSpawn()
    {
        if (isScenario)
            return; // Pas de spawn autour

        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            // 🔴 Vérifie si le spawn est actif et si le round a commencé
            if (!isActive || !_roundManager.currentRound.isActive || this == null)
                continue;

            // ✅ Spawn des zombies en cercle autour du joueur
            for (int i = 0; i < _roundManager.currentRound.nbZombieSpawn; i++)
            {
                if (_roundManager.currentRound.currentNbZombieSpawn >= _roundManager.currentRound.maxZombies)
                    break;

                // 🧭 Calcule une position en cercle autour du joueur
                float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad; // Angle aléatoire en radians
                Vector2 spawnOffset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnRadius;
                Vector2 spawnPosition = (Vector2)transform.position + spawnOffset;

                // ⛔ On vérifie si la tile est valide (sol)
                if (!IsValidSpawn(spawnPosition))
                {
                    continue; // Skip et passe au zombie suivant
                }

                GameObject newZombie = null;
                _roundManager.currentRound.currentNbZombieSpawn++;

                if (_roundManager.currentRound.isBoss && !_roundManager.currentRound.bossHasSpawn)
                {
                    _roundManager.currentRound.bossHasSpawn = true;
                    newZombie = _zombieFactory.InstantiateZombie(spawnPosition, _roundManager.currentRound.pvZombie, true, false).gameObject;
                }
                else
                {
                    newZombie = _zombieFactory.InstantiateZombie(spawnPosition, _roundManager.currentRound.pvZombie, false, false).gameObject;
                }

                // Stocke la référence pour suivi
                activeZombies.Add(newZombie);

                // Supprime de la liste à la mort
                Zombie zombieScript = newZombie.GetComponent<Zombie>();
                zombieScript.OnDeath += () => activeZombies.Remove(newZombie);
            }
        }
    }

    // 🧩 Visualisation dans la scène
    private void OnDrawGizmosSelected()
    {
        if (this != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
    }
}
