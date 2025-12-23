using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public float speed = 1f;
    public float lifetime = 1f;

    private TextMeshPro text;

    void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    public void Setup(int damage)
    {
        text.text = damage.ToString();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
