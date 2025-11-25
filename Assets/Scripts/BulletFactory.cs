using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject inventoryGo;
    private Inventory _inventory;

    public Sprite spriteGlace;
    public Sprite spriteFeu;
    public Sprite spriteElec;

    public Sprite spriteRocket;
    public Sprite spriteBoom;

    public Sprite spriteGrenade;

    public void Awake()
    {
        _inventory = inventoryGo.GetComponent<Inventory>();
    }
    public Bullet InstantiateBullet(Vector2 position, Quaternion rotation, bool isRocket, bool isGrenade)
    {

        GameObject GoInstantiate;
        Bullet bulletInstantiate;
        GoInstantiate = Instantiate(bulletPrefab, position, rotation);
        bulletInstantiate = GoInstantiate.GetComponent<Bullet>();

        // Ajout des differents type de balles
        if (_inventory.bulletGlace > 0)
        {
            bulletInstantiate.types.Add(new BulletType("glace", _inventory.bulletGlace));
            GoInstantiate.transform.Find("SpriteChild").GetComponent<SpriteRenderer>().sprite = spriteGlace;
        }

        if (_inventory.bulletFeu > 0)
        {
            bulletInstantiate.types.Add(new BulletType("feu", _inventory.bulletFeu));
            GoInstantiate.transform.Find("SpriteChild").GetComponent<SpriteRenderer>().sprite = spriteFeu;
        }

        if (_inventory.bulletElec > 0)
        {
            bulletInstantiate.types.Add(new BulletType("elec", _inventory.bulletElec));
            GoInstantiate.transform.Find("SpriteChild").GetComponent<SpriteRenderer>().sprite = spriteElec;
        }

        if (isRocket)
        {
            if (isGrenade)
            {
                GoInstantiate.transform.Find("SpriteChild").GetComponent<SpriteRenderer>().sprite = spriteGrenade;
            }
            else
            {
                GoInstantiate.transform.Find("SpriteChild").GetComponent<SpriteRenderer>().sprite = spriteRocket;
            }
            bulletInstantiate.isRocket = true;
            bulletInstantiate.spriteBoom = spriteBoom;
            bulletInstantiate.transform.localScale = new Vector3(2f, 2f, 1f);

        }

        return bulletInstantiate;
    }
}