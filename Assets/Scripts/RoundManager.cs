using System.Collections;
using Firebase.Analytics;
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

    public string currentMapName = "mapOne"; // TODO recuperer depuis le chargement de la scene

    void Awake()
    {
        _gameManager = gameManagerGo.GetComponent<GameManager>();
        _shopManager = shopManagerGo.GetComponent<ShopManager>();
        _pauseManager = pauseManagerGo.GetComponent<PauseManager>();
        _powerUpManager = powerUpManagerGo.GetComponent<PowerUpManager>();
        _exitManager = exitManagerGo.GetComponent<ExitManager>();
        _arenaManager = arenaManagerGo.GetComponent<ArenaManager>();
        currentRound = new Round(0, 5, 5, 1, false, false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextIsBoss = false;
        isMapFinish = false;
        if (isScenario)
            return;
        launchNextRound();
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
        if (zombies.Length == 0 && currentRound.currentNbZombieSpawn >= currentRound.maxZombies && !_shopManager.isActive && !_pauseManager.isActive && !_powerUpManager.isActive && !_exitManager.isActive && !_arenaManager.isActive)
        {
            // On passe au round suivant si plus de zombie et qu'ils ont tous spawn
            launchNextRound();
        }
    }

    public void launchNextRound()
    {
        int nextNumberRound = currentRound.numberRound + 1;
        int nextNbZombiesSpawn = currentRound.nbZombieSpawn + 10;
        int nextMaxZombies = nextNumberRound % 2 == 0  ? currentRound.maxZombies * 2 : currentRound.maxZombies + 10;
        int nextPvZombies = nextNumberRound % 2 == 0 ? currentRound.pvZombie + 1 : currentRound.pvZombie;
        nextIsBoss = nextNumberRound % 10 == 0 ? true : false;

        if (nextNumberRound == (10 + 1))
        {
            // Last round has been done, display victory screen
            isMapFinish = true;
            victoryScreenGo.SetActive(true);
            FirebaseAnalytics.LogEvent("victory", new Parameter("level", 1));
        }
        else
        {
            currentRound = new Round(nextNumberRound, nextNbZombiesSpawn, nextMaxZombies, nextPvZombies, nextIsBoss, false);
            StartCoroutine(activateRound());
        }
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

        roundTextGoPhone.GetComponent<TextMeshProUGUI>().text = "Wave " + currentRound.numberRound.ToString();
        yield return new WaitForSeconds(3f);
        roundTextGoPhone.SetActive(false);
        currentRound.isActive = true;
    }
}
