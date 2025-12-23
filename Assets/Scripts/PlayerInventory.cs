using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject moneyCounterUI;
    private int moneys;

    public void AddMoney()
    {
        moneys++;
        moneyCounterUI.GetComponent<TextMeshProUGUI>().text = moneys.ToString();
    }

}
