public static class Day6
{
    public static int DaySixA(string input)
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

    public static int DaySixB(string input)
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
}