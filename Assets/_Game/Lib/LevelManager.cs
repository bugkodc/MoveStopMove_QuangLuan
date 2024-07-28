using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Level[] levels;
    public Level currentLevel;
    public Player player;
    private void Start()
    {
        //PlayerPrefs.DeleteAll();// reset data
        UIManager.Instance.OpenUI<MainMenu>();
        Data.Instance.SetLevel(1);
        LoadLevel(Data.Instance.GetLevel());
    }

    public void OnStart()
    {
        GameManagerr.Instance.currentState = EGameState.GamePlay;
    }

    public void OnFinish()
    {
        GameManagerr.Instance.ChangeState(EGameState.Finish);
        if (currentLevel.isWin)
        {
            UIManager.Instance.OpenUI<Win>();
            player.gameObject.SetActive(true);
            player.ChangeAnim(Constant.ANIM_WIN);
        }
        else
        {
            UIManager.Instance.OpenUI<Lose>();
            player.gameObject.SetActive(true);
            player.ChangeAnim(Constant.ANIM_DEAD);
        }

        currentLevel.Despawn();
    }

    public void LoadLevel(int index)
    {
        if (currentLevel != levels[index - 1] && currentLevel != null)
        {
            currentLevel.Despawn();
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[index - 1]);
        if (currentLevel != null)
        {
            player.OnStart();
            currentLevel.OnInit();
        }
        Data.Instance.SetLevel(index);
    }


    public Vector3 RandomPoint() => currentLevel.RandomPos();


}
