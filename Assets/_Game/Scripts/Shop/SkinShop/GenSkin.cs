using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenSkin : MonoBehaviour
{
    [SerializeField] protected Button button;
    protected int indexType;
    protected bool isSpawn = false;
    protected Player player;
    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Select);
        player = LevelManager.Instance.player; // fix player
    }

    public virtual void SpawnSkin(ESkinType indexType, int indexItem)
    {

    }

    public virtual void DespawnSkin(ESkinType indexType, int indexItem)
    {

    }

    public virtual void Select()
    {

    }

    public void RefeshObj(Transform parentTF)
    {
        List<GameObject> listObj = GetAllChilds(parentTF.gameObject);
        for (int i = 0; i < listObj.Count; i++)
        {
            listObj[i]?.gameObject.SetActive(false);
        }
    }
    public static List<GameObject> GetAllChilds(GameObject Go)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        return list;
    }

}
