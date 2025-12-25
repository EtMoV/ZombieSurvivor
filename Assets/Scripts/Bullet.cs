using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    private float _speed = 10f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Déplace la bullet dans la direction où elle regarde
        rb.linearVelocity = transform.up * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Shop"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Door"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Barricade"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Weapon"))
        {
            Destroy(gameObject);
        }
    }
}
