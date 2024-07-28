using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantGenSkin : GenSkin
{
    [SerializeField] private PantDatas data;
    private SkinnedMeshRenderer meshRenderer;
    private Material material;
    private void Awake()
    {
        PresentSkin.Instance.listGenSkin[1] = this;
        button.onClick.AddListener(Select);
        player = LevelManager.Instance.player;
        meshRenderer = player.pantRender;

    }
    private void Start()
    {

        indexType = (int)ESkinType.Pant;

    }
    public override void SpawnSkin(ESkinType indexType, int indexItem)
    {
        material = data.GetMaterial(indexItem);
        Debug.Log("material: " + material);

        if (material != null)
        {
            player.pantRender.material = material;
        }

    }

    public override void DespawnSkin(ESkinType indexType, int indexItem)
    {

    }

    public override void Select()
    {
        Debug.Log("Pant");
        PresentSkin.Instance.currentSkin = this;
    }

}
