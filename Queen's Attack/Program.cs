using System.Collections.Generic;
using System.IO;
using System;

class Solution
{
    // Complete the queensAttack function below.
    static int queensAttack(int n, int k, int r_q, int c_q, int[][] obstacles)
    {
        var rowToAllObstacles = new Dictionary<int, HashSet<int>>();
        foreach (var value in obstacles)
        {
            if (!rowToAllObstacles.ContainsKey(value[0]))
            {
                rowToAllObstacles.Add(value[0], new HashSet<int>());
            }

            rowToAllObstacles[value[0]].Add(value[1]);
        }

        var totalMovableCells = 0;
        totalMovableCells += GetLeftObstacles(rowToAllObstacles, n, k, r_q, c_q, obstacles);
        totalMovableCells += GetRightObstacles(rowToAllObstacles, n, k, r_q, c_q, obstacles);
        totalMovableCells += GetDownObstacles(rowToAllObstacles, n, k, r_q, c_q, obstacles);
        totalMovableCells += GetUpperObstacles(rowToAllObstacles, n, k, r_q, c_q, obstacles);

        totalMovableCells += GetDownLeftDiagonalObstacles(rowToAllObstacles, n, k, r_q, c_q, obstacles);
        totalMovableCells += GetDownRightDiagonalObstacles(rowToAllObstacles, n, k, r_q, c_q, obstacles);

        totalMovableCells += GetUpperRightDiagonalObstacles(rowToAllObstacles, n, k, r_q, c_q, obstacles);
        totalMovableCells += GetUpperLeftDiagonalObstacles(rowToAllObstacles, n, k, r_q, c_q, obstacles);

        return totalMovableCells;
    }

    static int GetDownObstacles(Dictionary<int, HashSet<int>> rowToAllObstacles, int n, int k, int r_q, int c_q, int[][] obstacles)
    {
        var totalCells = -1;
        for (var r = r_q; r > 0; r--)
        {         
            if (rowToAllObstacles.ContainsKey(r) && rowToAllObstacles[r].Contains(c_q))
            {
                return totalCells;
            }
            totalCells++;
        }

        return totalCells;
    }


    static int GetUpperObstacles(Dictionary<int, HashSet<int>> rowToAllObstacles, int n, int k, int r_q, int c_q, int[][] obstacles)
    {
        var totalCells = -1;
        for (var r = r_q; r <= n; r++)
        { 
            if (rowToAllObstacles.ContainsKey(r) && rowToAllObstacles[r].Contains(c_q))
            {
                return totalCells;
            }
            totalCells++;
        }

        return totalCells;
    }

    static int GetRightObstacles(Dictionary<int, HashSet<int>> rowToAllObstacles, int n, int k, int r_q, int c_q, int[][] obstacles)
    {
        var totalCells = -1;
        for (var c = c_q; c <= n; c++)
        {   
            if (rowToAllObstacles.ContainsKey(r_q) && rowToAllObstacles[r_q].Contains(c))
            {
                return totalCells;
            }
            totalCells++;
        }

        return totalCells;
    }

    static int GetLeftObstacles(Dictionary<int, HashSet<int>> rowToAllObstacles, int n, int k, int r_q, int c_q, int[][] obstacles)
    {
        var totalCells = -1;
        for (var c = c_q; c > 0; c--)
        {
            if (rowToAllObstacles.ContainsKey(r_q) && rowToAllObstacles[r_q].Contains(c))
            {
                return totalCells;
            }
            totalCells++;
        }

        return totalCells;
    }

    static int GetDownLeftDiagonalObstacles(Dictionary<int, HashSet<int>> rowToAllObstacles, int n, int k, int r_q, int c_q, int[][] obstacles)
    {
        var totalCells = -1;
        for (var r = r_q; r > 0; r--)
        {       
            if (rowToAllObstacles.ContainsKey(r) && rowToAllObstacles[r].Contains(c_q))
            {
                return totalCells;
            }
            totalCells++;

            c_q--;
            if (c_q == 0)
            {
                return totalCells;
            }
        }

        return totalCells;
    }

    static int GetUpperRightDiagonalObstacles(Dictionary<int, HashSet<int>> rowToAllObstacles, int n, int k, int r_q, int c_q, int[][] obstacles)
    {
        var totalCells = -1;
        for (var r = r_q; r < n + 1; r++)
        {    
            if (rowToAllObstacles.ContainsKey(r) && rowToAllObstacles[r].Contains(c_q))
            {
                return totalCells;
            }
            totalCells++;

            c_q++;
            if (c_q == n + 1)
            {
                return totalCells;
            }
        }

        return totalCells;
    }


    static int GetDownRightDiagonalObstacles(Dictionary<int, HashSet<int>> rowToAllObstacles, int n, int k, int r_q, int c_q, int[][] obstacles)
    {
        var totalCells = -1;
        for (var r = r_q; r > 0; r--)
        {
            if (rowToAllObstacles.ContainsKey(r) && rowToAllObstacles[r].Contains(c_q))
            {
                return totalCells;
            }
            totalCells++;

            c_q++;
            if (c_q == n + 1)
            {
                return totalCells;
            }
        }

        return totalCells;
    }

    static int GetUpperLeftDiagonalObstacles(Dictionary<int, HashSet<int>> rowToAllObstacles, int n, int k, int r_q, int c_q, int[][] obstacles)
    {
        var totalCells = -1;
        for (var r = r_q; r < n + 1; r++)
        {   
            if (rowToAllObstacles.ContainsKey(r) && rowToAllObstacles[r].Contains(c_q))
            {
                return totalCells;
            }
            totalCells++;

            c_q--;
            if (c_q == 0)
            {
                return totalCells;
            }
        }

        return totalCells;
    }


    static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] nk = Console.ReadLine().Split(' ');

        int n = Convert.ToInt32(nk[0]);

        int k = Convert.ToInt32(nk[1]);

        string[] r_qC_q = Console.ReadLine().Split(' ');

        int r_q = Convert.ToInt32(r_qC_q[0]);

        int c_q = Convert.ToInt32(r_qC_q[1]);

        int[][] obstacles = new int[k][];

        for (int i = 0; i < k; i++)
        {
            obstacles[i] = Array.ConvertAll(Console.ReadLine().Split(' '), obstaclesTemp => Convert.ToInt32(obstaclesTemp));
        }

        int result = queensAttack(n, k, r_q, c_q, obstacles);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}