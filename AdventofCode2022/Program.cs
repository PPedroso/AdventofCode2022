using System.ComponentModel;
using System.Data;
using System.Globalization;
using static AdventofCode2022.Utils;

Console.WriteLine($"Day seven - {Main.DaySevenB()}");
Console.ReadLine();


public static class Main
{
    static string input = GetFileInput("day7.txt");

    #region DayOne

    internal static int DayOneA()
    {
        var elves = input.Split(Environment.NewLine);

        var max = 0;
        var total = 0;

        foreach (var calories in elves)
        {
            if (String.IsNullOrEmpty(calories))
            {
                if (total > max)
                    max = total;
                total = 0;
            }

            else
                total += Int32.Parse(calories);
        }

        return max;
    }

    internal static int DayOneB()
    {
        var elves = input.Split(Environment.NewLine);

        int[] maxCalories = new int[3] { 0, 0, 0 };
        var total = 0;

        foreach (var calories in elves)
        {
            if (String.IsNullOrEmpty(calories))
            {
                if (total > maxCalories[0])
                {
                    maxCalories[0] = total;
                    Array.Sort(maxCalories);
                }


                total = 0;
            }
            else
                total += Int32.Parse(calories);
        }

        if (total > maxCalories[0])
            maxCalories[0] = total;

        return maxCalories.Sum();

    }

    #endregion DayOne

    #region DayTwo

    internal static int DayTwoA()
    {
        var inputs = input.Split(Environment.NewLine);
        int total = 0;

        foreach (var play in inputs)
        {
            var choices = play
                            .Replace("X", "A")
                            .Replace("Y", "B")
                            .Replace("Z", "C")
                            .Split(" ")
                            .Select(x => Char.Parse(x))
                            .ToArray();

            int result = DoesFirstPlayerWin(choices[1], choices[0]);
            if (result > 0) total += choices[1].ToScore() + 6;
            if (result == 0) total += choices[1].ToScore() + 3;
            if (result < 0) total += choices[1].ToScore();
        }

        return total;
    }

    internal static int DayTwoB()
    {

        var inputs = input.Split(Environment.NewLine);

        int total = 0;

        foreach (var play in inputs)
        {
            var choices = play
                            .Split(" ")
                            .Select(x => Char.Parse(x))
                            .ToArray();

            var enemyChoice = choices[0];
            var desiredOutcome = choices[1];

            char complementaryCharacter = GetComplementaryCharacter(enemyChoice, desiredOutcome);
            int result = DoesFirstPlayerWin(complementaryCharacter, enemyChoice);
            if (result > 0) total += complementaryCharacter.ToScore() + 6;
            if (result == 0) total += complementaryCharacter.ToScore() + 3;
            if (result < 0) total += complementaryCharacter.ToScore();
        }

        return total;
    }

    static int ToScore(this Char c) => ((int)c) - 64;

    /// <summary>
    /// A - Rock, B - Paper, C - Scisors
    /// 1 for win, 0 for draw, -1 for loss
    /// </summary>
    static int DoesFirstPlayerWin(char mine, char his)
    {
        if (mine == his) return 0;
        if (mine == 'A' && his == 'C') return 1; //Rock(A) beats Scisors(C)) 
        if (mine == 'B' && his == 'A') return 1; //Paper(B) beats Rock(A) 
        if (mine == 'C' && his == 'B') return 1; //Scisors(C) beats Paper(B
        return -1;
    }

    static Char Beats(this Char c)
    {
        if (c == 'A') return 'C';
        if (c == 'B') return 'A';
        return 'B';
    }

    static Char IsBeatenBy(this Char c)
    {
        if (c == 'C') return 'A';
        if (c == 'A') return 'B';
        return 'C';
    }

    static Char GetComplementaryCharacter(Char choice, Char outcome)
    {
        if (outcome == 'X') return choice.Beats();
        if (outcome == 'Y') return choice;
        return choice.IsBeatenBy();
    }

    #endregion DayTwo

    #region DayThree

    public static int DayThreeA()
    {

        int total = 0;
        var rucksacks = input.Split(Environment.NewLine);

        foreach (var rucksack in rucksacks)
        {
            var firstCompartment = rucksack.Substring(0, rucksack.Length / 2);
            var secondCompartment = rucksack.Substring((rucksack.Length / 2));

            int index = secondCompartment.IndexOfAny(firstCompartment.ToCharArray());

            if (index != -1)
                total += secondCompartment[index].ToPriority();

        }

        return total;
    }

    public static int DayThreeB()
    {

        int total = 0;
        var rucksacks = input.Split(Environment.NewLine);

        int[] totals = new int[51];

        for (int i = 0; i < rucksacks.Length; i += 3)
        {
            var rucksack1 = rucksacks[i];
            var rucksack2 = rucksacks[i + 1];
            var rucksack3 = rucksacks[i + 2];

            var result = rucksack1.Where(x => rucksack2.Contains(x));
            result = result.Where(x => rucksack3.Contains(x));

            total += result.FirstOrDefault().ToPriority();
        }

        return total;
    }

    /// <summary>
    /// Converts to priority.
    /// Lowercase item types a through z have priorities 1 through 26.
    //  Uppercase item types A through Z have priorities 27 through 52.
    /// </summary>
    static int ToPriority(this Char c)
    {
        if (c >= 'a' && c <= 'z') return ((int)c) - 96;
        return ((int)c) - 38;
    }

