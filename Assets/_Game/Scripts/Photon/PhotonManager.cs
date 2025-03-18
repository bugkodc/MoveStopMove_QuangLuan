using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PhotonManager : MonoBehaviourPunCallbacks
{
    #region singleton
    private static PhotonManager instance;
    public static PhotonManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PhotonManager>();
            }
            if (instance == null)
            {
                instance = new GameObject().AddComponent<PhotonManager>();
            }

            return instance;
        }
    }
    #endregion
    public string _namePlayer;
    public string _nameRoom;

    public void CreateRoom()
    {
        if (CheckIsValues(_namePlayer, _nameRoom))
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.LeaveRoom();
                Debug.Log("LeaveRoom_Create");
            }
            else if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.LeaveLobby();
                Debug.Log("LeaveLobby_Create");
            }
            else if (PhotonNetwork.IsConnectedAndReady)
            {
                // Tạo phòng mới khi đã ở trên Master Server
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.MaxPlayers = 4;
                PhotonNetwork.CreateRoom(_nameRoom, roomOptions);
                Debug.Log("Done create Room");
            }
        }
        else
        {
            Debug.LogError("Room name or Name Player is null or empty");
            return;
        }
    }
    public void JoinRoom()
    {
        if (CheckIsValues(_namePlayer, _nameRoom))
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.LeaveRoom();
                Debug.Log("LeaveRoom_Join");
            }
            else
            {
                // RoomOptions roomOptions = new RoomOptions { MaxPlayers = 2 }; // Set room options
                PhotonNetwork.JoinRoom(_nameRoom);
                print("Join room by name" + _nameRoom);
            }

        }
    }
    public void SpawnPlayer(Player player)
    {
        //tạo vị trí ngẫu nhiên cho player
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Vector3 randomPosition = player.transform.position;
            Destroy(LevelManager.Instance.player.gameObject);
            PhotonNetwork.Instantiate(LevelManager.Instance.playerPrefab.name, randomPosition, Quaternion.identity).GetComponent<Player>();
            LevelManager.Instance.player = LevelManager.Instance.playerPrefab.GetComponent<Player>();
            LevelManager.Instance.player.LoadDataPlayer();
            LevelManager.Instance.currentLevel.player = LevelManager.Instance.player;
            LevelManager.Instance.currentLevel.player.level = LevelManager.Instance.currentLevel;
            LevelManager.Instance.currentLevel.player.level.SpawnPlayer();
            JoystickInput.Instance._rigidbody = LevelManager.Instance.player.GetComponent<Rigidbody>();
            JoystickInput.Instance.playerTF = JoystickInput.Instance._rigidbody.transform;
        }
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        UIManager.Instance.OpenUI<GamePlayUI>();
        SpawnPlayer(LevelManager.Instance.player);
        LevelManager.Instance.OnStart();
        GameManagerr.Instance.ChangeState(EGameState.GamePlay);
        DataPlayerController.coinInLevel =0;
        JoystickInput.Instance.isMouse = false;
        UIManager.Instance.CloseUI<UI_LoadRoom>();
        Debug.Log("Done JOin Room");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server.");
    }
    public bool CheckIsValues(string namePlayer , string nameRoom)
    {
        return !string.IsNullOrEmpty(namePlayer) || !string.IsNullOrEmpty(nameRoom);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("Disconnected from Master Server. Reason: {0}", cause);
    }
}
