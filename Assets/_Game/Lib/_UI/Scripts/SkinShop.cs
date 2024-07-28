using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShop : UICanvas
{
    [SerializeField] private List<GameObject> ListShopItem;
    public static ESkinType selectType;
    public Text moneyTxt;
    public Button MoneyBtn;
    public Button SelectButton;
    public Button EquippedBtn;

    private void Start()
    {
        UpdateSelect((int)ESkinType.Hat);
        PresentSkin.Instance.UpdatePresent((int)ESkinType.Hat, 0);
    }


    public void CloseBtn()
    {
        GameManagerr.Instance.ChangeState(EGameState.GamePlay);
        UIManager.Instance.OpenUI<GamePlayUI>();
        Close();
    }
    public void HatBtn()
    {
        selectType = ESkinType.Hat;
        UpdateSelect((int)ESkinType.Hat);
        PresentSkin.Instance.currentType = ESkinType.Hat;
        PresentSkin.Instance.UpdatePresent((int)ESkinType.Hat, 0);

    }

    public void PantBtn()
    {
        selectType = ESkinType.Pant;
        UpdateSelect((int)ESkinType.Pant);
        PresentSkin.Instance.currentType = ESkinType.Pant;
        PresentSkin.Instance.UpdatePresent((int)ESkinType.Pant, 0);
    }

    public void ShieldBtn()
    {
        selectType = ESkinType.Shield;
        UpdateSelect((int)ESkinType.Shield);
        PresentSkin.Instance.currentType = ESkinType.Shield;
        PresentSkin.Instance.UpdatePresent((int)ESkinType.Shield, 0);
    }

    public void SetBtn()
    {
        selectType = ESkinType.Skin;
        UpdateSelect((int)ESkinType.Skin);
        PresentSkin.Instance.currentType = ESkinType.Skin;
        PresentSkin.Instance.UpdatePresent((int)ESkinType.Skin, 0);
    }

    public void SelectBtn()
    {
        PresentSkin.Instance.SelectItem();
        EquippedActivate();
    }

    public void MoneyButton()
    {
        int cost = ((int)PresentSkin.Instance.currentType + 1) * 100 + PresentSkin.Instance.currentIndex * 10;
        if (DataPlayerController.IsEnoughMoney(cost))
        {
            PresentSkin.Instance.MoneyItem();
            SelectActivate();
        }
    }
    public void EquippedButton()
    {
        PresentSkin.Instance.EquippedItem();
        GameManagerr.Instance.ChangeState(EGameState.MainMenu);
        UIManager.Instance.OpenUI<MainMenu>();
        Close();
    }
    void UpdateSelect(int j)
    {
        for (int i = 0; i < ListShopItem.Count; i++)
        {
            if (i == j)
            {
                ListShopItem[i].SetActive(true);
            }
            else
            {
                ListShopItem[i].SetActive(false);
            }
        }
    }


    public void MoneyActivate()
    {

        MoneyBtn.gameObject.SetActive(true);
        SelectButton.gameObject.SetActive(false);
        EquippedBtn.gameObject.SetActive(false);
    }

    public void SelectActivate()
    {

        MoneyBtn.gameObject.SetActive(false);
        SelectButton.gameObject.SetActive(true);
        EquippedBtn.gameObject.SetActive(false);
    }
    public void EquippedActivate()
    {

        MoneyBtn.gameObject.SetActive(false);
        SelectButton.gameObject.SetActive(false);
        EquippedBtn.gameObject.SetActive(true);
    }

}
