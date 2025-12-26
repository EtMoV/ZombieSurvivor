using UnityEngine;

public class MerchantInput : MonoBehaviour
{
    public GameObject merchantManager;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            merchantManager.GetComponent<MerchantManager>().UpdateMerchantUI();
        }
    }
}
