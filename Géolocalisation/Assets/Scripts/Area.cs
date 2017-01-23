using UnityEngine;
using System.Collections;

public class Area
{

	public const double AREA_SIZE = 0.100;

	private const double HALF_AREA_SIZE = AREA_SIZE / 2.0;

	private double cornerLat;
	private double cornerLong;

	public Area(double centerLat, double centerLong)
	{
		cornerLat = centerLat + HALF_AREA_SIZE;
		cornerLong = centerLong + HALF_AREA_SIZE;
	}

	public bool contains(InvisiblePlayerController player)
	{
		return player.Lat >= cornerLat && player.Lat < cornerLat + HALF_AREA_SIZE
				&& player.Long >= cornerLong && player.Long < cornerLong + HALF_AREA_SIZE;
	}
}
