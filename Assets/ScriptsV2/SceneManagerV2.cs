
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerV2 : MonoBehaviour
{
    void Start()
    {
        SaveDataV2 data = SaveSystemV2.GetData();
        if (SceneManager.GetActiveScene().buildIndex != data.currentScene)
        {
            SceneManager.LoadScene(data.currentScene);
        }
    }
}
