using UnityEngine;
using System.Collections;

public class Controller : Photon.PunBehaviour
{
	private float speed = 10;
	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (photonView.isMine == false && PhotonNetwork.connected == true)
		{
			return;
		}

		if (Input.GetKey(KeyCode.Z))
		{
			transform.position += Vector3.up * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.position -= Vector3.up * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.Q))
		{
			transform.position += Vector3.left * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += Vector3.right * Time.deltaTime;
		}
	}
}
