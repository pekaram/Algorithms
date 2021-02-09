using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Solution
{
    // Complete the bomberMan function below.
    static string[] bomberMan(int n, string[] grid)
    {
        var evenMap = GetUpdatedGrid(2, grid);
        var threeMap = GetUpdatedGrid(3, grid);
        var fiveMap = GetUpdatedGrid(5, grid);
        var sevenMap = GetUpdatedGrid(7, grid);

        if (n == 1 || n == 0)
        {
            return grid;
        }
        if (n % 2 == 0)
        {
            return evenMap;
        }
        // Three shows once
        if (n == 3)
        {
            return threeMap;
        }

        // Remove first idle second
        // Find number of explosion
        // Even is 5
        // Odd is 7
        var numberOfBombPlants = (n - 1) / 2;
        if (numberOfBombPlants % 2 == 0)
        {
            return fiveMap;
        }
        else
        {
            return sevenMap;
        }       
    }

    private static string[] GetUpdatedGrid(int n, string[] grid)
    {
        var splitGrid = GetSplitGrid(grid);
        if (n % 2 == 0)
        {
            return GetAllBombs(grid, splitGrid);
        }
        var readyToExplode = GetInitialBombs(splitGrid);     
        UpdateGrid(readyToExplode, splitGrid, n);

        return GetFormattedOutput(grid, splitGrid);
    }

    private static Dictionary<int, List<char>> GetSplitGrid(string[] grid)
    {
        var splitGrid = new Dictionary<int, List<char>>();
        for (var i = 0; i < grid.Length; i++)
        {
            splitGrid.Add(i, grid[i].ToCharArray().ToList());
        }

        return splitGrid;
    }

    private static string[] GetAllBombs(string[] originalGrid, Dictionary<int, List<char>> splitGrid)
    {
        var output = new string[originalGrid.Length];
        foreach (var row in splitGrid)
        {
            for (var cellIndex = 0; cellIndex < row.Value.Count; cellIndex++)
            {
                splitGrid[row.Key][cellIndex] = 'O';
            }
        }


        foreach (var row in splitGrid)
        {
            output[row.Key] = new string(row.Value.ToArray());
        }
        return output;
    }

    private static List<Tuple<int, int>> GetInitialBombs(Dictionary<int, List<char>> splitGrid)
    {
        var readyToExplode = new List<Tuple<int, int>>();
        foreach (var row in splitGrid)
        {
            for (var cellIndex = 0; cellIndex < row.Value.Count; cellIndex++)
            {
                if (splitGrid[row.Key][cellIndex] == 'O')
                {
                    readyToExplode.Add(new Tuple<int, int>(row.Key, cellIndex));
                }
            }
        }

        return readyToExplode;
    }

    private static void UpdateGrid(List<Tuple<int, int>> readyToExplode, Dictionary<int, List<char>> splitGrid, int totalUpdates)
    {
        // Jump to explosions 
        for (var secondElapsed = 3; secondElapsed <= totalUpdates; secondElapsed += 2)
        {
            foreach (var bomb in readyToExplode)
            {
                // Blow armed bombs
                Explode(bomb, splitGrid);
            }

            readyToExplode.Clear();

            //Plant bombs
            PlantBombs(splitGrid, readyToExplode);
        }
    }

    private static string[] GetFormattedOutput(string[] originalGrid, Dictionary<int, List<char>> splitGrid)
    {
        var output = new string[originalGrid.Length];
        foreach (var row in splitGrid)
        {
            output[row.Key] = new string(row.Value.ToArray());
        }
        return output;
    }

    private static void PlantBombs(Dictionary<int, List<char>> splitGrid, List<Tuple<int, int>> readyToExplode)
    {
        foreach (var row in splitGrid)
        {
            for (var cellIndex = 0; cellIndex < row.Value.Count; cellIndex++)
            {
                // Free cells plant bombs and trigger for explode
                if (splitGrid[row.Key][cellIndex] == '.')
                {
                    splitGrid[row.Key][cellIndex] = 'O';
                    readyToExplode.Add(new Tuple<int, int>(row.Key, cellIndex));
                }

                //already planted bombs
                else if (splitGrid[row.Key][cellIndex] == '-')
                {
                    // Free exploded bomb cells
                    splitGrid[row.Key][cellIndex] = '.';
                }
            }
        }
    }


    private static void Explode(Tuple<int, int> index, Dictionary<int, List<char>> grid)
    {
        // - means just exploded
        // Explode cell
        grid[index.Item1][index.Item2] = '-';

        if (index.Item1 - 1 >= 0)
        {
            grid[index.Item1 - 1][index.Item2] = '-';
        }
        if (index.Item1 + 1 < grid.Count)
        {
            grid[index.Item1 + 1][index.Item2] = '-';
        }
        if (index.Item2 - 1 >= 0)
        {
            grid[index.Item1][index.Item2 - 1] = '-';
        }
        if (index.Item2 + 1 < grid[0].Count)
        {
            grid[index.Item1][index.Item2 + 1] = '-';
        }
    }

    static void Main(string[] args)
    {
        // Replace those lines with console print line to run as a console app
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] rcn = Console.ReadLine().Split(' ');

        int r = Convert.ToInt32(rcn[0]);

        int c = Convert.ToInt32(rcn[1]);

        int n = Convert.ToInt32(rcn[2]);

        string[] grid = new string[r];

        for (int i = 0; i < r; i++)
        {
            string gridItem = Console.ReadLine();
            grid[i] = gridItem;
        }

        string[] result = bomberMan(n, grid);

        textWriter.WriteLine(string.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
