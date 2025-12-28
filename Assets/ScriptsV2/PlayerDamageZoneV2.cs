using System.Collections;
using UnityEngine;

public class PlayerDamageZoneV2 : MonoBehaviour
{
    public PlayerV2 player;

    private Coroutine damageCoroutine;

    void Awake()
    {
        if (player == null)
            player = GetComponentInParent<PlayerV2>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zombie") && damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(DamageOverTime());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Zombie") && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    IEnumerator DamageOverTime()
    {
        if (!player.isDead)
        {
            while (true)
            {
                player.animator.Play("Player-hurt");
                player.TakeDamage(player.damagePerTick);
                yield return new WaitForSeconds(player.damageInterval);
            }
        }
    }
}
