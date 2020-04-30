using System;
using System.Collections.Generic;
					
public class Program
{
  public struct Coord
  {
    public Coord(int x, int y)
    {
      X = x;
      Y = y;
    }
    public int X { get; }
    public int Y { get; }
  }
  static List<Coord> crossroads = new List<Coord>();
  static int X = 1; 
  static int Y = 0;

	//Наш лабиринт - массив целочисленных значений
  //Обозначение:
  // -1 стенка
  //  0 проходы лабиринта
  // -5 и -10 вход и выход лабиринта
  static int[,] maze = new int[10,10]
	{
    {-1,-5,-1,-1,-1,-1,-1,-1,-1,-1},
    {-1,0,0,0,-1,-1,0,0,0,-1},
    {-1,-1,-1,0,0,-1,0,0,0,-1},
    {-1,0,-1,-1,0,-1,-1,-1,0,-1},
    {-1,0,0,0,0,0,0,0,0,-1},
    {-1,0,-1,-1,-1,-1,0,-1,-1,-1},
    {-1,0,0,0,0,-1,0,0,0,-1},
    {-1,-1,-1,0,-1,-1,0,-1,0,-1},
    {-1,0,0,0,0,0,0,-1,0,-1},
    {-1,-1,-1,-1,-1,-1,-1,-1,-10,-1},
	};
  
  //ОСНОВНОЙ БЛОК Проход по лабиринту
	static void Main()
	{
    int step = 0; //номер шага
    do
      {        
        if(maze[Y, X] < 1) 
        {
          step++;
          maze[Y, X] = step;
        }
        if (chk_crossroad(X, Y)) crossroads.Add(new Coord(X, Y));
        if(maze[Y + 1, X] == 0 || maze[Y + 1, X] < -1)
          Y = Y + 1;
        else if (maze[Y, X + 1] == 0 || maze[Y, X + 1] < -1)
                    X = X + 1;
        else if (maze[Y, X - 1] == 0 || maze[Y, X - 1] < -1)
                    X = X - 1;
        else if (maze[Y - 1, X] == 0 || maze[Y - 1, X] < -1)
                    Y = Y - 1;
        else if(!return_inCrossroad())
        {
                    Console.WriteLine("Выхода нет!!!");
                    return;
        }
      }
      while (maze[Y, X] != -10);
      Console.WriteLine("Выход найден!", "Победа!");

    //Проходим лабиринт
		for(int i = 0; i<10; i++)
		{
			for(int j = 0; j<10; j++)
				Console.Write("{0,4}", maze[i,j]);
			Console.WriteLine();
		}
	} 

  static bool return_inCrossroad()
  {
    if(crossroads.Count>0)
    { 
      X = crossroads[1].X;
      Y = crossroads[1].Y;
      if (!chk_crossroad(X, Y)) crossroads.Remove(crossroads[1]);
      return true;
    }
    return false;
  }

  static bool chk_crossroad(int x, int y)
  {
    int count = 0;
    if (maze[y + 1, x] == 0) count++;
    if (y>0 && maze[y - 1, x] == 0) count++;
    if (maze[y, x + 1] == 0) count++;
    if (x>0 && maze[y, x - 1] == 0) count++;
    if (count > 1) return true;
    else return false;
  }
}