namespace Advent_Of_Code_CS.AdventOfCode.Year.AOC2025;

using Position = (int X, int Y);

public class Day7 : Day
{
  public override void Part1(string input)
  {
    var board = ParseInput(input);
    var start = ParseInputFindStart(board);
    var result = CountSplits(start, board);
    
    Console.WriteLine($"result: {result}");
  }

  public override void Part2(string input)
  {
    var board = ParseInput(input);
    var start = ParseInputFindStart(board);
    var result = CountSplitsWithTime(start, board, new());
    
    Console.WriteLine($"result: {result}");
  }

  private static readonly Position DirectionOffset = (0, 1);

  private static int CountSplits(Position start, char[][] board)
  {
    var queue = new Queue<Position>([start]);
    var result = 0;
    // queue style
    while (queue.Count > 0)
    {
      var currentPosition = queue.Dequeue();
      Position nextPosition = (currentPosition.X + DirectionOffset.X, currentPosition.Y + DirectionOffset.Y);
      if (nextPosition.Y >= board.Length || nextPosition.X < 0 || nextPosition.X >= board.First().Length) continue;
      
      if (board[nextPosition.Y][nextPosition.X] != '^')
      {
        queue.Enqueue(nextPosition);
        continue;
      }
      if (board[currentPosition.Y][currentPosition.X] == '!') continue;
      board[currentPosition.Y][currentPosition.X] = '!';
      
      queue.Enqueue((nextPosition.X - 1, nextPosition.Y));
      queue.Enqueue((nextPosition.X + 1, nextPosition.Y));
      result += 1;
    }
    return result;
  }

  private static long CountSplitsWithTime(Position currentPosition, char[][] board, Dictionary<Position, long> memo)
  {
    // recursive style
    while (true)
    {
      Position nextPosition = (currentPosition.X + DirectionOffset.X, currentPosition.Y + DirectionOffset.Y);
      if (memo.TryGetValue(nextPosition, out var value2)) return value2;
      
      if (nextPosition.Y >= board.Length || nextPosition.X < 0 || nextPosition.X >= board.First().Length)
      {
        memo[currentPosition] = 1;
        return 1;
      }

      if (board[nextPosition.Y][nextPosition.X] != '^')
      {
        currentPosition = nextPosition;
        continue;
      }
      var left = CountSplitsWithTime((nextPosition.X - 1, nextPosition.Y), board, memo);      
      var right = CountSplitsWithTime((nextPosition.X + 1, nextPosition.Y), board, memo);      
      memo[currentPosition] = left + right;
      return left + right;
    }
  }

  private static char[][] ParseInput(string input) =>
    input
      .Split("\n", StringSplitOptions.RemoveEmptyEntries)
      .Select(line => line.ToCharArray())
      .ToArray();

  private static Position ParseInputFindStart(char[][] input)
  {
    var y = Array.FindIndex(input, chars => chars.Contains('S'));
    var x = input[y].IndexOf('S');
    return (x, y);
  }

  private static void printBoard(char[][] board)
  {
    foreach (var line in board)
    {
      foreach (var value in line)
        Console.Write(value);
      Console.WriteLine();
    }
  }
}