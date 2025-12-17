using System.Collections;
using Firebase.Analytics;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{

    public Round currentRound;

    public GameObject roundTextGoWatch;
    public GameObject roundTextGoPhone;
    public GameObject gameManagerGo;
    private GameManager _gameManager;
    public GameObject shopManagerGo;
    private ShopManager _shopManager;

    public GameObject pauseManagerGo;
    private PauseManager _pauseManager;

    public GameObject powerUpManagerGo;
    private PowerUpManager _powerUpManager;
    public GameObject exitManagerGo;
    private ExitManager _exitManager;

    public GameObject arenaManagerGo;
    private ArenaManager _arenaManager;

    public bool isScenario = false;

    private bool nextIsBoss = false;

    public GameObject bossAppearGo;

    public GameObject victoryScreenGo;

    public bool isMapFinish = false;

    public bool isTuto = false;

    public bool launchRoundTuto = false;

    private bool roundIsCreated = false;

    public GameObject inventoryGo;

    public GameObject adsManagerGo;
    public GameObject btnX2Reward;

    public GameObject textUiLoot;

    public GameObject killCount;

    void Awake()
    {
        _gameManager = gameManagerGo.GetComponent<GameManager>();
        _shopManager = shopManagerGo.GetComponent<ShopManager>();
        _pauseManager = pauseManagerGo.GetComponent<PauseManager>();
        _powerUpManager = powerUpManagerGo.GetComponent<PowerUpManager>();
        _exitManager = exitManagerGo.GetComponent<ExitManager>();
        _arenaManager = arenaManagerGo.GetComponent<ArenaManager>();
        if (isTuto)
        {
            currentRound = new Round(0, 0, 0, 1, false, false);
        }
        else
        {
            currentRound = new Round(0, 5, 5, 1, false, false);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextIsBoss = false;
        isMapFinish = false;
        if (isScenario)
            return;

        if (isTuto)
        {
            if (launchRoundTuto)
            {
                launchNextRound();
            }
        }
        else
        {
            launchNextRound();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (isScenario)
            return;

        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        if (!roundIsCreated && isTuto && launchRoundTuto && zombies.Length == 0 && currentRound.currentNbZombieSpawn >= currentRound.maxZombies && !_shopManager.isActive && !_pauseManager.isActive && !_powerUpManager.isActive && !_exitManager.isActive && !_arenaManager.isActive)
        {
            // On passe au round suivant si plus de zombie et qu'ils ont tous spawn
            roundIsCreated = true;
            launchNextRound();
        }
        else if (!roundIsCreated && !isTuto && zombies.Length == 0 && currentRound.currentNbZombieSpawn >= currentRound.maxZombies && !_shopManager.isActive && !_pauseManager.isActive && !_powerUpManager.isActive && !_exitManager.isActive && !_arenaManager.isActive)
        {
            // On passe au round suivant si plus de zombie et qu'ils ont tous spawn
            roundIsCreated = true;
            launchNextRound();
        }
    }

    public void launchNextRound()
    {
        int nextNumberRound = currentRound.numberRound + 1;
        int nextNbZombiesSpawn = currentRound.nbZombieSpawn + 10;
        int nextMaxZombies = nextNumberRound % 3 == 0 ? currentRound.maxZombies * 2 : currentRound.maxZombies + 10;
        int nextPvZombies = nextNumberRound % 3 == 0 ? currentRound.pvZombie + 1 : currentRound.pvZombie;
        nextIsBoss = nextNumberRound % 3 == 0 ? true : false;

        if (isTuto)
        {
            nextNumberRound = currentRound.numberRound + 1;
            nextNbZombiesSpawn = currentRound.nbZombieSpawn + 100;
            nextMaxZombies = currentRound.maxZombies + 100;
            nextPvZombies = currentRound.pvZombie + 1;
            nextIsBoss = false;
            FirebaseAnalytics.LogEvent("new_round_tuto", new Parameter("round", nextNumberRound));
        }

        if (nextNumberRound == (10 + 1) && !isTuto && !isMapFinish)
        {
            // Last round has been done, display victory screen
            isMapFinish = true;
            victoryScreenGo.SetActive(true);
            btnX2Reward.SetActive(true);
            textUiLoot.GetComponent<TextMeshProUGUI>().text = inventoryGo.GetComponent<Inventory>().lootQte.ToString();
            killCount.GetComponent<TextMeshProUGUI>().text = inventoryGo.GetComponent<Inventory>().totalKillCount.ToString();

            // Ajout du loot
            for (int i = 0; i < inventoryGo.GetComponent<Inventory>().lootQte; i++)
            {

                LootManager.AddLoot();
            }

            // On enregistre la map gagne
            SaveData data = SaveSystem.GetData();
            if (StoreDataScene.currentMap == "mapOne")
            {
                data.mapOneDone = true;
            }
            else if (StoreDataScene.currentMap == "mapTwo")
            {
                data.mapTwoDone = true;
            }
            else if (StoreDataScene.currentMap == "mapThree")
            {
                data.mapThreeDone = true;
            }
            else if (StoreDataScene.currentMap == "mapFour")
            {
                data.mapFourDone = true;
            }
            else if (StoreDataScene.currentMap == "mapFive")
            {
                data.mapFiveDone = true;
            }
            else if (StoreDataScene.currentMap == "mapSix")
            {
                data.mapSixDone = true;
            }
            else if (StoreDataScene.currentMap == "mapSeven")
            {
                data.mapSevenDone = true;
            }
            else if (StoreDataScene.currentMap == "mapEight")
            {
                data.mapEightDone = true;
            }
            else if (StoreDataScene.currentMap == "mapNine")
            {
                data.mapNineDone = true;
            }
            // AJOUTER ICI LES PROCHAINS LEVELS
            SaveSystem.Save(data);

            FirebaseAnalytics.LogEvent("victory", new Parameter("level", StoreDataScene.currentMap));
        }
        else if (!isMapFinish)
        {
            FirebaseAnalytics.LogEvent("new_round", new Parameter("round", nextNumberRound));
            currentRound = new Round(nextNumberRound, nextNbZombiesSpawn, nextMaxZombies, nextPvZombies, nextIsBoss, false);
            StartCoroutine(activateRound());
        }

        roundIsCreated = false;
    }

    private IEnumerator activateRound()
    {
        yield return new WaitForSeconds(1f);
        if (!_gameManager.isDead)
        {
            roundTextGoPhone.SetActive(true);
        }

        if (nextIsBoss)
        {
            bossAppearGo.SetActive(true);
            bossAppearGo.GetComponent<BossAppear>().FlashBossText();
        }

        roundTextGoPhone.GetComponent<TextMeshProUGUI>().text = "Wave " + currentRound.numberRound.ToString() + " / 10";
        yield return new WaitForSeconds(3f);
        roundTextGoPhone.SetActive(false);
        currentRound.isActive = true;
    }

    public void showRewardedVideo()
    {
        adsManagerGo.GetComponent<AdsManager>().ShowRewarded();

        // On rajoute le loot X2 (on relance la boucle d'ajout de loot)
        for (int i = 0; i < inventoryGo.GetComponent<Inventory>().lootQte; i++)
        {
            LootManager.AddLoot();
        }

        textUiLoot.GetComponent<TextMeshProUGUI>().text = (inventoryGo.GetComponent<Inventory>().lootQte * 2).ToString();
        btnX2Reward.SetActive(false);
    }
}
