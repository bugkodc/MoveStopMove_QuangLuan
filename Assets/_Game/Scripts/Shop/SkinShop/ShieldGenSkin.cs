using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenSkin : GenSkin
{
    [SerializeField] private ShieldDatas shieldData;
    private GameObject prefab;
    private GameObject shield;
    private Dictionary<GameObject, GameObject> dictHat = new Dictionary<GameObject, GameObject>();
    private Transform ShieldTF;

    protected override void Awake()
    {
        base.Awake();
        PresentSkin.Instance.listGenSkin[2] = this;
        button.onClick.AddListener(Select);
    }
    private void Start()
    {
        indexType = (int)ESkinType.Shield;
        ShieldTF = player.ShieldTF;
        PresentSkin.Instance.listGenSkin[2] = this;

    }
    public override void SpawnSkin(ESkinType iType, int indexItem)
    {

        prefab = shieldData.GetPrefab(indexItem);
        if (prefab == null) return;
        if (ShieldTF != null)
        {
            RefeshObj(ShieldTF);
        }
        SetShieldSkin(prefab);
    }
    public void SetShieldSkin(GameObject prefab)
    {
        if (dictHat.ContainsKey(prefab))
        {
            dictHat[prefab].SetActive(true);
        }
        else
        {
            shield = Instantiate(prefab, player.ShieldTF);
            dictHat.Add(prefab, shield);
        }
    }
    public override void DespawnSkin(ESkinType iType, int indexItem)
    {
        prefab = shieldData.GetPrefab(indexItem);
        dictHat[prefab].SetActive(false);
    }

    public override void Select()
    {
        Debug.Log("Shield");
        PresentSkin.Instance.currentSkin = this;
    }

}
