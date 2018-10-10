using System;
using System.Collections.Generic;
using SnakeMess;
using System.Linq;

namespace SnakeMess
{
	public class GameBoard
	{
		public static short NotGameOver(ref bool inUse, short newDir, Point food, Snake snake, Point tail, Point head,
			Point newHead)
		{
			short last;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.SetCursorPosition(head.X, head.Y);
			Console.Write("0");
			if (!inUse)
			{
				Console.SetCursorPosition(tail.X, tail.Y);
				Console.Write(" ");
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.SetCursorPosition(food.X, food.Y);
				Console.Write("$");
				inUse = false;
			}

			snake.Add(newHead);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.SetCursorPosition(newHead.X, newHead.Y);
			Console.Write("@");
			last = newDir;
			return last;
		}
	}
}