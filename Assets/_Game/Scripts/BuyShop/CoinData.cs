using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinData
{
    public int coin;
    public CoinData(int value)
    {
        coin= value;
    }
    public int GetCoin()
    {
        return coin;
    }
    public void SetCoint(int value)
    {
        coin = value;
    }
    public void AddCoin(int value)
    {
        coin+=value;
    }

    public void SubCoin(int cost)
    {
        coin-=cost;
    }

    public bool IsEnoughMoney(int cost)
    {
        return coin>= cost;
    }
}
