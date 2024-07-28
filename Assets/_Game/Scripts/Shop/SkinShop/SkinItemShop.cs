using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SkinItemShop : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private ESkinType indexType;
    [SerializeField] private int indexItem;
    [SerializeField] private GameObject lockObj;
    [SerializeField] private SkinShop skinShop;
    public int cost;
    int num;
    void Awake()
    {
        skinShop = FindObjectOfType<SkinShop>(); // fix late
        button = GetComponent<Button>();
        button.onClick.AddListener(Select);

    }
    void Start()
    {
        cost = ((int)indexType + 1) * 100 + indexItem * 10;
    }
    void Select() // Chon tung item
    {
        PresentSkin.Instance.currentIndex = indexItem;
        PresentSkin.Instance.currentType = indexType;
        skinShop.moneyTxt.text = "$ " + cost;
        PresentSkin.Instance.UpdatePresent((int)indexType, indexItem);
    }
    void Update()
    {
        SetLockObj();
    }

    public void SetLockObj()
    {
        if (DataPlayerController.IsOwnedSkin((int)indexType, indexItem))
        {
            lockObj.SetActive(false);
        }
        else
        {
            lockObj.SetActive(true);
        }
    }
    void UpdatePresent()
    {
        if (PresentSkin.Instance.isUsed((int)indexType, indexItem)) // Dang mang skin nay
        {
            skinShop.EquippedActivate();
        }
        else if (DataPlayerController.IsOwnedSkin((int)indexType, indexItem) && !PresentSkin.Instance.isUsed((int)indexType, indexItem)) // Dang so huu
        {
            skinShop.SelectActivate();
        }
        else
        {
            skinShop.MoneyActivate();
        }
    }


}
