using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject gameplayRootGo;
    public GameObject panelShopGoWatch;
    public GameObject panelShopGoPhone;
    public GameObject imageWeaponOneGoWatch;
    public GameObject imageWeaponOneGoPhone;
    public GameObject btnOneGoWatch;
    public GameObject btnTwoGoWatch;
    public GameObject btnOneGoPhone;
    public GameObject btnTwoGoPhone;
    public GameObject shopManagerGo;
    private ShopManager _shopManager;

    public float amplitude = 0.5f; // hauteur du rebond
    public float speed = 2f;       // vitesse du rebond
    private float startY; // position de départ sur l'axe Y

    public List<Weapon> randomWeapon;

    void Awake()
    {
        _shopManager = shopManagerGo.GetComponent<ShopManager>();

        randomWeapon = new List<Weapon>
        { 
            // Schema par defaut
            new Weapon("deagle"),
            new Weapon("subMachineGun"),
            new Weapon("shotgun"),
            new Weapon("bat"),
            new Weapon("assaultRifle"),
            new Weapon("grenade"),
            new Weapon("ak"),
            new Weapon("sniper"),
            new Weapon("rocketLauncher"),
            new Weapon("gatling"),
            new Weapon("spas"),
        };

        // Ajout des schemas debloques
       // randomWeapon.AddRange(WeaponUnlockManagerState.getAllWeapons().Select(s => new Weapon(s.type)).ToList());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        if (collision.gameObject.CompareTag("Player"))
        {

            // On cache ces btns par securites avant le spin des sprites pour ne pas a avoir le faire dans le shopManager
            if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
            {
                btnOneGoWatch.SetActive(false);
                btnTwoGoWatch.SetActive(false);
            }
            else
            {
                btnOneGoPhone.SetActive(false);
                btnTwoGoPhone.SetActive(false);
            }

            // Stopper le jeu 
            gameplayRootGo.SetActive(false);
            _shopManager.isActive = true;
            _shopManager._playerController.joystickUiGo.SetActive(false); // On cache le joystick
            _shopManager.iconPauseGoWatch.SetActive(false); // On cache le btn pause sur watch
            _shopManager.iconPauseGoPhone.SetActive(false); // On cache le btn pause sur tel
            if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
            {
                panelShopGoWatch.SetActive(true);
            }
            else
            {
                panelShopGoPhone.SetActive(true);
            }

            StartCoroutine(SpinSpriteWeapon());
        }
    }


    IEnumerator SpinSpriteWeapon()
    {
        int index1 = Random.Range(0, randomWeapon.Count);
        displaySprite(randomWeapon[index1].weaponData.sprite);

        yield return new WaitForSeconds(0.1f);
        int index2 = Random.Range(0, randomWeapon.Count);
        displaySprite(randomWeapon[index2].weaponData.sprite);

        yield return new WaitForSeconds(0.1f);
        int index3 = Random.Range(0, randomWeapon.Count);
        displaySprite(randomWeapon[index3].weaponData.sprite);

        yield return new WaitForSeconds(0.1f);
        int index4 = Random.Range(0, randomWeapon.Count);
        displaySprite(randomWeapon[index4].weaponData.sprite);

        yield return new WaitForSeconds(0.1f);
        int index5 = Random.Range(0, randomWeapon.Count);
        displaySprite(randomWeapon[index5].weaponData.sprite);

        yield return new WaitForSeconds(0.1f);
        int index6 = Random.Range(0, randomWeapon.Count);
        displaySprite(randomWeapon[index6].weaponData.sprite);

        yield return new WaitForSeconds(0.1f);
        int index7 = Random.Range(0, randomWeapon.Count);
        displaySprite(randomWeapon[index7].weaponData.sprite);

        yield return new WaitForSeconds(0.2f);
        int index8 = Random.Range(0, randomWeapon.Count);
        displaySprite(randomWeapon[index8].weaponData.sprite);

        yield return new WaitForSeconds(0.2f);
        int index9 = Random.Range(0, randomWeapon.Count);
        displaySprite(randomWeapon[index9].weaponData.sprite);

        yield return new WaitForSeconds(0.2f);
        int index10 = Random.Range(0, randomWeapon.Count);
        displaySprite(randomWeapon[index10].weaponData.sprite);

        _shopManager.weaponTmpOne = randomWeapon[index10].Clone();
        _shopManager.isBtnActive = true;

        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            btnOneGoWatch.SetActive(true);
            btnTwoGoWatch.SetActive(true);
        }
        else
        {
            btnOneGoPhone.SetActive(true);
            btnTwoGoPhone.SetActive(true);
        }

        // Desaffichage du coffre
        gameObject.SetActive(false);
    }

    public void displaySprite(Sprite sprite)
    {
        // Afficher l'ui du shop
        if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
        {
            imageWeaponOneGoWatch.GetComponent<Image>().sprite = sprite;
        }
        else
        {
            imageWeaponOneGoPhone.GetComponent<Image>().sprite = sprite;
        }

    }
}
