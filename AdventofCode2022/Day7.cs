using System.Data;

public static class Day7
{

    public static int DaySevenA(string input)
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

        return collection.Where(x => x.Value <= 100000).Sum(x => x.Value);
    }

    public static int DaySevenB(string input)
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
}