using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class MapManager
{
    public static char[,] ManualMap(out (int, int) start, out (int, int) end)
    {
        Console.Write("enter number of rows: ");
        int rows = int.Parse(Console.ReadLine());
        Console.Write("enter number of columns: ");
        int cols = int.Parse(Console.ReadLine());

        char[,] map = new char[rows, cols];
        start = (0, 0);
        end = (0, 0);

        Console.WriteLine("use S (start), F (finish), O (open), X (obstacle):");

        for (int r = 0; r < rows; r++)
        {
            var row = Console.ReadLine().Split(' ');
            for (int c = 0; c < cols; c++)
            {
                char ch = row[c][0];
                map[r, c] = ch;
                if (ch == 'S') start = (r, c);
                if (ch == 'F') end = (r, c);
            }
        }

        return map;
    }

    public static char[,] AutoGenerateMap(out (int, int) start, out (int, int) end)
    {
        Console.Write("rows: ");
        int rows = int.Parse(Console.ReadLine());
        Console.Write("columns: ");
        int cols = int.Parse(Console.ReadLine());
        Console.Write("obstacles: ");
        int obstacles = int.Parse(Console.ReadLine());

        char[,] map = new char[rows, cols];
        Random rng = new Random();

        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                map[r, c] = 'O';

        start = (rng.Next(rows), rng.Next(cols));
        do end = (rng.Next(rows), rng.Next(cols)); while (end == start);

        map[start.Item1, start.Item2] = 'S';
        map[end.Item1, end.Item2] = 'F';

        int placed = 0;
        while (placed < obstacles)
        {
            int r = rng.Next(rows), c = rng.Next(cols);
            if (map[r, c] == 'O')
            {
                map[r, c] = 'X';
                placed++;
            }
        }

        return map;
    }
}
