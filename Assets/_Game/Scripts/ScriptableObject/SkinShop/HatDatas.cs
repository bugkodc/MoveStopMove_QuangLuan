using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HatDatas", menuName = "Game/HatDatas", order = 0)]
public class HatDatas : ScriptableObject
{
    [SerializeField] List<HatData> listData;

    public GameObject GetPrefab(int indexItem)
    {
        for (int i = 0; i < listData.Count; i++)
        {
            if (listData[i].indexItem == indexItem)
            {
                return listData[i].prefab;
            }
        }
        return null;
    }
}
