using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
        }

    }

    // Restart the game
    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // Restart the game
    public void QuitToMenu()
    {
        var existingCanvas = FindFirstObjectByType<Canvas>();
        if (existingCanvas != null)
            Destroy(existingCanvas.gameObject);
        SceneManager.LoadScene(0);
    }
}

