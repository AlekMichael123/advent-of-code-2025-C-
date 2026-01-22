using System.Collections;

namespace Advent_Of_Code_CS.AdventOfCode.Year.AOC2025;

using IDRange = (double start, double end);

public class Day5 : Day
{
  public override void Part1(string input)
  {
    var (idRanges, ids) = _parseInput(input);
    var result = ids.Count(id => idRanges.Select(range => id >= range.start && id <= range.end).Any(condition => condition));

    Console.WriteLine($"Number of fresh ingredients: {result}");
  }

  public override void Part2(string input)
  {
    throw new NotImplementedException();
  }

  private (IDRange[] idRanges, double[] ids) _parseInput(string input)
  {
    var lines = input.Split("\n");
    var dividingIndex = 
      lines.Select((line,i) => (line, i)).First(lineIndex => lineIndex.line.Length == 0).i;
    var idRanges = 
      lines[..dividingIndex]
        .Select(line => line.Split('-').Select(double.Parse).ToArray())
        .Select((pair) => (pair[0], pair[1]))
        .ToArray();
    var ids = lines[(dividingIndex + 1)..].Select(double.Parse).ToArray();
    return (idRanges, ids);
  }

  // private IDRange[] _combineIdRanges(IDRange[] idRanges)
  // {
  //   var queue = new Queue(idRanges);
  //   var result = new List<
  // }
}