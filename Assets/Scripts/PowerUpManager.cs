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
            new PowerUp("angle", 1, "Dispersion", "+1 bullet","+1 bullet", true, false, "angleEvolution", "angleEvolutionDescription", "angleEvol"),
            new PowerUp("speed", 1, "Speed", "+1 speed","+1 speed", true, false, "speedEvolution", "speedEvolutionDescription", "speedEvol"),
            new PowerUp("range", 1, "Range", "+1 range","+1 range", true, false, "rangeEvolution", "rangeEvolutionDescription", "rangeEvol"),
            new PowerUp("size", 1, "Bullet Size", "+1 bullet size","+1 bullet size", true, false, "sizeEvolution", "sizeEvolutionDescription", "sizeEvol"),
            new PowerUp("life", 1, "More Life", "+1 life","+1 life", true, false, "lifeEvolution", "lifeEvolutionDescription", "lifeEvol"),
            new PowerUp("damage", 1, "More damage", "+1 bullet damage", "+1 bullet damage", true, false, "damageEvolution", "damageEvolutionDescription", "damageEvol"),
            new PowerUp("attackSpeed", 1, "Attack speed", "+1 attack speed","+1 attack speed", true, false, "attackSpeedEvolution", "attackSpeedEvolutionDescription", "attackSpeedEvol"),
            new PowerUp("bulletGlace", 1, "+1 Frozen Bullet", "More chance to fire a slow down bullet", "Increase slow", false, false, "bulletGlaceEvolution", "bulletGlaceEvolutionDescription", "bulletGlaceEvol"),
            new PowerUp("bulletFeu", 1, "+1 Fire Bullet", "More chance to fire a bullet who deals damage over time", "Increase damage over time", false, false, "bulletFeuEvolution", "bulletFeuEvolutionDescription", "bulletFeuEvol"),
            new PowerUp("bulletElec", 1, "+1 Electric Bullet", "More chance to fire a bullet who bounces off the nearest enemy", "Bounces on more ennemy", false, false,"bulletElecEvolution", "bulletElecEvolutionDescription", "bulletElecEvol"),
            new PowerUp("pistol", 1, "Pistol", "A simple pistol", "Increase damage", false, true, "Deagle", "A bigger pistol", "spas"),
        };
        isActive = false;
    }

    public void generateTmpPowerUp(bool isStat, bool hasFullWeapon)
    {
        if (isStat)
        {
            tmpPowerUpOne = possiblePowerUp.Where(p => p.isStat).OrderBy(_ => Random.value).FirstOrDefault();
            tmpPowerUpTwo = possiblePowerUp.Where(p => p.isStat).OrderBy(_ => Random.value).FirstOrDefault();

            // S'assurer que tmpPowerUpTwo est différent de tmpPowerUpOne
            while (tmpPowerUpTwo.type == tmpPowerUpOne.type)
            {
                tmpPowerUpTwo = possiblePowerUp.Where(p => p.isStat).OrderBy(_ => Random.value).FirstOrDefault();
            }
        }
        else
        {
            if (!hasFullWeapon)
            {
                tmpPowerUpOne = possiblePowerUp.Where(p => !p.isStat).OrderBy(_ => Random.value).FirstOrDefault();
                tmpPowerUpTwo = possiblePowerUp.Where(p => !p.isStat).OrderBy(_ => Random.value).FirstOrDefault();

                // S'assurer que tmpPowerUpTwo est différent de tmpPowerUpOne
                while (tmpPowerUpTwo.type == tmpPowerUpOne.type)
                {
                    tmpPowerUpTwo = possiblePowerUp.Where(p => !p.isStat).OrderBy(_ => Random.value).FirstOrDefault();
                }
            }
            else
            {
                // Ne propose que les power up et les armes qu'on a dans l'inventaire qui n'ont pas atteint le lvl max (les armes seulement)

                tmpPowerUpOne = possiblePowerUp.Where(p =>
                   {
                       if (!p.isStat && !p.isWeapon)
                       {
                           // C'est un power up standard
                           return true;
                       }
                       else if (!p.isStat && p.isWeapon)
                       {
                           // On check si l'arme est dans l'inventaire
                           foreach (Weapon w in _inventory.weapons)
                           {
                               if (w._name == p.type)
                               {
                                   // L'arme est dans l'inventaire
                                   if (w.lvl == 5)
                                   {
                                       return false; // Lvl max de l'arme atteint
                                   }
                                   return true;
                               }
                           }
                           return false; // L'arme n'est pas dans l'inventaire
                       }
                       else
                       {
                           return false;
                       }
                   }).OrderBy(_ => Random.value).FirstOrDefault();

                tmpPowerUpTwo = possiblePowerUp.Where(p =>
              {
                  if (!p.isStat && !p.isWeapon)
                  {
                      // C'est un power up standard
                      return true;
                  }
                  else if (!p.isStat && p.isWeapon)
                  {
                      // On check si l'arme est dans l'inventaire
                      foreach (Weapon w in _inventory.weapons)
                      {
                          if (w._name == p.type)
                          {
                              // L'arme est dans l'inventaire
                              if (w.lvl == 5)
                              {
                                  return false; // Lvl max de l'arme atteint
                              }
                              return true;
                          }
                      }
                      return false; // L'arme n'est pas dans l'inventaire
                  }
                  else
                  {
                      return false;
                  }
              }).OrderBy(_ => Random.value).FirstOrDefault();
                // S'assurer que tmpPowerUpTwo est différent de tmpPowerUpOne
                while (tmpPowerUpTwo.type == tmpPowerUpOne.type)
                {
                    tmpPowerUpTwo = possiblePowerUp.Where(p =>
                                 {
                                     if (!p.isStat && !p.isWeapon)
                                     {
                                         // C'est un power up standard
                                         return true;
                                     }
                                     else if (!p.isStat && p.isWeapon)
                                     {
                                         // On check si l'arme est dans l'inventaire
                                         foreach (Weapon w in _inventory.weapons)
                                         {
                                             if (w._name == p.type)
                                             {
                                                 // L'arme est dans l'inventaire
                                                 if (w.lvl == 5)
                                                 {
                                                     return false; // Lvl max de l'arme atteint
                                                 }
                                                 return true;
                                             }
                                         }
                                         return false; // L'arme n'est pas dans l'inventaire
                                     }
                                     else
                                     {
                                         return false;
                                     }
                                 }).OrderBy(_ => Random.value).FirstOrDefault();
                }
            }
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

        PowerUp powerUpFind = _inventory.powerUpList.Find(p => p.type == newInstancePowerUp.type);

        if (powerUpFind != null)
        {
            // Update
            powerUpFind.lvl++;
        }
        else
        {
            _inventory.powerUpList.Add(newInstancePowerUp);
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
