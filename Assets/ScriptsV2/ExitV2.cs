using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitV2 : MonoBehaviour
{
    public GameObject victoryScreen;

    public GameObject starOne;
    public GameObject starTwo;
    public GameObject starThree;

    public bool starIsDone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        starIsDone = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            ZombieHealthV2[] zombies = FindObjectsByType<ZombieHealthV2>(FindObjectsSortMode.None);
            bool flagAllZombieDead = true;
            foreach (var z in zombies)
            {
                if (!z.isDead) flagAllZombieDead = false;
            }

            // 1er etoile
            Image imgOne = starOne.GetComponent<Image>();
            imgOne.color = Color.white;

            // 2e etoile
            if (starIsDone)
            {
                Image imgTwo = starTwo.GetComponent<Image>();
                imgTwo.color = Color.white;
            }

            // 3e etoile
            if (flagAllZombieDead)
            {
                Image imgThree = starThree.GetComponent<Image>();
                imgThree.color = Color.white;
            }
            victoryScreen.SetActive(true);
        }
    }

    public void OnNextLvl()
    {
        SaveDataV2 data = SaveSystemV2.GetData();
        data.currentScene++;
        SaveSystemV2.Save(data);
        SceneManager.LoadScene(data.currentScene);
    }
}
