using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
// [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class JoystickInput : Singleton<JoystickInput>
{
    [SerializeField] private GameObject tfCenterJoystick;
    [SerializeField] public Transform playerTF;
    public Rigidbody _rigidbody;
    private float playerSpeed = 8;
    private float _moveSpeed = 8;

    public bool isMouse;
    public DynamicJoystick _joystick;
    private void Start()
    {
        _rigidbody = LevelManager.Instance.player.GetComponent<Rigidbody>(); // fix player
        _joystick = gameObject.GetComponentInChildren<DynamicJoystick>(); // late fix
        playerTF = _rigidbody.transform;
    }
    public bool isControl()
    {
        if(tfCenterJoystick == null)
        {
            Debug.Log("tfCenterJoystick null");
        }
       return Vector3.Distance(tfCenterJoystick.transform.localPosition, Vector3.zero) > 0.001;
    }
    private void FixedUpdate()
    { 
        CheckIsStateGamePlay(); 
    }
    public void CheckIsStateGamePlay()
    {
        if (GameManagerr.Instance.IsState(EGameState.GamePlay))
        {
            isMouse = Input.GetMouseButtonUp(0);
            if (isMouse)
            {
                this.gameObject.SetActive(true);
                tfCenterJoystick.transform.localPosition = Vector3.zero;
            }
        }
        else
        {
            this.gameObject.SetActive(false);
            isMouse = false;
            tfCenterJoystick.transform.localPosition = Vector3.zero;
        }
    }
    public void Move()
    {
        _moveSpeed = playerSpeed;
        Vector2 moveDir = new Vector2(_joystick.Horizontal, _joystick.Vertical);
        moveDir.Normalize();
        _rigidbody.velocity = isControl() ? new Vector3(moveDir.x * _moveSpeed, _rigidbody.velocity.y, moveDir.y * _moveSpeed) : Vector3.zero;
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            playerTF.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }
        _rigidbody.AddForce(Vector3.down * 10f);
    }
}