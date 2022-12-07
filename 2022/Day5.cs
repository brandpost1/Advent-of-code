internal class Day5
{
  private static string Input = "    [D]    \r\n[N] [C]    \r\n[Z] [M] [P]\r\n 1   2   3 \r\n\r\nmove 1 from 2 to 1\r\nmove 3 from 1 to 3\r\nmove 2 from 2 to 1\r\nmove 1 from 1 to 2";
  private static bool IsCrateMover9001 = true;

  public void Run()
  {
    //using var reader = new StringReader(Input);
    using var reader = new StreamReader("Day5.txt");
    


    var line = string.Empty;
    var stacks = new Dictionary<int, Stack<char>>();

    while((line = reader.ReadLine()) != null) 
    {
      if (string.IsNullOrWhiteSpace(line))
      {
        foreach(var item in stacks) 
        {
          stacks[item.Key] = new Stack<char>(item.Value);
        }
      }

      if (!string.IsNullOrWhiteSpace(line) && line[0..4] == "move")
      {
        var split = line.Split(' ');
        var fromStack = stacks[int.Parse(split[3])];
        var toStack = stacks[int.Parse(split[5])];
        var crateMover9001Stack = new Stack<char>();

        for (int i = 0; i < int.Parse(split[1]); i++)
        {
          var toMove = fromStack.Pop();

          if (IsCrateMover9001)
            crateMover9001Stack.Push(toMove);
          else
            toStack.Push(toMove);
        }
        
        if (IsCrateMover9001)
        {
          while (crateMover9001Stack.TryPop(out var result))
            toStack.Push(result);
        }

        continue;
      }

      for (int i = 0; i < line.Length; i += 4) 
      {
        var value = line[i + 1];
        if (!char.IsLetter(value))
          continue;

        var stackIndex = (i / 4) + 1;
        if (!stacks.TryGetValue(stackIndex, out var stack))
        {
          stack = new Stack<char>();
          stacks.Add(stackIndex, stack);
        }
        stack.Push(value);
      }
    }

    foreach(var item in stacks.OrderBy(x => x.Key)) 
    {
      Console.Write(item.Value.Peek());
    }
  }
}