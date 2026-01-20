namespace Advent_Of_Code_CS.AdventOfCode.Year.AOC2025;

public class Day2 : Day
{
  public override void Part1(string input)
  {
    long result = 0;
    foreach (var (start, end) in _parseInput(input))
    {
      for (var value = long.Parse(start); value <= long.Parse(end); value++)
      {
        if (_isInvalidPart1(value.ToString())) result += value;
      }
    }
    
    Console.WriteLine($"Sum of all invalid IDs: {result}");
  }

  public override void Part2(string input)
  {
    long result = 0;
    foreach (var (start, end) in _parseInput(input))
    {
      for (var value = long.Parse(start); value <= long.Parse(end); value++)
      {
        var valueString = value.ToString();
        if (valueString.Where((_, j) => _isInvalidPart2(valueString[0..j], valueString)).Any())
        {
          result += value;
        }
      }
    }
    
    Console.WriteLine($"Sum of all invalid IDs: {result}");
  }

  private (string start, string end)[] _parseInput(string input) =>
    input
      .Split(',')
      .Select(e => e.Split('-'))
      .Select(e => (e.First(), e.Last()))
      .ToArray();

  private bool _isInvalidPart1(string value) =>
    value[0..(value.Length / 2)] == value[(value.Length / 2)..];

  private bool _isInvalidPart2(string substr, string value) =>
    (value.Split(substr).Length - 1) * substr.Length == value.Length;
}