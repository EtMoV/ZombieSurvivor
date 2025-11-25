using UnityEngine;
using System.Collections;

public class RepairBarricade : MonoBehaviour
{
    public GameObject barricadeGo;
    private Barricade _barricade;
    public GameObject panelBarricadeGoWatch;
    public GameObject panelBarricadeGoPhone;
    private bool _isInZone;

    void Awake()
    {
        _barricade = barricadeGo.GetComponent<Barricade>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
            {
                panelBarricadeGoWatch.SetActive(true);
            }
            else
            {
                panelBarricadeGoPhone.SetActive(true);
            }
            _isInZone = true;

            StartCoroutine(RepairCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
            {
                panelBarricadeGoWatch.SetActive(false);

            }
            else
            {
                panelBarricadeGoPhone.SetActive(false);
            }
            _isInZone = false;
        }
    }

    IEnumerator RepairCoroutine()
    {
        yield return new WaitForSeconds(3f);
        if (_isInZone)
        {
            // On reaffiche la barricade
            _barricade.life = 5;
            // Afficher le visuel et activer le collider
            _barricade.RestoreObject();

            // On cache la zone car la barricade est up
            gameObject.SetActive(false);
        }
    }
}
