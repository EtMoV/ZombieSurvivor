using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject moneyCounterUI;
    public GameObject foodCanCounterUI;
    private int moneys;

    public void Start()
    {
        UpdateCanFood();
        UpdateMoneyUI();
    }
    
    public void AddCanFood()
    {
        LootManagerState.AddLoot(1);
        UpdateCanFood();
    }

    public void UpdateCanFood()
    {
        foodCanCounterUI.GetComponent<TextMeshProUGUI>().text = LootManagerState.GetLoot().ToString();
    }

    public void AddMoney()
    {
        moneys++;
        UpdateMoneyUI();
    }

    public bool ReduceMoney()
    {
        if (moneys > 0)
        {
            moneys--;
            UpdateMoneyUI();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateMoneyUI()
    {
        moneyCounterUI.GetComponent<TextMeshProUGUI>().text = moneys.ToString();
    }
}
