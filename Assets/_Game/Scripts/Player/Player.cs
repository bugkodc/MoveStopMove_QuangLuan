using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : Character
{
    private bool canAttack() 
    {
        if(weapon == null) SetWeapon();
        return weapon.isActiveAndEnabled;
    } 

    [Header("Transform for Skins")]
    public Transform HatTF;
    public Transform ShieldTF;
    public Transform WingTF;
    public Transform TailTF;

    public SkinnedMeshRenderer pantRender;
    public SkinnedMeshRenderer skinRender;
    public string playerName;
    public PhotonPlayerRPC _photonPlayerRPC; 
    private void Start()
    {
        _photonPlayerRPC = GetComponent<PhotonPlayerRPC>();
        LoadDataPlayer();
    }
    private void Update()
    {
        if (level == null)
        {
            level = LevelManager.Instance.currentLevel;
            return;
        }
        if (_photonPlayerRPC == null)
        {
            Debug.Log("NullPhoton");
            return;
        } 
        if (_photonPlayerRPC.view.IsMine)
        {
            Debug.Log("_photonPlayerRPC");
            CheckIsStateGamePlay();
            CheckIsStateGameFinish();
        }
        //CheckIsStateGamePlay();
       // CheckIsStateGameFinish();

    }
    // // rpc
    public void CheckIsStateGamePlay()
    {
        if (GameManagerr.Instance.IsState(EGameState.GamePlay))
        {
            Debug.Log("RunGame");
            if (!JoystickInput.Instance.isControl() && !isAttack()) // Dung va co bot trong vung tan cong
            {
                // rpc
                _photonPlayerRPC.view.RPC("ChangeAnimatorRPC", RpcTarget.All, Constant.ANIM_IDLE);
            }
            else if (!JoystickInput.Instance.isControl() && canAttack() && isAttack() && level.IsExistChar(FindCharacterClosed())) // Dung va co the tan cong, co bot trong vung tan cong
            {
                // rpc
                //StopMoving();
                _photonPlayerRPC.view.RPC("StopMoving", RpcTarget.All);
                _photonPlayerRPC.view.RPC("ChangeAnimatorRPC", RpcTarget.All, Constant.ANIM_ATTACK);
                timerWait += Time.deltaTime;
                _photonPlayerRPC.view.RPC("Throw", RpcTarget.All);
                //Throw();
                if (timerWait > 0.25)
                {
                    _photonPlayerRPC.view.RPC("AttackRPC", RpcTarget.All);
                    //Attack();
                }
            }
            else if (JoystickInput.Instance.isControl())
            {
                // rpc
                // ChangeAnim(Constant.ANIM_RUN);
                _photonPlayerRPC.view.RPC("ChangeAnimatorRPC", RpcTarget.All, Constant.ANIM_RUN);
                _photonPlayerRPC.view.RPC("MoveRPC", RpcTarget.All);
            }
        }
    }
    
    public void CheckIsStateGameFinish()
    {
        if (GameManagerr.Instance.IsState(EGameState.Finish))
        {
            if (!LevelManager.Instance.currentLevel.isWin)
            {
                ChangeAnim(Constant.ANIM_DEAD);
            }
            else
            {
                ChangeAnim(Constant.ANIM_WIN);
            }

        }
    }
    public override void OnInit()
    {
        this.gameObject.SetActive(true);
        IsDead = false;
        AssignAttackArea();
        SetData();
        SetSkin();
        SetWeapon();
        SetIndicator();
        ChangeAnim(Constant.ANIM_IDLE);
    }
    public void OnStart()
    {
        score = 0;
        gameObject.SetActive(true);
        TF.localScale = Vector3.one;
    }

    void SetData()
    {

        // float score = 0;
        EBodyMaterialType body = EBodyMaterialType.YELLOW;
        data?.SetBodyMaterial(body);
        skinnedMeshRenderer.material = data?.GetBodyMaterial();
        data?.SetName(playerName);
        data?.SetScore(score);

    }

    public override void SetSkin()
    {
        PresentSkin.Instance.EquippedItem();
    }

    public override void SetWeapon()
    {
        DespawnCurrentWeapon();
        currentWeaponType = (EWeaponType)DataPlayerController.GetCurrentWeapon().indexType;
        int idmaterial = DataPlayerController.GetCurrentWeapon().indexItem;
        SpawnWeapon(idmaterial);
    }
    public void LoadDataPlayer()
    {
        DataPlayerController.AddWeapon(0, 0);
    }
    public override void OnDespawn()
    {
        indicator.OnDespawn();
        this.gameObject.SetActive(false);
    }

    public override void OnDeath()
    {
        listCharInAttact.Clear();
        ChangeAnim(Constant.ANIM_DEAD);
        base.OnDeath();
        LevelManager.Instance.OnFinish();
        level.isWin = false;
    }
    public override void Move()
    {
        if (GameManagerr.Instance.IsState(EGameState.GamePlay))
        {
            JoystickInput.Instance.Move();
            base.Move();
        }

    }
    public void DespawnCurrentWeapon()
    {
        Weapon[] listWeapon = weaponGenTF.GetComponentsInChildren<Weapon>();
        for (int i = 0; i < listWeapon.Length; i++)
        {
            Destroy(listWeapon[i].gameObject);
        }
    }
    [PunRPC]
    private void ChangeAnimatorRPC(string Animtor)
    {
        ChangeAnim(Animtor);
    }
    [PunRPC]
    private void MoveRPC()
    {
        Move();
    }
    [PunRPC]
    private void AttackRPC()
    {
        Attack();
    }

}
