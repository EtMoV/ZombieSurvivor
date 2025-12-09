using UnityEngine;

public class GoOutInput : MonoBehaviour
{
    public GameObject panelGoOut;
    public GameObject btnLvlTwo;
    public GameObject btnLvlThree;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panelGoOut.SetActive(true);

            // On affiche que les niveaux debloques
            SaveData data = SaveSystem.GetData();
            btnLvlTwo.SetActive(false);
            btnLvlThree.SetActive(false);

            switch (data.lastLevelDone)
            {
                case "mapOne":
                    btnLvlTwo.SetActive(true);
                    break;
                case "mapTwo":
                    btnLvlTwo.SetActive(true);
                    btnLvlThree.SetActive(true);
                    break;
            }
        }
    }
}