    #endregion

    #region DayFour

    public static int DayFourA()
    {
        var total = 0;

        foreach (var assignemt in input.Split(Environment.NewLine))
        {
            var sections = assignemt.Split(',');

            if (sections[0].IsContaintedBy(sections[1]) || sections[1].IsContaintedBy(sections[0]))
                total++;
        }

        return total;
    }

    public static int DayFourB()
    {
        var total = 0;

        foreach (var assignemt in input.Split(Environment.NewLine))
        {
            var sections = assignemt.Split(',');

            if (sections[0].IsOverlapedBy(sections[1]) || sections[1].IsOverlapedBy(sections[0]))
                total++;
        }

        return total;
    }


    /// <summary>
    /// Format of the string -> 2-5
    /// </summary>
    static bool IsContaintedBy(this string str, string container)
    {
        var strLimits = str.Split('-');
        var containerLimits = container.Split('-');

        return int.Parse(containerLimits[0]) <= int.Parse(strLimits[0]) && int.Parse(containerLimits[1]) >= int.Parse(strLimits[1]);
    }

    /// <summary>
    /// Format of the string -> 2-5
    /// </summary>
    static bool IsOverlapedBy(this string str, string overlapper)
    {
        var strLimits = str.Split('-');
        var overlapperLimits = overlapper.Split('-');

        if (int.Parse(strLimits[0]) >= int.Parse(overlapperLimits[0]) && int.Parse(strLimits[0]) <= int.Parse(overlapperLimits[1]))
            return true;

        if (int.Parse(strLimits[1]) >= int.Parse(overlapperLimits[0]) && int.Parse(strLimits[1]) <= int.Parse(overlapperLimits[1]))
            return true;

        return false;
    }

    #endregion  DayFour

    #region DayFive

    public static string DayFiveA()
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

    public static string DayFiveB()
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

    #endregion

    #region DaySix

    public static int DaySixA()
    {
        for (int s = 0; (s + 4) < input.Length;)
        {
            string str = input.Substring(s, 4);

            var groups = str.GroupBy(g => g);

            if (groups.Count() == 4)
                return s + 4;
            else
                s += 1;
        }
        return 0;
    }

    public static int DaySixB()
    {
        for (int s = 0; (s + 14) < input.Length;)
        {
            string str = input.Substring(s, 14);

            var groups = str.GroupBy(g => g);

            if (groups.Count() == 14)
                return s + 14;
            else
                s += 1;
        }
        return 0;
    }

    #endregion DaySix

    #region DaySeven

    public static int DaySevenA()
    {
        Dir root = new Dir { Name = "/", Size = 0 };

        Dir currentNode = root;

        foreach (var command in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Skip(1))
        {
            var arguments = command.Split(' ');

            if (command.Contains("dir"))
                currentNode.Children.Add(new Dir { Name = arguments[1], Parent = currentNode });

            int result;
            if (int.TryParse(arguments[0], out result))
                currentNode.Children.Add(new Dir { Name = arguments[1], Size = result, Parent = currentNode });

            if (command.Contains("cd "))
            {
                if (arguments[2] == "..")
                    currentNode = currentNode.Parent;
                else
                    currentNode = currentNode.Children.Where(x => x.Name.Equals(arguments[2])).FirstOrDefault();
            }

        }

        
        List<KeyValuePair<string, int>> collection = new List<KeyValuePair<string, int>>();
        RegisterGetTotalSize(root, collection);

        return collection.Where(x => x.Value <=100000).Sum(x => x.Value);
    }

    private static int RegisterGetTotalSize(Dir node, List<KeyValuePair<string, int>> collection)
    {
        if (node.Children == null) return 0;
        
        int total = 0;
        
        foreach (var child in node.Children)
        {
            if (child.Size > 0)
                total += child.Size;
            else
               total += RegisterGetTotalSize(child, collection);
        }
        if (total > 0)
            collection.Add(new KeyValuePair<string, int>(node.Name, total));

        return total;
    }

    private class Dir
    {
        public Dir()
        {
            Children = new List<Dir>();
        }

        public Dir Parent { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public List<Dir> Children { get; set; }
    }

    public static int DaySevenB()
    {
        Dir root = new Dir { Name = "/", Size = 0 };

        Dir currentNode = root;

        foreach (var command in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Skip(1))
        {
            var arguments = command.Split(' ');

            if (command.Contains("dir"))
                currentNode.Children.Add(new Dir { Name = arguments[1], Parent = currentNode });

            int result;
            if (int.TryParse(arguments[0], out result))
                currentNode.Children.Add(new Dir { Name = arguments[1], Size = result, Parent = currentNode });

            if (command.Contains("cd "))
            {
                if (arguments[2] == "..")
                    currentNode = currentNode.Parent;
                else
                    currentNode = currentNode.Children.Where(x => x.Name.Equals(arguments[2])).FirstOrDefault();
            }

        }

        List<KeyValuePair<string, int>> collection = new List<KeyValuePair<string, int>>();
        RegisterGetTotalSize(root, collection);

        int remainingSpace = 70000000 - collection.FirstOrDefault(x => x.Key.Equals("/")).Value;
        int spaceNeededToUpdate = 30000000 - remainingSpace;


        return collection.Where(x => x.Value >= spaceNeededToUpdate).Min(x => x.Value);
    }

    #endregion DaySeven
}


