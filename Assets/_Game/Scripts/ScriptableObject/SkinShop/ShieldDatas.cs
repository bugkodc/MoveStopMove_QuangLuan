using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ShieldDatas", menuName = "Game/ShieldDatas", order = 0)]
public class ShieldDatas : ScriptableObject 
{
    [SerializeField] List<ShieldData> listData; 

   public GameObject GetPrefab(int indexItem)
   {
    for(int i =0; i<listData.Count; i++)
    {
        if(listData[i].indexItem == indexItem)
        {
            return listData[i].prefab;
        }
    }
    return null;
   }
}
