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

        LoginPopup.SetActive(true);
        ChattinPopup.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void InfoBtn() // 정보창 열렸다 닫혔다
    {
        Vector2 closeMinAnchors = new Vector2(1,0);
        Vector2 closeMaxAnchors = new Vector2(1.3f, 1);

        Vector2 openMinAnchors = new Vector2(0.7f, 0);
        Vector2 openMaxAnchors = new Vector2(1, 1);

        if(infoBox.anchorMin == closeMinAnchors)
        {
            StartCoroutine(
            SmoothCoroutine(infoBox, closeMinAnchors, closeMaxAnchors, openMinAnchors, openMaxAnchors, 0.25f));
        }
        else
        {
            StartCoroutine(
            SmoothCoroutine(infoBox, openMinAnchors, openMaxAnchors, closeMinAnchors, closeMaxAnchors, 0.25f));
        }
    }

    IEnumerator SmoothCoroutine(RectTransform original, Vector2 nowMin, Vector2 nowMax, Vector2 nextMin, Vector2 nextMax, float time) // 정보창 열렸다 닫혔다
    {
        Vector3 velocity = Vector3.zero;

        original.anchorMin = nowMin;
        original.anchorMax = nowMax;

        float offset = 0.01f;

        if(nowMin.x < nextMin.x) // 열려있을 때
        {
            while (nextMin.x - offset >= original.anchorMin.x)
            {
                original.anchorMin
                    = Vector3.SmoothDamp(original.anchorMin, nextMin, ref velocity, time);

                original.anchorMax
                    = Vector3.SmoothDamp(original.anchorMax, nextMax, ref velocity, time);

                yield return null;
            }
        }
        else // 닫혀있을 때
        {
            while (nextMin.x + offset <= original.anchorMin.x)
            {
                original.anchorMin
                    = Vector3.SmoothDamp(original.anchorMin, nextMin, ref velocity, time);

                original.anchorMax
                    = Vector3.SmoothDamp(original.anchorMax, nextMax, ref velocity, time);

                yield return null;
            }
        }
        

        original.anchorMin = nextMin;
        original.anchorMax = nextMax;

        yield return null;
    }
}
