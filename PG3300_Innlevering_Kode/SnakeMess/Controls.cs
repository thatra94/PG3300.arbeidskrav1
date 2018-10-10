using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeMess
{
	class Controls
	{
		public static void Control(ref bool gameOver, ref bool pause, ref short newDir, short last)
		{
			ConsoleKeyInfo cki = Console.ReadKey(true);
			if (cki.Key == ConsoleKey.Escape)
				gameOver = true;
			else if (cki.Key == ConsoleKey.Spacebar)
				pause = !pause;
			else if (cki.Key == ConsoleKey.UpArrow && last != 2)
				newDir = 0;
			else if (cki.Key == ConsoleKey.RightArrow && last != 3)
				newDir = 1;
			else if (cki.Key == ConsoleKey.DownArrow && last != 0)
				newDir = 2;
			else if (cki.Key == ConsoleKey.LeftArrow && last != 1)
				newDir = 3;
		}
	}
}
