using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


public static class CsvSaver
{
    public static void SaveGrid(string[,] grid, string path)
    {
        var sb = new StringBuilder();
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                sb.Append(grid[r, c]);
                if (c < cols - 1)
                    sb.Append(",");
            }
            sb.AppendLine();
        }

        File.WriteAllText(path, sb.ToString());
    }
}
