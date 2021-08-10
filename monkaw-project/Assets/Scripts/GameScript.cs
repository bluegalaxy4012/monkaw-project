using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameScript : MonoBehaviour
{
    public GameObject RubikCube;
    public GameObject PlayerPrefab;


    private void Awake()
    {
        
        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(-76.0f, 3.8f, -52.5f), Quaternion.identity, 0);
        PhotonNetwork.Instantiate(RubikCube.name, new Vector3(-70.65f, 3.7f, -51.96f), Quaternion.identity, 0);

    }
}
