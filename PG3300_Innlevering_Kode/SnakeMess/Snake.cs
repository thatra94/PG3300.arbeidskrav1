using System;
using System.Collections.Generic;
using SnakeMess;
using System.Linq;

namespace SnakeMess
{
	public class Snake
	{

		List<Point> snake = new List<Point>();

		public Snake()
		{
			snake.Add(new Point(10, 10)); snake.Add(new Point(10, 10)); snake.Add(new Point(10, 10)); snake.Add(new Point(10, 10));
		}

		public List<Point> GetSnake()
		{
			return snake;
		}

		public Point GetFirst()
		{
			return snake.First();
		}

		public Point GetLast()
		{
			return snake.Last();
		}

		public int GetCount()
		{
			return snake.Count();
		}

		public void Remove(int x)
		{
			snake.RemoveAt(x);
		}

		public void Add(Point a)
		{
			snake.Add(a);
		}
	}


}
