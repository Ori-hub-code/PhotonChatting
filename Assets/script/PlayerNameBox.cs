using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Photon.Pun;

public class PlayerNameBox : MonoBehaviour
{
    Image img;
    public TextMeshProUGUI playerTxt;

    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        img = GetComponent<Image>();
    }

    private void OnEnable()
    {
        this.gameObject.transform.SetParent(UIManager.Instance.playerNameParent.transform);

        if (pv.IsMine)
        {
            img.color = Color.yellow;

            pv.RPC("GetPlayer", RpcTarget.AllBuffered, UIManager.Instance.PhotonMgr.userId);

        }
        else
        {
            img.color = Color.gray;
        }

        UIManager.Instance.PhotonMgr.playerList.Add(this.gameObject);
    }

    [PunRPC]
    void GetPlayer(string text)
    {
        playerTxt.text = text;
    }
}
