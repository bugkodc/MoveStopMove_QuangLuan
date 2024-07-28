using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/BodyMaterialDatas")]
public class BodyMaterialDatas : ScriptableObject
{
    [SerializeField] private List<BodyMaterialData> bodyMaterialDatas;

    public Material GetMaterial(EBodyMaterialType eBodyMaterialType)
    {
        for (int i = 0; i < bodyMaterialDatas.Count; i++)
        {
            if (bodyMaterialDatas[i].bodyType == eBodyMaterialType)
            {
                return bodyMaterialDatas[i].material;
            }
        }
        return null;
    }
}