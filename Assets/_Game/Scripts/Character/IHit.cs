using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHit 
{
    public void OnHit(Bullet bullet, Character character);
    public void OnHitExit(Bullet bullet, Character character);
}
