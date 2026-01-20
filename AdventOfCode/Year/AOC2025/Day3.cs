namespace Advent_Of_Code_CS.AdventOfCode.Year.AOC2025;

public class Day3 : Day
{
  public override void Part1(string input)
  {
    var result = _parseInput(input).Sum(battery => _backtrackingBFS(battery, digits: 2));
    Console.WriteLine($"Total output joltage: {result}");
  }

  public override void Part2(string input)
  {
    var result = _parseInput(input).Sum(battery => _backtrackingBFS(battery, digits: 12));
    Console.WriteLine($"Total output joltage: {result}");
  }

  private double _backtrackingRecursive(int[] battery, double currTotal = 0, int index = 0, int digit = 11)
  {
    // if all digits are collected, return total
    if (digit < 0) return currTotal;
    // if reached end and not collected enough digits
    if (index >= battery.Length) return 0;
    
    // either take the digit or dont
    var bestPath = Math.Max(
      _backtrackingRecursive(battery, currTotal + battery[index] * Math.Pow(10, digit), index + 1, digit - 1),
      _backtrackingRecursive(battery, currTotal, index + 1, digit));
    return bestPath;
  }

  private double _backtrackingIterative(int[] battery, int digits)
  {
    double best = 0;

    var stack = new Stack<(int index, int digit, double currTotal)>();
    stack.Push((0, digits - 1, 0));

    while (stack.Count > 0)
    {
      var (index, digit, currTotal) = stack.Pop();

      // collected all digits
      if (digit < 0)
      {
        best = Math.Max(best, currTotal);
        continue;
      }

      // ran out of numbers before collecting digits
      if (index >= battery.Length)
        continue;

      // skip current number
      stack.Push((index + 1, digit, currTotal));

      // take current number
      stack.Push((
        index + 1,
        digit - 1,
        currTotal + battery[index] * Math.Pow(10, digit)
      ));
    }

    return best;
  }
  
  private double _backtrackingBFS(int[] battery, int digits = 12)
  {
    var startDigit = digits - 1;
    
    // (index, digit) -> best value so far
    var bestAt = new Dictionary<(int, int), double>();

    var queue = new Queue<(int index, int digit, double value)>();
    queue.Enqueue((0, startDigit, 0));

    double best = 0;

    while (queue.Count > 0)
    {
      var (index, digit, value) = queue.Dequeue();

      // all digits placed, check if it's the best result so far
      if (digit < 0)
      {
        best = Math.Max(best, value);
        continue;
      }

      // ran out of battery entries
      if (index >= battery.Length)
        continue;

      // prune if we've already seen a better value here
      var key = (index, digit);
      if (bestAt.TryGetValue(key, out var seen) && seen >= value)
        continue;

      bestAt[key] = value;

      // skip
      queue.Enqueue((index + 1, digit, value));

      // take
      queue.Enqueue((
        index + 1,
        digit - 1,
        value + battery[index] * Math.Pow(10, digit)
      ));
    }

    return best;
  }

  private int[][] _parseInput(string input) =>
    input
      .Split('\n')
      .Select(line => line.Trim().ToCharArray())
      .Select(lineChar => lineChar.Select(e => e - '0').ToArray())
      .ToArray();
}