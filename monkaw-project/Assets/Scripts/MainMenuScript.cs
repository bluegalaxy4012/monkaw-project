using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectMenu;
    [SerializeField] private TMP_InputField UsernameInput;
    [SerializeField] private TMP_InputField CreateGameInput;
    [SerializeField] private TMP_InputField JoinGameInput;
    [SerializeField] private GameObject PlayButton;
    [SerializeField] private Button CreateButton;
    [SerializeField] private Button JoinButton;
    public TMP_Text LoadingText;
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
    private void Update()
    {
        if (CreateGameInput.text.Length < 2 || CreateGameInput.text.Length > 12)
        {
            CreateButton.interactable = false;
        }
        else
        {
            CreateButton.interactable = true;
        }


        if (JoinGameInput.text.Length < 2 || JoinGameInput.text.Length > 12)
        {
            JoinButton.interactable = false;
        }
        else
        {
            JoinButton.interactable = true;
        }
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
        if (CreateGameInput.text.Length >= 2 && CreateGameInput.text.Length <= 12)
        {
            PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { MaxPlayers = 2 }, null);
            LoadingText.text = "Loading...";
            CreateButton.gameObject.SetActive(false);
            JoinButton.gameObject.SetActive(false);

        }
        
       
    }
    public void JoinGame()
    {
        if (JoinGameInput.text.Length >= 2 && JoinGameInput.text.Length <= 12)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);
            LoadingText.text = "Loading...";
            CreateButton.gameObject.SetActive(false);
            JoinButton.gameObject.SetActive(false);
        }
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
