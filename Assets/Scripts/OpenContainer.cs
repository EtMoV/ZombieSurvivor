using UnityEngine;
using System.Collections;

public class OpenContainer : MonoBehaviour
{
    public GameObject containerGo;
    private Container _container;
    public GameObject panelOpenContainerGoWatch;
    public GameObject panelOpenContainerGoPhone;
    private bool _isInZone;
    public float reduceCostTimer;

    void Awake()
    {
        _container = containerGo.GetComponent<Container>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
            {
                panelOpenContainerGoWatch.SetActive(true);
            }
            else
            {
                panelOpenContainerGoPhone.SetActive(true);
            }
            _isInZone = true;

            StartCoroutine(OpenContainerCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CanvaManager.isWatch.HasValue && CanvaManager.isWatch.Value)
            {
                panelOpenContainerGoWatch.SetActive(false);

            }
            else
            {
                panelOpenContainerGoPhone.SetActive(false);
            }
            _isInZone = false;
        }
    }

    IEnumerator OpenContainerCoroutine()
    {
        while (_isInZone)
        {
            yield return new WaitForSeconds(reduceCostTimer);
            _container.ReduceCost();
            if (_container.isOpen)
            {
                _isInZone = false;
                Destroy(gameObject);
            }
        }
    }
}
