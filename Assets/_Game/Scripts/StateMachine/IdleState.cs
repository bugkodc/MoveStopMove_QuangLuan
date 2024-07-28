using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    Vector2 delayTime = new Vector2(1,2);
    float randomTime;
    float timer;
    float waitTime ;
    public void OnEnter(Bot bot)
    {
        bot.StopMoving();
        bot.ChangeAnim(Constant.ANIM_IDLE);
        timer =0;
        waitTime = Random.Range(Constant.TIMER_MIN_WAIT, Constant.TIMER_MAX_WAIT);
        randomTime = Random.Range(Constant.TIMER_MIN_IDLE, Constant.TIMER_MAX_IDLE);
    }
    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if(timer> randomTime)
        {
            bot.ChangeState(new PartrolState());
        }

        if(bot.isAttack() && timer> waitTime)
        {
            bot.ChangeState(new AttackState());
        }
    }
    public void OnExit(Bot bot)
    {
        
    }
}
    

