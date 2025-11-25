using TMPro;
using UnityEngine;

public class Container : MonoBehaviour
{
    public bool isOpen;
    public int cost;
    public GameObject inventoryGO;
    private Inventory _inventory;
    public GameObject lootPrefab;

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
                int randomLoots = Random.Range(1, 4); // Nombre aléatoire entre 1 et 3
                for (int i = 0; i < randomLoots; i++)
                {
                    Loot lootInstantiate = Instantiate(lootPrefab, transform.position, Quaternion.identity).GetComponent<Loot>();
                    lootInstantiate.inventory = _inventory;
                }
            }
        }
    }
}
