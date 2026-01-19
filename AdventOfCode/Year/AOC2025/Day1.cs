using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;

namespace Advent_Of_Code_CS.AdventOfCode.Year.AOC2025;

public class Day1 : Day
{
  public override void Part1(string input)
  {
    var dial = new Dial();
    var result = _parseInput(input)
      .Count(instruction => dial.Turn(instruction.direction, instruction.amount));
    Console.WriteLine($"Number of times passing zero: {result}");
  }

  public override void Part2(string input)
  {
    Console.WriteLine($"Running Day 1: {input}");
  }

  private (char direction, int amount)[] _parseInput(string input) =>
  [..
    input.Split('\n')
      .Select(line => (line[0], int.Parse(line[1..])))
  ];

  private class Dial
  {
    private int _position = 50;

    public bool Turn(char direction, int amount)
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
      
      var hitZero = _position is < 0 or > 99;

      if (_position < 0) _position += 100;
      _position %= 100;
      
      return hitZero;
    }
  }
}