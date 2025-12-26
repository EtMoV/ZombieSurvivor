using UnityEngine;

public class ZombieFactory : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject gameplayRoot; // Le parent de tous les objets du jeu (ennemis, joueur, etc.)
    public GameObject damagePopupPrefab;
    public GameObject moneyPrefab;
    public GameObject foodCanPrefab;
    public ParticleSystem bloodHitEffectPrefab;
    public ParticleSystem bloodDeathEffectPrefab;
    public GameObject bloodDecalPrefab;
    public Sprite[] bloodDecalSprites;
    public GameObject InstantiateZombie(Vector2 position, int life)
    {
        GameObject GoInstantiate = Instantiate(zombiePrefab, position, Quaternion.identity, gameplayRoot.transform);
        ZombieLife zombieLifeInstantiate = GoInstantiate.GetComponent<ZombieLife>();
        zombieLifeInstantiate.life = life;
        zombieLifeInstantiate.damagePopupPrefab = damagePopupPrefab;
        zombieLifeInstantiate.moneyPrefab = moneyPrefab;
        zombieLifeInstantiate.foodCanPrefab = foodCanPrefab;
        zombieLifeInstantiate.bloodHitEffectPrefab = bloodHitEffectPrefab;
        zombieLifeInstantiate.bloodDeathEffectPrefab = bloodDeathEffectPrefab;
        zombieLifeInstantiate.bloodDecalPrefab = bloodDecalPrefab;
        zombieLifeInstantiate.bloodDecalSprites = bloodDecalSprites;
        return GoInstantiate;
    }
}