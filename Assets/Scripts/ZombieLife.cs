using UnityEngine;

public class ZombieLife : MonoBehaviour
{
    public bool isDead = false;
    public int life;
    public GameObject damagePopupPrefab;
    public GameObject moneyPrefab;
    public GameObject foodCanPrefab;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        life -= damage;
        if (life <= 0)
        {
            Die();
        }
        else
        {
            Hit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {

            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            TakeDamage(bullet.damage);
            ShowDamage(bullet.damage);
            Destroy(collision.gameObject);
        }
    }

    private void Die()
    {
        isDead = true;
        // Ajouter ici les logiques de mort du joueur (animations, UI, etc.)
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Zombie>().OnDeath?.Invoke();
        GetComponent<Animator>().Play("ZombieDie");
        DropMoney();
        Destroy(gameObject, 1f);
    }

    private void Hit()
    {
        _spriteRenderer.color = Color.pink;
        Invoke("ResetColor", 0.3f);
    }

    private void ResetColor()
    {
        _spriteRenderer.color = _originalColor;
    }

    void ShowDamage(int damage)
    {
        Vector3 spawnPos = transform.position + new Vector3(
            Random.Range(-0.3f, 0.3f),
            Random.Range(-0.3f, 0.3f),
            0f
        );

        GameObject popup = Instantiate(damagePopupPrefab, spawnPos, Quaternion.identity);
        popup.GetComponent<DamagePopup>().Setup(damage);
    }

    private void DropMoney()
    {
        // 0.1% de chance de drop une conserve de nourriture
        if (Random.value < 0.001f)
        {
            Instantiate(foodCanPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(moneyPrefab, transform.position, Quaternion.identity);
        }
    }
}
