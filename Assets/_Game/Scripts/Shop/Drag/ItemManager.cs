using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : Singleton<ItemManager>
{
    // dict for quick query Item prefab
    private Dictionary<System.Type, Item> itemsPrefab = new Dictionary<System.Type, Item>();
    // list from hirechi
    private Item[] itemResources;
    // item Activate
    private Dictionary<System.Type, Item> itemsActivate = new Dictionary<System.Type, Item>();
    private Dictionary<UICanvas, UnityAction> BackActionEvents = new Dictionary<UICanvas, UnityAction>();
    private List<Item> backItems = new List<Item>();

    public Transform ItemParentTF;
    public List<T> SpawnListItem<T>(int numberItem) where T : Item
    {
        List<T> listItem = new List<T>();
        for (int i = 0; i < numberItem; i++)
        {
            T item = SpawnItem<T>();
            listItem.Add(item);
        }
        return listItem as List<T>;
    }
    public T SpawnItem<T>() where T : Item
    {
        Item item = GetItem<T>();
        item.Setup();
        item.Activate();
        return item as T;

    }
    public void DespawnItem<T>() where T : Item
    {
        if (IsAcitvate<T>())
        {
            GetItem<T>().DeActivate();
        }
    }
    public bool IsAcitvate<T>() where T : Item
    {
        System.Type type = typeof(T);
        return itemsActivate.ContainsKey(type) && itemsActivate[type] != null;
    }

    public T GetItem<T>(int numberItem) where T : Item
    {
        if (!IsAcitvate<T>())
        {
            Item item = Instantiate(GetUIPrefab<T>(), ItemParentTF);
            itemsActivate[typeof(T)] = item;
        }
        return itemsActivate[typeof(T)] as T;
    }

    public T GetItem<T>() where T : Item
    {
        if (!IsAcitvate<T>())
        {
            Item item = Instantiate(GetUIPrefab<T>(), ItemParentTF);
            itemsActivate[typeof(T)] = item;
        }
        return itemsActivate[typeof(T)] as T;
    }

    public T GetUIPrefab<T>() where T : Item
    {
        for (int i = 0; i < itemResources.Length; i++)
        {
            if (itemResources[i] is T)
            {
                itemsPrefab[typeof(T)] = itemResources[i];
                break;
            }
        }
        return itemsPrefab[typeof(T)] as T;

    }



    public virtual void SetUp()
    {

    }
    public void AddBackItem(Item item)
    {
        if (!backItems.Contains(item))
        {
            backItems.Add(item);
        }
    }

    public void RemoveBackItem(Item item)
    {
        backItems.Remove(item);
    }

    public void ClearBackItem()
    {
        backItems.Clear();
    }
}
