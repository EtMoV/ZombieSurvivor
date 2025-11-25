using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject panelPauseWatch;
    public GameObject panelPausePhone;
    public GameObject iconPauseWatch;
    public GameObject iconPausePhone;
    public GameObject gameplayRootGo;

    public GameObject joystickGo;

    public GameObject roundTextGoWatch;
    public GameObject roundTextGoPhone;

    public bool isActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPause()
    {
        isActive = true;
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            panelPauseWatch.SetActive(true);
            iconPauseWatch.SetActive(false);
            roundTextGoWatch.SetActive(false);
        }
        else
        {
            panelPausePhone.SetActive(true);
            iconPausePhone.SetActive(false);
            joystickGo.SetActive(false);
            roundTextGoPhone.SetActive(false);
        }


        // On stoppe le jeu
        gameplayRootGo.SetActive(false);
    }

    public void OnResume()
    {
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            panelPauseWatch.SetActive(false);
            iconPauseWatch.SetActive(true);
        }
        else
        {
            panelPausePhone.SetActive(false);
            iconPausePhone.SetActive(true);
            joystickGo.SetActive(true);
        }

        // On relance le jeu
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
    }

}
