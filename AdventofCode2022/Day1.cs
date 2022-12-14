public static class Day1
{
    internal static int DayOneA(string input)
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

    internal static int DayOneB(string input)
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
}