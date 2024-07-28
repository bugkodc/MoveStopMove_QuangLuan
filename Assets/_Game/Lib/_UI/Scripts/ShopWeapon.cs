using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeapon : UICanvas
{
    [SerializeField] private GameObject selectBtn;
    [SerializeField] private Text MoneyTxt;
    public void SelectBtn() // Nut chon
    {
        Present.Instance.SelectItem();
    }
    public void MoneyBtn()
    {
        Present.Instance.MoneyItem();
    }

    public void UnClockBtn()
    {
        Present.Instance.UnClock();
    }
    public void CloseBtn()
    {
        GameManagerr.Instance.ChangeState(EGameState.MainMenu);
        UIManager.Instance.OpenUI<MainMenu>();
        Close();
    }
    public void EquippedBtn()
    {
        GameManagerr.Instance.ChangeState(EGameState.MainMenu);
        UIManager.Instance.OpenUI<MainMenu>();
        Close();
    }
}
