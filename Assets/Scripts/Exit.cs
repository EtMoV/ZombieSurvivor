using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject panelUiWantExitWatch;
    public GameObject panelUiWantExitPhone;
    public GameObject gameplayRootGo;
    public GameObject btnUiPauseWatch;
    public GameObject btnUiPausePhone;
    public GameObject textUiExit;
    public GameObject exitManagerGo;
    public GameObject joystickPhoneGo;
    private ExitManager _exitManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _exitManager = exitManagerGo.GetComponent<ExitManager>();
        _exitManager.isActive = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Desactivation du gameRoot
            gameplayRootGo.SetActive(false);

            // Signalement au roundManager de ne pas continuer
            _exitManager.isActive = true;
            if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
            {
                // Affichage du panel d'exit
                panelUiWantExitWatch.SetActive(true);
                // Desactivation du bouton pause
                btnUiPauseWatch.SetActive(false);
            }
            else
            {
                // Affichage du panel d'exit
                panelUiWantExitPhone.SetActive(true);
                // Desactivation du bouton pause
                btnUiPausePhone.SetActive(false);
                // desactivation du joystick
                joystickPhoneGo.SetActive(false);
            }

            // desaffichage de l'exit
            gameObject.SetActive(false);
            textUiExit.SetActive(false);
        }
    }
}
