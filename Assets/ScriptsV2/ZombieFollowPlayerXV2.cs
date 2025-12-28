using UnityEngine;

public class ZombieFollowPlayerXV2 : MonoBehaviour
{
    [Header("Movement")]
    public Transform player;
    public float speed = 2f;
    public float stopDistance = 0.1f;

    [Header("Detection Cone")]
    public float detectionDistance = 5f;
    [Range(0, 180)]
    public float detectionAngle = 45f; // demi-angle

    private bool playerDetected = false;

    void Update()
    {
        if (player == null) return;

        // Si le player n'a pas encore été détecté
        if (!playerDetected)
        {
            Vector3 dirToPlayer = player.position - transform.position;
            float distanceToPlayer = dirToPlayer.magnitude;

            // Vérifie si player est dans le rayon
            if (distanceToPlayer <= detectionDistance)
            {
                float angleToPlayer = Vector3.Angle(transform.right, dirToPlayer);
                if (angleToPlayer <= detectionAngle)
                {
                    playerDetected = true; // le zombie détecte le player
                }
            }
        }

        // Si le player a été détecté → suit le player indéfiniment
        if (playerDetected)
        {
            float targetX = player.position.x;
            float currentX = transform.position.x;
            float distanceX = Mathf.Abs(targetX - currentX);

            if (distanceX > stopDistance)
            {
                float direction = Mathf.Sign(targetX - currentX);

                Vector3 pos = transform.position;
                pos.x += direction * speed * Time.deltaTime;
                transform.position = pos;

                // Flip du sprite
                transform.localScale = new Vector3(
                    direction > 0 ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
            }
        }
    }

    // ---------------- Gizmo pour visualiser le cône ----------------
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 origin = transform.position;
        Gizmos.DrawWireSphere(origin, detectionDistance);

        int rayCount = 20;
        float angleStep = detectionAngle * 2 / rayCount;

        for (int i = 0; i <= rayCount; i++)
        {
            float angle = -detectionAngle + i * angleStep;
            float rad = Mathf.Deg2Rad * angle;
            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
            Gizmos.DrawLine(origin, origin + dir * detectionDistance);
        }
    }
}
