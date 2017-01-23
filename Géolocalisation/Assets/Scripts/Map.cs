using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map
{
	private const int MAP_SIZE = 3;

	private const int HALF_MAP_SIZE = MAP_SIZE / 2;

	private readonly Area[,] areas = new Area[MAP_SIZE, MAP_SIZE];

	public Map(double centerLat, double centerLong)
	{
		for (int x = -HALF_MAP_SIZE; x < HALF_MAP_SIZE; x++)
		{
			for (int y = -HALF_MAP_SIZE; y < HALF_MAP_SIZE; y++)
			{
				areas[x + HALF_MAP_SIZE, y + HALF_MAP_SIZE] =
					new Area(x * Area.AREA_SIZE, y * Area.AREA_SIZE);
			}
		}
	}

	public float[,] getRatios(List<InvisiblePlayerController> players)
	{
		float[,] quantitiesStrong = new float[MAP_SIZE, MAP_SIZE];
		float[,] quantitiesWeak = new float[MAP_SIZE, MAP_SIZE];

		float[,] ratios = new float[MAP_SIZE, MAP_SIZE];

		for (int x = 0; x < MAP_SIZE; x++)
		{
			for (int y = 0; y < MAP_SIZE; y++)
			{
				foreach (var player in players)
				{
					if (areas[x, y].contains(player))
					{
						if (player.IsStrong)
						{
							quantitiesStrong[x, y]++;
						}
						else
						{
							quantitiesWeak[x, y]++;
						}

						break;
					}
				}
			}
		}

		for (int x = 0; x < MAP_SIZE; x++)
		{
			for (int y = 0; y < MAP_SIZE; y++)
			{
				ratios[x, y] = quantitiesWeak[x, y] / quantitiesStrong[x, y];
			}
		}

		return ratios;
	}
}
