using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour {

    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }
    private PhotonView PhotonView;
    private int PlayersInGame = 0;

	// Use this for initialization
	private void Awake ()
    {
        Instance = this;
        PhotonView = GetComponent<PhotonView>();
        PlayerName = "Distul#" + Random.Range(1000, 9999);

        SceneManager.sceneLoaded += OnSceneFinishedLoding;
	}

    private void OnSceneFinishedLoding(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Shooting_Range(Multiplayer)")
        {
            if (PhotonNetwork.isMasterClient)
            {
                MasterLoadedGame();
            }
            else
            {
                NonMasterLoadedGame();
            }
        }
    }

    private void MasterLoadedGame()
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
        PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    private void NonMasterLoadedGame()
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
    }

    //Telling all other clients to load level 1 whenever master loads into the game level 1.
    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        PlayersInGame++;
        if(PlayersInGame == PhotonNetwork.playerList.Length)
        {
            Debug.Log("All players are in the game scene.");
        }
    }
}
