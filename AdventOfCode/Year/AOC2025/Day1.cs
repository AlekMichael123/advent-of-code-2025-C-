namespace Advent_Of_Code_CS.AdventOfCode.Year.AOC2025;

public class Day1 : Day
{
  public override void Part1(string input)
  {
    var dial = new Dial();
    var result = _parseInput(input)
      .Count(instruction => dial.TurnAndPointsAtZero(instruction.direction, instruction.amount));
    Console.WriteLine($"Number of times passing zero: {result}");
  }

  public override void Part2(string input)
  {
    var dial = new Dial();
    var result = _parseInput(input)
      .Sum(instruction => dial.TurnCountAllZeroInstances(instruction.direction, instruction.amount));
    Console.WriteLine($"Number of times hitting zero: {result}");
  }

  private (char direction, int amount)[] _parseInput(string input) =>
  [..
    input.Split('\n')
      .Select(line => (line[0], int.Parse(line[1..])))
  ];

  private class Dial
  {
    private int _position = 50;

    public bool TurnAndPointsAtZero(char direction, int amount)
    {
      switch (direction)
      {
        case 'L':
          _position -= amount;
          break;
        case 'R':
          _position += amount; 
          break;
        default:
          throw new ArgumentException($"Invalid direction: {direction}");
      }

      if (_position < 0) _position += 100;
      _position %= 100;
      
      return _position == 0;
    }
    public int TurnCountAllZeroInstances(char direction, int amount)
    {
      var start = _position;
      var count = 0;

      var fullLoops = amount / 100;
      var remainder = amount % 100;
      
      count += fullLoops;

      if (remainder > 0)
      {
        switch (direction)
        {
          case 'R':
          {
            if (start + remainder >= 100)
              count++;
            break;
          }
          case 'L':
          {
            if (start - remainder <= 0)
              count++;
            break;
          }
          default:
            throw new ArgumentException($"Invalid direction: {direction}");
        }
      }

      if (start == 0 && count > 0 && fullLoops != count)
        count--;

      _position = direction == 'R'
        ? (start + remainder) % 100
        : (start - (remainder) + 100) % 100;

      return count;
    }

  }
}