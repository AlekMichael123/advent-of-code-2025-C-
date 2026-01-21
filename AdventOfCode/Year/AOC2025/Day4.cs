namespace Advent_Of_Code_CS.AdventOfCode.Year.AOC2025;

public class Day4 : Day
{
  public override void Part1(string input)
  {
    var diagram = _parseInput(input);
    var result = 0;
    
    foreach (var (line, row) in diagram.Select((line, row) => (line, row)))
      foreach (var (cell, col) in line.Select((cell, col) => (cell, col)))
        result += cell == '@' && _isValidRoll(diagram, row, col) ? 1 : 0;
    
    Console.WriteLine($"Total rolls accessible: {result}");
  }

  public override void Part2(string input)
  {
    throw new NotImplementedException();
  }

  private bool _isValidRoll(char[][] diagram, int row, int col)
  {
    var directions = new[]
    {
      (-1, 1),  (0, 1),  (1, 1),
      (-1, 0),           (1, 0),
      (-1, -1), (0, -1), (1, -1),
    };
    var n = diagram.Length;
    var m = diagram.First().Length;
    var neighborCount = (
      from direction in directions 
      let colDiff = col + direction.Item1 
      let rowDiff = row + direction.Item2  
      where rowDiff >= 0 && colDiff >= 0 && rowDiff < n && colDiff < m 
      select diagram[rowDiff][colDiff] == '@' ? 1 : 0).Sum();
    
    return neighborCount < 4;
  }

  private char[][] _parseInput(string input) =>
    input.Split("\n").Select(line => line.ToCharArray()).ToArray();
}