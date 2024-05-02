using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public photonManager PhotonMgr;

    [Header ("# UI Object")]
    public GameObject LoginPopup; // 로그인 창
    public GameObject ChattinPopup; // 채팅 창

    [Header("# Login")]
    public TMP_InputField LoginInput; // 로그인 이름 input field


    [Header("# Chatting")]
    public GameObject chattingBoxParent; // 채팅 부모 오브젝트
    public TMP_InputField chatInput; // 채팅 입력 input field
    public Scrollbar scrollbar; // 채팅 scroll view 스크롤 바

    [Header("# InfoBox")]
    public RectTransform infoBox; // 정보창
    public GameObject playerNameParent; // 플레이어 이름 부모 오브젝트

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
