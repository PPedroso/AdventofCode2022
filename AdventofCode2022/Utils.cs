namespace AdventofCode2022
{
    internal static class Utils
    {
        internal static string GetFileInput(string filePath)
        {
            return File.ReadAllText($"../../../inputs/{filePath}");
        }
    }
}
