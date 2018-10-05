using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using SnakeMess;

namespace SnakeMess
{
    public class Food
    {

        bool freeSpot = true;
        private Point _point = new Point();

        public Food(int boardWidth, int BoardHeigth)
        {

            Random random = new Random();
            Point food = new Point();

            while (true)
            {
                food.X = random.Next(0, boardWidth); food.Y = random.Next(0, boardHeight);
                bool freeSpot = true;
                foreach (Point i in snake.GetSnake())
                    if (i.X == food.X && i.Y == food.Y)
                    {
                        freeSpot = false;
                        break;
                    }
                if (freeSpot)
                {
                    Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(food.X, food.Y); Console.Write("$");
                    break;
                }
            }
        }
    }
}