using Unity.Mathematics;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject inventoryGO;
    private Inventory _inventory;
    public BulletFactory bulletFactory;
    [SerializeField] private float detectionRadius = 7f; // 🟢 Zone autour du joueur pour le tir

    public MeleeAttackFactory meleeAttackFactory;

    void Awake()
    {
        _inventory = inventoryGO.GetComponent<Inventory>();
    }

    void FixedUpdate()
    {
        if (_inventory.hasWeapon)
        {
            foreach (Weapon weapon in _inventory.weapons)
            {
                if (weapon.CanFire())
                {
                    if (weapon.weaponData.typeFire == "shotgun")
                    {
                        angleFire(weapon, 4);
                    }
                    else if (weapon.weaponData.typeFire == "cac")
                    {
                        cacFire(weapon);
                    }
                    else if (weapon.weaponData.typeFire == "rocket")
                    {
                        rocketFire(weapon);
                    }
                    else if (weapon.weaponData.typeFire == "grenade")
                    {
                        grenadeFire(weapon);
                    }
                    else
                    {
                        Fire(weapon);
                    }
                    weapon.RecordShot();
                }
            }
        }
    }

    // -------------------------------
    // 🔍 Trouver le zombie le plus proche
    // -------------------------------
    private Transform GetClosestZombie()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        Transform closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 playerPos = transform.position;

        int wallLayer = LayerMask.GetMask("Wall", "Door");

        foreach (GameObject zombie in zombies)
        {
            if (zombie == null) continue;

            Zombie zScript = zombie.GetComponent<Zombie>();
            if (zScript == null || zScript.isDead)
                continue;

            Vector3 zombiePos = zombie.transform.position;
            float distance = Vector3.Distance(playerPos, zombiePos);

            // 🔹 Vérifie si le zombie est dans la zone de détection
            if (distance > detectionRadius + _inventory.range)
                continue;

            // 🔹 Vérifie si un mur/porte/barricade bloque la vue
            RaycastHit2D hit = Physics2D.Linecast(playerPos, zombiePos, wallLayer);
            if (hit.collider != null)
                continue;

            // 🔹 Aucun obstacle → zombie visible
            if (distance < minDistance + _inventory.range)
            {
                minDistance = distance;
                closest = zombie.transform;
            }
        }

        return closest;
    }

    // -------------------------------
    // 🧨 Fonctions de tir
    // -------------------------------
    private void Fire(Weapon weapon)
    {
        // Tire standard
        Transform target = GetClosestZombie();
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Vector2 positionBullet = transform.position + (Vector3)(direction * 0.5f);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            Bullet bullet = bulletFactory.InstantiateBullet(positionBullet, rotation, false, false);
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bullet.damage = weapon.weaponData.damage + _inventory.damage;
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x + _inventory.size, bullet.transform.localScale.y + _inventory.size, 1f);
        }
    }

    private void angleFire(Weapon weapon, int bulletCount)
    {
        Transform target = GetClosestZombie();
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Vector2 basePosition = transform.position + (Vector3)(direction * 0.5f);

        float spread = 20f;
        float radius = 0.2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float offsetAngle = -spread / 2 + i * (spread / (bulletCount - 1));
            float finalAngle = baseAngle + offsetAngle;

            Quaternion rotation = Quaternion.Euler(0, 0, finalAngle);
            Vector2 offset = rotation * Vector2.up * radius;
            Vector2 spawnPos = basePosition + offset;

            Bullet bullet = bulletFactory.InstantiateBullet(spawnPos, rotation, false, false);
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bullet.damage = weapon.weaponData.damage + _inventory.damage;
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x + _inventory.size, bullet.transform.localScale.y + _inventory.size, 1f);
        }
    }

    private void cacFire(Weapon weapon)
    {
        Transform target = GetClosestZombie();
        if (target == null) return;

        Vector2 direction = target != null ?
       (target.position - transform.position).normalized :
       transform.up;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        Vector2 spawnPos = (Vector2)transform.position + direction * 1.5f;
        MeleeAttack melee = meleeAttackFactory.InstantiateMeleeAttack(spawnPos, rotation);
        melee.damage = weapon.weaponData.damage + _inventory.damage;
    }

    private void rocketFire(Weapon weapon)
    {
        // Tire standard
        Transform target = GetClosestZombie();
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Vector2 positionBullet = transform.position + (Vector3)(direction * 0.5f);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            Bullet bullet = bulletFactory.InstantiateBullet(positionBullet, rotation, true, false);
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bullet.damage = weapon.weaponData.damage + _inventory.damage;
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x + _inventory.size, bullet.transform.localScale.y + _inventory.size, 1f);
        }
    }

    private void grenadeFire(Weapon weapon)
    {
        // Tire standard
        Transform target = GetClosestZombie();
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Vector2 positionBullet = transform.position + (Vector3)(direction * 0.5f);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            Bullet bullet = bulletFactory.InstantiateBullet(positionBullet, rotation, true, true);
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bullet.damage = weapon.weaponData.damage + _inventory.damage;
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x + _inventory.size, bullet.transform.localScale.y + _inventory.size, 1f);
        }
    }

    // 🧩 Visualisation dans la scène
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
