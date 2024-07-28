using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/ListWeaponMaterials")]
public class ListWeaponMaterials : ScriptableObject
{
    [SerializeField] List<WeaponMaterialDatas> listWeaponMaterialDatas;
    public List<Material> GetMaterial(EWeaponType weaponType, int index)
    {
        for (int i = 0; i < listWeaponMaterialDatas.Count; i++)
        {
            if ((int)weaponType == i)
            {
                return listWeaponMaterialDatas[i].GetMaterial(index);
            }
        }
        return null;
    }
    public WeaponMaterialDatas GetWeaponMaterialDatas(EWeaponType weaponType)
    {
        for (int i = 0; i < listWeaponMaterialDatas.Count; i++)
        {
            if ((int)weaponType == i)
            {
                return listWeaponMaterialDatas[i];
            }
        }
        return null;
    }
}

