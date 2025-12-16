using UnityEngine;

public class GoOutInput : MonoBehaviour
{
    public GameObject panelGoOut;
    public GameObject btnLvlTwo;
    public GameObject btnLvlThree;
    public GameObject btnLvlFour;
    public GameObject btnLvlFive;
    public GameObject btnLvlSix;
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
            btnLvlFive.SetActive(false);
            btnLvlSix.SetActive(false);

            if (data.mapSixDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                // btnLvlSeven.SetActive(true); TODO
            }
            else if (data.mapFiveDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
            }
            else if (data.mapFourDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
            }
            else if (data.mapThreeDone)
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
