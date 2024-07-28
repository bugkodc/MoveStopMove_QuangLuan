using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/SkinDatas")]
public class SkinDatas : ScriptableObject
{
    [SerializeField] private List<SkinDataObject> listDatas;

    public GameObject GetPrefab(int indexType, int indexIndex)
    {
        for (int i = 0; i < listDatas.Count; i++)
        {
            if (listDatas[i].intType == indexIndex && listDatas[i].indexItem == indexIndex)
            {
                return listDatas[i].prefab;
            }
        }
        return null;
    }


}
