using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    public GameObject bulletPrefab;

    public GameObject InstantiateBullet(Vector2 position, Quaternion rotation)
    {
        GameObject GoInstantiate;
        GoInstantiate = Instantiate(bulletPrefab, position, rotation);
        return GoInstantiate;
    }
}