using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartrolState : IState<Bot>
{
    float randomTime;
    float waitTime;
    float timer;
    public void OnEnter(Bot bot)
    {
        timer = 0;
        randomTime = Random.Range(Constant.TIMER_MIN_MOVE, Constant.TIMER_MAX_MOVE);
        waitTime = Random.Range(Constant.TIMER_MIN_WAIT, Constant.TIMER_MAX_WAIT);
        bot.SetDestination(LevelManager.Instance.RandomPoint());
    }
    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (bot.Target != null)
        {
            if (bot.isAttack()) /*&& timer > waitTime*/
            {
                bot.ChangeState(new AttackState());
            }
            else
            {
                bot.Move();
            }
        }
        else
        {
            if (timer < randomTime)
            {
                bot.Move();
            }
            else
            {
                bot.ChangeState(new IdleState());
            }
        }
    }
    public void OnExit(Bot bot)
    {

    }
}
