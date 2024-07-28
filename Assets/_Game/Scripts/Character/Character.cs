using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
public class Character : GameUnit, IHit
{

    [Header("DATA")]
    public WeaponDatas weaponDatas;
    public CharacterData data;

    [Header("TRANSFORM")]
    public Transform weaponGenTF;
    public GameObject UnderObj;
    [SerializeField] protected AttackArea attackArea;
    [SerializeField] protected SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private Animator anim;

    [Header("PREFAB")]
    public Indicator indicatorprefab;
    public List<Character> listCharInAttact = new List<Character>();
    [HideInInspector]
    public Weapon weapon;
    [HideInInspector]
    public Level level;
    [HideInInspector]
    public Vector3 dirAttact = Vector3.zero;
    [HideInInspector]
    public EWeaponType currentWeaponType;

    [HideInInspector]
    public Indicator indicator;
    [HideInInspector]
    public bool IsDead;

    [HideInInspector]
    public bool IsAttack => listCharInAttact.Count > 0;

    private string currentAnimName;
    private Dictionary<Weapon, Weapon> dictWeapon = new Dictionary<Weapon, Weapon>();
    private List<Weapon> listWeapon = new List<Weapon>();
    public float score = 0;
    public int coinInLevel = 0;
    public float timerWait = 0;
    void Start()
    {
        tf = transform;
    }

    public override void OnInit()
    {
        IsDead = false;
        TF.localScale = Vector3.one;
        UnderObj.SetActive(false);
        score = 0;
        AssignAttackArea();
        SetSkin();
        SetWeapon();
        SetIndicator();
        ChangeAnim(Constant.ANIM_IDLE);
    }

    public void AssignAttackArea()
    {
        this.attackArea.character = this;
    }

    public virtual void SetSkin() // name, score, body material //TODO: HAT, PANT,...
    {
        int index = Random.Range(0, BotDatasIns.BotName.Count);
        string name = BotDatasIns.BotName[index];
        EBodyMaterialType body = RandomBodyMat();
        data.SetBodyMaterial(body);
        skinnedMeshRenderer.material = data.GetBodyMaterial();
        data.SetName(name);
        data.SetScore(score);
    }
    public void SetIndicator()
    {
        indicator = SimplePool.Spawn<Indicator>(indicatorprefab);
        indicator.ownIndicator = this;
        indicator.OnInit();
    }
    public virtual void SetWeapon()
    {
        SpawnWeapon();
    }
    public override void OnDespawn()
    {
        indicator.OnDespawn();
        DespawnWeapon();
        UnderObj.SetActive(false);
        SimplePool.Despawn(this);
    }
    public void DespawnWeapon()
    {
        Weapon weaponPrefab = weaponDatas.GetWeaponPrefab(currentWeaponType);
        foreach (KeyValuePair<Weapon, Weapon> weapon in dictWeapon)
        {
            Destroy(weapon.Value.bullet);
            weapon.Value.gameObject.SetActive(false);
        }
    }

