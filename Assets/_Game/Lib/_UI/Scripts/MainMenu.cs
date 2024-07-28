using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : UICanvas
{
    [SerializeField] private Text playerName;
    public void PlayButton()
    {
        LevelManager.Instance.OnStart();
        GameManagerr.Instance.ChangeState(EGameState.GamePlay);
        DataPlayerController.coinInLevel =0;
        UIManager.Instance.OpenUI<GamePlayUI>();
        JoystickInput.Instance.isMouse = false;
        Close();
    }
    public void WeaponBtn()
    {
        GameManagerr.Instance.ChangeState(EGameState.Pause);
        UIManager.Instance.OpenUI<ShopWeapon>();
        LevelManager.Instance.player.ChangeAnim(Constant.ANIM_DANCE);
        Close();
    }
    public void SkinBtn()
    {
        GameManagerr.Instance.ChangeState(EGameState.Pause);
        UIManager.Instance.OpenUI<SkinShop>();
        LevelManager.Instance.player.ChangeAnim(Constant.ANIM_DANCE);
        Close();
    }

     public void ChangeName()
    {
        string newName = playerName.text;
        if (newName.Length > 0)
        {
           LevelManager.Instance.player.playerName = newName;
        }
    }

}
