using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;


namespace SpacePathFinder
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("welcome to my custom space path finder!");
            Console.WriteLine("choose map setup method:");
            Console.WriteLine("1 – manually input map layout");
            Console.WriteLine("2 – generate map randomly");

            string option = Console.ReadLine();

            char[,] spaceGrid;
            (int, int) startingPoint = (0, 0), goalPoint = (0, 0);

            if (option == "1")
                spaceGrid = MapManager.ManualMap(out startingPoint, out goalPoint);
            else if (option == "2")
                spaceGrid = MapManager.AutoGenerateMap(out startingPoint, out goalPoint);
            else
                return;

            Console.WriteLine("choose algorithm:");
            Console.WriteLine("1 – depth-first search (DFS)");
            Console.WriteLine("2 – breadth-first search (BFS)");

            string algoChoice = Console.ReadLine();
            List<(int, int)> bestRoute = null;
            int totalRoutes = 0;

            var pathLogic = new Pathfinder(spaceGrid, startingPoint, goalPoint);

            if (algoChoice == "1")
            {
                pathLogic.RunDFS();
                bestRoute = pathLogic.ShortestRoute;
                totalRoutes = pathLogic.TotalRoutesFound;
            }
            else if (algoChoice == "2")
            {
                bestRoute = pathLogic.RunBFS();
                totalRoutes = bestRoute != null ? 1 : 0;
            }
            else
                return;

            Console.WriteLine($"Paths discovered: {totalRoutes}");

            if (bestRoute != null)
            {
                Console.WriteLine($"Length of shortest route: {bestRoute.Count} steps");

                string[,] pathOverlay = new string[spaceGrid.GetLength(0), spaceGrid.GetLength(1)];
                int stepCounter = 1;

                foreach (var (r, c) in bestRoute)
                    pathOverlay[r, c] = stepCounter++.ToString();

                for (int r = 0; r < spaceGrid.GetLength(0); r++)
                {
                    for (int c = 0; c < spaceGrid.GetLength(1); c++)
                        Console.Write((pathOverlay[r, c] ?? spaceGrid[r, c].ToString()).PadLeft(3));
                    Console.WriteLine();
                }

                string reportFile = "space_path_report.csv";
                CsvSaver.SaveGrid(pathOverlay, reportFile);
                Console.WriteLine("Report saved to CSV.");

                Console.Write("Your email address: ");
                string sender = Console.ReadLine();
                Console.Write("Email password: ");
                string password = Console.ReadLine();
                Console.Write("Recipient email: ");
                string recipient = Console.ReadLine();

                MailDelivery.SendReport(sender, password, recipient, reportFile);
            }
            else
            {
                Console.WriteLine("No valid route found.");
            }
        }
    }
}

