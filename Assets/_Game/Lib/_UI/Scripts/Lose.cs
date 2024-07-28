using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : UICanvas
{
    public Text level;
    public Text coinInLevel;
    public Text bonusTxt;
    private Player player;
    private int bonus = 2;
    private void Start()
    {
        player = LevelManager.Instance.player;
    }

    private void OnEnable()
    {
        coinInLevel.text = DataPlayerController.coinInLevel.ToString();
        bonusTxt.text = "x" + bonus;
    }
    public void MainMenuButton()
    {
        UIManager.Instance.OpenUI<MainMenu>();
        Close();
    }

    public void ReLoadLevel()
    {
        UIManager.Instance.OpenUI<MainMenu>();
        DataPlayerController.AddCoin(DataPlayerController.coinInLevel);
        GameManagerr.Instance.ChangeState(EGameState.MainMenu);
        LevelManager.Instance.LoadLevel(Data.Instance.GetLevel());
        Close();
    }
    public void MultCoin()
    {
        DataPlayerController.coinInLevel *= bonus;
    }

    public void RePlayButton()
    {
        UIManager.Instance.OpenUI<MainMenu>();
        GameManagerr.Instance.ChangeState(EGameState.MainMenu);
        DataPlayerController.AddCoin(DataPlayerController.coinInLevel);
        Data.Instance.SetLevel(1);
        LevelManager.Instance.LoadLevel(2);
        Close();

    }
}
