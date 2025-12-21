using UnityEngine;

public class GoOutInput : MonoBehaviour
{
    public GameObject panelGoOut;
    public GameObject btnLvlTwo;
    public GameObject btnLvlThree;
    public GameObject btnLvlFour;
    public GameObject btnLvlFive;
    public GameObject btnLvlSix;
    public GameObject btnLvlSeven;

    public GameObject btnLvlEight;
    public GameObject btnLvlNine;
    public GameObject btnLvlTen;

    public GameObject btnLvlEleven;
    public GameObject btnLvlTwelve;
    public GameObject btnLvlThirteen;
    public GameObject btnLvlFourteen;
    public GameObject btnLvlFifteen;
    public GameObject btnLvlSixteen;
    public GameObject btnLvlSeventeen;
    public GameObject btnLvlEighteen;
    public GameObject btnLvlNineteen;
    public GameObject btnLvlTwenty;
    public GameObject btnLvlTwentyone;

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
            btnLvlSeven.SetActive(false);
            btnLvlEight.SetActive(false);
            btnLvlNine.SetActive(false);
            btnLvlTen.SetActive(false);
            btnLvlEleven.SetActive(false);
            btnLvlTwelve.SetActive(false);
            btnLvlThirteen.SetActive(false);
            btnLvlFourteen.SetActive(false);
            btnLvlFifteen.SetActive(false);
            btnLvlSixteen.SetActive(false);
            btnLvlSeventeen.SetActive(false);
            btnLvlEighteen.SetActive(false);
            btnLvlNineteen.SetActive(false);
            btnLvlTwenty.SetActive(false);
            btnLvlTwentyone.SetActive(false);
            
