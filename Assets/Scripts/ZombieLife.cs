using UnityEngine;

public class ZombieLife : MonoBehaviour
{
    public bool isDead = false;
    public int life;

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        life -= damage;
        if (life <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {

            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            TakeDamage(bullet.damage);
            Destroy(collision.gameObject);
        }
    }

    private void Die()
    {
        isDead = true;
        // Ajouter ici les logiques de mort du joueur (animations, UI, etc.)
        GetComponent<Zombie>().OnDeath?.Invoke();
        GetComponent<Animator>().Play("ZombieDie");
        Destroy(gameObject, 1f); // Détruit le zombie après 2 secondes
    }
}
