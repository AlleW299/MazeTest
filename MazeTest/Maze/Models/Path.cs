using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.Models
{
	public class Path
	{
		public int NrOfSteps { get; set; }
		public int[,] Steps { get; set; }
		public int Sum { get; set; }
		public Cursor Cursor { get; set; }
		public bool Finnished { get; set; }
	}
}
