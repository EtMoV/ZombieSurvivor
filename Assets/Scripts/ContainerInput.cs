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
    public Sprite spriteNormal;
    public Sprite spriteCooldown;
    private int tmpLoot;
    private SpriteRenderer containerSprite;
    private Collider2D colliderGo;
    public float respawnTime = 60f;
    private float amplitude = 0.2f;
    private float speed = 2f;
    private float startY;
    private bool isCoolDown = false;

    void Awake()
    {
        containerSprite = GetComponent<SpriteRenderer>();
        colliderGo = GetComponent<Collider2D>();
        startY = transform.position.y;
    }

    void Update()
    {
        if (!isCoolDown) return;

        // Rebond normal
        float newY = startY + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

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
            // Cache le container
            colliderGo.enabled = false;
            isCoolDown = true;
            SetCooldownSprite();
            Invoke(nameof(Respawn), respawnTime);
        }
    }

    private void Respawn()
    {
        containerSprite.enabled = true;
        colliderGo.enabled = true;
        isCoolDown = false;
        SetNormalSprite();
    }

    // Appelé quand le container entre en cooldown
    void SetCooldownSprite()
    {
        containerSprite.sprite = spriteCooldown;
        transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        transform.localScale = new Vector3(0.1f, 0.1f, 1f);
    }

    // Appelé quand le container réapparait
    void SetNormalSprite()
    {
        containerSprite.sprite = spriteNormal;
        transform.localScale = new Vector3(2f, 3f, 1f);
        transform.localEulerAngles = new Vector3(0f, 0f, 90f);
    }
}
