using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : UICanvas
{
    public Text level;
    public Text coinTxt;
    public Text bonusTxt;
    private int bonus = 2;
    private void OnEnable()
    {
        coinTxt.text = DataPlayerController.coinInLevel.ToString();
        bonusTxt.text = "x" + bonus;
    }

    public void MainMenuButton()
    {
        UIManager.Instance.OpenUI<MainMenu>();
        Close();
    }

    public void RePlayButton()
    {
        UIManager.Instance.OpenUI<MainMenu>();
        GameManagerr.Instance.ChangeState(EGameState.MainMenu);
        LevelManager.Instance.LoadLevel(1);
        Data.Instance.SetLevel(1);
        Close();

    }

    public void MultCoin()
    {
        DataPlayerController.coinInLevel *= bonus;
    }
    public void LoadNextLevel()
    {
        UIManager.Instance.OpenUI<MainMenu>();
        GameManagerr.Instance.ChangeState(EGameState.MainMenu);
        DataPlayerController.AddCoin(DataPlayerController.coinInLevel);
        LevelManager.Instance.LoadLevel(Data.Instance.GetNextLevel());
        Close();
    }

}
