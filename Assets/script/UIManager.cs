using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public photonManager PhotonMgr;

    [Header ("# UI Object")]
    public GameObject LoginPopup; // �α��� â
    public GameObject ChattinPopup; // ä�� â

    [Header("# Login")]
    public TMP_InputField LoginInput; // �α��� �̸� input field


    [Header("# Chatting")]
    public GameObject chattingBoxParent; // ä�� �θ� ������Ʈ
    public TMP_InputField chatInput; // ä�� �Է� input field
    public Scrollbar scrollbar; // ä�� scroll view ��ũ�� ��

    [Header("# InfoBox")]
    public RectTransform infoBox; // ����â
    public GameObject playerNameParent; // �÷��̾� �̸� �θ� ������Ʈ

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
