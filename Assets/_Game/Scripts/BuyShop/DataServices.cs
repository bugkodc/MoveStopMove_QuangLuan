using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class DataServices 
{
    public DataRepository dataRepository;
    private string KEY_DATA;
    private int maxItem;
    public ItemModel initItem ;

    
    public DataServices(string KEY,  ItemModel init, int maxitem=5)
    {
        KEY_DATA= KEY;
        maxItem= maxitem;
        initItem = new ItemModel(init.indexType, init.indexItem);
        InitDataServices();
    }
    public void InitDataServices()
    {
       
        dataRepository = JsonUtility.FromJson<DataRepository>(PlayerPrefs.GetString(KEY_DATA));
        string data = PlayerPrefs.GetString(KEY_DATA);
        
        if(dataRepository==null)
        {
            dataRepository = new DataRepository(maxItem, initItem);
            SaveData();
        }


    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(dataRepository);
        PlayerPrefs.SetString(KEY_DATA, data);
    }

    public bool IsOwnedItem(int type, int index, int factor =10)
    {
        return dataRepository.IsOwnedWithId(type, index, factor);
    }

    public bool IsOwnedType(int type)
    {
        return dataRepository.IsOwnedType(type);
    }

    public bool IsOwnedPrevType(int type)
    {
        return dataRepository.IsOwnedPrevType(type);
    }

    public void AddItem(int type, int index, int factor =10)
    {
        dataRepository.AddItem(type, index, factor);
        SaveData();
    }

    public void SetCurrentItem(int type, int index)
    {
        dataRepository.SetCurrentItem(type, index);
        SaveData();
    }

    public ItemModel GetCurrentItem()
    {
        return dataRepository.GetCurrentItem();
    }
    public ItemModel GetPrevItem()
    {
        ItemModel current = dataRepository.GetPrevItemId();
        SaveData();
        return current;
    }

    public ItemModel GetNextItem()
    {
        ItemModel current = dataRepository.GetNextItemId();
        SaveData();
        return current;
    }
}
