using UnityEngine;

public class ZoneLaunchTutoSpawn : MonoBehaviour
{
    public GameObject roundManagerGo;
    private RoundManager _roundManager;
    public void Awake()
    {
        _roundManager = roundManagerGo.GetComponent<RoundManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _roundManager.launchRoundTuto = true;
            Destroy(gameObject);
        }
    }
}
