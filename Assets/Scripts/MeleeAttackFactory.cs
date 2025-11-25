using UnityEngine;

public class MeleeAttackFactory : MonoBehaviour
{
    public GameObject meleeAttackPrefab;
    public GameObject inventoryGo;
    private Inventory _inventory;

    public Sprite spriteGlace;
    public Sprite spriteFeu;
    public Sprite spriteElec;

    public void Awake()
    {
        _inventory = inventoryGo.GetComponent<Inventory>();
    }
    public MeleeAttack InstantiateMeleeAttack(Vector2 position, Quaternion rotation)
    {
        GameObject GoInstantiate = Instantiate(meleeAttackPrefab, position, rotation);
        MeleeAttack meleeAttackInstantiate = GoInstantiate.GetComponent<MeleeAttack>();

        // Ajout des differents type de balles
        if (_inventory.bulletGlace > 0)
        {
            meleeAttackInstantiate.types.Add(new BulletType("glace", _inventory.bulletGlace));
            GoInstantiate.transform.Find("SpriteChild").GetComponent<SpriteRenderer>().sprite = spriteGlace;
        }

        if (_inventory.bulletFeu > 0)
        {
            meleeAttackInstantiate.types.Add(new BulletType("feu", _inventory.bulletFeu));
            GoInstantiate.transform.Find("SpriteChild").GetComponent<SpriteRenderer>().sprite = spriteFeu;
        }

        if (_inventory.bulletElec > 0)
        {
            meleeAttackInstantiate.types.Add(new BulletType("elec", _inventory.bulletElec));
            GoInstantiate.transform.Find("SpriteChild").GetComponent<SpriteRenderer>().sprite = spriteElec;
        }

        return meleeAttackInstantiate;
    }
}