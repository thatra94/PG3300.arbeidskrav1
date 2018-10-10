using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using SnakeMess;

namespace SnakeMess
{

    public class Food : Point
    {
        Point newFood = new Point();
        Random random = new Random();


        public Food(int boardWidth, int boardHeight, Snake snake)
        {
            newFood.X = random.Next(0, boardWidth);
            newFood.Y = random.Next(0, boardHeight);            
            //CheckFreeSpot(snake.GetSnake());
        }

       /* public void CheckFreeSpot(List<Point> snake)
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
        }*/
        public static void PlaceFood(int boardWidth, int boardHeight, Random random, Point food, Snake snake)
        {
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
