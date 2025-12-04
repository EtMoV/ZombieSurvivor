using System.Collections.Generic;
using System.Linq;
using Firebase.Analytics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject panelMenuWatch;
    public GameObject panelMenuPhone;
    public GameObject panelQuestsWatch;
    public GameObject panelQuestsPhone;
    public GameObject questTextOneWatch;
    public GameObject questTextOnePhone;
    public GameObject questTextTwoWatch;
    public GameObject questTextTwoPhone;
    public GameObject questTextThreeWatch;
    public GameObject questTextThreePhone;

    public GameObject panelPrestigeMenuWatch;
    public GameObject panelPrestigeMenuPhone;
    public GameObject panelSchemaMenuWatch;
    public GameObject panelSchemaMenuPhone;

    private string tempSchemaId;

    public GameObject textSchemaGoWatch;
    public GameObject textSchemaGoPhone;
    public GameObject imageSchemaGoWatch;
    public GameObject imageSchemaGoPhone;

    public GameObject textBeforePrestigeGoWatch;
    public GameObject textBeforePrestigeGoPhone;
    public GameObject textAfterPrestigeGoWatch;
    public GameObject textAfterPrestigeGoPhone;

    public GameObject panelFabricationWatch;
    public GameObject panelFabricationPhone;
    public GameObject panelResultFabricationWatch;
    public GameObject panelResultFabricationPhone;

    public GameObject imageResultFabricationWatch;
    public GameObject titleResultFabricationWatch;
    public GameObject descriptionResultFabricationWatch;
    public GameObject imageResultFabricationPhone;
    public GameObject titleResultFabricationPhone;
    public GameObject descriptionResultFabricationPhone;

    public GameObject btnCreateFabricationWatch;
    public GameObject btnCreateFabricationPhone;

    public GameObject mapGo;
    public GameObject titleMapGo;

    private MapZombie currentMapZombie;
    private int currentIndexMapZombie;

    private List<MapZombie> mapsZombie = new List<MapZombie>
    {
      new MapZombie("The sheriff's small town", "mapOne"),
      new MapZombie("Stay tuned !", "mapStayTuned")
    };

    public List<PowerUpState> possiblePowerUp = new List<PowerUpState>
        {
            new PowerUpState("speed", "speed", "Increase speed of player"),
            new PowerUpState("range", "range", "Increase range of weapon"),
            new PowerUpState("life", "life", "Can take ONE hit"),
            new PowerUpState("damage", "damage", "Increase damage of weapon"),
            new PowerUpState("attackSpeed", "attack Speed", "Increase attack speed of weapon"),
        };

    public GameObject menuGo;

    public GameObject storyMenuGo;

    public void Start()
    {
        // Gestion du menu fight par defaut
        currentIndexMapZombie = 0;
        currentMapZombie = mapsZombie[currentIndexMapZombie];
        onChangeMap(0); // Pour mettre a jour l'ui

        // Gestion des quetes
        /*QuestState questState = QuestManager.getLastCompletedQuest();
        if (questState != null)
        {
            tempSchemaId = questState.schemaReward;
            if (!SchemaManager.SchemaExists(tempSchemaId))
            {
                if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
                {
                    // On affiche le panel de prestige car un nouveau schema a ete debloque
                    panelMenuWatch.SetActive(false);
                    panelPrestigeMenuWatch.SetActive(true);
                    // Affichage du prestige
                    textBeforePrestigeGoWatch.GetComponent<TextMeshProUGUI>().text = PrestigeManager.getPrestige().ToString();
                    textAfterPrestigeGoWatch.GetComponent<TextMeshProUGUI>().text = (PrestigeManager.getPrestige() + 1).ToString();
                }
                else
                {
                    // On affiche le panel de prestige car un nouveau schema a ete debloque
                    panelMenuPhone.SetActive(false);
                    panelPrestigeMenuPhone.SetActive(true);
                    // Affichage du prestige
                    textBeforePrestigeGoPhone.GetComponent<TextMeshProUGUI>().text = PrestigeManager.getPrestige().ToString();
                    textAfterPrestigeGoPhone.GetComponent<TextMeshProUGUI>().text = (PrestigeManager.getPrestige() + 1).ToString();
                }

            }
        }*/
    }

    public void onPlay()
    {
        var existingCanvas = FindFirstObjectByType<Canvas>();
        if (currentIndexMapZombie == 0)
        {
            FirebaseAnalytics.LogEvent("game_started", new Parameter("level", 1));
            if (existingCanvas != null)
                Destroy(existingCanvas.gameObject);
            SceneManager.LoadScene(1);
        }
    }

    public void onQuests()
    {
        List<QuestState> threeQuestFree = new List<QuestState>();

        for (int i = 0; i < QuestManager.questsInit.Count; i++)
        {
            bool questCompleted = QuestManager.IsQuestCompleted(QuestManager.questsInit[i].id);
            if (!questCompleted)
            {
                QuestState questState = QuestManager.getQuestById(QuestManager.questsInit[i].id);
                if (questState == null)
                    threeQuestFree.Add(QuestManager.questsInit[i]);
                else
                    threeQuestFree.Add(questState);
            }

            if (threeQuestFree.Count == 3)
            {
                break;
            }
        }

        panelMenuPhone.SetActive(false);
        panelQuestsPhone.SetActive(true);
        if (threeQuestFree.Count > 0)
        {
            questTextOnePhone.GetComponent<TextMeshProUGUI>().text = threeQuestFree[0].descriptionOne;
        }
        else
        {
            questTextOnePhone.SetActive(false);
        }
        if (threeQuestFree.Count > 1)
        {
            questTextTwoPhone.GetComponent<TextMeshProUGUI>().text = threeQuestFree[1].descriptionOne;
        }
        else
        {
            questTextTwoPhone.SetActive(false);
        }
        if (threeQuestFree.Count > 2)
        {
            questTextThreePhone.GetComponent<TextMeshProUGUI>().text = threeQuestFree[2].descriptionOne;
        }
        else
        {
            questTextThreePhone.SetActive(false);
        }

    }

    public void onYeah()
    {
        QuestState questState = QuestManager.getLastCompletedQuest();
        PrestigeManager.increasePrestige(); // On incremente le prestige

        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            panelPrestigeMenuWatch.SetActive(false);
            panelSchemaMenuWatch.SetActive(true);
            // Affichage des bon elements a l'ecran
            textSchemaGoWatch.GetComponent<TextMeshProUGUI>().text = questState.schemaReward;
            imageSchemaGoWatch.GetComponent<Image>().sprite = Resources.Load<Sprite>(questState.schemaReward + "Sprite");
        }
        else
        {
            panelPrestigeMenuPhone.SetActive(false);
            panelSchemaMenuPhone.SetActive(true);
            // Affichage des bon elements a l'ecran
            textSchemaGoPhone.GetComponent<TextMeshProUGUI>().text = questState.schemaReward;
            imageSchemaGoPhone.GetComponent<Image>().sprite = Resources.Load<Sprite>(questState.schemaReward + "Sprite");
        }
    }

    public void onGreat()
    {
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            panelSchemaMenuWatch.SetActive(false);
            panelMenuWatch.SetActive(true);
        }
        else
        {
            panelSchemaMenuPhone.SetActive(false);
            panelMenuPhone.SetActive(true);
        }

        // Add Schema to inventory physic SaveData
        SchemaManager.addSchema(tempSchemaId);
    }

    public void onMenu()
    {
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            panelMenuWatch.SetActive(true);
            panelQuestsWatch.SetActive(false);
        }
        else
        {
            panelMenuPhone.SetActive(true);
            panelQuestsPhone.SetActive(false);
        }
    }

    public void onFabrication()
    {
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            panelMenuWatch.SetActive(false);
            panelFabricationWatch.SetActive(true);
            int countLoot = LootManager.getLoots();
            if (countLoot < 5)
            {
                btnCreateFabricationWatch.GetComponent<Image>().color = Color.gray;
            }
            else
            {
                btnCreateFabricationWatch.GetComponent<Image>().color = new Color(0f, 0.858f, 0.016f);
            }
        }
        else
        {
            panelMenuPhone.SetActive(false);
            panelFabricationPhone.SetActive(true);
            int countLoot = LootManager.getLoots();
            if (countLoot < 5)
            {
                btnCreateFabricationPhone.GetComponent<Image>().color = Color.gray;
            }
            else
            {
                btnCreateFabricationPhone.GetComponent<Image>().color = new Color(0f, 0.858f, 0.016f);
            }
        }
    }

    public void onBackFromFabrication()
    {
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            panelMenuWatch.SetActive(true);
            panelFabricationWatch.SetActive(false);
        }
        else
        {
            panelMenuPhone.SetActive(true);
            panelFabricationPhone.SetActive(false);
        }
    }

    public void onCreateFabrication()
    {
        int countLoot = LootManager.getLoots();
        if (countLoot >= 5)
        {
            PowerUpState powerUpState = generateFabrication();
            LootManager.subLootFive();

            if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
            {
                // Set up des elements ui
                imageResultFabricationWatch.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerUps/" + powerUpState.id);
                titleResultFabricationWatch.GetComponent<TextMeshProUGUI>().text = powerUpState.title;
                descriptionResultFabricationWatch.GetComponent<TextMeshProUGUI>().text = powerUpState.description;

                // Activation de l'affichage
                panelFabricationWatch.SetActive(false);
                panelResultFabricationWatch.SetActive(true);
            }
            else
            {
                // Set up des elements ui
                imageResultFabricationPhone.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerUps/" + powerUpState.id);
                titleResultFabricationPhone.GetComponent<TextMeshProUGUI>().text = powerUpState.title;
                descriptionResultFabricationPhone.GetComponent<TextMeshProUGUI>().text = powerUpState.description;

                // Activation de l'affichage
                panelFabricationPhone.SetActive(false);
                panelResultFabricationPhone.SetActive(true);
            }
        }
    }

    public void onGreatFabrication()
    {
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            panelFabricationWatch.SetActive(true);
            panelResultFabricationWatch.SetActive(false);
            int countLoot = LootManager.getLoots();
            if (countLoot < 5)
            {
                btnCreateFabricationWatch.GetComponent<Image>().color = Color.gray;
            }
            else
            {
                btnCreateFabricationWatch.GetComponent<Image>().color = new Color(0f, 0.858f, 0.016f);
            }
        }
        else
        {
            panelFabricationPhone.SetActive(true);
            panelResultFabricationPhone.SetActive(false);
            int countLoot = LootManager.getLoots();
            if (countLoot < 5)
            {
                btnCreateFabricationPhone.GetComponent<Image>().color = Color.gray;
            }
            else
            {
                btnCreateFabricationPhone.GetComponent<Image>().color = new Color(0f, 0.858f, 0.016f);
            }
        }
    }

    private PowerUpState generateFabrication()
    {
        // Generate a PowerState
        PowerUpState randomPowerUp = GetRandomPowerUp();

        // Save to data
        PowerUpStateManager.AddPowerUp(randomPowerUp);

        // Return
        return randomPowerUp;
    }

    private PowerUpState GetRandomPowerUp()
    {
        int randomIndex = Random.Range(0, possiblePowerUp.Count);
        return possiblePowerUp[randomIndex].Clone();
    }

    public void launchEpisodeOne()
    {
        var existingCanvas = FindFirstObjectByType<Canvas>();
        if (existingCanvas != null)
            Destroy(existingCanvas.gameObject);
        SceneManager.LoadScene(2);
    }

    public void onStoryMode()
    {
        menuGo.SetActive(false);
        storyMenuGo.SetActive(true);
    }

    public void onBackMenu()
    {
        menuGo.SetActive(true);
        storyMenuGo.SetActive(false);
    }

    public void onJoinDiscord()
    {
        Application.OpenURL("https://discord.gg/37s6ujvcPY");
    }

    public void onLeftChangeMap()
    {
        onChangeMap(-1);
    }

    public void onRightChangeMap()
    {
        onChangeMap(1);
    }

    private void onChangeMap(int changePos)
    {
        if (currentIndexMapZombie == 0 && changePos == -1)
        {
            return; // On est au max a gauche
        }


        if (currentIndexMapZombie == mapsZombie.Count - 1 && changePos == 1)
        {
            return; // On est au max a droite
        }

        mapGo.SetActive(true);
        currentIndexMapZombie += changePos;
        currentMapZombie = mapsZombie[currentIndexMapZombie];
        mapGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Maps/" + currentMapZombie.pathImage);
        titleMapGo.GetComponent<TextMeshProUGUI>().text = currentMapZombie.title;
        if (currentIndexMapZombie == mapsZombie.Count - 1)
        {
            mapGo.SetActive(false);
        }
    }
}
