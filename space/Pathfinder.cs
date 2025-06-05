using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Pathfinder
{
    private readonly char[,] map;
    private readonly (int, int) start;
    private readonly (int, int) end;
    private readonly bool[,] visited;
    private readonly List<(int, int)> currentPath;
    private readonly int[] dRow = { -1, 1, 0, 0 };
    private readonly int[] dCol = { 0, 0, -1, 1 };

    public List<(int, int)> ShortestRoute { get; private set; }
    public int TotalRoutesFound { get; private set; }

    public Pathfinder(char[,] map, (int, int) start, (int, int) end)
    {
        this.map = map;
        this.start = start;
        this.end = end;
        this.visited = new bool[map.GetLength(0), map.GetLength(1)];
        this.currentPath = new List<(int, int)>();
        this.ShortestRoute = null;
        this.TotalRoutesFound = 0;
    }

    public void RunDFS()
    {
        Search(start.Item1, start.Item2);
    }

    private void Search(int r, int c)
    {
        if (r < 0 || r >= map.GetLength(0) || c < 0 || c >= map.GetLength(1)) return;
        if (map[r, c] == 'X' || visited[r, c]) return;

        visited[r, c] = true;
        currentPath.Add((r, c));

        if ((r, c) == end)
        {
            TotalRoutesFound++;
            if (ShortestRoute == null || currentPath.Count < ShortestRoute.Count)
                ShortestRoute = new List<(int, int)>(currentPath);
        }
        else
        {
            for (int i = 0; i < 4; i++)
                Search(r + dRow[i], c + dCol[i]);
        }

        visited[r, c] = false;
        currentPath.RemoveAt(currentPath.Count - 1);
    }

    public List<(int, int)> RunBFS()
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        var queue = new Queue<List<(int, int)>>();
        bool[,] visitedBFS = new bool[rows, cols];

        queue.Enqueue(new List<(int, int)> { start });
        visitedBFS[start.Item1, start.Item2] = true;

        while (queue.Count > 0)
        {
            var path = queue.Dequeue();
            var (r, c) = path[path.Count - 1];

            if ((r, c) == end)
                return path;

            for (int i = 0; i < 4; i++)
            {
                int newRow = r + dRow[i];
                int newCol = c + dCol[i];

                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols &&
                    map[newRow, newCol] != 'X' && !visitedBFS[newRow, newCol])
                {
                    visitedBFS[newRow, newCol] = true;
                    var newPath = new List<(int, int)>(path) { (newRow, newCol) };
                    queue.Enqueue(newPath);
                }
            }
        }

        return null;
    }
}
