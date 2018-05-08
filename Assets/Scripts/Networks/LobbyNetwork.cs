using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour {

	// Use this for initialization
	private void Start ()
    {
        if (!PhotonNetwork.connected)
        {
            Debug.Log("Connecting to server...");
            PhotonNetwork.ConnectUsingSettings("0.0.0");
        }
	}

    private void OnConnectedToMaster()
    {
        Debug.Log("Connected to master.");
        PhotonNetwork.automaticallySyncScene = false;
        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    private void OnJoinedLobby()
    {
        Debug.Log("Joined lobby.");

        if (!PhotonNetwork.inRoom)
        {
            MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();
        }
    }
}
