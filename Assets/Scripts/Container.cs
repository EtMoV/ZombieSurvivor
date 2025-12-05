using TMPro;
using UnityEngine;

public class Container : MonoBehaviour
{
    public bool isOpen;
    public int cost;
    public GameObject inventoryGO;
    private Inventory _inventory;
    public GameObject xpPrefab;

    public Sprite openSprite;

    void Awake()
    {
        isOpen = false;
        _inventory = inventoryGO.GetComponent<Inventory>();
    }

    public void ReduceCost()
    {
        if (cost > 0)
        {
            cost--;
        }
        else
        {
            if (!isOpen)
            {
                isOpen = true;

                // Update Sprite
                GetComponent<SpriteRenderer>().sprite = openSprite;

                // Instanciation du loot
                int randomLoots = Random.Range(1, 11); // Nombre aléatoire entre 1 et 10
                for (int i = 0; i < randomLoots; i++)
                {
                    XP xpInstantiate = Instantiate(xpPrefab, transform.position, Quaternion.identity).GetComponent<XP>();
                    xpInstantiate.inventory = _inventory;
                }
            }
        }
    }
}
