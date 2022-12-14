using System.Data;

public static class Day3
{




    public static int DayThreeA(string input)
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

    public static int DayThreeB(string input)
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
}