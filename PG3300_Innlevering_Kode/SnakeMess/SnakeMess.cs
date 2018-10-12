using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
            var random = new Random();
            var food = new Point();
            var snake = new Snake();
            var controls = new Controls();
	        var gameBoard = new GameBoard();

            Console.CursorVisible = false;
            Console.Title = "SNAKE";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(10, 10);
            Console.Write("@");

            //Places food on the board
            inUse = Food.PlaceFood(boardWidth, boardHeight, random, food, snake);

            var time = new Stopwatch();
            time.Start();

            //Main game-loop
            while (!gameOver)
            {
                if (Console.KeyAvailable)
                {
                    controls.Control(ref gameOver, ref pause, ref newDir, last);
                }
                if (!pause)
                {
                    if (time.ElapsedMilliseconds < 100)
                        continue;
                    time.Restart();
                    var tail = new Point(snake.GetFirst());
                    var head = new Point(snake.GetLast());
                    var newHead = new Point(head);

                    controls.ChangeDirection(newDir, newHead);
                    gameBoard.GameOver(ref gameOver, ref inUse, boardWidth, boardHeight, random, food, snake, newHead);
                    if (!gameOver)
                    {
                        last = gameBoard.NotGameOver(ref inUse, newDir, food, snake, tail, head, newHead);
                    }
                }
            }
        }
    }
}