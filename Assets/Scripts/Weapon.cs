using UnityEngine;

public class Weapon
{
    public WeaponData weaponData;
    public string _name;
    private float _nextFireTime = 0f;

    public Weapon(string dataName)
    {
        WeaponData data = Resources.Load<WeaponData>(dataName);
        weaponData = data;
        _name = dataName;
    }

    public Weapon Clone()
    {
        Weapon clone = new Weapon(_name);
        return clone;
    }

    // -------------------------------
    // ⚙️ Gestion du tir
    // -------------------------------
    public void RecordShot()
    {
        float attackSpeed = weaponData.attackSpeed;
        float fireRate = Mathf.Max(attackSpeed, 0.1f);
        _nextFireTime = Time.time + fireRate;
    }

    public bool CanFire()
    {
        return Time.time >= _nextFireTime;
    }
}
