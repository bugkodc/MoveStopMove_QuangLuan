using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinGenSkin : GenSkin
{
    [SerializeField] SkinDataas skinData;
    private Dictionary<GameObject, GameObject> dictHat = new Dictionary<GameObject, GameObject>();
    private Dictionary<GameObject, GameObject> dictTail = new Dictionary<GameObject, GameObject>();
    private Dictionary<GameObject, GameObject> dictWing = new Dictionary<GameObject, GameObject>();
    private Transform HatTF;
    private Transform TailTF;
    private Transform WingTF;
    private Material SkinMaterial;
    private Material PantMaterial;

    protected override void Awake()
    {
        base.Awake();
        PresentSkin.Instance.listGenSkin[3] = this;
        button.onClick.AddListener(Select);
        player = LevelManager.Instance.player;

    }
    private void Start()
    {
        indexType = (int)ESkinType.Shield;
        HatTF = player.HatTF;
        TailTF = player.TailTF;
        WingTF = player.WingTF;
        PresentSkin.Instance.listGenSkin[3] = this;

    }

    public override void SpawnSkin(ESkinType iType, int indexItem)
    {
        SkinMaterial = skinData.GetSkinMaterial(indexItem);
        PantMaterial = skinData.GetPantMaterial(indexItem);
        if (SkinMaterial != null && PantMaterial != null)
        {
            player.skinRender.material = SkinMaterial;
            player.pantRender.material = PantMaterial;
        }
        GameObject HatPrefab = skinData.GetHatPrefab(indexItem);
        GameObject TailPrefab = skinData.GetTailPrefab(indexItem);
        GameObject WingPrefab = skinData.GetWingPrefab(indexItem);
        if (HatPrefab != null && TailPrefab != null && WingPrefab != null)
        {
            RefeshObj(player.HatTF);
            RefeshObj(player.TailTF);
            RefeshObj(player.WingTF);
            SpawnObj(dictHat, HatPrefab, player.HatTF);
            SpawnObj(dictTail, TailPrefab, player.TailTF);
            SpawnObj(dictWing, WingPrefab, player.WingTF);
        }
    }


    public override void DespawnSkin(ESkinType iType, int indexItem)
    {
        GameObject HatPrefab = skinData.GetHatPrefab(indexItem);
        GameObject TailPrefab = skinData.GetHatPrefab(indexItem);
        GameObject WingPrefab = skinData.GetHatPrefab(indexItem);
        HatPrefab.SetActive(false);
        TailPrefab.SetActive(false);
        WingPrefab.SetActive(false);
    }

    public override void Select()
    {
        Debug.Log("Skin");
        PresentSkin.Instance.currentSkin = this;
    }


    public void SpawnObj(Dictionary<GameObject, GameObject> dict, GameObject prefab, Transform parentTF)
    {
        if (dict.ContainsKey(prefab))
        {
            dict[prefab].SetActive(true);
        }
        else
        {
            GameObject obj = Instantiate(prefab, parentTF);
            dict.Add(prefab, obj);
        }
    }

}
