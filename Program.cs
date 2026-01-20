using Advent_Of_Code_CS.AdventOfCode;
using Advent_Of_Code_CS.AdventOfCode.Year.AOC2025;

Dictionary<string, Dictionary<string, Day>> solutions = new()
{
  { 
    "2025", 
    new () {
      { "1",  new Day1() },
      { "2",  new Day2() },
    } 
  },
};

var year = args[0];
var day = args[1];

Console.WriteLine($"Running Year {year} Day {day}");

var input = File.ReadAllText("input.txt");


Console.WriteLine("Part 1");
solutions[year][day].Part1(input);
Console.WriteLine("Part 2");
solutions[year][day].Part2(input);
