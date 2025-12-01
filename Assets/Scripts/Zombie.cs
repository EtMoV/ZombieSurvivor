using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public System.Action OnDeath;
    private float moveSpeed = 1.5f;
    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    public Inventory inventory;
    public Vector2 knockbackVelocity;
    public float knockbackDecay = 10f;
    public bool isDead = false;
    public float life;
    private Animator animator;
    private bool _isFlashing;

    [Header("Pathfinding")]
    public Pathfinding pathfinding;
    private List<GridManager.Node> currentPath;
    public bool isBoss;

    private float attackTimer = 0f;
    private float attackRate = 1f;

    private bool attackingBarricade = false;
    private Barricade targetBarricade;

    private float maxDistance = 20f;

    public RoundManager roundManager;

    private float pathUpdateTimer = 0f;
    private float pathUpdateInterval = 0.5f;

    private Vector3 lastPosition;
    private int stuckFrames = 0;
    private int maxStuckFrames = 10; // nombre de frames avant de considérer que le zombie est bloqué

    public Sprite zombieBossSprite;

    public GameObject xpPrefab;

    public GameObject lootPrefab;

    public bool isScenario = false;

    public bool hasSpawnFromSpawnPoint = false;

    public RuntimeAnimatorController fireAnimatorController;
    public Sprite iceSprite;

    public GameObject bonusGetPrefab;

    public Sprite attackSpeedBonusSprite;
    public Sprite lifeBonusSprite;
    public Sprite speedBonusSprite;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        _originalColor = _spriteRenderer.color;
        _isFlashing = false;
    }

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        if (pathfinding == null)
            pathfinding = FindFirstObjectByType<Pathfinding>();

        if (isBoss)
        {
            animator.Play("ZombieBossRun");
        }
    }

    void FixedUpdate()
    {
        if (player == null || isDead || player.gameObject.GetComponent<PlayerController>().isDead) return;

        // 🔹 Auto-destruction si trop loin SI ca ne vient PAS d'un SpawnPoint
        if (!hasSpawnFromSpawnPoint)
        {
            CheckDistanceToPlayer();
        }

        // 🔹 Gestion attaque barricade
        if (attackingBarricade && targetBarricade != null)
        {
            HandleBarricadeAttack();
            return;
        }

        Vector2 dirToPlayer = player.position - transform.position;
        bool canSeePlayer = !Physics2D.Raycast(transform.position, dirToPlayer.normalized,
                                               dirToPlayer.magnitude, pathfinding.obstacleMask);

        if (canSeePlayer)
        {
            // ✅ Fonce droit sur le joueur
            currentPath = null;
            rb.linearVelocity = dirToPlayer.normalized * moveSpeed + knockbackVelocity;
            _spriteRenderer.flipX = player.position.x < transform.position.x;
        }
        else
        {
            // 🔹 Pathfinding si le joueur n'est pas visible
            pathUpdateTimer += Time.fixedDeltaTime;
            if (pathUpdateTimer >= pathUpdateInterval)
            {
                pathUpdateTimer = 0f;
                PathfindingManager.Instance.RequestPath(transform.position, player.position, OnPathFound);
            }

            // 🔹 Déplacement le long du chemin
            MoveAlongPath();
        }

        knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.zero, knockbackDecay * Time.fixedDeltaTime);
    }

    private void OnPathFound(List<GridManager.Node> newPath)
    {
        if (isDead || !gameObject.activeInHierarchy) return;
        currentPath = newPath;
    }

    private void MoveAlongPath()
    {
        if (currentPath == null || currentPath.Count == 0) return;

        Vector3 nextPos = currentPath[0].worldPosition;
        Vector2 moveDir = ((Vector2)nextPos - (Vector2)transform.position).normalized;

        rb.linearVelocity = moveDir * moveSpeed + knockbackVelocity;
        knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.zero, knockbackDecay * Time.fixedDeltaTime);
        _spriteRenderer.flipX = player.position.x < transform.position.x;

        // Supprime le node si proche
        if (Vector2.Distance(transform.position, nextPos) < 0.1f)
            currentPath.RemoveAt(0);

        // Détection si bloqué
        if (Vector3.Distance(transform.position, lastPosition) < 0.01f)
            stuckFrames++;
        else
            stuckFrames = 0;

        lastPosition = transform.position;

        // Si bloqué, débloquer en cherchant un voisin libre **sur le chemin ou vers le chemin**
        if (stuckFrames >= maxStuckFrames)
        {
            GridManager.Node currentNode = pathfinding.gridManager.NodeFromWorldPoint(transform.position);
            GridManager.Node bestNode = null;
            float minDistToPath = float.MaxValue;

            foreach (var neighbor in pathfinding.gridManager.GetNeighbors(currentNode))
            {
                if (!neighbor.walkable) continue;

                // Distance au prochain node du chemin officiel
                float distToPath = Vector2.Distance(neighbor.worldPosition, currentPath[0].worldPosition);
                if (distToPath < minDistToPath)
                {
                    minDistToPath = distToPath;
                    bestNode = neighbor;
                }
            }

            if (bestNode != null)
            {
                currentPath.Insert(0, bestNode);
                stuckFrames = 0;
            }
        }
    }

    // Ca tue les zombies dehors qui ne sont pas de l'arene comme ca, il n'y a que les zombies de l'arene a l'interieur de l'arene
    public void autoKillArena()
    {
        roundManager.currentRound.currentNbZombieSpawn -= 1;
        Destroy(gameObject);
    }

    private void CheckDistanceToPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > maxDistance && roundManager != null)
        {
            roundManager.currentRound.currentNbZombieSpawn -= 1;
            Destroy(gameObject);
        }
    }

    private void HandleBarricadeAttack()
    {
        rb.linearVelocity = Vector2.zero;
        attackTimer += Time.fixedDeltaTime;

        if (attackTimer >= attackRate)
        {
            attackTimer = 0f;
            if (targetBarricade != null)
            {
                targetBarricade.reduceLife();
                animator.SetBool("IsRunning", false);
            }
            else
            {
                animator.SetBool("IsRunning", true);
                attackingBarricade = false;
            }
        }
    }

    public void StopAttackingBarricade()
    {
        targetBarricade = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {

            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            // On applique les effets de la balles
            foreach (BulletType bulletType in bullet.types)
            {
                switch (bulletType.type)
                {
                    case "glace":
                        moveSpeed = moveSpeed - (0.3f * bulletType.lvl);
                        _spriteRenderer.color = Color.blue; // On met le zombie en bleu
                        GameObject effectOnZombie = transform.Find("Effect").gameObject;
                        effectOnZombie.GetComponent<SpriteRenderer>().sprite = iceSprite;
                        break;
                    case "feu":
                        StartCoroutine(OnFire(bulletType.lvl));
                        break;
                    case "elec":
                        StartCoroutine(FlashHitYellow());
                        Zombie closest = FindClosestZombie(this, null);
                        // Si un autre zombie existe → déclenche effet
                        if (closest != null)
                        {
                            closest.Electrocute(bulletType.lvl - 1, this);  // méthode à déclencher en cas d'electrocution
                        }
                        break;
                }
            }

            if (bullet.isRocket)
            {
                // Degat de zone
                ActivateNearestZombies(this, 2.5f);
            }

            // On applique le damage au zombie courant
            life -= bullet.damage;


            if (!bullet.isRocket)
            {
                // Destruction de la balle si pas rocket
                Destroy(collision.gameObject);
            }

            if (life <= 0)
                Die();
            else if (bullet.types.Count == 0)
                StartCoroutine(FlashHit());
        }
        else if (collision.gameObject.CompareTag("CAC"))
        {

            MeleeAttack meleeAttack = collision.gameObject.GetComponent<MeleeAttack>();

            // On applique le damage
            life -= meleeAttack.damage;

            foreach (BulletType bulletType in meleeAttack.types)
            {
                switch (bulletType.type)
                {
                    case "glace":
                        moveSpeed = moveSpeed - (0.3f * bulletType.lvl);
                        _spriteRenderer.color = Color.blue; // On met le zombie en bleu
                        GameObject effectOnZombie = transform.Find("Effect").gameObject;
                        effectOnZombie.GetComponent<SpriteRenderer>().sprite = iceSprite;
                        break;
                    case "feu":
                        StartCoroutine(OnFire(bulletType.lvl));
                        break;
                    case "elec":
                        StartCoroutine(FlashHitYellow());
                        Zombie closest = FindClosestZombie(this, null);
                        // Si un autre zombie existe → déclenche effet
                        if (closest != null)
                        {
                            closest.Electrocute(bulletType.lvl - 1, this);  // méthode à déclencher en cas d'electrocution
                        }
                        break;
                }
            }

            if (life <= 0)
                Die();
            else if (meleeAttack.types.Count == 0)
                StartCoroutine(FlashHit());
        }
        else if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
            playerScript.Die();
        }
        else if (collision.gameObject.CompareTag("Barricade"))
        {
            targetBarricade = collision.gameObject.GetComponent<Barricade>();
            if (targetBarricade != null)
            {
                attackingBarricade = true;
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    private Zombie FindClosestZombie(Zombie origin, Zombie previousOrigin)
    {
        // Trouver tous les zombies de la scène
        Zombie[] allZombies = FindObjectsByType<Zombie>(FindObjectsSortMode.None);

        Zombie closest = null;
        float minDist = Mathf.Infinity;

        foreach (var z in allZombies)
        {
            if (z == origin || z == previousOrigin) continue; // on évite celui touché ou celui qui a transmis precedement

            float dist = Vector3.Distance(origin.transform.position, z.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                closest = z;
            }
        }

        return closest;
    }

    private void ActivateNearestZombies(Zombie origin, float radius)
    {
        // On récupère tous les zombies actifs dans la scène
        Zombie[] allZombies = FindObjectsByType<Zombie>(FindObjectsSortMode.None);

        Vector3 originPos = origin.transform.position;

        foreach (var z in allZombies)
        {
            // Ignore le zombie d’origine ou ceux morts
            if (z == origin || z.isDead)
                continue;

            float dist = Vector3.Distance(originPos, z.transform.position);

            // On garde seulement ceux dans le rayon défini
            if (dist <= radius)
            {
                z.TakeHitRocket();
            }
        }

    }


    public void createBoss()
    {
        transform.localScale = new Vector3(2f, 2f, 1f);
        life *= 2;
        isBoss = true;
        GetComponent<SpriteRenderer>().sprite = zombieBossSprite;
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        knockbackVelocity = direction.normalized * force;
    }

    public void Die()
    {
        if (isDead) return;
        inventory.killCount++;
        inventory.totalKillCount++;
        isDead = true;
        currentPath = null;
        pathUpdateTimer = 0f;
        inventory.updateKillCountUI();
        OnDeath?.Invoke();

        if (!isScenario)
        {
            // Instanciation de l'xp
            XP xpInstantiate = Instantiate(xpPrefab, transform.position, Quaternion.identity).GetComponent<XP>();
            xpInstantiate.inventory = inventory;

            // Instanciation du loot si boss
            /*if (isBoss)
            {
                Loot lootInstantiate = Instantiate(lootPrefab, transform.position, Quaternion.identity).GetComponent<Loot>();
                lootInstantiate.inventory = inventory;
            }*/

            // Generate random bonus
            if (Random.value < 0.05f)
            {
                string[] options = { "attackSpeed", "life", "speed" };
                string choice = options[Random.Range(0, options.Length)];
                BonusGet bonusInstantiate = Instantiate(bonusGetPrefab, transform.position, Quaternion.identity).GetComponent<BonusGet>();
                bonusInstantiate.inventory = inventory;
                switch (choice)
                {
                    case "attackSpeed":
                        bonusInstantiate.bonusType = "attackSpeed";
                        bonusInstantiate.bonusSprite = attackSpeedBonusSprite;
                        bonusInstantiate.transform.localScale = new Vector3(2f, 2f, 2f);
                        bonusInstantiate.updateSprite();
                        break;
                    case "life":
                        bonusInstantiate.bonusType = "life";
                        bonusInstantiate.bonusSprite = lifeBonusSprite;
                        bonusInstantiate.updateSprite();
                        break;
                    case "speed":
                        bonusInstantiate.bonusType = "speed";
                        bonusInstantiate.bonusSprite = speedBonusSprite;
                        bonusInstantiate.updateSprite();
                        bonusInstantiate.transform.localScale = new Vector3(2f, 2f, 2f);
                        break;
                }

            }
        }

        // Desactivation du collider
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        if (rb != null) rb.simulated = false;

        if (isBoss)
        {
            animator.Play("ZombieBossDie");
        }
        else
        {
            animator.Play("ZombieDie");
        }
        Destroy(gameObject, 0.5f);
    }

    private IEnumerator FlashHit()
    {
        if (_isFlashing) yield break;
        _isFlashing = true;

        _spriteRenderer.color = Color.pink;
        yield return new WaitForSeconds(0.3f);
        _spriteRenderer.color = _originalColor;

        _isFlashing = false;
    }

    private IEnumerator OnFire(float damage)
    {
        // Pendant 5 secondes le dot
        GameObject effectOnZombie = transform.Find("Effect").gameObject;
        Animator anim = effectOnZombie.GetComponent<Animator>();
        anim.runtimeAnimatorController = fireAnimatorController; // ton controller

        _spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(2f);
        life -= damage;
        if (life <= 0)
            Die();
        yield return new WaitForSeconds(2f);
        life -= damage;
        if (life <= 0)
            Die();
        yield return new WaitForSeconds(2f);
        life -= damage;
        if (life <= 0)
            Die();
        yield return new WaitForSeconds(2f);
        life -= damage;
        if (life <= 0)
            Die();
        yield return new WaitForSeconds(2f);
        effectOnZombie.GetComponent<Animator>().runtimeAnimatorController = null;
        effectOnZombie.GetComponent<SpriteRenderer>().sprite = null;
        _spriteRenderer.color = _originalColor;
    }

    public void Electrocute(int lvl, Zombie origin)
    {
        life -= 1f;
        StartCoroutine(FlashHitYellow());
        if (lvl > 0)
        {
            // On electrocute un autre zombie
            Zombie closest = FindClosestZombie(this, origin);
            // Si un autre zombie existe → déclenche effet
            if (closest != null)
            {
                closest.Electrocute(lvl - 1, this);  // méthode à déclencher en cas d'electrocution
            }
            if (life <= 0)
                Die();
        }
    }

    public void TakeHitRocket()
    {
        life -= 1f;
    }

    private IEnumerator FlashHitYellow()
    {
        if (_isFlashing) yield break;
        _isFlashing = true;
        _spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(0.3f);
        _spriteRenderer.color = _originalColor;

        _isFlashing = false;
    }
}
