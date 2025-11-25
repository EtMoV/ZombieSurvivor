using UnityEngine;

public class Barricade : MonoBehaviour
{
    public int life = 5;
    public GameObject repairBarricadeGo;

    private bool originalSRState;
    private bool originalColliderState;

    void Awake()
    {
        // Sauvegarde de l'état initial
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) originalSRState = sr.enabled;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) originalColliderState = col.enabled;
    }

    // Réduction de la vie par les zombies
    public void reduceLife()
    {
        life -= 1;
        if (life <= 0)
        {
            DestroyBarricade();
        }
    }

    private void DestroyBarricade()
    {
        // notifier les zombies proches pour qu'ils reprennent le pathfinding
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 5f);
        foreach (var hit in hits)
        {
            Zombie z = hit.GetComponent<Zombie>();
            if (z != null)
            {
                z.StopAttackingBarricade(); // méthode à implémenter dans Zombie
            }
        }

        // Masquer le visuel et désactiver le collider
        HideObject();

        // On active la zone de reparation
        repairBarricadeGo.SetActive(true);
    }

    private void HideObject()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
    }

    // Restaurer l'état original
    public void RestoreObject()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = originalSRState;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = originalColliderState;
    }
}
