using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour {
    [SerializeField]
    private Text _roomName;
    private Text RoomName
    {
        get { return _roomName; }
    }

    public void OnClick_CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };

        if (PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
        {
            Debug.Log("Create Room succesfully sent.");
        }
        else
        {
            Debug.Log("Create Room failed to send");
        }
    }

    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        Debug.Log("Create Room failed: " + codeAndMessage[1]);
    }

    private void OnCreatedRoom()
    {
        Debug.Log("Room created succesfully.");
    }
}
