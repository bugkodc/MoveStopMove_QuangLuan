using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    public bool IsDestroyOnClose = false;
    protected RectTransform m_RectTransform;

    private void Start()
    {
        Init();
    }

    protected void Init()
    {
        m_RectTransform = GetComponent<RectTransform>();
    
    }

    public virtual void Setup()
    {
        UIManager.Instance.AddBackUI(this);
        UIManager.Instance.PushBackAction(this, BackKey);
    }

    public virtual void BackKey()
    {

    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        //anim
    }

    public virtual void Close()
    {
        UIManager.Instance.RemoveBackUI(this);
        //anim
        gameObject.SetActive(false);
        if (IsDestroyOnClose)
        {
            Destroy(gameObject);
        }
        
    }


}
