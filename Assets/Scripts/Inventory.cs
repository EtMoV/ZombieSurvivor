using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Weapon> weapons;
    public int lifeCount;
    public int maxLife;
    public int killCount;
    public int totalKillCount;
    public GameObject killCountUiTextWatch;
    public GameObject killCountUiTextPhone;
    public bool hasWeapon;

    public List<PowerUp> powerUpList;
    public GameObject gameplayRootGo;
    public GameObject panelPowerUpGoWatch;
    public GameObject panelPowerUpGoPhone;
    public GameObject imagePowerUpOneGoWatch;
    public GameObject imagePowerUpTwoGoWatch;
    public GameObject imagePowerUpOneGoPhone;
    public GameObject imagePowerUpTwoGoPhone;
    public GameObject textPowerUpTitleOneWatch;
    public GameObject textPowerUpTitleTwoWatch;
    public GameObject textPowerUpTitleOnePhone;
    public GameObject textPowerUpTitleTwoPhone;
    public GameObject textPowerUpDescriptionOnePhone;
    public GameObject textPowerUpDescriptionTwoPhone;

    public GameObject questUiGoWatch;
    public GameObject textQuestUiGoWatch;
    public GameObject questUiGoPhone;
    public GameObject textQuestUiGoPhone;

    public GameObject powerUpManagerGo;
    private PowerUpManager _powerUpManager;
    public int xp;
    public int nextXp;

    public int angle; // Nombre de balles tirées en angle
    public int speed;
    public int range;
    public int size;
    public int damage;
    public float attackSpeed;
    public int bulletGlace;
    public int bulletFeu;
    public int bulletElec;

    public int lootQte;

    public bool questIsDone;

    public GameObject fillBarXp;
    public GameObject fillBarLife;

    public bool isScenario = false;

    void Awake()
    {
        weapons = new List<Weapon>();
        _powerUpManager = powerUpManagerGo.GetComponent<PowerUpManager>();
        maxLife = 5;
        lifeCount = maxLife;
        killCount = 0;
        totalKillCount = 0;
        hasWeapon = false;
        powerUpList = new List<PowerUp>();
        angle = 0;
        speed = 0;
        range = 0;
        size = 0;
        damage = 0;
        attackSpeed = 0;
        bulletGlace = 0;
        bulletFeu = 0;
        bulletElec = 0;
        xp = 0;
        nextXp = 2;
        lootQte = 0;
        questIsDone = false;
    }

    void Start()
    {
        updateKillCountUI();
        addWeapon(new Weapon("pistol")); // Premiere arme
        List<PowerUpState> powerUpStates = PowerUpStateManager.getPowerUps();
        foreach (PowerUpState p in powerUpStates)
        {
            switch (p.id)
            {
                case "life":
                    lifeCount += 1;
                    break;
                case "range":
                    range += 1;
                    break;
                case "speed":
                    speed += 1;
                    break;
                case "damage":
                    damage += 1;
                    break;
                case "attackSpeed":
                    attackSpeed += 0.1f;
                    break;
            }
        }

        PowerUpStateManager.clear();

    }

    public void FixedUpdate()
    {
        float lifeProgress = lifeCount / (float)maxLife;
        fillBarLife.GetComponent<Image>().fillAmount = lifeProgress;

        if (isScenario)
        {
            return; // Pas d'xp en scenario
        }

        float xpProgress = xp / (float)nextXp;
        fillBarXp.GetComponent<Image>().fillAmount = xpProgress;



        if (xp >= nextXp)
        {
            // Level up
            // Stopper le jeu 
            gameplayRootGo.SetActive(false);
            _powerUpManager.isActive = true; // Pour la coroutines de waves
            _powerUpManager._playerController.joystickUiGo.SetActive(false); // On cache le joystick
            _powerUpManager.iconPauseGoWatch.SetActive(false); // On cache le btn pause sur watch
            _powerUpManager.iconPauseGoPhone.SetActive(false); // On cache le btn pause sur tel

            // generation des powerUp
            _powerUpManager.generateTmpPowerUp(false);

            Sprite spriteOne = Resources.Load<Sprite>("PowerUps/" + _powerUpManager.tmpPowerUpOne.type);
            Sprite spriteTwo = Resources.Load<Sprite>("PowerUps/" + _powerUpManager.tmpPowerUpTwo.type);

            // Afficher l'ui du shop
            if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
            {

                panelPowerUpGoWatch.SetActive(true);
                imagePowerUpOneGoWatch.GetComponent<Image>().sprite = spriteOne;
                imagePowerUpTwoGoWatch.GetComponent<Image>().sprite = spriteTwo;
                textPowerUpTitleOneWatch.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpOne.title;
                textPowerUpTitleTwoWatch.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpTwo.title;
            }
            else
            {
                panelPowerUpGoPhone.SetActive(true);
                imagePowerUpOneGoPhone.GetComponent<Image>().sprite = spriteOne;
                imagePowerUpTwoGoPhone.GetComponent<Image>().sprite = spriteTwo;
                textPowerUpTitleOnePhone.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpOne.title;
                textPowerUpTitleTwoPhone.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpTwo.title;
                textPowerUpDescriptionOnePhone.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpOne.description;
                textPowerUpDescriptionTwoPhone.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpTwo.description;
            }

            // Nouveau niveau d'xp
            xp = 0;
            nextXp = nextXp * 2;
        }

        // Manage quests
        //manageQuest();
    }

    public void updateKillCountUI()
    {
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            killCountUiTextWatch.GetComponent<TextMeshProUGUI>().text = killCount.ToString();
        }
        else
        {
            killCountUiTextPhone.GetComponent<TextMeshProUGUI>().text = killCount.ToString();
        }

    }

    public void addWeapon(Weapon newWeapon)
    {
        Weapon newWeaponClone = newWeapon.Clone();
        newWeaponClone.inventory = this;
        weapons.Add(newWeaponClone);
        hasWeapon = true;
    }

    public void managePowerUp()
    {
        foreach (PowerUp powerUp in powerUpList)
        {
            switch (powerUp.type)
            {
                case "angle":
                    angle = powerUp.lvl + 1; // A 1 on tire 2 balles
                    break;
                case "speed":
                    speed = powerUp.lvl;
                    break;
                case "range":
                    range = powerUp.lvl;
                    break;
                case "size":
                    size = powerUp.lvl;
                    break;
                case "life":
                    lifeCount += 1; // Ajoute une vie
                    break;
                case "damage":
                    damage += powerUp.lvl;
                    break;
                case "attackSpeed":
                    attackSpeed += powerUp.lvl;
                    break;
                case "bulletGlace":
                    bulletGlace += powerUp.lvl;
                    break;
                case "bulletFeu":
                    bulletFeu += powerUp.lvl;
                    break;
                case "bulletElec":
                    bulletElec += powerUp.lvl;
                    break;
            }
        }
    }

    /* private bool questOne = false;
     private bool questTwo = false;
     private bool questThree = false;
     private bool questFour = false;
     private bool questFive = false;
     private bool questSix = false;
     private bool questSeven = false;
     private bool questEight = false;
     private bool questNine = false;

     public GameObject openDoorParking;
     public GameObject openDoorForest;
     public GameObject openDoorPublicDump;
     public GameObject openDoorCity;
     public GameObject openDoorCity2;*/

    public void manageQuest()
    {
        /* QuestState qs = QuestManager.getCurrentQuest();
         if (!questIsDone && "1" == qs.id && !questOne && killCount >= 100 && lootQte >= 5 && !openDoorParking.activeSelf)
         {
             QuestManager.CompleteQuest("1");
             questOne = true;
             questIsDone = true;
             DisplayNotification();
         }
         else if (!questIsDone && "2" == qs.id && !questTwo && killCount >= 200 && lootQte >= 5 && !openDoorForest.activeSelf)
         {
             QuestManager.CompleteQuest("2");
             questTwo = true;
             questIsDone = true;
             DisplayNotification();
         }
         else if (!questIsDone && "3" == qs.id && !questThree && killCount >= 300 && lootQte >= 5 && !openDoorPublicDump.activeSelf)
         {
             QuestManager.CompleteQuest("3");
             questThree = true;
             questIsDone = true;
             DisplayNotification();
         }
         else if (!questIsDone && "4" == qs.id && !questFour && killCount >= 400 && lootQte >= 5 && !openDoorParking.activeSelf)
         {
             QuestManager.CompleteQuest("4");
             questFour = true;
             questIsDone = true;
             DisplayNotification();
         }
         else if (!questIsDone && "5" == qs.id && !questFive && killCount >= 500 && lootQte >= 5 && (!openDoorCity.activeSelf || !openDoorCity2.activeSelf))
         {
             QuestManager.CompleteQuest("5");
             questFive = true;
             questIsDone = true;
             DisplayNotification();
         }
         else if (!questIsDone && "6" == qs.id && !questSix && killCount >= 600 && lootQte >= 5 && !openDoorForest.activeSelf)
         {
             QuestManager.CompleteQuest("6");
             questSix = true;
             questIsDone = true;
             DisplayNotification();
         }
         else if (!questIsDone && "7" == qs.id && !questSeven && killCount >= 700 && lootQte >= 5 && !openDoorPublicDump.activeSelf)
         {
             QuestManager.CompleteQuest("7");
             questSeven = true;
             questIsDone = true;
             DisplayNotification();
         }
         else if (!questIsDone && "8" == qs.id && !questEight && killCount >= 800 && lootQte >= 10 && (!openDoorCity.activeSelf || !openDoorCity2.activeSelf))
         {
             QuestManager.CompleteQuest("8");
             questEight = true;
             questIsDone = true;
             DisplayNotification();
         }
         else if (!questIsDone && "9" == qs.id && !questNine && killCount >= 1000 && lootQte >= 10 && (!openDoorCity.activeSelf || !openDoorCity2.activeSelf))
         {
             QuestManager.CompleteQuest("9");
             questNine = true;
             questIsDone = true;
             DisplayNotification();
         }*/
    }

    private void DisplayNotification()
    {
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            questUiGoWatch.SetActive(true);
            textQuestUiGoWatch.GetComponent<TextMeshProUGUI>().text = "Quest done !";
            textQuestUiGoWatch.SetActive(true);
        }
        else
        {
            questUiGoPhone.SetActive(true);
            textQuestUiGoPhone.GetComponent<TextMeshProUGUI>().text = "Quest done !";
            textQuestUiGoPhone.SetActive(true);
        }
        StartCoroutine(hideQuestNotification());
    }

    IEnumerator hideQuestNotification()
    {
        yield return new WaitForSeconds(3f);
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            questUiGoWatch.SetActive(false);
            textQuestUiGoWatch.SetActive(false);
        }
        else
        {
            questUiGoPhone.SetActive(false);
            textQuestUiGoPhone.SetActive(false);
        }
    }
}
