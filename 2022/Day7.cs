using System.Collections.Concurrent;

internal class Day7
{

  private static string Input = "$ cd /\r\n$ ls\r\ndir a\r\n14848514 b.txt\r\n8504156 c.dat\r\ndir d\r\n$ cd a\r\n$ ls\r\ndir e\r\n29116 f\r\n2557 g\r\n62596 h.lst\r\n$ cd e\r\n$ ls\r\n584 i\r\n$ cd ..\r\n$ cd ..\r\n$ cd d\r\n$ ls\r\n4060174 j\r\n8033020 d.log\r\n5626152 d.ext\r\n7214296 k";

  public void Run()
  {
    var test = new ConcurrentDictionary<string, long>();
    var s = new Stack<string>();

    using var reader = new StreamReader("Day7.txt");
    //using var reader = new StringReader(Input);

    var line = string.Empty;
    var readDir = false;
    while ((line = reader.ReadLine()) != null)
    {

      if (line[0] == '$')
      {
        readDir = false;

        if (line == "$ ls")
        {
          readDir = true;
          continue;
        }

        if (line == "$ cd ..")
        {
          s.Pop();
          continue;
        }

        if (line == "$ cd /")
        {
          s.Clear();
        }

        s.Push(line[5..]);
      }

      if (readDir)
      {
        if (line[0..3] == "dir")
          continue;

        var path = string.Empty;
        foreach (var item in s.Reverse())
        {
          path = path + item + '/';
          test.AddOrUpdate(path, int.Parse(line.Split(' ')[0]), (k, v) => { return v + int.Parse(line.Split(' ')[0]); });
        }
      }
    }

    // Part 1
    Console.WriteLine(test.Where(x => x.Value < 100000).Sum(x => x.Value));

    // Part 2
    var largestDir = test["//"];
    var availableSpace = 70000000 - largestDir;
    var spaceMissing = 30000000 - availableSpace;

    var result = test
      .Where(x => x.Value >= spaceMissing)
      .OrderBy(x => x.Value)
      .First();

    Console.WriteLine(result.Value);
  }
}