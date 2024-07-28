using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRepository 
{   
    public DataRepository(int number, ItemModel item)
    {
        maxItem = number;
        // currentItem = item;
       
        indexItem = item.indexItem;
        indexType = item.indexType;
    }
    public List<int> listItems = new List<int>();
    //item weapon
    public int indexType ;
    public int indexItem ;
    
    public int maxItem ; 
    #region variable & propoty
    public ItemModel currentItem ; //khong save duoc
    #endregion

    //Bien tam
    

    #region  logic: check, add, getprev, getnext
    public bool IsOwnedWithId(int idTyp, int idIte, int factor=10)
    {
        return listItems.Contains(idTyp*factor+idIte);
    }

    public bool IsOwnedType(int idType, int factor =10)
    {
        for(int i =0; i< listItems.Count; i++)
        {
            int iType = listItems[i]/factor;
            if(iType == idType) 
            {
                return true;
            }
        }
        return false;
    }

    public bool IsOwnedPrevType(int idType)
    {
        return IsOwnedType(idType-1);
    }

    public void AddItem(int idType, int idItem, int factor =10)
    {   
        if(IsOwnedWithId( idType,  idItem)) 
        {
            return;
        }
       
        listItems.Add(idType*factor+idItem);
    }

    public void SetCurrentItem(int idType, int idItem )
    {
        this.indexType = idType;
        this.indexItem = idItem;
        this.currentItem= new ItemModel(idType, idItem);
    }

    public void SetTypeCurrItem(int idType)
    {
        indexType = idType;
    }

    public void SetIndexCurrItem(int idItem)
    {
        indexType = idItem;
    }

    public ItemModel GetCurrentItem()
    {
        return new ItemModel(this.indexType, this.indexItem);
    }

    public int GetTypeCurrItem()
    {
        return indexType;
    }

    public int GetIndexCurrItem()
    {
        return indexItem;
    }

    public ItemModel GetPrevItemId(int factor =10)
    {
        ItemModel vitem =new ItemModel(indexType, indexItem);  //TODO: CHECK 0 or 1

        int currentIndex = listItems.IndexOf(indexType*factor+  indexItem);
        if(currentIndex > 0)
        {
            int numberItem = listItems[currentIndex-1]; 
            int idType = numberItem%factor;
            int idItem = numberItem - idType*factor;
            vitem = new ItemModel(idType, idItem);
        }
        currentItem = vitem;
        return vitem;
    }

    public ItemModel GetNextItemId(int factor =10)
    {
        ItemModel vitem = new ItemModel(indexType, indexItem);  //TODO: CHECK 0 or 1
        int currentIndex = listItems.IndexOf(indexType*factor+  indexItem);
        if(currentIndex < maxItem-1 )
        {
            int numberItem = listItems[currentIndex+1]; 
            int idType = numberItem%factor;
            int idItem = numberItem - idType*factor;
            vitem = new ItemModel(idType, idItem);
            
        }
        currentItem = vitem;
        return vitem;
    }
    #endregion
}
