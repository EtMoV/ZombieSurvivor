using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float damage;
    public float lifetime = 0.2f;
    public List<BulletType> types;

    void Awake()
    {
        types = new List<BulletType>();
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
