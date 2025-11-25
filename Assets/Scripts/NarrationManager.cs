using TMPro;
using UnityEngine;

public class NarrationManager : MonoBehaviour
{

    public GameObject textNarrationGo;

    public GameObject panelNarrationGo;

    public void ShowPanel()
    {
        panelNarrationGo.SetActive(true);
    }

    public void HidePanel()
    {
        panelNarrationGo.SetActive(false);
    }

    public void UpdateText(string text)
    {
        textNarrationGo.GetComponent<TextMeshProUGUI>().text = text;
    }
}

