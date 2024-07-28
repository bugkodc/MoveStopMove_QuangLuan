using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool IsDestroyOnDespawn = false;
    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public virtual void DeActivate()
    {
        gameObject.SetActive(false);
        if (IsDestroyOnDespawn)
        {
            Destroy(gameObject);
        }
    }
    public virtual void Setup()
    {
        ItemManager.Instance.AddBackItem(this);
    }
  
}


