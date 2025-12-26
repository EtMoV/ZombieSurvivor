using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public GameObject hitPanel;
    public GameObject fillBarLife;
    public GameObject touchArea;
    public GameObject dieMenu;
    public bool isDead = false;
    private int life;
    private int maxLife;

    public void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void FreezeAnimator()
    {
        Animator anim = GetComponent<Animator>();
        anim.speed = 0f;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        life -= damage;
        UpdateLifeBar();
        if (life <= 0)
        {
            Die();
        }
        else
        {
            Hit();
        }
    }

    public void SetArmor(string nameItem)
    {
        switch (nameItem)
        {
            case "woodArmor":
                maxLife = 11;
                break;
            case "leatherArmor":
                maxLife = 12;
                break;
            case "copperArmor":
                maxLife = 13;
                break;
            case "metalArmor":
                maxLife = 14;
                break;
            case "diamondArmor":
                maxLife = 15;
                break;
            default:
                maxLife = 1;
                break;
        }

        life = maxLife;
        UpdateLifeBar();
    }

    void Start()
    {
        if (ItemManagerState.getLastArmor() != null)
        {
            SetArmor(ItemManagerState.getLastArmor().nameItem);
        }
        else
        {
            SetArmor("default");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            TakeDamage(1);
        }
    }

    private void Hit()
    {
        GetComponent<Animator>().Play("PlayerHit");
        StartCoroutine(CameraShake(0.1f, 0.3f));
        HitPanelAlphaCoroutine();
        Invoke("ResetHitStateAfterDelay", 0.8f);

        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (GameObject zombie in zombies)
        {
            Zombie zombieScript = zombie.GetComponent<Zombie>();
            if (zombieScript != null)
            {
                Vector2 knockbackDir = ((Vector2)zombie.transform.position - (Vector2)transform.position).normalized;
                zombieScript.ApplyKnockback(knockbackDir, 30f);
            }
        }
    }

    private void Die()
    {
        isDead = true;
        GetComponent<Animator>().Play("PlayerDie");
        StartCoroutine(CameraShake(0.1f, 0.3f));
        touchArea.SetActive(false);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // Stop la physique
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
        // Stop l’input
        enabled = false; // désactive le script de mouvement
        dieMenu.SetActive(true);
    }

    private IEnumerator CameraShake(float duration, float magnitude)
    {
        Camera camera = Camera.main;
        Vector3 originalPosition = camera.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float randomX = Random.Range(-1f, 1f) * magnitude;
            float randomY = Random.Range(-1f, 1f) * magnitude;
            camera.transform.position = originalPosition + new Vector3(randomX, randomY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        camera.transform.position = originalPosition;
    }

    private void HitPanelAlphaCoroutine()
    {
        Image image = hitPanel.GetComponent<Image>();
        Color color = image.color;
        color.a = 0.25f; // 25% d'opacité
        image.color = color;
        Invoke("HitPanelOriginalColor", 0.8f);
    }

    private void HitPanelOriginalColor()
    {
        Image image = hitPanel.GetComponent<Image>();
        Color color = image.color;
        color.a = 0f; // Retour à transparent
        image.color = color;
    }

    private void UpdateLifeBar()
    {
        float lifeProgress = life / (float)maxLife;
        fillBarLife.GetComponent<Image>().fillAmount = lifeProgress;
    }
}
