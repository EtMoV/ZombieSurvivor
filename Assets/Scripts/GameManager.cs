using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Analytics;

public class GameManager : MonoBehaviour
{
    public GameObject playerGO;
    private PlayerController _playerController;
    public GameObject losePanelWatch; // ton Panel UI
    public GameObject losePanelPhone; // ton Panel UI
    public GameObject gameplayRoot; // Le parent de tous les objets du jeu (ennemis, joueur, etc.)
    public GameObject killCountDieUiTextWatch;
    public GameObject killCountDieUiTextPhone;
    public GameObject inventoryGO;
    private Inventory _inventory;
    public GridManager gridManager;
    public bool isDead;

    public GameObject iconPauseGoWatch;
    public GameObject iconPauseGoPhone;

    public GameObject textUiLoot;

    public GameObject adsManagerGo;

    public GameObject btnX2Reward;

    void Awake()
    {
        _inventory = inventoryGO.GetComponent<Inventory>();
        _playerController = playerGO.GetComponent<PlayerController>();
        // Génère la grille avant que les zombies ne bougent
        gridManager.GenerateGrid();
    }

    void Start()
    {
        isDead = false;
    }

    void Update()
    {
    }

    // Display Lose Screen
    public void displayLoseScreen()
    {
        isDead = true;
        iconPauseGoWatch.SetActive(false);
        iconPauseGoPhone.SetActive(false);
        _playerController.joystickUiGo.SetActive(false); // On cache le joystick
        gameplayRoot.SetActive(false); // stoppe le jeu sans toucher au temps
        // Ajout du loot
        for (int i = 0; i < _inventory.lootQte; i++)
        {
            LootManager.AddLoot();
        }
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            losePanelWatch.SetActive(true); // Affichage du panel de défaite
            killCountDieUiTextWatch.GetComponent<TextMeshProUGUI>().text = _inventory.totalKillCount.ToString();
        }
        else
        {
            losePanelPhone.SetActive(true); // Affichage du panel de défaite
            killCountDieUiTextPhone.GetComponent<TextMeshProUGUI>().text = _inventory.totalKillCount.ToString();
            textUiLoot.GetComponent<TextMeshProUGUI>().text = _inventory.lootQte.ToString();
            btnX2Reward.SetActive(true);
        }

    }

    public void displayLoseTutoScreen()
    {
        // Tuto est realise
        SaveData data = SaveSystem.GetData();
        data.isTutoDone = true;
        SaveSystem.Save(data);
        
        isDead = true;
        iconPauseGoWatch.SetActive(false);
        iconPauseGoPhone.SetActive(false);
        _playerController.joystickUiGo.SetActive(false); // On cache le joystick
        gameplayRoot.SetActive(false); // stoppe le jeu sans toucher au temps
        // Ajout du loot
        for (int i = 0; i < _inventory.lootQte; i++)
        {
            LootManager.AddLoot();
        }

        losePanelPhone.SetActive(true); // Affichage du panel de défaite
        killCountDieUiTextPhone.GetComponent<TextMeshProUGUI>().text = _inventory.totalKillCount.ToString();
        textUiLoot.GetComponent<TextMeshProUGUI>().text = _inventory.lootQte.ToString();
    }

    // Restart the game
    public void RestartGame()
    {
        FirebaseAnalytics.LogEvent("restart_game", new Parameter("level", StoreDataScene.currentMap), new Parameter("totalKill", _inventory.totalKillCount));
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitToMenu()
    {
        FirebaseAnalytics.LogEvent("quit_to_menu", new Parameter("level", 1), new Parameter("totalKill", _inventory.totalKillCount));
        var existingCanvas = FindFirstObjectByType<Canvas>();
        if (existingCanvas != null)
            Destroy(existingCanvas.gameObject);
        SceneManager.LoadScene(0);
    }

    public void QuitToHub()
    {
        FirebaseAnalytics.LogEvent("quit_to_hub_tuto", new Parameter("totalKill", _inventory.totalKillCount));
        var existingCanvas = FindFirstObjectByType<Canvas>();
        if (existingCanvas != null)
            Destroy(existingCanvas.gameObject);
        SceneManager.LoadScene(0);
    }

    public void showRewardedVideo()
    {
        adsManagerGo.GetComponent<AdsManager>().ShowRewarded();
        
        // On rajoute le loot X2 (on relance la boucle d'ajout de loot)
         for (int i = 0; i < _inventory.lootQte; i++)
        {
            LootManager.AddLoot();
        }

        textUiLoot.GetComponent<TextMeshProUGUI>().text = (_inventory.lootQte * 2).ToString();
        btnX2Reward.SetActive(false);
    }


}

