using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour
{
    public ZombieFactory zombieFactory;
    public float spawnInterval; // Temps entre chaque spawn
    public int pvZombie;
    public int nbZombie;
    private List<GameObject> activeZombies;

    void Start()
    {
        activeZombies = new List<GameObject>();
        StartCoroutine(SpawnZombies());
    }

    public void RelaunchSpawn()
    {
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        // ✅ Spawn des zombies
        while (true)
        {
            if (activeZombies.Count >= nbZombie)
            {
                yield return new WaitUntil(() => activeZombies.Count < nbZombie);
            }

            GameObject newZombie = null;

            newZombie = zombieFactory.InstantiateZombie(transform.position, pvZombie);

            // Stocke la référence pour suivi
            activeZombies.Add(newZombie);

            // Supprime de la liste à la mort
            Zombie zombieScript = newZombie.GetComponent<Zombie>();
            zombieScript.OnDeath += () => { activeZombies.Remove(newZombie); };
            yield return new WaitForSeconds(spawnInterval);
        }

    }
}
