using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject LoginPopup;
    public GameObject ChattinPopup;
    public TMP_InputField LoginInput;
    public photonManager PhotonMgr;

    // Ã¤ÆÃ
    public GameObject chattingBoxParent;
    public TMP_InputField chatInput;

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
