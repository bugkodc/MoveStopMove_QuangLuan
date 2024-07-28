using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WeaponMaterialDatas
{
    public int numberMaterial;
    [SerializeField] private EWeaponType weaponType;
    [SerializeField] private List<WeaponMaterialData> weaponMaterialDatas;

    public List<Material> GetMaterial(int index)
    {
        numberMaterial = weaponMaterialDatas.Count;
        for (int i = 0; i < numberMaterial; i++)
        {
            if (index == i)
            {
                return weaponMaterialDatas[i].materials;

            }
        }
        return null;
    }
}


