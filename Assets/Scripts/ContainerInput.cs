using Firebase.Analytics;
using TMPro;
using UnityEngine;

public class ContainerInput : MonoBehaviour
{
    public GameObject containerUI;
    public GameObject numberText;
    public GameObject spinBtn;
    public GameObject parentBtnEnd;
    public GameObject adBtn;
    public AdmobManager admobManager;
    public PlayerInventory playerInventory;
    private int tmpLoot;

    public void OnSpin()
    {
        int randomNumber = Random.Range(1, 6);
        numberText.GetComponent<TextMeshProUGUI>().text = randomNumber.ToString();
        spinBtn.SetActive(false);
        parentBtnEnd.SetActive(true);
        adBtn.SetActive(true);
        tmpLoot = randomNumber;
        FirebaseAnalytics.LogEvent("Spin");
    }

    public void OnClose()
    {
        LootManagerState.AddLoot(tmpLoot);
        playerInventory.UpdateCanFood();
        containerUI.SetActive(false);
    }

    public void OnAd()
    {
        admobManager.GetComponent<AdmobManager>().showRewardedAd();
        adBtn.SetActive(false);
        tmpLoot *= 2;
        numberText.GetComponent<TextMeshProUGUI>().text = tmpLoot.ToString();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tmpLoot = 0;
            containerUI.SetActive(true);
            numberText.SetActive(true);
            numberText.GetComponent<TextMeshProUGUI>().text = "?";
            spinBtn.SetActive(true);
            parentBtnEnd.SetActive(false);
            adBtn.SetActive(false);
            gameObject.SetActive(false);
        }
    }

}
