using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeaponElement : Item
{
    [SerializeField] protected GameObject ClockObj;
    private EWeaponType eWeaponType;
    private int indexMaterial;
    private Player player;

    public bool isOwned;
    public bool isUsed;
    public MeshRenderer meshRenderer;



    void Awake()
    {
        player = LevelManager.Instance.player; // fix player
    }
    private void Start()
    {
        ActivateClock();
        Present.Instance.MoneyEven.AddListener(ActivateClock);
    }
    public void SetIndexMaterial(int id)
    {
        indexMaterial = id;
    }

    public void SetWeaponType(EWeaponType eWeaponType)
    {
        this.eWeaponType = eWeaponType;
    }
    public void OnSelect()
    {
        Present.Instance.indexSelect = indexMaterial;
        Present.Instance.UpdateSelect();
        Present.Instance.UpdateBtn((int)eWeaponType, (int)indexMaterial);
        Present.Instance.MoneyTxt.text = "Buy with " + Present.Instance.GetCost((int)eWeaponType, (int)indexMaterial);
    }
    public void ActivateClock()
    {
        if (!DataPlayerController.IsOwnedWeapon((int)eWeaponType, indexMaterial))
        {
            ClockObj.SetActive(true);
        }
        else
        {
            ClockObj.SetActive(false);
        }
    }

    public void IsUsed(int itype, int index)
    {
        isOwned = (itype == (int)eWeaponType && index == indexMaterial);
    }


}
