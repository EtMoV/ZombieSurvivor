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

        // Determine le type de balle aléatoirement selon les quantités disponibles
        System.Collections.Generic.List<string> availableBulletTypes = new System.Collections.Generic.List<string>();

        // Ajouter chaque type autant de fois que sa quantité (pondération)
        for (int i = 0; i < 5; i++)
            availableBulletTypes.Add("normal"); // Balle normal

        for (int i = 0; i < _inventory.bulletGlace; i++)
            availableBulletTypes.Add("glace");

        for (int i = 0; i < _inventory.bulletFeu; i++)
            availableBulletTypes.Add("feu");

        for (int i = 0; i < _inventory.bulletElec; i++)
            availableBulletTypes.Add("elec");



        // Si au moins un type est disponible, sélectionner aléatoirement
        if (availableBulletTypes.Count > 0)
        {
            string selectedType = availableBulletTypes[Random.Range(0, availableBulletTypes.Count)];

            switch (selectedType)
            {
                case "glace":
                    bulletInstantiate.types.Add(new BulletType("glace", _inventory.bulletGlace));
                    SpriteRenderer glaceSprite = GoInstantiate.transform.Find("SpriteChild").GetComponent<SpriteRenderer>();
                    glaceSprite.sprite = spriteGlace;
                    glaceSprite.color = new Color(0f, 0.8f, 1f); // Bleu cyan éclat
                    break;
                case "feu":
                    bulletInstantiate.types.Add(new BulletType("feu", _inventory.bulletFeu));
                    SpriteRenderer feuSprite = GoInstantiate.transform.Find("SpriteChild").GetComponent<SpriteRenderer>();
                    feuSprite.sprite = spriteFeu;
                    feuSprite.color = new Color(1f, 0.3f, 0f); // Rouge éclatant
                    break;
                case "elec":
                    bulletInstantiate.types.Add(new BulletType("elec", _inventory.bulletElec));
                    SpriteRenderer elecSprite = GoInstantiate.transform.Find("SpriteChild").GetComponent<SpriteRenderer>();
                    elecSprite.sprite = spriteElec;
                    elecSprite.color = new Color(0.8f, 1f, 0.2f); // Jaune-vert éclatant
                    break;
                case "normal":
                    // On ne fait rien
                    break;
            }
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