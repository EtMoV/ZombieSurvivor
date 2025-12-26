using System.Collections.Generic;
using Firebase.Analytics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MerchantInput : MonoBehaviour
{
    public GameObject merchantUI;
    public GameObject titleWeaponDetailUI;
    public GameObject priceWeaponDetailUI;
    public GameObject imageWeaponDetailUI;
    public GameObject titleArmorDetailUI;
    public GameObject priceArmorDetailUI;
    public GameObject imageArmorDetailUI;
    public GameObject playerGo;
    private Item itemWeapon;
    private Item itemArmor;
    private bool noWeaponAvailable = false;
    private bool noArmorAvailable = false;

    private List<Item> items = new List<Item>
    {
        new Item(1, 2, "berreta","Berreta", "", "pistol", "weapon"),
        new Item(2, 2, "woodArmor","Wood armor", "", "woodArmor", "armor"),
        new Item(3, 10, "mp5", "MP5", "A high rate of fire", "subMachineGun", "weapon"),
        new Item(4, 20, "leatherArmor", "Leather armor", "A better protection, give 2 more HP", "leatherArmor", "armor"),
        new Item(5, 15, "shotgun", "Shotgun", "Very effective at short range", "shotgun", "weapon"),
        new Item(6, 30, "copperArmor", "Copper armor", "A good protection, give 3 more HP", "copperArmor", "armor"),
        new Item(7, 20, "m16", "M16", "Very powerful and accurate", "assaultRifle", "weapon"),
        new Item(8, 40, "metalArmor", "Metal armor", "A very good protection, give 4 more HP", "metalArmor", "armor"),
        new Item(9, 25, "grenade", "Grenade", "K - BOOM", "grenade", "weapon"),
        new Item(10, 50, "diamondArmor", "Diamond armor", "The best protection, give 5 more HP", "diamondArmor", "armor"),
    };

    public void CloseMerchantUI()
    {
        merchantUI.SetActive(false);
    }

    public void OnBuyItemWeapon()
    {
        if (!noWeaponAvailable && LootManagerState.GetLoot() >= itemWeapon.price)
        {
            FirebaseAnalytics.LogEvent("buy_" + itemWeapon.title);
            LootManagerState.SubLoots(itemWeapon.price);
            playerGo.GetComponent<PlayerInventory>().UpdateCanFood();
            ItemManagerState.AddItem(itemWeapon);
            playerGo.GetComponent<PlayerWeapon>().SetWeapon(itemWeapon.nameItem);
            UpdateMerchantUI();
        }
    }

    public void OnBuyItemArmor()
    {
        if (!noArmorAvailable && LootManagerState.GetLoot() >= itemArmor.price)
        {
            FirebaseAnalytics.LogEvent("buy_" + itemArmor.title);
            LootManagerState.SubLoots(itemArmor.price);
            playerGo.GetComponent<PlayerInventory>().UpdateCanFood();
            ItemManagerState.AddItem(itemArmor);
            playerGo.GetComponent<PlayerLife>().SetArmor(itemArmor.nameItem);
            UpdateMerchantUI();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpdateMerchantUI();
            merchantUI.SetActive(true);
        }
    }

    private void UpdateMerchantUI()
    {
        itemWeapon = null;
        itemArmor = null;
        bool weaponAssigned = false;
        bool armorAssigned = false;

        foreach (Item item in items)
        {
            if (!ItemManagerState.existItem(item))
            {
                if (!weaponAssigned && item.category == "weapon")
                {
                    itemWeapon = item;
                    weaponAssigned = true;
                }
                if (!armorAssigned && item.category == "armor")
                {
                    itemArmor = item;
                    armorAssigned = true;
                }
            }
        }

        if (weaponAssigned)
        {
            SetItem(itemWeapon);
        }
        else
        {
            SetItemNone("weapon");
        }

        if (armorAssigned)
        {
            SetItem(itemArmor);
        }
        else
        {
            SetItemNone("armor");
        }

    }

    private void SetItem(Item itemInput)
    {
        if (itemInput.category == "armor")
        {
            titleArmorDetailUI.GetComponent<TextMeshProUGUI>().text = itemInput.title;
            priceArmorDetailUI.GetComponent<TextMeshProUGUI>().text = itemInput.price.ToString();
            imageArmorDetailUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + itemInput.spriteName);
        }
        else if (itemInput.category == "weapon")
        {
            titleWeaponDetailUI.GetComponent<TextMeshProUGUI>().text = itemInput.title;
            priceWeaponDetailUI.GetComponent<TextMeshProUGUI>().text = itemInput.price.ToString();
            imageWeaponDetailUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + itemInput.spriteName);
        }
    }

    private void SetItemNone(string categoryInput)
    {
        if (categoryInput == "armor")
        {
            titleArmorDetailUI.GetComponent<TextMeshProUGUI>().text = "No more item";
            priceArmorDetailUI.GetComponent<TextMeshProUGUI>().text = "-";
            imageArmorDetailUI.SetActive(false);
            noArmorAvailable = true;
        }
        else if (categoryInput == "weapon")
        {
            titleWeaponDetailUI.GetComponent<TextMeshProUGUI>().text = "No more item";
            priceWeaponDetailUI.GetComponent<TextMeshProUGUI>().text = "-";
            imageWeaponDetailUI.SetActive(false);
            noWeaponAvailable = true;
        }
    }
}
