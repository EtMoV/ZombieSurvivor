using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArmoryInput : MonoBehaviour
{
    public GameObject panelInventory;

    public GameObject lootPanel;

    public Transform inventoryParent;

    public GameObject itemPrefab;

    public GameObject panelDetailItem;
    public GameObject titleDetailItemBuy;
    public GameObject descriptionDetailItemBuy;
    public GameObject imageDetailItemBuy;
    private ItemState _itemTmp;

    public GameObject itemWeaponGo;
    public GameObject itemArmorGo;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpdateEquipment();

            panelInventory.SetActive(true);
            lootPanel.SetActive(false);
            // On nettoie la grid
            foreach (Transform child in inventoryParent)
            {
                Destroy(child.gameObject);
            }

            // On ajoute tout les items de l'inventaire
            foreach (ItemState item in InventoryManagerState.getItems())
            {
                addItemToGrid(item);
            }
        }
    }

    public void addItemToGrid(ItemState item)
    {
        GameObject itemInstantiate = Instantiate(itemPrefab, inventoryParent);
        ItemBuy itemBuyInstantiate = itemInstantiate.GetComponent<ItemBuy>();
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
        entry.callback.AddListener((data) => { OnClickItem(itemBuyInstantiate); });

        // Ajoute l'entrée à l'EventTrigger
        trigger.triggers.Add(entry);
    }

    public void OnClickItem(ItemBuy item)
    {
        panelInventory.SetActive(false);
        panelDetailItem.SetActive(true);
        titleDetailItemBuy.GetComponent<TextMeshProUGUI>().text = item.title;
        descriptionDetailItemBuy.GetComponent<TextMeshProUGUI>().text = item.description;
        imageDetailItemBuy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + item.spriteName);
        _itemTmp = new ItemState(item.id, item.cost, item.title, item.description, item.spriteName, item.type);
    }

    public void OnBackInventory()
    {
        panelInventory.SetActive(false);
        lootPanel.SetActive(true);
    }

    public void OnBackDetailItem()
    {
        panelInventory.SetActive(true);
        panelDetailItem.SetActive(false);
    }

    public void OnEquipDetailItem()
    {
        panelInventory.SetActive(true);
        panelDetailItem.SetActive(false);
        if (_itemTmp.type == "weapon")
        {
            SaveData data = SaveSystem.GetData();

            data.equipment.weapon = _itemTmp;
        }
        else if (_itemTmp.type == "armor")
        {
            SaveData data = SaveSystem.GetData();

            data.equipment.armor = _itemTmp;
        }

        UpdateEquipment();
    }

    private void UpdateEquipment()
    {
        SaveData data = SaveSystem.GetData();

        if (data.equipment.weapon != null && data.equipment.weapon.spriteName != "")
        {
            itemWeaponGo.SetActive(true);
            itemWeaponGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + data.equipment.weapon.spriteName);
        }
        else
        {
            // Pistol par defaut
            data.equipment.weapon = new ItemState(0, 0, "Pistol", "A simple pistol", "pistol", "weapon");
            itemWeaponGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + data.equipment.weapon.spriteName);
            itemWeaponGo.SetActive(true);
        }

        if (data.equipment.armor != null && data.equipment.armor.spriteName != "")
        {
            itemArmorGo.SetActive(true);
            itemArmorGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + data.equipment.armor.spriteName);
        }
        else
        {
            itemArmorGo.SetActive(false);
        }

        SaveSystem.Save(data);
    }
}
