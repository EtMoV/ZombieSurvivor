using UnityEngine;

public class ZombieLife : MonoBehaviour
{
    public bool isDead = false;
    public int life;
    public GameObject damagePopupPrefab;
    public GameObject moneyPrefab;
    public GameObject foodCanPrefab;
    public ParticleSystem bloodHitEffectPrefab;
    public ParticleSystem bloodDeathEffectPrefab;
    public GameObject bloodDecalPrefab;
    public Sprite[] bloodDecalSprites;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    public void TakeDamage(int damage, Vector3 hitDirection)
    {
        if (isDead) return;

        life -= damage;
        if (life <= 0)
        {
            Die(hitDirection);
        }
        else
        {
            Hit(hitDirection);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {

            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            Vector3 hitDirection = (transform.position - collision.transform.position).normalized;
            TakeDamage(bullet.damage, hitDirection);
            ShowDamage(bullet.damage);
            Destroy(collision.gameObject);
        }
    }

    private void Die(Vector3 hitDirection)
    {
        isDead = true;
        // Ajouter ici les logiques de mort du joueur (animations, UI, etc.)
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Zombie>().OnDeath?.Invoke();
        GetComponent<Animator>().Play("ZombieDie");
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, hitDirection);
        Instantiate(bloodDeathEffectPrefab, transform.position, rot);
        DropMoney();
        SpawnBloodDecal();
        SpawnBloodDecal();
        Destroy(gameObject, 1f);
    }

    private void Hit(Vector3 hitDirection)
    {
        _spriteRenderer.color = Color.pink;
        Invoke("ResetColor", 0.3f);
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, hitDirection);
        Instantiate(bloodHitEffectPrefab, transform.position, rot);
        // Lors d'un coup
        if (Random.value < 0.3f)
        {
            SpawnBloodDecal();
        }
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

    private void SpawnBloodDecal()
    {
        Vector3 pos = transform.position;
        pos.z = 0.1f; // légèrement au-dessus du sol

        GameObject decal = Instantiate(bloodDecalPrefab, pos, Quaternion.identity);

        // Choisir un sprite aléatoire si tu as plusieurs decals
        if (bloodDecalSprites.Length > 0)
        {
            SpriteRenderer sr = decal.GetComponent<SpriteRenderer>();
            sr.sprite = bloodDecalSprites[Random.Range(0, bloodDecalSprites.Length)];
        }
    }

}
