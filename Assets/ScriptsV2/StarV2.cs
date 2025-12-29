using UnityEngine;

public class StarV2 : MonoBehaviour
{
    public ExitV2 exit;
    private float amplitude = 0.5f;
    private float speed = 2f;
    private float startY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startY = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        // Rebond normal
        float newY = startY + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            exit.starIsDone = true;
            Destroy(gameObject);
        }
    }
}
