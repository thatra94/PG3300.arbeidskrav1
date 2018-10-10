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

            var snake = new Snake();
			var Controls = new Controls();

            Console.CursorVisible = false;
            Console.Title = "Høyskolen Kristiania - SNAKE";
            Console.ForegroundColor = ConsoleColor.Green; 
	        Console.SetCursorPosition(10, 10); 
	        Console.Write("@");

            //Places food on the board
            inUse = Food.PlaceFood(boardWidth, boardHeight, random, food, snake);

            Stopwatch time = new Stopwatch();
            time.Start();

			//Main gameloop
            while (!gameOver)
            {
                if (Console.KeyAvailable)
                {
                    Controls.Control(ref gameOver, ref pause, ref newDir, last);
                }
                if (!pause)
                {
                    if (time.ElapsedMilliseconds < 100)
                        continue;
                    time.Restart();
                    Point tail = new Point(snake.GetFirst());
                    Point head = new Point(snake.GetLast());
                    Point newHead = new Point(head);
                    switch (newDir)
                    {
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
                    if (newHead.X == food.X && newHead.Y == food.Y)
                    {
                        if (snake.GetCount() + 1 >= boardWidth * boardHeight)
                            // No more room to place apples - game over.
                            gameOver = true;
                        else
                        {
                            inUse = Food.PlaceFood(boardWidth, boardHeight, random, food, snake);
                        }
                    }
                    if (!inUse)
                    {
                        gameOver = Death(gameOver, snake, newHead);
                    }
                    if (!gameOver)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(head.X, head.Y); Console.Write("0");
                        if (!inUse)
                        {
                            Console.SetCursorPosition(tail.X, tail.Y); Console.Write(" ");
                        }
                        else
                        {
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



       /*
        private static void Controls(ref bool gameOver, ref bool pause, ref short newDir, short last)
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
		*/
    }
}