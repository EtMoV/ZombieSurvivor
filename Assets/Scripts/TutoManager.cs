using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    public GameObject panelCinematique;

    public GameObject textCinematiqueGo;

    public GameObject fadeBlackGo;

    public GameObject gameRootGo;

    private List<string> cinematiqueText = new List<string>()
    {
      "The world has been destroyed",
      "There aren't many of you left trying to survive",
      "But as you know, everyone eventually dies"
    };

    private List<string> narrationText = new List<string>()
    {
      "I need to find a way out !",
      "And get back to my base."
    };

    private int cptCinematique = 0;

    public GameObject panelNarrationGo;
    public GameObject textNarrationGo;
    public GameObject playerGo;

    void Start()
    {
        panelCinematique.SetActive(true);
        NextTextCinematique();
    }

    public void NextTextCinematique()
    {
        if (cptCinematique < cinematiqueText.Count)
        {
            textCinematiqueGo.GetComponent<TextMeshProUGUI>().text = cinematiqueText[cptCinematique];
            cptCinematique++;
        }
        else
        {
            fadeBlackGo.GetComponent<FadeBlack>().ShowBlack(0.5f);
            panelCinematique.SetActive(false);
            gameRootGo.SetActive(false);
            Invoke("DisplayGameRootAfterCinematique", 0.5f);
        }
    }

    public void onClicNextNarration()
    {
        if (narrationText.Count > 0)
        {
            textNarrationGo.GetComponent<TextMeshProUGUI>().text = narrationText[0];
            narrationText.RemoveAt(0);
        }
        else
        {
            panelNarrationGo.SetActive(false);
            playerGo.GetComponent<PlayerZombieSpawn>().RelaunchSpawn();
        }
    }

    private void DisplayGameRootAfterCinematique()
    {
        gameRootGo.SetActive(true);
        panelNarrationGo.SetActive(true);
        onClicNextNarration();
    }
}
