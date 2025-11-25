using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed;
    private Rigidbody2D rb;
    public float damage;
    public List<BulletType> types;

    public bool isRocket;

    public Sprite spriteBoom;

    void Awake()
    {
        _speed = 10f;
        rb = GetComponent<Rigidbody2D>();
        types = new List<BulletType>();
        isRocket = false;
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
        else if (collision.gameObject.CompareTag("Zombie") && isRocket)
        {
            // Affichage d'un boom puis destruction
            transform.Find("SpriteChild").GetComponent<SpriteRenderer>().sprite = spriteBoom;
            GetComponent<Collider2D>().enabled = false;
            rb.linearVelocity = Vector2.zero;
            Destroy(gameObject, 0.5f);
        }
    }
}
