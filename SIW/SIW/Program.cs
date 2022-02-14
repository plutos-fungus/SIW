namespace SIW // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataManager vars = new DataManager();
            Lexer lexer = new Lexer(@"..\..\..\Files\Test.txt");
            Token t; 
            while( (t = lexer.Next()!) != null )
            {
                Console.WriteLine("Token: contents: {0}, type: {1}, line: {2}", t.Contents, t.Type, t.Line);
            }
        }
    }
}