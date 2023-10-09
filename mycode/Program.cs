using FluentColorConsole;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var text = ColorConsole.WithRedText;
        text.WriteLine("My red text");
    }
}