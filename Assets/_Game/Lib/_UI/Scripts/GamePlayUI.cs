using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : UICanvas
{
    [SerializeField] private GameObject settingBtn;
    public Text Alive;

    private void Update()
    {
        Alive.text = "Alive: " + LevelManager.Instance.currentLevel.totalAmount; // fix late
    }
    public void SettingBtn()
    {
        GameManagerr.Instance.ChangeState(EGameState.Pause);
        UIManager.Instance.OpenUI<Setting>();
        Close();
    }

}
