using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class Present : Singleton<Present>
{
    [SerializeField] PresentWeapons presentWeapons;
    [SerializeField] private ShopItemSelect shopItemSelectPrefab;
    [SerializeField] private Transform TF;
    [SerializeField] private Transform PageTF;

    private ShopWeaponElement shopWeaponElementPrefab;
    private Dictionary<ShopWeaponElement, bool> listShopItems = new Dictionary<ShopWeaponElement, bool>();
    private List<ShopWeaponElement> listWeaponPrefab = new List<ShopWeaponElement>();
    private Dictionary<ShopWeaponElement, ShopWeaponElement> dictItems = new Dictionary<ShopWeaponElement, ShopWeaponElement>();
    private List<ShopItemSelect> listSelectedItem = new List<ShopItemSelect>();
    private Dictionary<Weapon, Weapon> dictWeaponInPlayer = new Dictionary<Weapon, Weapon>();

    public WeaponDataa weaponDataa;
    public List<ShopWeaponElement> listWeapons = new List<ShopWeaponElement>();
    //UI
    public Button SelectBtn;
    public GameObject UnClockBtn;
    public GameObject MoneyBtn;
    public Button Equipped;
    public Text MoneyTxt;


    public Player player;
    public RectTransform weapon;
    private Vector2 sizeCanvas;
    private int count;
    public int indexSelect = 0; //index Material
    ShopItemSelect shopItemSelect;
    public int currentWeaponType = 0;

    public UnityEvent MoneyEven = new UnityEvent();
    void Awake()
    {
        shopItemSelect = new ShopItemSelect();
        player = LevelManager.Instance.player;// fix player
    }
    private void Start()
    {

        weaponDataa?.SetEWeaponType(currentWeaponType);
        InitSelectedItem();
        InitData();
        ItemModel item = DataPlayerController.GetCurrentWeapon();
    }

    public void OnNext()
    {
        listSelectedItem[currentWeaponType].gameObject.SetActive(false);
        if (currentWeaponType < presentWeapons.GetCountWeapon() - 1)
        {

            currentWeaponType++;
            MoneyTxt.text = "Buy with " + GetCost(currentWeaponType, 0) + "$";
            Present.Instance.UpdateBtn(currentWeaponType, 0);

        }
        shopItemSelectPrefab = presentWeapons.GetPrefabItemSelect(currentWeaponType);

        listSelectedItem[currentWeaponType].gameObject.SetActive(true);
        InitData();

    }
    public void OnPrev()
    {
        listSelectedItem[currentWeaponType].gameObject.SetActive(false);
        if (currentWeaponType > 0)
        {

            currentWeaponType--;
            MoneyTxt.text = "Buy with " + GetCost(currentWeaponType, 0) + "$";
            Present.Instance.UpdateBtn(currentWeaponType, 0);

        }

        InitData();
    }

    private void InitSelectedItem()
    {
        for (int i = 0; i < presentWeapons?.GetCountWeapon(); i++)
        {
            ShopItemSelect prefab = presentWeapons.GetPrefabItemSelect(i);
            ShopItemSelect item = Instantiate(prefab, PageTF);
            item.gameObject.SetActive(false);
            listSelectedItem.Add(item);
        }
    }
    private void InitData()
    {
        CreatListItem();
        indexSelect = 0;
        UpdateSelect();
        listSelectedItem[currentWeaponType].gameObject.SetActive(true);
    }

    private void CreatListItem()
    {
        shopWeaponElementPrefab = presentWeapons?.GetPrefabWeapon(currentWeaponType);
        if (!listWeaponPrefab.Contains(shopWeaponElementPrefab))
        {
            SpawnListItem(currentWeaponType, shopWeaponElementPrefab);
        }
        else
        {
            DeActivateListExceptItem(shopWeaponElementPrefab);
            ActivateListItem(shopWeaponElementPrefab);
        }
    }

    void ActivateListItem(ShopWeaponElement shopWeaponElementPrefab)
    {
        List<ShopWeaponElement> listWeapons = dictItems.Where(kvp => kvp.Value == shopWeaponElementPrefab).Select(kvp => kvp.Key).ToList();
        for (int i = 0; i < listWeapons.Count; i++)
        {
            listWeapons[i].gameObject.SetActive(true);
        }
    }

    void DeActivateListExceptItem(ShopWeaponElement shopWeaponElementPrefab)
    {
        List<ShopWeaponElement> listDeWeapons = dictItems.Where(kvp => kvp.Value != shopWeaponElementPrefab).Select(kvp => kvp.Key).ToList();
        for (int i = 0; i < listDeWeapons.Count; i++)
        {
            listDeWeapons[i].gameObject.SetActive(false);
        }
    }

    void SpawnListItem(int currentWeaponType, ShopWeaponElement shopWeaponElementPrefab)
    {
        count = GetNumMatsOfAWeapon((EWeaponType)(currentWeaponType));
        listWeaponPrefab.Add(shopWeaponElementPrefab);
        for (int i = 0; i < count; i++)
        {
            ShopWeaponElement weapon = SetWeapon(i, shopWeaponElementPrefab);
            // weapon.present = this;
            weapon.SetIndexMaterial(i);
            weapon.SetWeaponType((EWeaponType)(currentWeaponType));
            dictItems.Add(weapon, shopWeaponElementPrefab);
        }
        DeActivateListExceptItem(shopWeaponElementPrefab);
    }




    private ShopWeaponElement SetWeapon(int indexMaterial, ShopWeaponElement prefab) // Tao 1 cai
    {
        ShopWeaponElement shopWeaponElement = Instantiate(prefab, TF);
        listWeapons.Add(shopWeaponElement);
        weaponDataa?.SetEWeaponType(currentWeaponType);
        weaponDataa?.SetMaterial(indexMaterial);
        shopWeaponElement.meshRenderer.materials = weaponDataa.GetMaterial().ToArray();
        weapon = shopWeaponElement.GetComponent<RectTransform>();
        Vector3 pos = Vector3.zero;
        pos.x = 0.5f * ((count + 1) % 2) + 1 * (indexMaterial - count / 2) + indexMaterial * 0.3f;
        weapon.anchoredPosition3D = pos;
        return shopWeaponElement;
    }



    public void UpdateSelect()
    {
        shopItemSelect = listSelectedItem[currentWeaponType];
        shopItemSelect.weaponDataa?.SetEWeaponType(currentWeaponType);
        shopItemSelect.weaponDataa.SetMaterial(indexSelect);
        shopItemSelect.meshRenderer.materials = shopItemSelect.weaponDataa.GetMaterial().ToArray();
    }


    public List<Item> LoadPrefab()
    {
        List<Item> listItems = new List<Item>();
        for (int i = 0; i < presentWeapons.GetCountWeapon(); i++)
        {
            listItems.Add(presentWeapons.listDataWeapons[i].weaponprefab);
        }
        return listItems;

    }

    public int GetNumMatsOfAWeapon(EWeaponType sweaponType)
    {

        return (int)weaponDataa?.listWeaponMaterials.GetWeaponMaterialDatas(sweaponType).numberMaterial;

    }


    public void SelectItem()  // Chon de dung 
    {
        player.DespawnCurrentWeapon();
        player.currentWeaponType = (EWeaponType)Present.Instance.currentWeaponType;
        player.weapon = player.SpawnWeapon(indexSelect);
        player.weapon.InitData(Present.Instance.currentWeaponType, Present.Instance.indexSelect);
        DataPlayerController.SetCurrentWeapon(Present.Instance.currentWeaponType, Present.Instance.indexSelect);
        UpdateBtn(Present.Instance.currentWeaponType, indexSelect);
    }

    public void MoneyItem() // Buttom mua
    {
        if (!DataPlayerController.IsOwnedWeapon(Present.Instance.currentWeaponType, indexSelect)) // Chua so huu thi lay gia tien va mua 
        {
            int cost = GetCost(Present.Instance.currentWeaponType, 0);
            DataPlayerController.SubCoin(cost);
            DataPlayerController.AddWeapon(Present.Instance.currentWeaponType, Present.Instance.indexSelect);
            MoneyTxt.text = "Buy with " + GetCost(currentWeaponType, 0) + " $ ";
            UpdateBtn(Present.Instance.currentWeaponType, Present.Instance.indexSelect);
            MoneyEven.Invoke();

        }
    }

    public void UnClock()
    {
        if (!DataPlayerController.IsOwnedWeapon(Present.Instance.currentWeaponType, indexSelect))
        {
            DataPlayerController.AddWeapon(Present.Instance.currentWeaponType, Present.Instance.indexSelect);
            MoneyTxt.text = "Buy with " + GetCost(currentWeaponType, 0) + " $ ";
            UpdateBtn(Present.Instance.currentWeaponType, Present.Instance.indexSelect);
            MoneyEven.Invoke();

        }
    }
    public void EquiedItem()
    {

    }

    public void UpdateBtn(int idType, int idIndex)
    {

        if (DataPlayerController.IsCurrentWeapon(idType, idIndex))
        {
            Equipped.gameObject.SetActive(true);
            SelectBtn.gameObject.SetActive(false);
            UnClockBtn.SetActive(false);
            MoneyBtn.SetActive(false);
        }
        else if (DataPlayerController.IsOwnedWeapon(idType, idIndex))
        {
            Equipped.gameObject.SetActive(false);
            SelectBtn.gameObject.SetActive(true);
            UnClockBtn.SetActive(false);
            MoneyBtn.SetActive(false);
        }
        else if (DataPlayerController.IsOwnedWeaponType(idType))
        {
            Equipped.gameObject.SetActive(false);
            SelectBtn.gameObject.SetActive(false);
            UnClockBtn.SetActive(true);
            MoneyBtn.SetActive(false);
        }
        else
        {
            Equipped.gameObject.SetActive(false);
            SelectBtn.gameObject.SetActive(false);
            UnClockBtn.SetActive(false);
            MoneyBtn.SetActive(true);
        }
    }

    public int GetCost(int idType, int idIndex)
    {
        return idType * 100 + idIndex * 10;
    }

    void OnSelectBtn()
    {

    }

    void OnEquippedBtn()
    {
        GameManagerr.Instance.ChangeState(EGameState.MainMenu);
    }

}
