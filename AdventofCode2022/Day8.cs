using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

public static class Day8
{
    public static string GetColumn(char[][] matrix, int column)
    {
        var chars = new List<char>();

        foreach (var row in matrix)
        {
            chars.Add(row[column]);
        }

        return new string(chars.ToArray());
    }


    public static int DayEightA(string filePath)
    {
        var matrix = File.ReadAllLines($"../../../inputs/{filePath}").Select(x => x.ToArray()).ToArray();

        var totalTrees = 0;
        var j = 0;
        var i = 0;

        for (; i < matrix.Length; ++i)
        {
            var row = new string(matrix[i]);

            for (; j < matrix.Length; ++j)
            {
                var column = GetColumn(matrix, j);
                if (column.Substring(0, i).CanBeSeen(matrix[i][j]) ||
                    column.Substring(i+1).CanBeSeen(matrix[i][j]) ||
                    row.Substring(0, j).CanBeSeen(matrix[i][j]) ||
                    row.Substring(j+1).CanBeSeen(matrix[i][j])) {
                    totalTrees++;
                }
                

            }
            j = 0;
        }
        return totalTrees;
    }

    static bool CanBeSeen(this string str, char tree)
    {
        if (string.IsNullOrWhiteSpace(str)) return true;
        return str.All(x => int.Parse(x.ToString()) < int.Parse(tree.ToString()));
    }

    public static int DayEightB(string filePath)
    {
        var matrix = File.ReadAllLines($"../../../inputs/{filePath}").Select(x => x.ToArray()).ToArray();

        var totalTrees = 0;
        var j = 0;
        var i = 0;

        for (; i < matrix.Length; ++i)
        {
            var row = new string(matrix[i]);

            for (; j < matrix.Length; ++j)
            {
                var column = GetColumn(matrix, j);
                if (column.Substring(0, i).CanBeSeen(matrix[i][j]) ||
                    column.Substring(i + 1).CanBeSeen(matrix[i][j]) ||
                    row.Substring(0, j).CanBeSeen(matrix[i][j]) ||
                    row.Substring(j + 1).CanBeSeen(matrix[i][j]))
                {
                    totalTrees++;
                }


            }
            j = 0;
        }
        return totalTrees;
    }

    private int GetScenicScore() {
        return 0;
    }
}
