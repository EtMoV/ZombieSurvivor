using UnityEngine;

public class CanvaManager : MonoBehaviour
{
    public GameObject panelWatch;
    public GameObject panelPhone;
    public static bool? isWatch = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (isWatch == null)
        {

            float aspect = (float)Screen.width / Screen.height;
            if (aspect >= 0.95f && aspect <= 1.05f) // Carré / montre
            {
                panelPhone.SetActive(false);
                panelWatch.SetActive(true);
                isWatch = true;
            }
            else
            {
                panelPhone.SetActive(true);
                panelWatch.SetActive(false);
                isWatch = false;
            }
        }
        else
        {
            if (isWatch == true)
            {
                panelPhone.SetActive(false);
                panelWatch.SetActive(true);
                isWatch = true;
            }
            else
            {
                panelPhone.SetActive(true);
                panelWatch.SetActive(false);
                isWatch = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
