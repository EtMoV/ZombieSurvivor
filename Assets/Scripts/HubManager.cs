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
        new ItemBuy(1, 20, "MP5", "A high rate of fire", "subMachineGun", "weapon"),
        new ItemBuy(2, 10, "Leather armor", "A little protection, give 1 more HP", "leatherArmor", "armor")
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

    public void Start()
    {
        updateStoreItem();
        updateLootCount();
        InventoryManagerState.AddItem(new ItemBuy(0, 0, "Pistol", "A simple pistol", "pistol", "weapon")); // Add Pistol has default weapon
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

    public void OnYesGoOutMenu()
    {
        panelGoOut.SetActive(false);
        var existingCanvas = FindFirstObjectByType<Canvas>();
        StoreDataScene.currentMap = "mapOne";
        FirebaseAnalytics.LogEvent("game_started", new Parameter("level", StoreDataScene.currentMap));
        if (existingCanvas != null)
            Destroy(existingCanvas.gameObject);
        SceneManager.LoadScene(1);
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

