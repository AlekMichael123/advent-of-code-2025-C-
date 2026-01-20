namespace Advent_Of_Code_CS.AdventOfCode.Year.AOC2025;

public class Day2 : Day
{
  public override void Part1(string input)
  {
    var parsedInput = _parseInput(input);
    long result = 0;
    foreach (var (start, end) in parsedInput)
    {
      for (var i = long.Parse(start); i <= long.Parse(end); i++)
      {
        if (_isInvalid(i.ToString())) result += i;
      }
    }
    
    Console.WriteLine($"Sum of all invalid IDs: {result}");
  }

  public override void Part2(string input)
  {
    throw new NotImplementedException();
  }

  private (string start, string end)[] _parseInput(string input) =>
    input
      .Split(',')
      .Select(e => e.Split('-'))
      .Select(e => (first: e.First(), second: e.Last()))
      .ToArray();

  private bool _isInvalid(string value) =>
    value[0..(value.Length / 2)] == value[(value.Length / 2)..];
}