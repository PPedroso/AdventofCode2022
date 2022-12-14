public static class Day5
{




    public static string DayFiveA(string input)
    {
        Stack<Char>[] stacks = new Stack<Char>[9]
        {
            new Stack<Char>(),
            new Stack<Char>(),
            new Stack<Char>(),
            new Stack<Char>(),
            new Stack<Char>(),
            new Stack<Char>(),
            new Stack<Char>(),
            new Stack<Char>(),
            new Stack<Char>(),
        };

        bool processMoves = false;

        foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            if (line[1] >= '1' && line[0] <= '9')
            {
                processMoves = true;

                for (int i = 0; i < stacks.Length; ++i)
                    stacks[i] = new Stack<char>(stacks[i]);

                continue;
            }

            if (!processMoves)
            {
                for (int i = 0; i < line.Length; i += 4)
                {
                    if (line[i + 1] != ' ')
                        stacks[(i / 4)].Push(line[i + 1]);
                }
            }
            else
            {
                var move = line.Replace("move", "").Replace("from", "").Replace("to", "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int number = int.Parse(move[0]);
                int from = int.Parse(move[1]) - 1;
                int to = int.Parse(move[2]) - 1;

                for (int i = 0; i < number; ++i)
                {
                    stacks[to].Push(stacks[from].Pop());
                }
            }


        }



        string result = string.Empty;

        foreach (var stack in stacks)
        {
            result += stack.Pop();
        }

        return result;

    }

    public static string DayFiveB(string input)
    {
        const int NUMBER_OF_STACKS = 9;

        Stack<Char>[] stacks = new Stack<Char>[NUMBER_OF_STACKS];
        for (int i = 0; i < NUMBER_OF_STACKS; ++i)
            stacks[i] = new Stack<char>();

        bool processMoves = false;

        foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            if (line[1] >= '1' && line[0] <= '9')
            {
                processMoves = true;

                for (int i = 0; i < stacks.Length; ++i)
                    stacks[i] = new Stack<char>(stacks[i]);

                continue;
            }

            if (!processMoves)
            {
                for (int i = 0; i < line.Length; i += 4)
                {
                    if (line[i + 1] != ' ')
                        stacks[(i / 4)].Push(line[i + 1]);

                }
            }
            else
            {
                var move = line.Replace("move", "").Replace("from", "").Replace("to", "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int number = int.Parse(move[0]);
                int from = int.Parse(move[1]) - 1;
                int to = int.Parse(move[2]) - 1;
                List<char> cratesToAdd = new List<char>();

                for (int i = 0; i < number; ++i)
                    cratesToAdd.Add(stacks[from].Pop());

                cratesToAdd.Reverse();

                foreach (var crate in cratesToAdd)
                    stacks[to].Push(crate);
            }


        }


        string result = string.Empty;

        foreach (var stack in stacks)
        {
            result += stack.Pop();
        }

        return result;

    }
}