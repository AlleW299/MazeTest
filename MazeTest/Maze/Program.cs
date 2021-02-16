using Maze.CommonFunctions;
using Maze.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Maze
{
	/// <summary>
	/// Maze path algorithm
	/// 
	/// With a little more time I would move all but the presentation to external functions
	/// </summary>
	class Program
	{
		const int STD_OUTPUT_HANDLE = -11;
		const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 4;

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern IntPtr GetStdHandle(int nStdHandle);

		[DllImport("kernel32.dll")]
		static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

		[DllImport("kernel32.dll")]
		static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

		static void Main(string[] args)
		{
			Beginning:
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.WriteLine("------------------------------------");
			Console.WriteLine("  Alex Maze path algorithm");

			// Set up comparison value so it can stop running paths that no longer can beat the existing low score
			var lowestPathValueSoFar = 100;

			// Declare list for multiple paths through the maze
			var paths = new List<Path>();

			// Setup map
			var labyrinth = new Labyrinth();

			// Create a first path
			var currentPath = new Path();
			currentPath.Steps = new int[5, 5];
			currentPath.Steps[0, 0] = 1;
			currentPath.Sum += labyrinth.Locations[0, 0];
			currentPath.Cursor = new Cursor { Xpos = 0, Ypos = 0 };
			currentPath.NrOfSteps = 1;
			paths.Add(currentPath);

			// Calculate paths through the labyrinth one step at a time
			for (int step = 0; step < 25; step++)
			{
				// Loop through all pathes
				for (int p=0; p < paths.Count; p++)
				{
					var path = paths[p];
					// Bonus Feature. Ignor all pathes that already have a higher value then the sortest found so far.
					if (path.Sum <= lowestPathValueSoFar)
					{
						if (path.Cursor.Xpos == 4 && path.Cursor.Ypos == 4)
						{
							path.Finnished = true;
							lowestPathValueSoFar = paths.FindAll(o => o.Finnished == true).Select(s => s.Sum).Min();
						}
						else
						{
							var futureCursorPos = Steps.FindTheNextSteps(path);
							if (futureCursorPos.Count > 0)
							{
								// Bifurcation when there is more then one possible path
								// No need to calculate bifurcations from step one, just clone the original path and contine from there.
								for (var c = 1; c < futureCursorPos.Count; c++)
								{
									var pathClone = Bifurcations.CloneExistingPath(path);
									pathClone.Steps[futureCursorPos[c].Xpos, futureCursorPos[c].Ypos] = 1;
									pathClone.Sum += labyrinth.Locations[futureCursorPos[c].Xpos, futureCursorPos[c].Ypos];
									pathClone.Cursor = futureCursorPos[c];
									paths.Add(pathClone);
								}

								// Mod the original path last so not to mess with bifurcation rutine.
								path.Steps[futureCursorPos[0].Xpos, futureCursorPos[0].Ypos] = 1;
								path.Sum += labyrinth.Locations[futureCursorPos[0].Xpos, futureCursorPos[0].Ypos];
								path.Cursor = futureCursorPos[0];
							}
						}
					}
				}
			}

			var triedPaths = paths.Count;
			var lowestPathValue = paths.FindAll(o => o.Finnished == true).Select(s => s.Sum).Min();

			// Presentation
			Console.WriteLine("------------------------------------");
			Console.WriteLine(String.Format("  {0} tried paths.", triedPaths));
			Console.WriteLine(String.Format("  Lowest value path found: {0}", lowestPathValue));
			Console.WriteLine("------------------------------------");
			// Render Maze with shortest paths
			foreach (var path in paths.FindAll(o => o.Finnished == true && o.Sum == lowestPathValue))
			{
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Green;
				for (var x = 0; x < 5; x++)
				{
					Console.Write("  ");
					for (var y = 0; y < 5; y++)
					{
						if (path.Steps[x, y] > 0)
						{
							WriteUnderline(labyrinth.Locations[x, y]);
							Console.Write(" ");
						}
						else
						{
							Console.Write(labyrinth.Locations[x, y] + " ");
						}
					}
					Console.WriteLine();
				}
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine();
				Console.WriteLine("  Sum: " + path.Sum);
				Console.WriteLine("------------------------------------");
			}
			
			Console.WriteLine();
			Console.WriteLine("Want to run again? type 'y' ");
			var key = Console.ReadKey().Key;
			if (key == ConsoleKey.Y)
				goto Beginning;

		}
	
		private static void WriteUnderline(int s)
		{
			var handle = GetStdHandle(STD_OUTPUT_HANDLE);
			uint mode;
			GetConsoleMode(handle, out mode);
			mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
			SetConsoleMode(handle, mode);
			Console.Write($"\x1B[4m{s.ToString()}\x1B[24m");
		}
	}
}
