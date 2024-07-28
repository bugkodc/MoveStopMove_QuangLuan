using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponDatas : ScriptableObject
{
    [SerializeField] private List<WeaponData> weapons;

    public Weapon GetWeaponPrefab(EWeaponType eWeaponType)
    {

        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].weaponType == eWeaponType)
            {
                return weapons[i].weaponPrefab;
            }
        }
        return null;
    }

    public Bullet GetBulletPrefab(EWeaponType eWeaponType)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].weaponType == eWeaponType)
            {
                return weapons[i].bulletPrefab;
            }
        }
        return null;
    }
}
