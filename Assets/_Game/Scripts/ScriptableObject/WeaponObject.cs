using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    public EWeaponType weaponType;
    public Transform TF;
    public WeaponDatas weaponDatas;
    public Weapon GetWeaponPrefab(EWeaponType eWeaponType)
    {
        return weaponDatas.GetWeaponPrefab(eWeaponType);
    }
}
