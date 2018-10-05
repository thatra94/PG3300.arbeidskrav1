using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Snake;
using SnakeMess;

namespace SnakeMess
{

    public class Food
    {
        Point newFood = new Point();
        Random random = new Random();

        public Food()
        {
            newFood.X = random.Next(0, boardWidth);
            newFood.Y = random.Next(0, boardHeight);            
            CheckFreeSpot(Snake.getSnake());
        }

        public void CheckFreeSpot(List<Point> snake)
        {
            while (true)
            {
                bool freeSpot = true;
                foreach (Point i in snake)
                    if (i.X == newFood.X && i.Y == newFood.Y)
                    {
                        freeSpot = false;
                        break;
                    }
                if (freeSpot)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(newFood.X, newFood.Y);
                    Console.Write("$");
                    break;
                }
            }
        }
    }
}
