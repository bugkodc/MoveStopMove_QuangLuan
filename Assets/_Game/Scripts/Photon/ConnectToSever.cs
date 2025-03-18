using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class ConnectToSever : MonoBehaviourPunCallbacks
{
    [SerializeField] private float _timeLoading;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected");
        Invoke("LoadLevel", _timeLoading);
    }
    void LoadLevel()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
