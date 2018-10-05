using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

// WARNING: DO NOT code like this. Please. EVER! 
//          "Aaaargh!" 
//          "My eyes bleed!" 
//          "I facepalmed my facepalm." 
//          Etc.
//          I had a lot of fun obfuscating this code! And I can now (proudly?) say that this is the uggliest short piece of code I've ever written!
//          (It could maybe have been even ugglier? But the idea wasn't to make it fuggly-uggly, just funny-uggly or sweet-uggly. ;-)
//
//          -Tomas
//
//			funker? joa neida
namespace SnakeMess
{
	class Point
	{
		public const string Ok = "Ok";

		public int X; public int Y;
		public Point(int x = 0, int y = 0) { X = x; Y = y; }
		public Point(Point input) { X = input.X; Y = input.Y; }
	}

	class SnakeMess
	{
		public static void Main(string[] arguments)
		{
			bool gameOver = false, pause = false, inUse = false;
			short newDir = 2; // 0 = up, 1 = right, 2 = down, 3 = left
			short last = newDir;
			int boardWidth = Console.WindowWidth, boardHeight = Console.WindowHeight;
			Random random = new Random();
			Point food = new Point();
			List<Point> snake = new List<Point>();
			snake.Add(new Point(10, 10)); snake.Add(new Point(10, 10)); snake.Add(new Point(10, 10)); snake.Add(new Point(10, 10));
			Console.CursorVisible = false;
			Console.Title = "Høyskolen Kristiania - SNAKE";
			Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(10, 10); Console.Write("@");
			while (true) {
				food.X = random.Next(0, boardWidth); food.Y = random.Next(0, boardHeight);
				bool freeSpot = true;
				foreach (Point i in snake)
					if (i.X == food.X && i.Y == food.Y) {
						freeSpot = false;
						break;
					}
				if (freeSpot) {
					Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(food.X, food.Y); Console.Write("$");
					break;
				}
			}
			Stopwatch time = new Stopwatch();
			time.Start();
			while (!gameOver) {
				if (Console.KeyAvailable) {
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
				if (!pause) {
					if (time.ElapsedMilliseconds < 100)
						continue;
					time.Restart();
					Point tail = new Point(snake.First());
					Point head = new Point(snake.Last());
					Point newHead = new Point(head);
					switch (newDir) {
						case 0:
							newHead.Y -= 1;
							break;
						case 1:
							newHead.X += 1;
							break;
						case 2:
							newHead.Y += 1;
							break;
						default:
							newHead.X -= 1;
							break;
					}
					if (newHead.X < 0 || newHead.X >= boardWidth)
						gameOver = true;
					else if (newHead.Y < 0 || newHead.Y >= boardHeight)
						gameOver = true;
					if (newHead.X == food.X && newHead.Y == food.Y) {
						if (snake.Count + 1 >= boardWidth * boardHeight)
							// No more room to place apples - game over.
							gameOver = true;
						else {
							while (true) {
								food.X = random.Next(0, boardWidth); food.Y = random.Next(0, boardHeight);
								bool found = true;
								foreach (Point i in snake)
									if (i.X == food.X && i.Y == food.Y) {
										found = false;
										break;
									}
								if (found) {
									inUse = true;
									break;
								}
							}
						}
					}
					if (!inUse) {
						snake.RemoveAt(0);
						foreach (Point x in snake)
							if (x.X == newHead.X && x.Y == newHead.Y) {
								// Death by accidental self-cannibalism.
								gameOver = true;
								break;
							}
					}
					if (!gameOver) {
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.SetCursorPosition(head.X, head.Y); Console.Write("0");
						if (!inUse) {
							Console.SetCursorPosition(tail.X, tail.Y); Console.Write(" ");
						} else {
							Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(food.X, food.Y); Console.Write("$");
							inUse = false;
						}
						snake.Add(newHead);
						Console.ForegroundColor = ConsoleColor.Yellow; Console.SetCursorPosition(newHead.X, newHead.Y); Console.Write("@");
						last = newDir;
					}
				}
			}
		}
	}
}