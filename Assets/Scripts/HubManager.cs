using System.Collections.Generic;
using Firebase.Analytics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{
    public GameObject panelBuyNarration;
    public GameObject panelBuy;
    public GameObject itemBuyPrefab;
    public Transform buyParent;
    private List<ItemBuy> itemsBuy = new List<ItemBuy>
    {
        new ItemBuy(1, 10, "Wood armor", "A little protection, give 1 more HP", "woodArmor", "armor"),
        new ItemBuy(2, 25, "MP5", "A high rate of fire", "subMachineGun", "weapon"),
        new ItemBuy(3, 25, "Leather armor", "A better protection, give 2 more HP", "leatherArmor", "armor"),
        new ItemBuy(4, 50, "Shotgun", "Very effective at short range", "shotgun", "weapon"),
        new ItemBuy(5, 50, "Copper armor", "A good protection, give 3 more HP", "cooperArmor", "armor"),
        new ItemBuy(6, 100, "M16", "Very powerful and accurate", "assaultRifle", "weapon"),
        new ItemBuy(7, 100, "Metal armor", "A very good protection, give 4 more HP", "metalArmor", "armor"),
        new ItemBuy(8, 200, "Grenade", "K - BOOM", "grenade", "weapon"),
        new ItemBuy(9, 200, "Diamond armor", "The best protection, give 5 more HP", "diamondArmor", "armor"),
    };

    public GameObject panelDetailItemBuy;
    public GameObject titleDetailItemBuy;
    public GameObject descriptionDetailItemBuy;
    public GameObject costDetailItemBuy;
    public GameObject imageDetailItemBuy;
    private ItemBuy _itemBuyTmp;
    public GameObject textCountLootGo;
    public GameObject panelGoOut;
    public GameObject panelInventory;

    public List<string> narrationTutoText = new List<string>()
    {
        "Well, dead again...",
        "Here I can upgrade my gear before I go try to survive outside again.",
        "Luckily I have some savings."
    };

    public GameObject panelNarrationTuto;

    public GameObject textNarrationTuto;

    public void Start()
    {
        updateStoreItem();
        updateLootCount();
        InventoryManagerState.AddItem(new ItemBuy(0, 0, "Pistol", "A simple pistol", "pistol", "weapon")); // Add Pistol has default weapon

        SaveData data = SaveSystem.GetData();

        if (!data.isTutoDone)
        {
            // Mise en place de l'equipement de base
            data.equipment.weapon = new ItemState(0, 0, "Pistol", "A simple pistol", "pistol", "weapon");

            FirebaseAnalytics.LogEvent("launch_tuto");
            var existingCanvas = FindFirstObjectByType<Canvas>();
            if (existingCanvas != null)
                Destroy(existingCanvas.gameObject);
            SceneManager.LoadScene(1);
        }
        else if (!data.isTutoHubDone)
        {
            FirebaseAnalytics.LogEvent("launch_tuto_hub");
            for (int i = 0; i < 10; i++)
            {
                LootManager.AddLoot();
            }
            updateLootCount();
            onClickNextNarrationTuto();
        }
    }

    public void onClickNextNarrationTuto()
    {
        if (narrationTutoText.Count > 0)
        {
            panelNarrationTuto.SetActive(true);
            textNarrationTuto.GetComponent<TextMeshProUGUI>().text = narrationTutoText[0];
            narrationTutoText.RemoveAt(0);
        }
        else
        {
            panelNarrationTuto.SetActive(false);
            // Le tuto est realise
            SaveData data = SaveSystem.GetData();
            data.isTutoHubDone = true;
            SaveSystem.Save(data);
        }
    }

    public void onClicNextNarration()
    {
        panelBuyNarration.SetActive(false);
        panelBuy.SetActive(true);
    }

    public void onBackBuyMenu()
    {
        panelBuy.SetActive(false);
    }

    public void addItemBuyToGrid(ItemBuy item)
    {
        if (!InventoryManagerState.ExistItem(item.id))
        {
            GameObject itemInstantiate = Instantiate(itemBuyPrefab, buyParent);
            ItemBuy itemBuyInstantiate = itemInstantiate.GetComponent<ItemBuy>();
            itemBuyInstantiate.id = item.id;
            itemBuyInstantiate.cost = item.cost;
            itemBuyInstantiate.description = item.description;
            itemBuyInstantiate.title = item.title;
            itemBuyInstantiate.spriteName = item.spriteName;
            itemBuyInstantiate.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + item.spriteName);
            itemBuyInstantiate.type = item.type;

            // Ajout de l'event onClick
            EventTrigger trigger = itemInstantiate.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = gameObject.AddComponent<EventTrigger>();

            // Crée une entrée pour un type d’événement
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;

            // Ajoute la méthode à appeler
            entry.callback.AddListener((data) => { OnClickItemBuy(itemBuyInstantiate); });

            // Ajoute l'entrée à l'EventTrigger
            trigger.triggers.Add(entry);
        }
    }

    public void OnClickItemBuy(ItemBuy itemBuy)
    {
        panelBuy.SetActive(false);
        panelDetailItemBuy.SetActive(true);
        titleDetailItemBuy.GetComponent<TextMeshProUGUI>().text = itemBuy.title;
        descriptionDetailItemBuy.GetComponent<TextMeshProUGUI>().text = itemBuy.description;
        costDetailItemBuy.GetComponent<TextMeshProUGUI>().text = "Cost : " + itemBuy.cost.ToString();
        imageDetailItemBuy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + itemBuy.spriteName);
        _itemBuyTmp = itemBuy;
    }

    public void OnBackDetailItemBuy()
    {
        panelBuy.SetActive(true);
        panelDetailItemBuy.SetActive(false);
    }

    public void OnBuyDetailItemBuy()
    {
        if (LootManager.getLoots() >= _itemBuyTmp.cost)
        {
            FirebaseAnalytics.LogEvent("buy_item", new Parameter("item", _itemBuyTmp.title));
            LootManager.subLoot(_itemBuyTmp.cost);
            updateLootCount();
            InventoryManagerState.AddItem(_itemBuyTmp);
            panelBuy.SetActive(true);
            panelDetailItemBuy.SetActive(false);
            updateStoreItem();
        }
    }

    public void updateLootCount()
    {
        textCountLootGo.GetComponent<TextMeshProUGUI>().text = LootManager.getLoots().ToString();
    }

    public void OnNoGoOutMenu()
    {
        panelGoOut.SetActive(false);
    }

    public void OnBackGoOut()
    {
        panelGoOut.SetActive(false);
    }

    public void DoGoOut()
    {
        panelGoOut.SetActive(false);
        var existingCanvas = FindFirstObjectByType<Canvas>();
        FirebaseAnalytics.LogEvent("game_started", new Parameter("level", StoreDataScene.currentMap));
        if (existingCanvas != null)
            Destroy(existingCanvas.gameObject);
        SceneManager.LoadScene(2);
    }

    public void OnLvlOne()
    {
        StoreDataScene.currentMap = "mapOne";
        DoGoOut();
    }

    public void OnLvlTwo()
    {
        StoreDataScene.currentMap = "mapTwo";
        DoGoOut();
    }

    public void OnLvlThree()
    {
        StoreDataScene.currentMap = "mapThree";
        DoGoOut();
    }

    private void updateStoreItem()
    {
        foreach (Transform child in buyParent)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemBuy item in itemsBuy)
        {
            addItemBuyToGrid(item);
        }

    }
}

