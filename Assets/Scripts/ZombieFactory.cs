using UnityEngine;

public class ZombieFactory : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject inventoryGo;
    private Inventory _inventory;
    public GameObject gameplayRoot; // Le parent de tous les objets du jeu (ennemis, joueur, etc.)

    public GameObject roundManagerGo;
    private RoundManager _roundManager;

    public Sprite zombieBossSprite;

    public GameObject xpPrefab;

    public GameObject lootPrefab;

    public bool isScenario = false;

    public RuntimeAnimatorController fireAnimatorController;
    public Sprite iceSprite;

    public void Awake()
    {
        _inventory = inventoryGo.GetComponent<Inventory>();
        _roundManager = roundManagerGo.GetComponent<RoundManager>();
    }

    public Zombie InstantiateZombie(Vector2 position, int life, bool isBoss, bool fromSpawnPoint)
    {
        GameObject GoInstantiate = Instantiate(zombiePrefab, position, Quaternion.identity, gameplayRoot.transform);
        Zombie zombieInstantiate = GoInstantiate.GetComponent<Zombie>();
        zombieInstantiate.xpPrefab = xpPrefab;
        zombieInstantiate.inventory = _inventory; // Ajout de la référence à l'inventaire
        zombieInstantiate.roundManager = _roundManager;
        zombieInstantiate.life = life;
        zombieInstantiate.lootPrefab = lootPrefab;
        zombieInstantiate.isScenario = isScenario;
        zombieInstantiate.hasSpawnFromSpawnPoint = fromSpawnPoint;
        zombieInstantiate.fireAnimatorController = fireAnimatorController;
        zombieInstantiate.iceSprite = iceSprite;
        if (isBoss)
        {
            zombieInstantiate.zombieBossSprite = zombieBossSprite;
            zombieInstantiate.createBoss();
        }

        return zombieInstantiate;
    }
}