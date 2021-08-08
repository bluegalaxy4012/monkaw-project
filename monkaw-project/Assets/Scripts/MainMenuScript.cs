using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MainMenuScript : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectMenu;
    [SerializeField] private TMP_InputField UsernameInput;
    [SerializeField] private TMP_InputField CreateGameInput;
    [SerializeField] private TMP_InputField JoinGameInput;
    [SerializeField] private GameObject PlayButton;
    public void ExitFunction()
    {
        Application.Quit();
    }
    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Start()
    {
        UsernameMenu.SetActive(true);
        ConnectMenu.SetActive(false);
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public void ChangeUserNameInput()
    {
        if (UsernameInput.text.Length >= 2 && UsernameInput.text.Length <= 12)
        {
            PlayButton.SetActive(true);
        }
        else
        {
            PlayButton.SetActive(false);
        }
    }
    public void SetUserName()
    {
        UsernameMenu.SetActive(false);
        PhotonNetwork.NickName = UsernameInput.text;
        ConnectMenu.SetActive(true);
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { MaxPlayers = 2 }, null);
    }
    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);
    }
    public void BackFunction()
    {
        UsernameMenu.SetActive(true);
        ConnectMenu.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainGame");
    }


}
