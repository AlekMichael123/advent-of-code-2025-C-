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
    throw new NotImplementedException();
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
}