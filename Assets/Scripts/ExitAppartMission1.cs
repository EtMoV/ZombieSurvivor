using System.Collections;
using UnityEngine;

public class ExitAppartMission1 : MonoBehaviour
{
    public GameObject fadeBlackGo;

    public GameObject PlayerGo;

    public GameObject cameraGo;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fadeBlackGo.GetComponent<FadeBlack>().ShowBlack(0.5f);

            StartCoroutine(CoroutineCollision(collision));
        }
    }

    private IEnumerator CoroutineCollision(Collision2D collision)
    {
        yield return new WaitForSeconds(0.6f);

        PlayerGo.transform.position = new Vector3(-15f, 125f, 0); // On remet le joueur à l'entrée de l'arène
        Camera cam = cameraGo.GetComponent<Camera>();
        cam.orthographicSize = 13f;
    }
}
