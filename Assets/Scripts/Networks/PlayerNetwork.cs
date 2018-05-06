using UnityEngine;

public class PlayerNetwork : MonoBehaviour {

    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }

	// Use this for initialization
	private void Awake () {
        Instance = this;
        PlayerName = "Distul#" + Random.Range(1000, 9999);
	}
}
