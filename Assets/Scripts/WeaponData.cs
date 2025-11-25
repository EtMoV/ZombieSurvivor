using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Items/WeaponData")]
public class WeaponData : ScriptableObject
{
    public Sprite sprite;
    public string typeFire;
    public float attackSpeed;
    public float damage;
}
