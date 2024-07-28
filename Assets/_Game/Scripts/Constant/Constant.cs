using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    public static string ANIM_IDLE = "IsIdle";
    public static string ANIM_DEAD = "IsDead";
    public static string ANIM_WIN = "IsWin";
    public static string ANIM_DANCE = "IsDance";
    public static string ANIM_ULTI = "IsUlti"; //an qua
    public static string ANIM_ATTACK = "IsAttack";
    public static string ANIM_RUN = "IsRun";

    public static float NUMBER_WEAPONS = System.Enum.GetValues(typeof(EWeaponType)).Length;
    public static float NUMBER_BODY_MATERIAL = System.Enum.GetValues(typeof(EBodyMaterialType)).Length;
    public static float TIMER_ATTACK = 0.5f;
    public static float TIMER_MIN_WAIT = 0.5f;
    public static float TIMER_MAX_WAIT = 1.0f; //THOIGIANDOITANCONG
    public static float TIMER_MIN_IDLE = 1f;
    public static float TIMER_MAX_IDLE = 2f;
    public static float TIMER_MIN_MOVE = 3f;
    public static float TIMER_MAX_MOVE = 6f;
    public static float TIMER_DEATH = 1f;
    public static float DISTANCE_DESTINATION = 1f;

    public static int SHOP_WEAPON_MAX_ITEM = 9;
    public static int SHOP_SKIN_MAX_ITEM = 9;
    public static int COIN_INCR = 10;
}