    public virtual void OnDeath() //khi bi tieu dieu, goi ham OnDeath
    {
        StopMoving();
        UnderObj.SetActive(false);
        listCharInAttact.Clear();
        ChangeAnim(Constant.ANIM_DEAD);
        Invoke(nameof(OnDespawn), Constant.TIMER_DEATH);

    }
    public void OnHit(Bullet bullet, Character character)
    {
        if (bullet.character != this)
        {
            bullet.OnDespawn();
            character.listCharInAttact.Remove(this);
            character.UnderObj.SetActive(false);
            CheckDead(character);
        }
    }
    public void CheckDead(Character character)
    {
        if (this.IsDead) return;
        this.IsDead = true;
        level.DespawnChar(this);
        AddScore(character);
        character.Scale();
        if (character is Player)
        {
            DataPlayerController.coinInLevel += Constant.COIN_INCR;
        }
        level.UpdateListChar();
        level.CheckCountChar();
    }
    public void OnHitExit(Bullet bullet, Character character)
    {

    }
    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
    public virtual void Move()
    {

    }
    public virtual void StopMoving()
    {

    }
    public void SpawnWeapon()
    {
        currentWeaponType = (EWeaponType)Random.Range(0, Constant.NUMBER_WEAPONS);
        Weapon weaponPrefab = weaponDatas.GetWeaponPrefab(currentWeaponType);
        SetSpawnWeapon(weaponPrefab);
        Vector3 postion = weaponGenTF.position;
        postion.y = weaponGenTF.position.y;
        weapon.InitData((int)currentWeaponType, weapon.indexMat);

        //TODO: cache transform
        weapon.transform.position = postion;
        weapon.character = this;
        weapon.gameObject.SetActive(true);
    }
    public void SetSpawnWeapon(Weapon weaponPrefab)
    {
        if (!dictWeapon.ContainsKey(weaponPrefab))
        {
            weapon = Instantiate(weaponPrefab, weaponGenTF);
            dictWeapon.Add(weaponPrefab, weapon);
            listWeapon.Add(weapon);
        }
        else
        {
            weapon = dictWeapon[weaponPrefab];
            weapon.gameObject.SetActive(true);
        }

    }
    public Weapon SpawnWeapon(int material)
    {
        Weapon weaponPrefab = weaponDatas.GetWeaponPrefab(currentWeaponType);
        Vector3 postion = weaponGenTF.position;
        postion.y = weaponGenTF.position.y;
        weapon = Instantiate(weaponPrefab, weaponGenTF);
        weapon.indexMat = material;
        weapon.InitData((int)currentWeaponType, weapon.indexMat);
        weapon.transform.position = postion;
        weapon.character = this;
        weapon.gameObject.SetActive(true);
        return weapon;
    }

    public void Scale()
    {
        Vector3 scale = TF.localScale;
        scale *= 1.07f;
        TF.localScale = scale;
    }

    public void FaceTarget(Character target)
    {
        level.UpdateListChar();
        if (this.level.listCharacters.Contains(target))
        {
            Vector3 position = target.TF.position;
            position.y = TF.position.y;
            Vector3 direction = (position - TF.position).normalized;
            TF.forward = direction;
            dirAttact = direction;
            tf.LookAt(target.tf.position + Vector3.up * (tf.position.y - target.tf.position.y));
        }
        else
        {
            TF.forward = attackArea.transform.forward;
        }
    }

    public Character FindCharacterClosed()
    {
        Character closedChar = null;
        float distance = Mathf.Infinity;
        level.UpdateListChar();
        for (int i = 0; i < listCharInAttact.Count; i++)
        {
            if (this.level.listCharacters.Contains(listCharInAttact[i]))
            {
                if (Vector3.Distance(TF.position, listCharInAttact[i].TF.position) < distance)
                {
                    closedChar = listCharInAttact[i];
                }
            }
        }
        return closedChar;
    }


    public void Throw()
    {

        Character closed = FindCharacterClosed();
        if (listCharInAttact.Count > 0 && level.IsExistChar(closed))
        {
            FaceTarget(closed);
            ChangeAnim(Constant.ANIM_ATTACK);
        }
    }

    public void Attack()
    {
        weapon.gameObject.SetActive(false);
        weapon.Attack();
        timerWait = 0;
    }

    public EBodyMaterialType RandomBodyMat()
    {
        EBodyMaterialType body = EBodyMaterialType.YELLOW;
        if (level != null)
        {
            int index = (int)Random.Range(0, (float)(level?.listBodyMaterialType.Count));
            body = level.listBodyMaterialType[index];
        }
        return body;
    }

    public virtual void InitData()
    {
        int index = Random.Range(0, BotDatasIns.BotName.Count);
        string name = BotDatasIns.BotName[index];
        float score = Random.Range(0, 5);
        EBodyMaterialType body = RandomBodyMat();
        data?.SetBodyMaterial(body);
        skinnedMeshRenderer.material = data?.GetBodyMaterial();
        data?.SetName(name);
        data?.SetScore(score);
    }

    public bool isAttack()
    {
        for (int i = 0; i < listCharInAttact.Count; i++)
        {
            if (!listCharInAttact[i].IsDead)
            {
                return true;
            }
        }
        return false;
    }

    public void AddScore(Character character)
    {
        character.score++;
        character.data.SetScore(character.score);
        character.indicator.SetScore();
    }

}
