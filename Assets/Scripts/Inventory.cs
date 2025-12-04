using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public GameObject fromEvolutionOne;
    public GameObject arrowEvolutionOne;
    public GameObject afterEvolutionOne;

    public GameObject fromEvolutionTwo;
    public GameObject arrowEvolutionTwo;
    public GameObject afterEvolutionTwo;

    public GameObject starOneOne;
    public GameObject starTwoOne;
    public GameObject starThreeOne;
    public GameObject starFourOne;

    public GameObject starOneTwo;
    public GameObject starTwoTwo;
    public GameObject starThreeTwo;
    public GameObject starFourTwo;

    public Coroutine tmpCoroutineStar;

    public GameObject roundManagerGo;
    private RoundManager _roundManager;
    public GameObject arenaManagerGo;
    private ArenaManager _arenaManager;

    void Awake()
    {
        weapons = new List<Weapon>();
        _powerUpManager = powerUpManagerGo.GetComponent<PowerUpManager>();
        _roundManager = roundManagerGo.GetComponent<RoundManager>();
        _arenaManager = arenaManagerGo.GetComponent<ArenaManager>();
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
        nextXp = 5;
        lootQte = 0;
        questIsDone = false;
    }

    void Start()
    {
        updateKillCountUI();
        addWeapon(new Weapon("pistol"), null); // Premiere arme
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
            int lvlOne = 0;
            int lvlTwo = 0;
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
                    else
                    {
                        lvlOne = foundWeapon.lvl;
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
                    else
                    {
                        lvlTwo = foundWeapon.lvl;
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
                fromEvolutionOne.SetActive(false);
                arrowEvolutionOne.SetActive(false);
                afterEvolutionOne.SetActive(false);
                fromEvolutionTwo.SetActive(false);
                arrowEvolutionTwo.SetActive(false);
                afterEvolutionTwo.SetActive(false);

                // Affichage des stars
                if (lvlOne == 1)
                {
                    starOneOne.SetActive(true);
                    starTwoOne.SetActive(true);
                    tmpCoroutineStar = StartCoroutine(Blink(starTwoOne.GetComponent<Image>(), 1f));
                }
                else if (lvlOne == 2)
                {
                    starOneOne.SetActive(true);
                    starTwoOne.SetActive(true);
                    starThreeOne.SetActive(true);
                    tmpCoroutineStar = StartCoroutine(Blink(starThreeOne.GetComponent<Image>(), 1f));
                }
                else if (lvlOne == 3)
                {
                    starOneOne.SetActive(true);
                    starTwoOne.SetActive(true);
                    starThreeOne.SetActive(true);
                    starFourOne.SetActive(true);
                    tmpCoroutineStar = StartCoroutine(Blink(starFourOne.GetComponent<Image>(), 1f));
                }
                else
                {
                    starOneOne.SetActive(false);
                    starTwoOne.SetActive(false);
                    starThreeOne.SetActive(false);
                    starFourOne.SetActive(false);
                }

                if (lvlTwo == 1)
                {
                    starOneTwo.SetActive(true);
                    starTwoTwo.SetActive(true);
                    tmpCoroutineStar = StartCoroutine(Blink(starTwoTwo.GetComponent<Image>(), 1f));
                }
                else if (lvlTwo == 2)
                {
                    starOneTwo.SetActive(true);
                    starTwoTwo.SetActive(true);
                    starThreeTwo.SetActive(true);
                    tmpCoroutineStar = StartCoroutine(Blink(starThreeTwo.GetComponent<Image>(), 1f));
                }
                else if (lvlTwo == 3)
                {
                    starOneTwo.SetActive(true);
                    starTwoTwo.SetActive(true);
                    starThreeTwo.SetActive(true);
                    starFourTwo.SetActive(true);
                    tmpCoroutineStar = StartCoroutine(Blink(starFourTwo.GetComponent<Image>(), 1f));
                }
                else
                {
                    starOneTwo.SetActive(false);
                    starTwoTwo.SetActive(false);
                    starThreeTwo.SetActive(false);
                    starFourTwo.SetActive(false);
                }

                if (isEvolOne)
                {
                    // On affiche l'evolution de l'arme
                    imagePowerUpOneGoPhone.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerUps/" + _powerUpManager.tmpPowerUpOne.typeEvol);
                    textPowerUpTitleOnePhone.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpOne.titleHasEvol;
                    textPowerUpDescriptionOnePhone.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpOne.descriptionHasEvol;
                    fromEvolutionOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerUps/" + _powerUpManager.tmpPowerUpOne.type);
                    afterEvolutionOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerUps/" + _powerUpManager.tmpPowerUpOne.typeEvol);
                    fromEvolutionOne.SetActive(true);
                    arrowEvolutionOne.SetActive(true);
                    afterEvolutionOne.SetActive(true);
                }

                if (isEvolTwo)
                {
                    // On affiche l'evolution de l'arme
                    imagePowerUpTwoGoPhone.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerUps/" + _powerUpManager.tmpPowerUpTwo.typeEvol);
                    textPowerUpTitleTwoPhone.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpTwo.titleHasEvol;
                    textPowerUpDescriptionTwoPhone.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpTwo.descriptionHasEvol;
                    fromEvolutionTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerUps/" + _powerUpManager.tmpPowerUpTwo.type);
                    afterEvolutionTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerUps/" + _powerUpManager.tmpPowerUpTwo.typeEvol);
                    fromEvolutionTwo.SetActive(true);
                    arrowEvolutionTwo.SetActive(true);
                    afterEvolutionTwo.SetActive(true);
                }
            }

            // Nouveau niveau d'xp
            xp = 0;
            nextXp = nextXp + 5;
        }

        // Manage quests
        manageQuest();
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

    public void addWeapon(Weapon newWeapon, PowerUp powerUp)
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
            for (int j = 0; j < weapons.Count; j++)
            {
                if (weapons[j]._name == newWeapon._name)
                {
                    weapons[j].lvl++;
                    if (weapons[j].lvl == 5)
                    {
                        // Evolution de l'arme -> Afficher meilleur arme -> et meme mettre a l'ecran une animation de flash qui fait votre arme evolue comme pokemon
                        for (int i = 0; i < weapons.Count; i++)
                        {
                            if (weapons[i]._name == powerUp.type)
                            {
                                Weapon newWeaponEvol = new Weapon(powerUp.typeEvol);
                                newWeaponEvol.pos = weapons[i].pos;
                                weapons[i] = newWeaponEvol;
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
                    addWeapon(new Weapon("pistol"), powerUp);
                    break;
                case "shotgun":
                    addWeapon(new Weapon("shotgun"), powerUp);
                    break;
                case "subMachineGun":
                    addWeapon(new Weapon("subMachineGun"), powerUp);
                    break;
                case "assaultRifle":
                    addWeapon(new Weapon("assaultRifle"), powerUp);
                    break;
                case "grenade":
                    addWeapon(new Weapon("grenade"), powerUp);
                    break;
            }
        }

        powerUpList.Clear();
    }

    private bool questOne = false;
    private bool questTwo = false;
    private bool questThree = false;

    public void manageQuest()
    {
        List<QuestState> qs = QuestManager.GetThreeQuestToDo();
        if (!questOne)
        {
            if (qs.Any(w => w.id == "1"))
            {
                if (killCount >= 1000)
                {
                    QuestManager.CompleteQuest("1");
                    questOne = true;
                    DisplayNotification();
                }
            }
        }

        if (!questTwo)
        {
            if (qs.Any(w => w.id == "2"))
            {
                if (_roundManager.isMapFinish && StoreDataScene.currentMap == "mapOne")
                {
                    QuestManager.CompleteQuest("2");
                    questTwo = true;
                    DisplayNotification();
                }
            }
        }

        if (!questThree)
        {
            if (qs.Any(w => w.id == "3"))
            {
                if (_arenaManager.nbArenaDone == _arenaManager.nbMaxArena && StoreDataScene.currentMap == "mapOne")
                {
                    QuestManager.CompleteQuest("3");
                    questThree = true;
                    DisplayNotification();
                }
            }
        }
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

    IEnumerator Blink(Image img, float speedBlink)
    {
        while (true)
        {
            // on fait osciller l’alpha entre 0 et 1 avec Mathf.PingPong
            float a = Mathf.PingPong(Time.time * speedBlink, 1f);

            Color c = img.color;
            c.a = a;
            img.color = c;

            yield return null;
        }
    }

    public void StopBlink()
    {
        if (tmpCoroutineStar != null)
        {
            StopCoroutine(tmpCoroutineStar);
            tmpCoroutineStar = null;
        }
    }
    public void ApplyBonus(Bonus bonus)
    {
        switch (bonus.type)
        {
            case "speed":
                speed += 2;
                // Réinitialiser après 10 sec
                Invoke("ResetBonus", 10f);
                break;
            case "life":
                lifeCount = maxLife;
                break;
            case "attackSpeed":
                attackSpeed += 2;
                // Réinitialiser après 10 sec
                Invoke("ResetBonus", 10f);
                break;
        }
    }

    private void ResetBonus()
    {
        speed = 0;
        attackSpeed = 0;
    }
}
