using UnityEngine;

public class GoOutInput : MonoBehaviour
{
    public GameObject panelGoOut;
    public GameObject btnLvlTwo;
    public GameObject btnLvlThree;
    public GameObject btnLvlFour;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panelGoOut.SetActive(true);

            // On affiche que les niveaux debloques
            SaveData data = SaveSystem.GetData();
            // AJOUTER DANS L'ORDRE INVERSE LES MAP
            btnLvlTwo.SetActive(false);
            btnLvlThree.SetActive(false);
            btnLvlFour.SetActive(false);
            if (data.mapFourDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                //btnLvlFive.SetActive(true); TODO
            }
            if (data.mapThreeDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
            }
            else if (data.mapTwoDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
            }
            else if (data.mapOneDone)
            {
                btnLvlTwo.SetActive(true);
            }
        }
    }
}