            if (data.mapTwentyoneDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
                btnLvlEight.SetActive(true);
                btnLvlNine.SetActive(true);
                btnLvlTen.SetActive(true);
                btnLvlEleven.SetActive(true);
                btnLvlTwelve.SetActive(true);
                btnLvlThirteen.SetActive(true);
                btnLvlFourteen.SetActive(true);
                btnLvlFifteen.SetActive(true);
                btnLvlSixteen.SetActive(true);
                btnLvlSeventeen.SetActive(true);
                btnLvlEighteen.SetActive(true);
                btnLvlNineteen.SetActive(true);
                btnLvlTwenty.SetActive(true);
                btnLvlTwentyone.SetActive(false);
                // btnLvlTwentytwo.SetActive(false); TODO
            }
            else if (data.mapTwentyDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
                btnLvlEight.SetActive(true);
                btnLvlNine.SetActive(true);
                btnLvlTen.SetActive(true);
                btnLvlEleven.SetActive(true);
                btnLvlTwelve.SetActive(true);
                btnLvlThirteen.SetActive(true);
                btnLvlFourteen.SetActive(true);
                btnLvlFifteen.SetActive(true);
                btnLvlSixteen.SetActive(true);
                btnLvlSeventeen.SetActive(true);
                btnLvlEighteen.SetActive(true);
                btnLvlNineteen.SetActive(true);
                btnLvlTwenty.SetActive(true);
                btnLvlTwentyone.SetActive(false);
            }
            else if (data.mapNineteenDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
                btnLvlEight.SetActive(true);
                btnLvlNine.SetActive(true);
                btnLvlTen.SetActive(true);
                btnLvlEleven.SetActive(true);
                btnLvlTwelve.SetActive(true);
                btnLvlThirteen.SetActive(true);
                btnLvlFourteen.SetActive(true);
                btnLvlFifteen.SetActive(true);
                btnLvlSixteen.SetActive(true);
                btnLvlSeventeen.SetActive(true);
                btnLvlEighteen.SetActive(true);
                btnLvlNineteen.SetActive(true);
                btnLvlTwenty.SetActive(true);
            }
            else if (data.mapEighteenDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
                btnLvlEight.SetActive(true);
                btnLvlNine.SetActive(true);
                btnLvlTen.SetActive(true);
                btnLvlEleven.SetActive(true);
                btnLvlTwelve.SetActive(true);
                btnLvlThirteen.SetActive(true);
                btnLvlFourteen.SetActive(true);
                btnLvlFifteen.SetActive(true);
                btnLvlSixteen.SetActive(true);
                btnLvlSeventeen.SetActive(true);
                btnLvlEighteen.SetActive(true);
                btnLvlNineteen.SetActive(true);
            }
            else if (data.mapSeventeenDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
                btnLvlEight.SetActive(true);
                btnLvlNine.SetActive(true);
                btnLvlTen.SetActive(true);
                btnLvlEleven.SetActive(true);
                btnLvlTwelve.SetActive(true);
                btnLvlThirteen.SetActive(true);
                btnLvlFourteen.SetActive(true);
                btnLvlFifteen.SetActive(true);
                btnLvlSixteen.SetActive(true);
                btnLvlSeventeen.SetActive(true);
                btnLvlEighteen.SetActive(true);
            }
            else if (data.mapSixteenDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
                btnLvlEight.SetActive(true);
                btnLvlNine.SetActive(true);
                btnLvlTen.SetActive(true);
                btnLvlEleven.SetActive(true);
                btnLvlTwelve.SetActive(true);
                btnLvlThirteen.SetActive(true);
                btnLvlFourteen.SetActive(true);
                btnLvlFifteen.SetActive(true);
                btnLvlSixteen.SetActive(true);
                btnLvlSeventeen.SetActive(true);
            }
            else if (data.mapFifteenDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
                btnLvlEight.SetActive(true);
                btnLvlNine.SetActive(true);
                btnLvlTen.SetActive(true);
                btnLvlEleven.SetActive(true);
                btnLvlTwelve.SetActive(true);
                btnLvlThirteen.SetActive(true);
                btnLvlFourteen.SetActive(true);
                btnLvlFifteen.SetActive(true);
                btnLvlSixteen.SetActive(true);
            }
            else if (data.mapFourteenDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
                btnLvlEight.SetActive(true);
                btnLvlNine.SetActive(true);
                btnLvlTen.SetActive(true);
                btnLvlEleven.SetActive(true);
                btnLvlTwelve.SetActive(true);
                btnLvlThirteen.SetActive(true);
                btnLvlFourteen.SetActive(true);
                btnLvlFifteen.SetActive(true);
            }
            else if (data.mapThirteenDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
                btnLvlEight.SetActive(true);
                btnLvlNine.SetActive(true);
                btnLvlTen.SetActive(true);
                btnLvlEleven.SetActive(true);
                btnLvlTwelve.SetActive(true);
                btnLvlThirteen.SetActive(true);
                btnLvlFourteen.SetActive(true);
            }
            else if (data.mapTwelveDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
                btnLvlEight.SetActive(true);
                btnLvlNine.SetActive(true);
                btnLvlTen.SetActive(true);
                btnLvlEleven.SetActive(true);
                btnLvlTwelve.SetActive(true);
                btnLvlThirteen.SetActive(true);
            }
            else if (data.mapElevenDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
                btnLvlEight.SetActive(true);
                btnLvlNine.SetActive(true);
                btnLvlTen.SetActive(true);
                btnLvlEleven.SetActive(true);
                btnLvlTwelve.SetActive(true);
            }
            else if (data.mapTenDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
                btnLvlEight.SetActive(true);
                btnLvlNine.SetActive(true);
                btnLvlTen.SetActive(true);
                btnLvlEleven.SetActive(true);
            }
            else if (data.mapNineDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
                btnLvlEight.SetActive(true);
                btnLvlNine.SetActive(true);
                btnLvlTen.SetActive(true);
            }
            else if (data.mapEightDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
                btnLvlEight.SetActive(true);
                btnLvlNine.SetActive(true);
            }
            else if (data.mapSevenDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
                btnLvlEight.SetActive(true);
            }
            else if (data.mapSixDone)
            {
                btnLvlTwo.SetActive(true);
                btnLvlThree.SetActive(true);
                btnLvlFour.SetActive(true);
                btnLvlFive.SetActive(true);
                btnLvlSix.SetActive(true);
                btnLvlSeven.SetActive(true);
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
