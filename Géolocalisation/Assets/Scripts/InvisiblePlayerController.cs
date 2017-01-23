using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public class InvisiblePlayerController : Photon.PunBehaviour, IPunObservable
{
	public double? Lat { get; private set; }
	public double? Long { get; private set; }
	public bool Alive { get; private set; }
	public bool IsStrong { get; private set; }

	private Text Debug;

	private List<double> playersDistance = new List<double>();

	// Use this for initialization
	void Start ()
	{
		Lat = null;
		Long = null;
		Alive = true;

		Debug = FindObjectsOfType<Text>()[0];
	}
	
	// Update is called once per frame
	void Update () {

		if (photonView.isMine == false && PhotonNetwork.connected == true)
		{
			return;
		}

		updateLocation();

		StringBuilder str = new StringBuilder();

		InvisiblePlayerController[] t = FindObjectsOfType<InvisiblePlayerController>();

		foreach (var player in t)
		{
			if (player == this || player.Lat == null || !player.Alive)
			{
				continue;
			}

			double distance = GPSTools.GPSDistanceInMeters(
				this.Lat.Value,
				this.Long.Value,
				player.Lat.Value,
				player.Long.Value
			);

			playersDistance.Add(distance);

			str.Append(distance).Append("\n");
		}

		Debug.text = str.ToString();
	}

	void updateLocation()
	{
		Lat = Input.location.lastData.latitude;
		Long = Input.location.lastData.longitude;
	}

	void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(Lat);
			stream.SendNext(Long);
			stream.SendNext(Alive);
		}
		else
		{
			Lat = (double?) stream.ReceiveNext();
			Long = (double?)stream.ReceiveNext();
			Alive = (bool)stream.ReceiveNext();
		}
	}
}
