using UnityEngine;

public class HubBuyEnter : MonoBehaviour
{
    public GameObject panelBuyNarration;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panelBuyNarration.SetActive(true);
        }
    }
}
