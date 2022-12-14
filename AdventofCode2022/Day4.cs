public static class Day4
{




    public static int DayFourA(string input)
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

    public static int DayFourB(string input)
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
}