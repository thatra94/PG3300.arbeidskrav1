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
            Console.Title = "SNAKE";
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

                    ChangeDirection(newDir, newHead);
                    GameOver(ref gameOver, ref inUse, boardWidth, boardHeight, random, food, snake, newHead);
                    if (!gameOver)
                    {
                        last = NotGameOver(ref inUse, newDir, food, snake, tail, head, newHead);
                    }
                }
            }
        }

        private static void GameOver(ref bool gameOver, ref bool inUse, int boardWidth, int boardHeight, Random random, Point food, Snake snake, Point newHead)
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

        private static void ChangeDirection(short newDir, Point newHead)
        {
            // Snake switches direction based on changes to newDir set in Controls
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
        }

        private static short NotGameOver(ref bool inUse, short newDir, Point food, Snake snake, Point tail, Point head, Point newHead)
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