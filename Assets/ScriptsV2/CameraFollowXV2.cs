using UnityEngine;

public class CameraFollowXV2 : MonoBehaviour
{

    public Transform player;
    public float smoothSpeed = 5f;
    public float offsetX = 0f;

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (player == null) return;

        float targetX = player.position.x + offsetX;

        Vector3 targetPosition = new Vector3(
            Mathf.Lerp(transform.position.x, targetX, smoothSpeed * Time.deltaTime),
            fixedY,
            fixedZ
        );

        transform.position = targetPosition;
    }
}
