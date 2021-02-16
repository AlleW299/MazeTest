using Maze.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.CommonFunctions
{
	public class Steps
	{
		/// <summary>
		/// Finds the lowest next step(s)
		/// Some logical assuptions are made...
		/// 1. if the cursor is at the last 2 possitions to the right, there is no way going upwards can be a shorter rout.
		/// 2. If the cursor is at the bottom 2 possitions, there is no way going left can be the shourter rout.
		/// 
		/// ( 1 and two is not nessecery true if maze possition values can be negative )
		/// </summary>
		/// <param name="maze"></param>
		/// <param name="cursor"></param>
		/// <param name="currentPath"></param>
		/// <returns></returns>
		public static List<Cursor> FindTheNextSteps(Path currentPath)
		{
			var futureSteps = new List<Cursor>();

			// Up
			if (currentPath.Cursor.Ypos > 0 && currentPath.Steps[currentPath.Cursor.Xpos, currentPath.Cursor.Ypos - 1] == 0 
				&& currentPath.Cursor.Xpos < 3)
			{
				futureSteps.Add( new Cursor { Xpos = currentPath.Cursor.Xpos, Ypos = currentPath.Cursor.Ypos - 1 });
			}

			// Right
			if (currentPath.Cursor.Xpos < 4 && currentPath.Steps[currentPath.Cursor.Xpos + 1, currentPath.Cursor.Ypos] == 0)
			{
				futureSteps.Add(new Cursor { Xpos = currentPath.Cursor.Xpos + 1, Ypos = currentPath.Cursor.Ypos });
			}

			// Down
			if (currentPath.Cursor.Ypos < 4 && currentPath.Steps[currentPath.Cursor.Xpos, currentPath.Cursor.Ypos + 1] == 0)
			{
				futureSteps.Add(new Cursor { Xpos = currentPath.Cursor.Xpos, Ypos = currentPath.Cursor.Ypos + 1 });
			}

			// left
			if (currentPath.Cursor.Xpos > 0 && currentPath.Steps[currentPath.Cursor.Xpos - 1, currentPath.Cursor.Ypos] == 0 
				&& currentPath.Cursor.Ypos < 3)
			{
				futureSteps.Add(new Cursor { Xpos = currentPath.Cursor.Xpos - 1, Ypos = currentPath.Cursor.Ypos });
			}

			return futureSteps;
		}
	}
}
