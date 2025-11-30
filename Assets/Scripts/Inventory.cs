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

    public GameObject weaponPlayerPos1;
    public GameObject weaponPlayerPos2;
    public GameObject weaponPlayerPos3;
    public GameObject weaponPlayerPos4;

    public bool fullWeapon = false;

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
        addWeapon(new Weapon("pistol")); // Premiere arme
        addWeapon(new Weapon("pistol")); // Premiere arme
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
            if (weapons.Count == 4)
            {
                _powerUpManager.generateTmpPowerUp(false, true);
            }
            else
            {
                _powerUpManager.generateTmpPowerUp(false, false);
            }

            string textDescriptionTmpPowerUpOne = "";
            string textDescriptionTmpPowerUpTwo = "";
            bool isEvolOne = false;
            bool isEvolTwo = false;
            // Check des proposition
            // Proposition 1
            if (_powerUpManager.tmpPowerUpOne.isWeapon)
            {
                // On check le cas ARME
                if (weapons.Count == 4)
                {
                    // Afficher alors la description d'evolution
                    textDescriptionTmpPowerUpOne = _powerUpManager.tmpPowerUpOne.descriptionEvol;
                    Weapon foundWeapon = null;
                    foreach (Weapon w in weapons)
                    {
                        if (w._name == _powerUpManager.tmpPowerUpOne.type)
                        {
                            foundWeapon = w;
                            break;
                        }
                    }
                    if (foundWeapon.lvl == 4)
                    {
                        isEvolOne = true;
                    }
                }
                else
                {
                    // Afficher alors l'ajout de l'arme
                    textDescriptionTmpPowerUpOne = _powerUpManager.tmpPowerUpOne.description;
                }
            }
            else
            {
                // On check le cas POWER UP standard
                if (alreadyHavePowerUp(_powerUpManager.tmpPowerUpOne))
                {
                    textDescriptionTmpPowerUpOne = _powerUpManager.tmpPowerUpOne.descriptionEvol;
                }
                else
                {
                    textDescriptionTmpPowerUpOne = _powerUpManager.tmpPowerUpOne.description;
                }
            }

            // Proposition 2
            if (_powerUpManager.tmpPowerUpTwo.isWeapon)
            {
                // On check le cas ARME
                if (weapons.Count == 4)
                {
                    // Afficher alors la description d'evolution
                    textDescriptionTmpPowerUpTwo = _powerUpManager.tmpPowerUpTwo.descriptionEvol;
                    Weapon foundWeapon = null;
                    foreach (Weapon w in weapons)
                    {
                        if (w._name == _powerUpManager.tmpPowerUpTwo.type)
                        {
                            foundWeapon = w;
                            break;
                        }
                    }
                    if (foundWeapon.lvl == 4)
                    {
                        isEvolTwo = true;
                    }

                }
                else
                {
                    // Afficher alors l'ajout de l'arme
                    textDescriptionTmpPowerUpTwo = _powerUpManager.tmpPowerUpTwo.description;
                }
            }
            else
            {
                // On check le cas POWER UP standard
                if (alreadyHavePowerUp(_powerUpManager.tmpPowerUpTwo))
                {
                    textDescriptionTmpPowerUpTwo = _powerUpManager.tmpPowerUpTwo.descriptionEvol;
                }
                else
                {
                    textDescriptionTmpPowerUpTwo = _powerUpManager.tmpPowerUpTwo.description;
                }
            }

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
                textPowerUpDescriptionOnePhone.GetComponent<TextMeshProUGUI>().text = textDescriptionTmpPowerUpOne;
                textPowerUpDescriptionTwoPhone.GetComponent<TextMeshProUGUI>().text = textDescriptionTmpPowerUpTwo;

                if (isEvolOne)
                {
                    // On affiche l'evolution de l'arme
                    imagePowerUpOneGoPhone.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerUps/" + _powerUpManager.tmpPowerUpOne.typeEvol);
                    textPowerUpTitleOnePhone.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpOne.titleHasEvol;
                    textPowerUpDescriptionOnePhone.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpOne.descriptionHasEvol;
                    for (int i = 0; i < weapons.Count; i++)
                    {
                        if (weapons[i]._name == _powerUpManager.tmpPowerUpOne.type)
                        {
                            Weapon newWeapon = new Weapon(_powerUpManager.tmpPowerUpOne.typeEvol);
                            newWeapon.pos = weapons[i].pos;
                            weapons[i] = newWeapon;
                            weapons[i].inventory = this;
                            switch (weapons[i].pos)
                            {
                                case 0:
                                    weaponPlayerPos1.GetComponent<SpriteRenderer>().sprite = weapons[i].weaponData.sprite;
                                    break;
                                case 1:
                                    weaponPlayerPos2.GetComponent<SpriteRenderer>().sprite = weapons[i].weaponData.sprite;
                                    break;
                                case 2:
                                    weaponPlayerPos3.GetComponent<SpriteRenderer>().sprite = weapons[i].weaponData.sprite;
                                    break;
                                case 3:
                                    weaponPlayerPos4.GetComponent<SpriteRenderer>().sprite = weapons[i].weaponData.sprite;
                                    break;
                            }
                        }
                    }
                }

                if (isEvolTwo)
                {
                    // On affiche l'evolution de l'arme
                    imagePowerUpTwoGoPhone.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerUps/" + _powerUpManager.tmpPowerUpTwo.typeEvol);
                    textPowerUpTitleTwoPhone.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpTwo.titleHasEvol;
                    textPowerUpDescriptionTwoPhone.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpTwo.descriptionHasEvol;
                    for (int i = 0; i < weapons.Count; i++)
                    {
                        if (weapons[i]._name == _powerUpManager.tmpPowerUpTwo.type)
                        {
                            Weapon newWeapon = new Weapon(_powerUpManager.tmpPowerUpTwo.typeEvol);
                            newWeapon.pos = weapons[i].pos;
                            weapons[i] = newWeapon;
                            weapons[i].inventory = this;
                            switch (weapons[i].pos)
                            {
                                case 0:
                                    weaponPlayerPos1.GetComponent<SpriteRenderer>().sprite = weapons[i].weaponData.sprite;
                                    break;
                                case 1:
                                    weaponPlayerPos2.GetComponent<SpriteRenderer>().sprite = weapons[i].weaponData.sprite;
                                    break;
                                case 2:
                                    weaponPlayerPos3.GetComponent<SpriteRenderer>().sprite = weapons[i].weaponData.sprite;
                                    break;
                                case 3:
                                    weaponPlayerPos4.GetComponent<SpriteRenderer>().sprite = weapons[i].weaponData.sprite;
                                    break;
                            }
                        }
                    }
                }
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
        if (weapons.Count == 0)
        {
            // Ajout 1er arme
            newWeaponClone.pos = 0;
            weapons.Add(newWeaponClone);
            hasWeapon = true;
            weaponPlayerPos1.GetComponent<SpriteRenderer>().sprite = newWeaponClone.weaponData.sprite;
        }
        else if (weapons.Count == 1)
        {
            // Ajout 2e arme
            newWeaponClone.pos = 1;
            weapons.Add(newWeaponClone);
            hasWeapon = true;
            weaponPlayerPos2.GetComponent<SpriteRenderer>().sprite = newWeaponClone.weaponData.sprite;
        }
        else if (weapons.Count == 2)
        {
            // Ajout 3e arme
            newWeaponClone.pos = 2;
            weapons.Add(newWeaponClone);
            hasWeapon = true;
            weaponPlayerPos3.GetComponent<SpriteRenderer>().sprite = newWeaponClone.weaponData.sprite;
        }
        else if (weapons.Count == 3)
        {
            // Ajout 4e arme
            newWeaponClone.pos = 3;
            weapons.Add(newWeaponClone);
            hasWeapon = true;
            weaponPlayerPos4.GetComponent<SpriteRenderer>().sprite = newWeaponClone.weaponData.sprite;
            fullWeapon = true;
        }
        else
        {
            // Upgrade weapon
            foreach (Weapon w in weapons)
            {
                if (w._name == newWeapon._name)
                {
                    w.lvl++;
                    if (w.lvl == 5)
                    {
                        // Evolution de l'arme -> Afficher meilleur arme -> et meme mettre a l'ecran une animation de flash qui fait votre arme evolue comme pokemon
                    }
                }
            }
            // POSSIBLE QU'A 5 MAX APRES GIGA EVOLUTION
            // Ajouter toute les armes
        }

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
                case "pistol":
                    addWeapon(new Weapon("pistol"));
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

    private bool alreadyHavePowerUp(PowerUp powerUp)
    {
        return false;
    }
}
