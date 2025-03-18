using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PhotonPlayerRPC : MonoBehaviour
{
    public PhotonView view;
    public Player player;
    void Start()
    {
        view = GetComponent<PhotonView>();
        player = GetComponent<Player>();
    }

  
}
