using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChattingBox : MonoBehaviour
{
    PhotonView pv;

    VerticalLayoutGroup verticalLG;
    public Image img;
    public TextMeshProUGUI chatText;



    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        verticalLG = GetComponent<VerticalLayoutGroup>();

    }

    void OnEnable()
    {
        this.gameObject.transform.SetParent(UIManager.Instance.chattingBoxParent.transform);

        if(pv.IsMine == true)
        {
            img.color = Color.yellow;
            verticalLG.childAlignment = TextAnchor.UpperRight;

            pv.RPC("SetText", RpcTarget.AllBuffered, UIManager.Instance.chatInput.text);

            UIManager.Instance.chatInput.text = "";

            UIManager.Instance.scrollbar.value = 0;
        }
        else
        {
            img.color = Color.gray;
            verticalLG.childAlignment = TextAnchor.UpperLeft;
        }

    }

    [PunRPC]
    void SetText(string text)
    {
        chatText.text = "";

        char[] chars = text.ToCharArray();

        for(int i = 0; i < chars.Length; i++)
        {
            if(i % 20 == 0 && i != 0)
            {
                chatText.text += System.Environment.NewLine + chars[i];
            } 
            else
            {
                chatText.text += chars[i];
            }
        }
    }

}
