using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour
{
    public float reduceCostTimer;
    public GameObject doorGo;
    private Door _door;
    private bool _isInZone;
    public GameObject panelOpenDoorWatch;
    public GameObject panelOpenDoorPhone;
    public GameObject inventoryGO;
    private Inventory _inventory;
    public bool isScenario = false;

    void Awake()
    {
        _door = doorGo.GetComponent<Door>();
        _inventory = inventoryGO.GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isInZone = true;
            if (isScenario)
            {
                StartCoroutine(PayDoor());
                return;
            }
            
            if (_inventory.killCount > 0)
            {
                // Assez d'argent
                if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
                {
                    panelOpenDoorWatch.SetActive(false);
                }
                else
                {
                    panelOpenDoorPhone.SetActive(false);
                }
                StartCoroutine(PayDoor());
            }
            else
            {
                if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
                {
                    panelOpenDoorWatch.SetActive(true);
                }
                else
                {
                    panelOpenDoorPhone.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
            {
                panelOpenDoorWatch.SetActive(false);
            }
            else
            {
                panelOpenDoorPhone.SetActive(false);
            }
            _isInZone = false;
        }
    }

    IEnumerator PayDoor()
    {
        while (_isInZone)
        {
            yield return new WaitForSeconds(reduceCostTimer);
            _door.ReduceCost();
            if (_door.isOpen)
            {
                _isInZone = false;
                gameObject.SetActive(false);
            }
        }
    }
}
