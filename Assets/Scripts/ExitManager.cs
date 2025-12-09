using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    public GameObject panelUiWantExitWatch;
    public GameObject panelUiResumeExitWatch;
    public GameObject panelUiWantExitPhone;
    public GameObject panelUiResumeExitPhone;
    public GameObject gameplayRootGo;
    public GameObject btnUiPauseWatch;
    public GameObject btnUiPausePhone;
    public GameObject joystickPhoneGo;
    public bool isActive;
    public GameObject exitGo;
    private Exit _exit;

    public GameObject textUiLootWatch;
    public GameObject textUiLootPhone;
    public GameObject textUiKillWatch;
    public GameObject textUiKillPhone;

    public GameObject inventoryGo;
    private Inventory _inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        isActive = false;
        _exit = exitGo.GetComponent<Exit>();
        _inventory = inventoryGo.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnExit()
    {
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            // Affichage de la pop up de resume de partie
            panelUiWantExitWatch.SetActive(false);
            panelUiResumeExitWatch.SetActive(true);
            textUiLootWatch.GetComponent<TextMeshProUGUI>().text = _inventory.lootQte.ToString();
            textUiKillWatch.GetComponent<TextMeshProUGUI>().text = _inventory.totalKillCount.ToString();
        }
        else
        {
            // Affichage de la pop up de resume de partie
            panelUiWantExitPhone.SetActive(false);
            panelUiResumeExitPhone.SetActive(true);
            textUiLootPhone.GetComponent<TextMeshProUGUI>().text = _inventory.lootQte.ToString();
            textUiKillPhone.GetComponent<TextMeshProUGUI>().text = _inventory.totalKillCount.ToString();
        }
    }

    public void OnCancel()
    {
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            // On desactive le panel d'exit 
            panelUiWantExitWatch.SetActive(false);
            // Reactivation du bouton pause
            btnUiPauseWatch.SetActive(true);
        }
        else
        {
            // On desactive le panel d'exit 
            panelUiWantExitPhone.SetActive(false);
            // Reactivation du bouton pause
            btnUiPausePhone.SetActive(true);
            // Reactivation du joystick
            joystickPhoneGo.SetActive(true);
        }

        // Reactivation du gameRoot
        gameplayRootGo.SetActive(true);
        // Signalement au roundManager qu'il peut reprendre
        isActive = false;

        // affichage de l'exit
        _exit.gameObject.SetActive(true);
        _exit.textUiExit.SetActive(true);

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

        // On active le timer pour la prochaine collision
        StartCoroutine(DisableCollisionCoroutine());
    }

    public void OnQuit()
    {
        // Enregistre l'inventaire
        for (int i = 0; i < _inventory.lootQte; i++)
        {
            LootManager.AddLoot();
        }

        // Load la scene du hub
        SceneManager.LoadScene(0);
    }

    private System.Collections.IEnumerator DisableCollisionCoroutine()
    {
        // Récupération du collider de l'objet exit
        Collider2D collider = _exit.GetComponent<Collider2D>();

        if (collider != null)
        {
            // Désactivation de la collision
            collider.enabled = false;

            // Attente de 1 seconde
            yield return new WaitForSeconds(3f);

            // Réactivation de la collision
            collider.enabled = true;
        }
    }
}
