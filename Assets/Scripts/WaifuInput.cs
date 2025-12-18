using UnityEngine;

public class WaifuInput : MonoBehaviour
{
    public GameObject panelWaifu;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panelWaifu.SetActive(true);
        }
    }
}
