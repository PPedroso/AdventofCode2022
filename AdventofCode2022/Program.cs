

Console.WriteLine($"Day one - {DayOne()}");
Console.ReadLine();


int DayOne()
{
    var input = File.ReadAllText("../../../inputs/dayone.txt");

    var elves = input.Split(Environment.NewLine);

    var max = 0;
    var total = 0;

    foreach (var elf in elves)
    {
        if (String.IsNullOrEmpty(elf))
        {
            if (total > max)
                max = total;
            total = 0;
        }

        else
            total += Int32.Parse(elf);
    }

    return max;
}

