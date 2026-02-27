using System.Globalization;
using System.Text;

namespace Advent_Of_Code_CS.AdventOfCode.Year.AOC2025;

public class Day6 : Day
{
  public override void Part1(string input)
  {
    var (numbersRows, operations) = _parseInput(input, parseHorizontally: true);
    var result = 0D;
    for (var j = 0; j < numbersRows.First().Length; j++)
    {
      var values = numbersRows.Select(t => t[j]).ToArray();
      var total = _calculateOperation(values, operations[j]);
      result += total;
    }

    Console.WriteLine($"Sum of all equations: {result}");
  }

  public override void Part2(string input)
  {
    var (numbersCols, operations) = _parseInput(input, parseHorizontally: false);
    var op = 0;
    var result = numbersCols.Sum(values => _calculateOperation(values, operations[op++]));
    Console.WriteLine($"Sum of all equations: {result}");
  }

  private double _calculateOperation(double[] values, char operation) =>
    values.Aggregate(operation == '*' ? 1D : 0D, (current, value) =>
      operation == '*'
        ? value * current
        : value + current);

  private (double[][] numbersRows, char[] operations) _parseInput(string input, bool parseHorizontally)
  {
    var lines = input.Split('\n');
    var (numbersStrings, operationsString) = (lines[..^1], lines.Last());
    var numbersRows = 
      parseHorizontally
      ? _parseNumbersHorizontally(numbersStrings)
      : _parseNumbersVertically(numbersStrings);
    var operations = _parseOperations(operationsString);
    return (numbersRows, operations);
  }
  
  private char[] _parseOperations(string operationsString) =>
    operationsString
      .Split(' ', StringSplitOptions.RemoveEmptyEntries)
      .Select(s => s.Trim()[0])
      .ToArray();
  
  private double[][] _parseNumbersHorizontally(string[] numbersStrings) =>
    numbersStrings
      .Select(str =>
        str
          .Split(' ', StringSplitOptions.RemoveEmptyEntries)
          .Select(double.Parse)
          .ToArray())
      .ToArray();
  
  private double[][] _parseNumbersVertically(string[] lines)
  {
    // could not find a very clean way to do this :(
    var height = lines.Length;
    var width = lines.Max(l => l.Length);

    var grid = lines
      .Select(l => l.PadRight(width))
      .ToArray();

    var result = new List<double[]>();

    var col = 0;

    while (col < width)
    {
      if (!grid.Any(row => char.IsDigit(row[col])))
      {
        col++;
        continue;
      }

      var start = col;

      while (col < width && grid.Any(row => char.IsDigit(row[col])))
        col++;

      var end = col;

      var numbers = new List<double>();

      for (var digitCol = start; digitCol < end; digitCol++)
      {
        var current = "";

        for (var row = 0; row < height; row++)
        {
          var c = grid[row][digitCol];

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