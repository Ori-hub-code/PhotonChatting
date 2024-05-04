using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using WebSocketSharp;

public class photonManager : MonoBehaviourPunCallbacks
{
    private readonly string version = "1.0f";
    public string userId;
    public GameObject chattingBoxprefab;
    public GameObject playerBoxPrefab;
    public List<GameObject> playerList;

    UIManager uiManager;

    private void Awake()
    {
        uiManager = UIManager.Instance;

        playerList = new List<GameObject>();
    }


    // 포톤 서버에 접속 후 호출되는 콜백 함수
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        Debug.Log($"PhotonNectwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby();
    }

    // 로비에 접속 후 호출되는 콜백 함수
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNectwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinRandomRoom();
    }

    //룸 입장 실패했을 경우
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandom Filed {returnCode}:{message}");

        //룸의 속성 정의
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;
        ro.IsOpen = true;
        ro.IsVisible = true; //로비에서 룸 목록에 노출 시킬지 여부
        ro.CleanupCacheOnLeave = false;  // 포톤 서버를 종료해도 본인이 생성한 오브젝트를 자동으로 삭제 안함.

        //룸 생성
        PhotonNetwork.CreateRoom("My Room", ro);
    }

    //룸 생성이 완료된 후 호출되는 콜백 함수
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    //룸에 입장한 후
    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNectwork.InRoom = {PhotonNetwork.InRoom}");
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        // 룸에 접속한 사용자 정보확인
        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName}, {player.Value.ActorNumber}");
        }

        PhotonNetwork.Instantiate(playerBoxPrefab.name, transform.position, Quaternion.identity);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"{newPlayer.NickName} 님이 접속했습니다.");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"{otherPlayer.NickName} 님이 나가셨습니다.");

        DeletePlayer(otherPlayer.ActorNumber);
    }


    // -------------------------------------------------------------

    public void JoinPhoton()
    {
        if (uiManager.LoginInput.text.IsNullOrEmpty())
        {
            Debug.Log("입력바람");
        }
        else
        {
            // 같은 룸의 유저들에게 자동으로 씬을 로딩
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = version;
            Debug.Log(PhotonNetwork.SendRate);

            userId = uiManager.LoginInput.text;
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
            Debug.Log("입력바람....");
        }
        else
        {
            PhotonNetwork.Instantiate(chattingBoxprefab.name, transform.position, Quaternion.identity);
        }
    }

    void DeletePlayer(int actNum)
    {
        for(int i =0; i<playerList.Count; i++)
        {
            PhotonView pv = playerList[i].GetComponent<PhotonView>();

            if(pv.ViewID / 1000 == actNum)
            {
                Destroy(pv.gameObject);
                playerList.Remove(playerList[i]);
            }
        }
    }

}
