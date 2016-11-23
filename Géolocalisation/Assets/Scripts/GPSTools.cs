using UnityEngine;
using System.Collections;

public class GPSTools : MonoBehaviour
{
	static double refLat = 48.846599;
	static double refLon = 2.385307;
	

	public static Vector3 toLocalCoordinates(double lat, double lon)
	{
		lat -= refLat;
		lon -= refLon;

		float scaleFactor = 10000;
		return new Vector3((float)(lat * scaleFactor), (float)(lon * scaleFactor), 0);
	}

	public static float GPSDistanceInMeters(double lat1, double lon1, double lat2, double lon2)
	{
		float R = 6371; // km
		float dLat = (float)(lat2 - lat1) * Mathf.Deg2Rad;
		float dLon = (float)(lon2 - lon1) * Mathf.Deg2Rad;
		lat1 = lat1 * Mathf.Deg2Rad;
		lat2 = lat2 * Mathf.Deg2Rad;

		float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
				Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2) * Mathf.Cos((float)lat1) * Mathf.Cos((float)lat2);
		float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
		float d = R * c;
		return d * 1000;
	}


	// returns a percent from min to max bound pos
	/*public static Vector3 GPSPosToBoundsRelativePos(GPSPos pos, GPSBounds bounds)
  {
	Vector3 v = Vector3.zero;
	double distToBoundsMinX = pos.lon - bounds.min.lon;
	double distToBoundsMinY = pos.lat - bounds.min.lat;
	double boundsSizeX = bounds.max.lon - bounds.min.lon;
	double boundsSizeY = bounds.max.lat - bounds.min.lat;
	if(boundsSizeX < 0) boundsSizeX *= -1;
	if(boundsSizeY < 0) boundsSizeY *= -1;

	v.x = (float)(distToBoundsMinX / boundsSizeX);
	v.y = (float)(distToBoundsMinY / boundsSizeY);
	return v;
  }*/
}
