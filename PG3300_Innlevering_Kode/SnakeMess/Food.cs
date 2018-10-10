using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using SnakeMess;

namespace SnakeMess
{

    public class Food : Point
    {

        public Food(int boardWidth, int boardHeight, Snake snake)
        {
        }

        public static bool PlaceFood(int boardWidth, int boardHeight, Random random, Point food, Snake snake)
        {
            bool inUse;
            while (true)
            {
                food.X = random.Next(0, boardWidth); food.Y = random.Next(0, boardHeight);
                bool found = true;
                foreach (Point i in snake.GetSnake())
                    if (i.X == food.X && i.Y == food.Y)
                    {
                        found = false;
                        break;
                    }
                if (found)
                {
                    inUse = true;
                    break;
                }
            }

            return inUse;
        }
    }
}
