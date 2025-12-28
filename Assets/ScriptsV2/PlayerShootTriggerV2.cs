using UnityEngine;

public class PlayerShootTrigger : MonoBehaviour
{
    private PlayerV2 player;
    private float shootTimer;
    void Start()
    {
        player = GetComponentInParent<PlayerV2>();

        if (player == null)
            Debug.LogError("PlayerV2 introuvable dans le parent !");
    }

    void Update()
    {
        if (shootTimer > 0f)
            shootTimer -= Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (player == null) return;
        if (!player.canShoot) return;
        if (shootTimer > 0f) return;
        if (!player.isDead)
        {
            if (other.CompareTag("Zombie"))
            {
                ZombieHealthV2 zombie = other.GetComponent<ZombieHealthV2>();
                if (zombie != null)
                {
                    player.animator.Play("Player-shot");
                    zombie.TakeDamage(player.damagePerShot);
                    shootTimer = player.shootInterval;
                }
            }
        }
    }
}
