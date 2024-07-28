using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/PresentWeapons")]
public class PresentWeapons : ScriptableObject
{
    public List<PresentWeapon> listDataWeapons;
    public int GetIndexTypeWeapon(PresentWeapon weapon)
    {
        return weapon.indexTypeWeapon;
    }

    public ShopWeaponElement GetPrefabWeapon(int index)
    {
        for (int i = 0; i < listDataWeapons.Count; i++)
        {
            if (listDataWeapons[i].indexTypeWeapon == index)
            {
                return listDataWeapons[i].weaponprefab;
            }
        }
        return null;
    }

    public ShopItemSelect GetPrefabItemSelect(int index)
    {
        for (int i = 0; i < listDataWeapons.Count; i++)
        {
            if (listDataWeapons[i].indexTypeWeapon == index)
            {
                return listDataWeapons[i].itemselectPrefab;
            }
        }
        return null;
    }
    public int GetCountWeapon()
    {
        return listDataWeapons.Count;
    }

}
