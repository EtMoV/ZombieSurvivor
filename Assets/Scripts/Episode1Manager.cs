using System;
using System.Collections.Generic;
using UnityEngine;

public class Episode1Manager : MonoBehaviour
{
    public GameObject endPanelEpisode;

    public GameObject spawnPointOne;

    public GameObject spawnPointTwo;

    public GameObject spawnPointThree;

    public GameObject spawnPointFour;

    public GameObject spawnPointFive;

    public GameObject touchArea;

    public GameObject pauseIcon;

    public GameObject narraitionManagerGo;
    private NarrationManager _narrationManager;

    List<string> narrationTexts = new List<string>();
    bool isEndTextOne = false;
    bool isEndTextTwo = false;
    bool isEndTextThree = false;

    public void Awake()
    {
        narrationTexts.Add("What is that noise ?");
        _narrationManager = narraitionManagerGo.GetComponent<NarrationManager>();
    }

    public void Start()
    {
        _narrationManager.ShowPanel();
        _narrationManager.UpdateText(narrationTexts[0]);
        narrationTexts.RemoveAt(0);
    }

    public void onClicNextNarration()
    {
        string nextText = null;
        if (narrationTexts.Count > 0)
        {
            nextText = narrationTexts[0];
            narrationTexts.RemoveAt(0);
        }

        if (nextText == null)
        {

            _narrationManager.HidePanel();
            if (isEndTextThree)
            {
                // Dernier texte
                ShowEndPanel();
            }
        }
        else
        {
            _narrationManager.UpdateText(nextText);
        }
    }

    public void FixedUpdate()
    {
        if (spawnPointOne.GetComponent<SpawnPoint>().zombieSpawnHasDie)
        {
            if (!isEndTextOne)
            {
                isEndTextOne = true;
                narrationTexts.Add("That was hard...");
                narrationTexts.Add("I need to get out of here !");
                _narrationManager.ShowPanel();

                _narrationManager.UpdateText(narrationTexts[0]);
                narrationTexts.RemoveAt(0);
            }
        }

        if (isEndTextOne && spawnPointTwo.GetComponent<SpawnPoint>().zombieSpawnHasDie)
        {
            if (!isEndTextTwo)
            {
                isEndTextTwo = true;
                narrationTexts.Add("I can't leave my neighbors like this...");
                narrationTexts.Add("I have to kill them all");
                _narrationManager.ShowPanel();

                _narrationManager.UpdateText(narrationTexts[0]);
                narrationTexts.RemoveAt(0);
            }
        }

        if (isEndTextOne && isEndTextTwo && spawnPointOne.GetComponent<SpawnPoint>().zombieSpawnHasDie && spawnPointTwo.GetComponent<SpawnPoint>().zombieSpawnHasDie && spawnPointThree.GetComponent<SpawnPoint>().zombieSpawnHasDie && spawnPointFour.GetComponent<SpawnPoint>().zombieSpawnHasDie && spawnPointFive.GetComponent<SpawnPoint>().zombieSpawnHasDie)
        {
            if (!isEndTextThree)
            {
                isEndTextThree = true;
                narrationTexts.Add("Now i can get out...");
                _narrationManager.ShowPanel();

                _narrationManager.UpdateText(narrationTexts[0]);
                narrationTexts.RemoveAt(0);
            }
        }

    }

    private void ShowEndPanel()
    {
        _narrationManager.HidePanel();
        touchArea.SetActive(false);
        pauseIcon.SetActive(false);
        endPanelEpisode.SetActive(true);
    }
}
