using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SIW
{
    public enum TokenType
    {
        // const int lol: int = 100
        // unconst lol = 10000
    }

    public class Token
    {
        string contents;
        TokenType type;
    }

    public class Lexer
    {
        private string[] lines = null;
        private string[] line = null;
        private int lineNo = 0;
        private int wordNo = 0;
        public Lexer(string path)
        {
            path = Path.GetFullPath(path);
            try
            {
                lines = System.IO.File.ReadAllLines(path);
            } catch(FileNotFoundException)
            {
                Console.Error.WriteLine("Error: file {0} not found", path);
                Environment.Exit(1);
            }
        }

        public string Next()
        {
        }

        public void PrintAllLines()
        {
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
