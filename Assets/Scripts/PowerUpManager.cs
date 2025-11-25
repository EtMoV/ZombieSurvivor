using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameObject inventoryGo;

    private Inventory _inventory;

    public PowerUp tmpPowerUpOne;

    public PowerUp tmpPowerUpTwo;

    public List<PowerUp> possiblePowerUp;

    public bool isActive;

    public GameObject gameplayRootGo;
    public GameObject panelPowerUpGoWatch;
    public GameObject panelPowerUpGoPhone;

    public GameObject iconPauseGoWatch;
    public GameObject iconPauseGoPhone;

    public GameObject playerControllerGo;
    public PlayerController _playerController;

    void Awake()
    {
        _inventory = inventoryGo.GetComponent<Inventory>();
        _playerController = playerControllerGo.GetComponent<PlayerController>();
        possiblePowerUp = new List<PowerUp>
        {
            new PowerUp("angle", 1, "Dispersion", "+1 bullet", true),
            new PowerUp("speed", 1, "Speed", "+1 speed", true),
            new PowerUp("range", 1, "Range", "+1 range", true),
            new PowerUp("size", 1, "Bullet Size", "+1 bullet size", true),
            new PowerUp("life", 1, "More Life", "+1 life", true),
            new PowerUp("damage", 1, "More damage", "+1 bullet damage", true),
            new PowerUp("attackSpeed", 1, "Attack speed", "+1 attack speed", true),
            new PowerUp("bulletGlace", 1, "Glass Bullet", "Slows down the enemy", false),
            new PowerUp("bulletFeu", 1, "Fire Bullet", "Deals damage over time", false),
            new PowerUp("bulletElec", 1, "Electric Bullet", "The damage bounces off the nearest enemy", false),
        };
        isActive = false;
    }

    public void generateTmpPowerUp(bool isStat)
    {

        if (isStat)
        {
            tmpPowerUpOne = possiblePowerUp.Where(p => p.isStat).OrderBy(_ => Random.value).FirstOrDefault();
            tmpPowerUpTwo = possiblePowerUp.Where(p => p.isStat).OrderBy(_ => Random.value).FirstOrDefault();
        }
        else
        {
            tmpPowerUpOne = possiblePowerUp.Where(p => !p.isStat).OrderBy(_ => Random.value).FirstOrDefault();
            tmpPowerUpTwo = possiblePowerUp.Where(p => !p.isStat).OrderBy(_ => Random.value).FirstOrDefault();
        }
    }

    public void onAddPowerUpOne()
    {
        onAddPowerUp(tmpPowerUpOne);
    }

    public void onAddPowerUpTwo()
    {
        onAddPowerUp(tmpPowerUpTwo);
    }

    private void onAddPowerUp(PowerUp powerUp)
    {
        PowerUp newInstancePowerUp = powerUp.Clone();

        PowerUp powerUpFind = _inventory.powerUpList.Find(p => p.type == powerUp.type);

        if (powerUpFind != null)
        {
            // Update
            powerUpFind.lvl++;
        }
        else
        {
            _inventory.powerUpList.Add(powerUp);
        }

        // On manage les powers ups
        _inventory.managePowerUp();

        // On desactive le panel de power up et on relance le jeu

        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            panelPowerUpGoWatch.SetActive(false);
            iconPauseGoWatch.SetActive(true);
        }
        else
        {
            panelPowerUpGoPhone.SetActive(false);
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
    }
}
