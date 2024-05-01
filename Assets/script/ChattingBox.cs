using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChattingBox : MonoBehaviour
{
    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    void OnEnable()
    {
        this.gameObject.transform.SetParent(UIManager.Instance.chattingBoxParent.transform);
    }

}
