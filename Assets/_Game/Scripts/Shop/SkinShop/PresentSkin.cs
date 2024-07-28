using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentSkin : Singleton<PresentSkin>
{
    public GameObject MoneyBtn;
    public GameObject SelectBtn;
    public GameObject EquippedBtn;
    public ESkinType currentType;
    public int currentIndex;
    public GenSkin currentSkin;
    public bool isCurrent, isOwned, noHave;
    public GenSkin[] listGenSkin = new GenSkin[4];
    [SerializeField] private SkinShop skinShop;
    private void Start()
    {
        currentType = ESkinType.Hat;
    }
    public void SpawnItem()
    {
        currentSkin.SpawnSkin(currentType, currentIndex);
    }

    public void MoneyItem() //Button money
    {
        int cost = ((int)currentType + 1) * 100 + currentIndex * 10;
        DataPlayerController.SubCoin(cost);
        DataPlayerController.AddSkin((int)currentType, currentIndex);
    }

    public void SelectItem() //Button select/equip
    {
        if (DataPlayerController.IsOwnedSkin((int)currentType, currentIndex)) //Da su huu thi spawn
        {
            PresentSkin.Instance.SpawnItem();
        }
        DataPlayerController.SetCurrentSkin((int)currentType, currentIndex); //Set hien tai

        if ((int)currentType == 3) //neu chon set
        {
            for (int i = 0; i < 3; i++)
            {
                DataPlayerController.SetCurrentSkin((int)i, -1); //Bo nhung cai hien tai cua tap truoc di
            }
        }
    }

    public void EquippedItem()
    {

        SpawnSaveItem();
    }

    public void SpawnSaveItem()
    {
        ItemModel itemSet = DataPlayerController.GetCurrentSkin(3);
        if (itemSet.indexItem < 0) // Ko chon ca set
        {
            for (int i = 0; i < 3; i++)
            {
                ItemModel item = DataPlayerController.GetCurrentSkin(i);
                Debug.Log("current item: " + item.indexType + " index: " + item.indexItem);
                listGenSkin[i]?.SpawnSkin((ESkinType)item.indexType, item.indexItem);
            }
        }
        else // chon ca set
        {
            listGenSkin[3]?.SpawnSkin((ESkinType)itemSet.indexType, itemSet.indexItem);
            for (int i = 0; i < 3; i++)
            {
                ItemModel item = DataPlayerController.GetCurrentSkin(i);
                listGenSkin[i]?.SpawnSkin((ESkinType)item.indexType, item.indexItem);
            }
        }
    }

    public bool isUsed(int itype, int index)
    {
        ItemModel item = DataPlayerController.GetCurrentSkin(itype);
        return item.indexItem == index;
    }

    public void ActivateBtn(int num)
    {
        int first = num % 100;
        int second = (num - first * 100) % 10;
        int last = num - first * 100 - second * 10;
        isCurrent = ConvertIntToBool(last);
        isOwned = ConvertIntToBool(second);
        noHave = ConvertIntToBool(first);
    }

    bool ConvertIntToBool(int i)
    {
        return i != 0;
    }

    public int GetCostItem()
    {
        return ((int)currentType + 1) * 100 + currentIndex * 10;
    }


    public void UpdatePresent(int iType, int index)
    {
        if (skinShop == null)
        {
            skinShop = FindObjectOfType<SkinShop>(); // fix late
        }

        if (PresentSkin.Instance.isUsed(iType, index)) // Dang mang skin nay
        {
            skinShop.EquippedActivate();
        }

        else if (DataPlayerController.IsOwnedSkin((int)iType, index) && !PresentSkin.Instance.isUsed((int)iType, index)) // Dang so huu
        {
            skinShop.SelectActivate();
        }
        else
        {
            skinShop.MoneyActivate();
        }
    }
}
