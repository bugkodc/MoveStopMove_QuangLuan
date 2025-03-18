using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UI_LoadRoom : UICanvas
{
    [SerializeField] private Text playerName;
    [SerializeField] private Text RoomName;
    public void CreateRoom()
    {
        PhotonNetwork.NickName = playerName.text;
        PhotonManager.Instance._namePlayer = playerName.text;
        PhotonManager.Instance._nameRoom = RoomName.text;
        PhotonManager.Instance.CreateRoom();
        Close();

    }
    public void JoinRoom()
    {
        PhotonNetwork.NickName = playerName.text;
        PhotonManager.Instance._namePlayer = playerName.text;
        PhotonManager.Instance._nameRoom = RoomName.text;
        PhotonManager.Instance.JoinRoom();
        Close();
    }
}
