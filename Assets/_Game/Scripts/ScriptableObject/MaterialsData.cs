using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Game/MaterialData")]
public class MaterialsData<T> : ScriptableObject
{
    [SerializeField] List<T> materialDatas;
}
