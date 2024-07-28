using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackState : IState<Bot>
{
    private float timer;
    private float timerWait = 0.25f;
    private float waitTime;
    public void OnEnter(Bot bot)
    {
        waitTime = 0;
        timer = 0;
    }
    public void OnExecute(Bot bot)
    {
        waitTime += Time.deltaTime;
        timer += Time.deltaTime;
        CheckBotTaget(bot, waitTime);
        CheckTimer(bot, timer);
    }
    public void OnExit(Bot bot)
    {

    }
    public void CheckBotTaget(Bot bot, float waitTime)
    {
        if (bot.Target != null)
        {

            bot.StopMoving();
            bot.ChangeAnim(Constant.ANIM_ATTACK);
            bot.Throw();
            CheckWaitTime(bot, waitTime);
        }
    }
    public void CheckTimer(Bot bot, float time)
    {
        if (time >= Constant.TIMER_ATTACK)
        {
            bot.ChangeState(new IdleState());
        }
    }
    public void CheckWaitTime(Bot bot, float waitTime)
    {
        if (waitTime > timerWait)
        {
            bot.Attack();
            bot.ChangeState(new IdleState());
        }
    }
}
