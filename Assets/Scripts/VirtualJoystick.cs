using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform touchArea;
    public RectTransform background;
    public RectTransform handle;

    public float handleLimit = 1f;

    private Vector2 inputVector = Vector2.zero;
    public Vector2 Direction => inputVector;

    public void OnPointerDown(PointerEventData eventData)
    {
        // Active le cercle du joystick
        background.gameObject.SetActive(true);

        // Place le joystick là où l'on touche
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            touchArea,
            eventData.position,
            null,
            out pos
        );

        background.anchoredPosition = pos;

        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,
            eventData.position,
            null,
            out pos
        );

        // Normalisation sur la taille du joystick
        pos /= background.sizeDelta;

        // pos devient l’input brut
        inputVector = new Vector2(pos.x * 2, pos.y * 2);
        if (inputVector.magnitude > 1)
            inputVector = inputVector.normalized;

        // Déplace le handle
        handle.anchoredPosition = (inputVector * (background.sizeDelta / 2f) * handleLimit);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
        background.gameObject.SetActive(false);
    }
}
