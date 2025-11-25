using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUnlockManager : MonoBehaviour
{

    public GameObject imageUiWeapon;
    public GameObject textUiGoldCost;

    public GameObject goldManagerGo;
    private GoldManager _goldManager;

    public void Awake()
    {
        _goldManager = goldManagerGo.GetComponent<GoldManager>();
    }

    public void Start()
    {
        getCurrentWeaponToUnlock();
    }

    public void getCurrentWeaponToUnlock()
    {
        WeaponUnlockState wUS = WeaponUnlockManagerState.getNextWeapon();
        if (wUS != null)
        {
            // On met a jour l'UI
            imageUiWeapon.GetComponent<Image>().sprite = Resources.Load<Sprite>(wUS.type + "Sprite");
            textUiGoldCost.GetComponent<TextMeshProUGUI>().text = wUS.cost.ToString();
        }
        else
        {
            // Plus d'arme a unlock
            imageUiWeapon.SetActive(false);
            textUiGoldCost.GetComponent<TextMeshProUGUI>().text = "N/A";
        }
    }

    public void BuyWeapon()
    {
        int goldKeep = LootManager.getLoots();
        WeaponUnlockState wUS = WeaponUnlockManagerState.getNextWeapon();
        if (wUS != null && goldKeep >= wUS.cost)
        {
            LootManager.subLoot(wUS.cost);
            WeaponUnlockManagerState.AddWeaponUnlock();
            getCurrentWeaponToUnlock();
            _goldManager.UpdateGoldDisplay();
        }
    }


}

