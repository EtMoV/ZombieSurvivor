using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private float detectionRadius = 7f;
    public GameObject weaponPlayerPos1;
    public Weapon weapon;
    public BulletFactory bulletFactory;

    public void SetWeapon(string nameWeapon)
    {
        weapon = new Weapon(nameWeapon);
        weaponPlayerPos1.GetComponent<SpriteRenderer>().sprite = weapon.weaponData.sprite;

    }
    
    void Start()
    {
        if (ItemManagerState.getLastWeapon() != null)
        {
            SetWeapon(ItemManagerState.getLastWeapon().nameItem);
        }
        else
        {
            // Arme par defaut
            SetWeapon("pistol");
        }
    }

    void FixedUpdate()
    {
        if (weapon.CanFire())
        {
            Fire(weapon);
            weapon.RecordShot();
        }
    }

    // -------------------------------
    // 🔍 Trouver le zombie le plus proche
    // -------------------------------
    private Transform GetClosestZombie(GameObject weaponPlayerPos)
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        Transform closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 posWeapon = weaponPlayerPos.transform.position;

        int wallLayer = LayerMask.GetMask("Wall", "Door");

        foreach (GameObject zombie in zombies)
        {
            if (zombie == null) continue;

            Zombie zScript = zombie.GetComponent<Zombie>();
            if (zScript == null || zScript.GetComponent<ZombieLife>().isDead)
                continue;

            Vector3 zombiePos = zombie.transform.position;
            float distance = Vector3.Distance(posWeapon, zombiePos);

            // 🔹 Vérifie si le zombie est dans la zone de détection
            if (distance > detectionRadius)
                continue;

            // 🔹 Vérifie si un mur/porte/barricade bloque la vue
            RaycastHit2D hit = Physics2D.Linecast(posWeapon, zombiePos, wallLayer);
            if (hit.collider != null)
                continue;

            // 🔹 Aucun obstacle → zombie visible
            if (distance < minDistance)
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
        GameObject weaponPlayerPos = weaponPlayerPos1;

        Transform target = GetClosestZombie(weaponPlayerPos);
        if (target != null)
        {
            Vector2 direction = (target.position - weaponPlayerPos.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Vector2 positionBullet = weaponPlayerPos.transform.position + (Vector3)(direction * 0.5f);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Quaternion rotationWeapon = Quaternion.Euler(0, 0, angle + 90f);

            // Changer la rotation du weaponPlayerPos
            weaponPlayerPos.transform.rotation = rotationWeapon;

            Bullet bullet = bulletFactory.InstantiateBullet(positionBullet, rotation).GetComponent<Bullet>();
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bullet.damage = weapon.weaponData.damage;
        }
    }
}
