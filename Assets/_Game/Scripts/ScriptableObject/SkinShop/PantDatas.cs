using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PantDatas", menuName = "Game/PantDatas", order = 0)]
public class PantDatas : ScriptableObject 
{
   [SerializeField] private List<PantData> listData;
   public Material GetMaterial(int index)
   {
    for(int i =0; i<listData.Count; i++)
    {
        if(listData[i].indexItem == index)
        {
            return listData[i].material;
        }
    }
    return null;
   }
}
