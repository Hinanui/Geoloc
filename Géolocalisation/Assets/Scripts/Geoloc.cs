using UnityEngine;
using System.Collections;

public class Geoloc : MonoBehaviour
{
	public bool useFakeCamPos = true;
	public static double camLat = 48.846599;
	public static double camLong = 2.385307;

	InputAverage compassValues = new InputAverage();

	IEnumerator Start()
	{
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
			yield break;

		// Start service before querying location
		Input.location.Start(0.1f, 0.1f);

		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			print("Timed out");
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			print("Unable to determine device location");
			yield break;
		}
		else
		{
			Input.compass.enabled = true;
			//UpdateCamPosToCurrentLocation();
//			while (true)
//			{
//				compassValues.AddInput(Input.compass.trueHeading);
//				Camera.main.transform.rotation = Quaternion.AngleAxis(90, Vector3.right);
//				Camera.main.transform.rotation *= Quaternion.AngleAxis(compassValues.getAverage(), Camera.main.transform.up);
//
//				yield return null;
//			}
		}
	}

	void UpdateCamPosToCurrentLocation()
	{
		camLat = Input.location.lastData.latitude;
		camLong = Input.location.lastData.longitude;
		Debug.Log("Geoloc::LOCATION: " + camLat + " " + camLong);
		Vector3 currentPos = GPSTools.toLocalCoordinates(camLat, camLong);
		Debug.Log("Geoloc::translated pos currentPos: " + currentPos);
		currentPos.y = 10;
		Camera.main.transform.position = currentPos;
	}

	void Update()
	{
		Debug.Log("Geoloc::LOCATION: " + camLat + " " + camLong);
		Debug.Log("Geoloc::Mag HEADING: " + Input.compass.magneticHeading);
		Debug.Log("Geoloc::True HEADING: " + Input.compass.trueHeading);
	}


	void OnDestroy()
	{
		Input.location.Stop();
	}
}