using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{

    public GameObject textUiGold;

    public void Start()
    {
        UpdateGoldDisplay();
    }

    public void UpdateGoldDisplay()
    {
        textUiGold.GetComponent<TextMeshProUGUI>().text = LootManager.getLoots().ToString();
    }
}

