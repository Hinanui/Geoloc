using UnityEngine;
using System.Collections;

public class GameManager : Photon.PunBehaviour {

	[Tooltip("The prefab to use for representing the player")]
	public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
		if (playerPrefab == null)
		{
			Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
		}
		else
		{
			Debug.Log("We are Instantiating LocalPlayer from " + Application.loadedLevelName);
			// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
			PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
