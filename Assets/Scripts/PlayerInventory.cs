using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject moneyCounterUI;
    private int moneys;

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
