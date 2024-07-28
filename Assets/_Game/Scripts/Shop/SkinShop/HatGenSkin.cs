using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatGenSkin : GenSkin
{
    [SerializeField] HatDatas hatDatas;
    private GameObject prefab;
    private GameObject Hat;
    private Dictionary<GameObject, GameObject> dictHat = new Dictionary<GameObject, GameObject>();
    private Transform HatTF;

    protected override void Awake()
    {
        base.Awake();
        PresentSkin.Instance.listGenSkin[0] = this;
        button.onClick.AddListener(Select);
        if (player != null)
        {
            HatTF = player.HatTF;
        }
    }
    private void Start()
    {
        indexType = (int)ESkinType.Hat;
        PresentSkin.Instance.currentSkin = this;

    }
    public override void SpawnSkin(ESkinType iType, int indexItem)
    {

        prefab = hatDatas.GetPrefab(indexItem);
        if (prefab == null) return;
        if (HatTF != null)
        {
            RefeshObj(HatTF);
        }
        SetHatSkin(prefab);
    }
    public void SetHatSkin(GameObject prefab)
    {
        if (dictHat.ContainsKey(prefab))
        {
            dictHat[prefab].SetActive(true);
        }
        else
        {
            Hat = Instantiate(prefab, player.HatTF);
            dictHat.Add(prefab, Hat);
        }
    }
    public override void DespawnSkin(ESkinType iType, int indexItem)
    {
        prefab = hatDatas.GetPrefab(indexItem);
        dictHat[prefab].SetActive(false);
    }

    public override void Select()
    {
        Debug.Log("Hat");
        PresentSkin.Instance.currentSkin = this;
    }
}
