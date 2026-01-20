namespace Advent_Of_Code_CS.AdventOfCode.Year.AOC2025;

public class Day3 : Day
{
  public override void Part1(string input)
  {
    var result = 0;
    foreach (var line in _parseInput(input))
    {
      var bestPossibleCombination = int.MinValue;
      foreach (var (firstDigit, indexOfFirstDigit) in line.Select((c, i) => (c, i)))
      {
        var maxPossibleAfter = int.MinValue;
        for (var indexPast = indexOfFirstDigit + 1; indexPast < line.Length; indexPast++)
          if (maxPossibleAfter < line[indexPast]) maxPossibleAfter = line[indexPast];
        
        bestPossibleCombination = Math.Max(bestPossibleCombination, (firstDigit * 10) + maxPossibleAfter);
      }
      result += bestPossibleCombination;
    }
    
    Console.WriteLine($"Total output joltage: {result}");
  }

  public override void Part2(string input)
  {
    throw new NotImplementedException();
  }

  private int[][] _parseInput(string input) =>
    input
      .Split('\n')
      .Select(line => line.Trim().ToCharArray())
      .Select(lineChar => lineChar.Select(e => e - '0').ToArray())
      .ToArray();
}