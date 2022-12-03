using System.Data;
using static AdventofCode2022.Utils;

Console.WriteLine($"Day two - {Main.DayTwoB()}");
Console.ReadLine();

public static class Main
{
    #region DayOne

    internal static int DayOneA()
    {
        var input = GetFileInput("dayone.txt");

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
        var input = GetFileInput("dayone.txt");
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
        var inputs = GetFileInput("daytwo.txt").Split(Environment.NewLine);

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

        var inputs = GetFileInput("daytwo.txt").Split(Environment.NewLine);

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

}


