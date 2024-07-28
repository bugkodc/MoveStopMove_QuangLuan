using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "SkinDataas", menuName = "")]
public class SkinDataas : ScriptableObject
{
    [SerializeField] public List<SkinDataa> listData;

    public GameObject GetHatPrefab(int index)
    {
        for (int i = 0; i < listData.Count; i++)
        {
            if (listData[i].indexItem == index)
            {
                return listData[i].HatPrefab;
            }
        }
        return null;
    }

    public GameObject GetTailPrefab(int index)
    {
        for (int i = 0; i < listData.Count; i++)
        {
            if (listData[i].indexItem == index)
            {
                return listData[i].tailPrefab;
            }
        }
        return null;
    }

    public GameObject GetWingPrefab(int index)
    {
        for (int i = 0; i < listData.Count; i++)
        {
            if (listData[i].indexItem == index)
            {
                return listData[i].wingPrefab;
            }
        }
        return null;
    }

    public Material GetSkinMaterial(int index)
    {
        for (int i = 0; i < listData.Count; i++)
        {
            if (listData[i].indexItem == index)
            {
                return listData[i].SkinMaterial;
            }
        }
        return null;

    }

    public Material GetPantMaterial(int index)
    {
        for (int i = 0; i < listData.Count; i++)
        {
            if (listData[i].indexItem == index)
            {
                return listData[i].PantMaterial;
            }
        }
        return null;

    }
}
