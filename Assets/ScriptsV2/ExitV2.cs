using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitV2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SaveDataV2 data = SaveSystemV2.GetData();
            data.currentScene++;
            SaveSystemV2.Save(data);
            SceneManager.LoadScene(data.currentScene);
        }
    }
}
