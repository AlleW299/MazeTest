using Maze.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.CommonFunctions
{
	public class Bifurcations
	{
		public static int GetIndexFromList(List<int> list)
		{
			int minima = 20;
			int mindex = 0;

			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] < minima)
				{ 
					minima = list[i];
					mindex = i; 
				}
			}
			return mindex;
		}

		/// <summary>
		/// Clone existing path for when there are multiple paths forward
		/// </summary>
		/// <param name="oldPath"></param>
		/// <returns></returns>
		public static Path CloneExistingPath(Path oldPath)
		{
			var newPath = new Path();
			newPath.Sum = oldPath.Sum;
			newPath.Cursor = new Cursor { Xpos = oldPath.Cursor.Xpos, Ypos = oldPath.Cursor.Ypos };
			newPath.Steps = new int[5,5];
			for (var x = 0; x < 5; x++)
			{
				for (var y = 0; y < 5; y++)
				{
					newPath.Steps[x, y] = oldPath.Steps[x, y];
				}
			}
			newPath.NrOfSteps = oldPath.NrOfSteps;
			return newPath;
		}
	}
}
