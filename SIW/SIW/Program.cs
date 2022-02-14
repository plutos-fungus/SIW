namespace SIW // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataManager vars = new DataManager();
            Lexer lexer = new Lexer(@"..\..\..\Files\Test.txt");
            lexer.PrintAllLines();
        }
    }
}