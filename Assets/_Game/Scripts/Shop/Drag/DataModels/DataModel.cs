using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataModel
{
    public List<int> listItems;
    public int currentItem;
    public int coin;
    public bool IsOwnedWithId(int id)
    {
        return listItems.Contains(id);
    }

    public void AddItem(int id)
    {
        if (!IsOwnedWithId(id)) return;
        listItems.Add(id);
    }

    public void SetCurrentItem(int current)
    {
        currentItem = current;
    }
    public int GetCurrentItem()
    {
        return currentItem;
    }

    public int GetPrevItemId()
    {
        int itemId = 1;
        int currentIndex = listItems.IndexOf(currentItem);
        if (currentIndex == 0)
        {
            itemId = listItems[listItems.Count - 1];
        }
        else
        {
            itemId = listItems[currentIndex - 1];
        }
        currentItem = itemId;
        return itemId;
    }

    public int GetNextItemId()
    {
        int itemId = 1;
        int currentIndex = listItems.IndexOf(currentItem);
        if (currentIndex == 3)
        {
            itemId = listItems[0];
        }
        else
        {
            itemId = listItems[currentIndex + 1];
        }
        currentItem = itemId;
        return itemId;
    }

    public void AddCoin(int value)
    {
        this.coin += value;
    }
    public void SubCoin(int value)
    {
        this.coin -= value;
    }

    public int GetCoin()
    {
        return coin;
    }
    public bool IsEnoughMoney(int cost)
    {
        return coin >= cost;
    }
}
