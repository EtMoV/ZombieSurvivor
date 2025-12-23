using System.Collections;
using TMPro;
using UnityEngine;

public class DoorInput : MonoBehaviour
{
    public GameObject doorGo;
    public GameObject costGo;
    public GameObject textCostGo;
    public GameObject playerGo;
    public int doorCost;
    public float reduceCostTimer;
    private bool isOpen = false;
    private PlayerInventory playerInventory;
    private TextMeshPro textCost;
    private bool isInZone = false;

    void Start()
    {
        playerInventory = playerGo.GetComponent<PlayerInventory>();
        textCost = textCostGo.GetComponent<TextMeshPro>();
        UpdateCostUI();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = true;
            StartCoroutine(PayDoor());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = false;
        }
    }

    private IEnumerator PayDoor()
    {
        while (isInZone)
        {
            yield return new WaitForSeconds(reduceCostTimer);
            ReduceCost();
        }
    }

    private void ReduceCost()
    {
        if (doorCost > 0)
        {
            bool reduceCostOp = playerInventory.ReduceMoney();
            if (reduceCostOp)
            {
                doorCost--;
                UpdateCostUI();
            }
        }
        else
        {
            if (isOpen) return;

            isOpen = true;
            Destroy(doorGo);
            Destroy(costGo);
            Destroy(gameObject);
        }
    }

    private void UpdateCostUI()
    {
        textCost.text = doorCost.ToString();
    }
}
