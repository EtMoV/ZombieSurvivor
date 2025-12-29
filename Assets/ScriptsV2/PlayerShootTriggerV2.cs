using UnityEngine;

public class PlayerShootingZone : MonoBehaviour
{
    public PlayerV2 player;          // Référence au Player
    private Collider2D detectionCollider;


    void Start()
    {
        detectionCollider = GetComponent<Collider2D>();
        if (detectionCollider == null)
            Debug.LogError("Pas de Collider2D attaché à DetectionZone !");
    }

    void Update()
    {
    }

    // ---------------- Tir public pour bouton ----------------
    public void OnFire()
    {
        if (player.nbBullet > 0)
        {
            if (player == null) return;
            if (player.isDead) return;

            // Animation du player
            player.animator.Play("Player-shot");
            player.subOneBullet();
            // Overlap avec le même collider
            Collider2D[] hits = new Collider2D[20]; // augmente si besoin

            // Nouvelle API Overlap
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(LayerMask.GetMask("Zombie"));
            filter.useTriggers = true;

            int count = detectionCollider.Overlap(filter, hits);

            for (int i = 0; i < count; i++)
            {
                Collider2D hit = hits[i];
                if (hit == null) continue;

                ZombieHealthV2 zombie = hit.GetComponent<ZombieHealthV2>();
                if (zombie != null)
                {
                    // Applique les dégâts
                    zombie.TakeDamage(player.damagePerShot);
                }
            }
        }
    }

    // ---------------- Gizmo visuel ----------------
    private void OnDrawGizmos()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col == null) return;

        Gizmos.color = Color.red;

        if (col is BoxCollider2D box)
        {
            Gizmos.DrawWireCube(box.bounds.center, box.bounds.size);
        }
        else if (col is CircleCollider2D circle)
        {
            float radius = circle.bounds.extents.x;
            Gizmos.DrawWireSphere(circle.bounds.center, radius);
        }
        else if (col is CapsuleCollider2D capsule)
        {
            Gizmos.DrawWireCube(capsule.bounds.center, capsule.bounds.size);
        }
    }
}
