using UnityEngine;

public class CameraFollowV2 : MonoBehaviour
{
    public Transform target;     // CameraTarget (pas le player direct)
    public float smoothSpeed = 5f;
    public Vector2 offset;       // ajustable dans l'inspector

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}
