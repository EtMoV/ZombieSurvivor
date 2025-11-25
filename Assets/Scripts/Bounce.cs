using UnityEngine;

public class Bounce : MonoBehaviour
{
    private float _amplitude; // hauteur du rebond
    private float _frequency;   // vitesse du rebond

    private Vector3 startPos;

    void Start()
    {
        _amplitude = 5f;
        _frequency = 1f;
        startPos = transform.localPosition;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * _frequency) * _amplitude;
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }
}
