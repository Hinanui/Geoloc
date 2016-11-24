using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controller : Photon.PunBehaviour
{
	private float speed = 10;

	public Text Debug;
	// Use this for initialization
	void Start()
	{
		Debug = GameObject.Find("Text").GetComponent<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		if (photonView.isMine == false && PhotonNetwork.connected == true)
		{
			return;
		}

		transform.position = 
			GPSTools.toLocalCoordinates(Input.location.lastData.latitude, Input.location.lastData.longitude);

		Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);

		Debug.text = "Lattitude : " + Input.location.lastData.latitude +
									"\nLongitude : " + Input.location.lastData.longitude +
									"\nXpos : " + transform.position.x +
									"\nYpos : " + transform.position.y;
	}
}
