using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpGet : MonoBehaviour
{
    public GameObject gameplayRootGo;
    public GameObject panelPowerUpGoWatch;
    public GameObject panelPowerUpGoPhone;
    public GameObject imagePowerUpOneGoWatch;
    public GameObject imagePowerUpTwoGoWatch;
    public GameObject imagePowerUpOneGoPhone;
    public GameObject imagePowerUpTwoGoPhone;
    private bool _canTrigger = true;
    public GameObject powerUpManagerGo;
    private PowerUpManager _powerUpManager;
    public GameObject textPowerUpDescriptionOneWatch;
    public GameObject textPowerUpDescriptionTwoWatch;
    public GameObject textPowerUpDescriptionOnePhone;
    public GameObject textPowerUpDescriptionTwoPhone;

    public float amplitude = 0.5f; // hauteur du rebond
    public float speed = 2f;       // vitesse du rebond

    private float startY; // position de départ sur l'axe Y
    void Awake()
    {
        _powerUpManager = powerUpManagerGo.GetComponent<PowerUpManager>();
    }

    void Start()
    {
        // Enregistre la position de départ
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Calcule le décalage vertical
        float newY = startY + Mathf.Sin(Time.time * speed) * amplitude;

        // Applique le mouvement
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_canTrigger) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            _canTrigger = false;

            // Stopper le jeu 
            gameplayRootGo.SetActive(false);
            _powerUpManager.isActive = true; // Pour la coroutines de waves
            _powerUpManager._playerController.joystickUiGo.SetActive(false); // On cache le joystick
            _powerUpManager.iconPauseGoWatch.SetActive(false); // On cache le btn pause sur watch
            _powerUpManager.iconPauseGoPhone.SetActive(false); // On cache le btn pause sur tel

            // generation des powerUp
            _powerUpManager.generateTmpPowerUp(true);

            Sprite spriteOne = Resources.Load<Sprite>("PowerUps/" + _powerUpManager.tmpPowerUpOne.type);
            Sprite spriteTwo = Resources.Load<Sprite>("PowerUps/" + _powerUpManager.tmpPowerUpTwo.type);

            // Afficher l'ui du shop
            if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
            {

                panelPowerUpGoWatch.SetActive(true);
                imagePowerUpOneGoWatch.GetComponent<Image>().sprite = spriteOne;
                imagePowerUpTwoGoWatch.GetComponent<Image>().sprite = spriteTwo;
                textPowerUpDescriptionOneWatch.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpOne.description;
                textPowerUpDescriptionTwoWatch.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpTwo.description;
                Destroy(gameObject);
            }
            else
            {
                panelPowerUpGoPhone.SetActive(true);
                imagePowerUpOneGoPhone.GetComponent<Image>().sprite = spriteOne;
                imagePowerUpTwoGoPhone.GetComponent<Image>().sprite = spriteTwo;
                textPowerUpDescriptionOnePhone.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpOne.description;
                textPowerUpDescriptionTwoPhone.GetComponent<TextMeshProUGUI>().text = _powerUpManager.tmpPowerUpTwo.description;
                Destroy(gameObject);
            }
        }
    }
}
