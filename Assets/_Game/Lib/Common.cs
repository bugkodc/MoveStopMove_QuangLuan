using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common 
{
    public static float MathMod(float a, float b) 
    {
        return (Mathf.Abs(a * b) + a) % b;
    }  
}
