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

        LoginPopup.SetActive(true);
        ChattinPopup.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void InfoBtn() // ����â ���ȴ� ������
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

    IEnumerator SmoothCoroutine(RectTransform original, Vector2 nowMin, Vector2 nowMax, Vector2 nextMin, Vector2 nextMax, float time) // ����â ���ȴ� ������
    {
        Vector3 velocity = Vector3.zero;

        original.anchorMin = nowMin;
        original.anchorMax = nowMax;

        float offset = 0.01f;

        if(nowMin.x < nextMin.x) // �������� ��
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
        else // �������� ��
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
