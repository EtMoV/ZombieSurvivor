using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public GameObject inventoryGo;
    private Inventory _inventory;
    public GameObject panelShopGoWatch;
    public GameObject panelShopGoPhone;
    public GameObject gameplayRootGo;
    public Weapon weaponTmpOne;
    public Weapon weaponTmpTwo;

    public bool isActive;

    public GameObject playerGO;
    public PlayerController _playerController;
    public GameObject iconPauseGoWatch;
    public GameObject iconPauseGoPhone;

    public bool isBtnActive;

    public GameObject arenaManagerGo;
    private ArenaManager _arenaManager;

    void Awake()
    {
        _inventory = inventoryGo.GetComponent<Inventory>();
        _playerController = playerGO.GetComponent<PlayerController>();
        _arenaManager = arenaManagerGo.GetComponent<ArenaManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isActive = false;
        isBtnActive = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitShop()
    {
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            panelShopGoWatch.SetActive(false);
            iconPauseGoWatch.SetActive(true);
        }
        else
        {
            panelShopGoPhone.SetActive(false);
            //iconPauseGoPhone.SetActive(true);
            _playerController.joystickUiGo.SetActive(true); // On reaffiche le joystick
        }

        gameplayRootGo.SetActive(true);
        isActive = false;

        // Relancer les coroutines
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        GameObject[] playerSpawnPoints = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject sp in spawnPoints)
        {
            sp.GetComponent<SpawnPoint>().RelaunchSpawn(false);
        }

        foreach (GameObject psp in playerSpawnPoints)
        {
            psp.GetComponent<PlayerZombieSpawn>().RelaunchSpawn();
        }

        isBtnActive = false;

        // Active l'exit de l'arène
        _arenaManager.canExit();
    }

    public void onChooseWeaponOne()
    {
        if (isBtnActive)
        {
            _inventory.addWeapon(weaponTmpOne);
            QuitShop();
        }
    }

    public void onChooseWeaponTwo()
    {
        if (isBtnActive)
        {
            // On PASS et on ne choisis pas de nouvelle arme
            QuitShop();
        }
    }
}
