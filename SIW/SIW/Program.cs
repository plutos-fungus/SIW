namespace SIW // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataManager d = new DataManager();
            d.AddVar("a", 100);
            d.AddVar("b", 3);
            Console.WriteLine("a = {0}", d.GetVar("a"));
            Console.WriteLine("b = {0}", d.GetVar("b"));
            d.SetVar("a", 50);
            Console.WriteLine("a = {0}", d.GetVar("a"));
        }
    }
}