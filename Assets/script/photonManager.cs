using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using WebSocketSharp;

public class photonManager : MonoBehaviourPunCallbacks
{
    private readonly string version = "1.0f";
    private string userId = "Mary";
    public GameObject chattingBoxprefab;

    UIManager uiManager;

    private void Awake()
    {
        uiManager = UIManager.Instance;
    }


    // ���� ������ ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        Debug.Log($"PhotonNectwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby();
    }

    // �κ� ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNectwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinRandomRoom();
    }

    //�� ���� �������� ���
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandom Filed {returnCode}:{message}");

        //���� �Ӽ� ����
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;
        ro.IsOpen = true;
        ro.IsVisible = true; //�κ񿡼� �� ��Ͽ� ���� ��ų�� ����

        //�� ����
        PhotonNetwork.CreateRoom("My Room", ro);
    }

    //�� ������ �Ϸ�� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    //�뿡 ������ ��
    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNectwork.InRoom = {PhotonNetwork.InRoom}");
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        // �뿡 ������ ����� ����Ȯ��
        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName}, {player.Value.ActorNumber}");
        }
    }


    // -------------------------------------------------------------

    public void JoinPhoton()
    {
        if (uiManager.LoginInput.text.IsNullOrEmpty())
        {
            Debug.Log("�Է¹ٶ�");
        }
        else
        {
            // ���� ���� �����鿡�� �ڵ����� ���� �ε�
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = version;
            Debug.Log(PhotonNetwork.SendRate);

            PhotonNetwork.NickName = uiManager.LoginInput.text;
            uiManager.LoginInput.text = "";


            PhotonNetwork.ConnectUsingSettings();

            uiManager.ChattinPopup.SetActive(true);
            uiManager.LoginPopup.SetActive(false);
        }
    }

    public void SendMessage()
    {
        if (uiManager.chatInput.text.IsNullOrEmpty())
        {
            Debug.Log("�Է¹ٶ�....");
        }
        else
        {
            PhotonNetwork.Instantiate(chattingBoxprefab.name, transform.position, Quaternion.identity);
        }
    }

}
