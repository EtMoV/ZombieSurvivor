using UnityEngine;

public class GoOutInput : MonoBehaviour
{
    public GameObject panelGoOut;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panelGoOut.SetActive(true);
        }
    }
}
