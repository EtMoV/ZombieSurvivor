using System.Collections;
using UnityEngine;

public class scavengeInput : MonoBehaviour
{
    public GameObject inventoryGO;
    private Inventory _inventory;
    public float reduceCostTimer;
    public GameObject panelScavengingPhone;
    public int cost;
    public GameObject lootPrefab;
    private bool isOpen = false;
    private bool _isInZone = false;

    void Awake()
    {
        _inventory = inventoryGO.GetComponent<Inventory>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            panelScavengingPhone.SetActive(true);
            _isInZone = true;
            StartCoroutine(OpenContainerCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            panelScavengingPhone.SetActive(false);
            _isInZone = false;
        }
    }

    IEnumerator OpenContainerCoroutine()
    {
        while (_isInZone)
        {
            yield return new WaitForSeconds(reduceCostTimer);
            if (cost > 0)
            {
                cost--;
            }
            else
            {
                isOpen = true;
            }

            if (isOpen)
            {
                // Instanciation du loot
                int randomLoots = Random.Range(1, 3); // Nombre aléatoire entre 1 et 2
                for (int i = 0; i < randomLoots; i++)
                {
                    Loot lootInstantiate = Instantiate(lootPrefab, transform.position, Quaternion.identity).GetComponent<Loot>();
                    lootInstantiate.inventory = _inventory;
                }

                _isInZone = false;
                Destroy(gameObject);
            }
        }
    }
}
