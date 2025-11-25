using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen;
    public int cost;
    public GameObject zombieHeadUIGo;
    public GameObject costTextUIGo;
    private TextMeshProUGUI _costTextUI;
    public GameObject inventoryGO;
    private Inventory _inventory;
    public GameObject zombieSpawnGO;
    private SpawnPoint _spawnPoint;
    public bool isScenario = false;

    public Sprite doorOpenSprite;


    void Awake()
    {
        isOpen = false;
        _costTextUI = costTextUIGo.GetComponent<TextMeshProUGUI>();
        _costTextUI.text = cost.ToString();
        _inventory = inventoryGO.GetComponent<Inventory>();
        _spawnPoint = zombieSpawnGO.GetComponent<SpawnPoint>();
    }

    public void ReduceCost()
    {
        if (isScenario)
        {
            isOpen = true;
            _spawnPoint.isActive = true;
            GetComponent<SpriteRenderer>().sprite = doorOpenSprite;
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject);
            return;
        }

        if (cost > 0)
        {
            if (_inventory.killCount <= 0)
                return;
            else
            {
                _inventory.killCount--;
                _inventory.updateKillCountUI();
                cost--;
                _costTextUI.text = cost.ToString();
            }
        }
        else
        {
            if (!isOpen)
            {
                isOpen = true;
                _costTextUI.text = "";
                Destroy(zombieHeadUIGo);
                Destroy(costTextUIGo);
                _spawnPoint.isActive = true;
                Destroy(gameObject);
            }
        }
    }
}
