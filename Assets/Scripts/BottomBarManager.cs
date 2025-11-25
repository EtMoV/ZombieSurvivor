using UnityEngine;

public class BottomBarManager : MonoBehaviour
{
    public GameObject laterMenu;
    public GameObject playMenu;
    public GameObject xpMenu;
    public GameObject aventureMenu;

    public void ShowLaterMenu()
    {
        laterMenu.SetActive(true);
        playMenu.SetActive(false);
        xpMenu.SetActive(false);
        aventureMenu.SetActive(false);
    }

    public void ShowPlayMenu()
    {
        laterMenu.SetActive(false);
        playMenu.SetActive(true);
        xpMenu.SetActive(false);
        aventureMenu.SetActive(false);
    }

    public void ShowXpMenu()
    {
        laterMenu.SetActive(false);
        playMenu.SetActive(false);
        xpMenu.SetActive(true);
        aventureMenu.SetActive(false);
    }

    public void ShowAventureMenu()
    {
        laterMenu.SetActive(false);
        playMenu.SetActive(false);
        xpMenu.SetActive(false);
        aventureMenu.SetActive(true);
    }
}
