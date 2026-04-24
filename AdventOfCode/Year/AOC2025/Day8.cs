namespace Advent_Of_Code_CS.AdventOfCode.Year.AOC2025;

internal readonly record struct JunctionLocation(int X, int Y, int Z);

public class Day8 : Day
{
  public override void Part1(string input)
  {
    var junctionLocations = ParseInput(input);
    var n = junctionLocations.Length;

    var parent = Enumerable.Range(0, n).ToArray();
    var rank = new int[n];

    var pq = new PriorityQueue<(int a, int b), long>();
    
    for (var i = 0; i < n; i++)
      for (var j = i + 1; j < n; j++)
        pq.Enqueue((i, j), Dist(i, j));
    
    // check if this is the sample data
    var take = (n == 20) ? 10 : 1000;
    for (var k = 0; k < take && pq.Count > 0; k++)
    {
      var (a, b) = pq.Dequeue();
      Union(a, b);
    }
            
    var groups = Enumerable.Range(0, n)
      .GroupBy(Find)
      .Select(g => g.Count())
      .OrderByDescending(x => x)
      .ToArray();

    var product = groups.Take(3).Aggregate(1, (a, b) => a * b);

    Console.WriteLine($"Product of top three groups: {product}");
    // Console.WriteLine($"top three groups: {string.Join(",", groups)}");
    
    long Dist(int i, int j)
    {
      long dx = junctionLocations[i].X - junctionLocations[j].X;
      long dy = junctionLocations[i].Y - junctionLocations[j].Y;
      long dz = junctionLocations[i].Z - junctionLocations[j].Z;
      return dx * dx + dy * dy + dz * dz;
    }

    int Find(int x)
    {
      if (parent[x] != x)
        parent[x] = Find(parent[x]);
      return parent[x];
    }

    void Union(int a, int b)
    {
      a = Find(a);
      b = Find(b);
      if (a == b) return;

      if (rank[a] < rank[b]) parent[a] = b;
      else if (rank[a] > rank[b]) parent[b] = a;
      else
      {
        parent[b] = a;
        rank[a]++;
      }
    }
  }
  
  public override void Part2(string input)
  {
    var junctionLocations = ParseInput(input);
    var n = junctionLocations.Length;

    var parent = Enumerable.Range(0, n).ToArray();
    var rank = new int[n];

    var pq = new PriorityQueue<(int a, int b), long>();
    
    for (var i = 0; i < n; i++)
    for (var j = i + 1; j < n; j++)
      pq.Enqueue((i, j), Dist(i, j));
    
    var components = n;
    (int a, int b) lastEdge = (-1, -1);

    while (pq.Count > 0 && components > 1)
    {
      var (a, b) = pq.Dequeue();

      var ra = Find(a);
      var rb = Find(b);

      if (ra == rb) continue;

      Union(ra, rb);
      components--;

      lastEdge = (a, b);
    }

    var result =
      (long)junctionLocations[lastEdge.a].X *
      junctionLocations[lastEdge.b].X;
    
    Console.WriteLine($"Result: {result}");
    
    long Dist(int i, int j)
    {
      long dx = junctionLocations[i].X - junctionLocations[j].X;
      long dy = junctionLocations[i].Y - junctionLocations[j].Y;
      long dz = junctionLocations[i].Z - junctionLocations[j].Z;
      return dx * dx + dy * dy + dz * dz;
    }

    int Find(int x)
    {
      if (parent[x] != x)
        parent[x] = Find(parent[x]);
      return parent[x];
    }

    void Union(int a, int b)
    {
      a = Find(a);
      b = Find(b);
      if (a == b) return;

      if (rank[a] < rank[b]) parent[a] = b;
      else if (rank[a] > rank[b]) parent[b] = a;
      else
      {
        parent[b] = a;
        rank[a]++;
      }
    }
  }
  
  private static JunctionLocation[] ParseInput(string input) =>
    input
      .Split('\n', StringSplitOptions.RemoveEmptyEntries)
      .Select(line =>
      {
          var v = line.Split(',').Select(int.Parse).ToArray();
          return new JunctionLocation(v[0], v[1], v[2]);
      })
      .ToArray();
}