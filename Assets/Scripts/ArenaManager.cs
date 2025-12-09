
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public bool isActive;

    private int nbZombie = 5;

    public GameObject shopGo;

    public GameObject exitArenaGo;
    public GameObject exitArenaTextGo;
    public Vector3 posEnter = new Vector3(0, 0, 0);

    public Vector3 posEnterArena = new Vector3(5, 0, 0);

    public List<GameObject> arenaObjects = new List<GameObject>();

    public GameObject lootPrefab;

    public GameObject inventoryGo;
    private Inventory _inventory;

    public int nbMaxArena = 4;

    public int nbArenaDone = 0;

    void Awake()
    {
        isActive = false;
        _inventory = inventoryGo.GetComponent<Inventory>();
    }

    public int getZombieCount()
    {
        nbZombie += 1;
        nbZombie = nbZombie * 2;
        return nbZombie;
    }

    public bool getIsBoss()
    {
        return false;
    }

    public void endArena(GameObject lastZombie)
    {
        // Faire spawn un coffre à la fin de l'arène
        //shopGo.SetActive(true);
        nbArenaDone++;

        // Active l'exit de l'arène
        canExit();

        // Spawn XP en explosion depuis le centre
        StartCoroutine(SpawnXPExplosion(lastZombie));

        // Faire spawn la fleche vers le coffre
    }

    private System.Collections.IEnumerator SpawnXPExplosion(GameObject lastZombie)
    {
        int nbXP = 3;
        float maxRadius = 1.5f;
        float angleStep = 360f / nbXP;
        Vector3 centerPos = lastZombie.transform.position;

        for (int i = 0; i < nbXP; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;

            Loot lootInstantiate = Instantiate(lootPrefab, centerPos, Quaternion.identity).GetComponent<Loot>();
            lootInstantiate.inventory = _inventory;

            // Désactiver la physique pour contrôler l'animation
            Rigidbody2D rb = lootInstantiate.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }

            // Animation de déplacement vers l'extérieur en cercle
            StartCoroutine(AnimateXPExplosion(lootInstantiate.transform, centerPos, angle, maxRadius));

            yield return new WaitForSeconds(0.05f);
        }
    }

    private System.Collections.IEnumerator AnimateXPExplosion(Transform xpTransform, Vector3 center, float angle, float distance)
    {
        float elapsedTime = 0f;
        float duration = 0.4f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            // Easing: ralentit progressivement
            float easeProgress = progress * progress;

            float currentDistance = distance * easeProgress;
            Vector3 newPos = center + new Vector3(Mathf.Cos(angle) * currentDistance, Mathf.Sin(angle) * currentDistance, 0);
            xpTransform.position = newPos;

            yield return null;
        }
    }

    public void canExit()
    {
        // Afficher la sortie de l'arène
        exitArenaGo.SetActive(true);
        exitArenaTextGo.SetActive(true);


        // Afficher la fleche qui dirige vers la sortie de l'arène
    }
}
