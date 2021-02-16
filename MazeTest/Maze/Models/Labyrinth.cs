using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.Models
{
	public class Labyrinth
	{
		public Labyrinth()
		{
			// Declare randomizer
			Random random = new Random();

			Locations = new int[5, 5];
			for (var x = 0; x < 5; x++)
			{
				for (var y = 0; y < 5; y++)
				{
					Locations[x, y] = random.Next(9) + 1;
				}
			}
		}

		public int[,] Locations { get; set; }
	}
}
