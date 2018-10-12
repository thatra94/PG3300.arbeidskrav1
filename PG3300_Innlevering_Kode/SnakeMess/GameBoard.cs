using System;
using System.Collections.Generic;
using SnakeMess;
using System.Linq;

namespace SnakeMess
{
	public class GameBoard
	{
		public short NotGameOver(ref bool inUse, short newDir, Point food, Snake snake, Point tail, Point head,
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

		public void GameOver(ref bool gameOver, ref bool inUse, int boardWidth, int boardHeight, Random random, Point food, Snake snake, Point newHead)
		{
			// Going out of bounds ends game
			if (newHead.X < 0 || newHead.X >= boardWidth)
				gameOver = true;
			else if (newHead.Y < 0 || newHead.Y >= boardHeight)
				gameOver = true;

			// When an apple is eaten
			if (newHead.X == food.X && newHead.Y == food.Y)
			{
				if (snake.GetCount() + 1 >= boardWidth * boardHeight)
					// No more room to place apples - game over
					gameOver = true;
				else
				{
					// Keep placing apples.
					inUse = Food.PlaceFood(boardWidth, boardHeight, random, food, snake);
				}
			}
			if (!inUse)
			{
				// Checks if snake crashes with snake
				gameOver = Death(gameOver, snake, newHead);
			}
		}

		private static bool Death(bool gameOver, Snake snake, Point newHead)
		{
			snake.Remove(0);
			foreach (Point x in snake.GetSnake())
				if (x.X == newHead.X && x.Y == newHead.Y)
				{
					// Death by accidental self-cannibalism.
					gameOver = true;
					break;
				}

			return gameOver;
		}
	}
}