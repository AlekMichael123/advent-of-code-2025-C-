using System.Globalization;
using System.Text;

namespace Advent_Of_Code_CS.AdventOfCode.Year.AOC2025;

public class Day6 : Day
{
  public override void Part1(string input)
  {
    var (numbersRows, operations) = _parseInput(input);
    var result = 0D;
    for (var j = 0; j < numbersRows.First().Length; j++)
    {
      var values = numbersRows.Select(t => t[j]).ToList();
      var total =
        values.Aggregate(operations[j] == '*' ? 1D : 0D, (current, value) =>
          operations[j] == '*'
            ? value * current
            : value + current);
      result += total;
    }

    Console.WriteLine($"Sum of all equations: {result}");
  }

  public override void Part2(string input)
  {
    var (numbersCols, operations) = _parseInputVertical(input);
    var result = 0D;
    var op = 0;
    foreach (var values in numbersCols)
    {
      Console.Write("[");
      foreach (var val  in values) Console.Write($"{val},");
      Console.Write("]\n");
      
      var total = 
        values.Aggregate(operations[op] == '*' ? 1D : 0D, (current, value) =>
          operations[op] == '*'
            ? value * current
            : value + current);
      op += 1;
      result += total;
    }
    // for (var j = 0; j < numbersCols.First().Length; j++)
    // {
    //   var values = numbersCols.Select(t => t[j]).ToList();
    //   var total =
    //     values.Aggregate(operations[j] == '*' ? 1D : 0D, (current, value) =>
    //       operations[j] == '*'
    //         ? value * current
    //         : value + current);
    //   result += total;
    // }

    Console.WriteLine($"Sum of all equations: {result}");
  }


  private (double[][] numbersRows, char[] operations) _parseInput(string input)
  {
    var lines = input.Split('\n');
    var (numbersStrings, operationsString) = (lines[..^1], lines.Last());
    var numbersRows = 
      numbersStrings
        .Select(str => 
          str
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(double.Parse)
            .ToArray())
        .ToArray();
    var operations = 
      operationsString
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(s => s.Trim()[0])
        .ToArray();
    return (numbersRows, operations);
  }
  
  private (double[][] numberCols, char[] operations) _parseInputVertical(string input)
  {
    var lines = input.Split('\n');
    var (numberCols, operationsString) = (_parseNumbersVertical(lines[..^1]), lines.Last());
    var operations = 
      operationsString
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(s => s.Trim()[0])
        .ToArray();
    return (numberCols, operations);
  }
  private double[][] _parseNumbersVertical(string[] lines)
  {

    int height = lines.Length;
    int width = lines.Max(l => l.Length);

    // Pad lines so indexing is safe
    var grid = lines
      .Select(l => l.PadRight(width))
      .ToArray();

    var result = new List<double[]>();

    int col = 0;

    while (col < width)
    {
      // Skip empty vertical space
      if (!grid.Any(row => char.IsDigit(row[col])))
      {
        col++;
        continue;
      }

      // Found start of a visual number column block
      int start = col;

      // Move until column no longer contains digits
      while (col < width && grid.Any(row => char.IsDigit(row[col])))
        col++;

      int end = col;

      // Now process this visual block
      var numbers = new List<double>();

      for (int digitCol = start; digitCol < end; digitCol++)
      {
        string current = "";

        for (int row = 0; row < height; row++)
        {
          char c = grid[row][digitCol];

          if (char.IsDigit(c))
            current += c;
        }

        if (current.Length > 0)
          numbers.Add(double.Parse(current));
      }

      result.Add(numbers.ToArray());
    }

    return result.ToArray();
  }
}