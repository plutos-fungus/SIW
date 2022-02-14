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
        NONE,
        INT,
        NAME,
        OPERATOR,
        // const int lol: int = 100
        // unconst lol = 10000
    }

    public class Token
    {
        public string Contents;
        public TokenType Type;
        public int Line;

        public Token(string contents, TokenType type, int line)
        {
            if (contents == null)
            {
                Console.Error.WriteLine("Error: token created with null string");
                Environment.Exit(1);
            }
            else
            {
                Contents = contents;
            }
            Type = type;
            Line = line;
        }
    }

    public class Lexer
    {
        private string[] lines;
        private string[]? line = null;
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

        public Token? Next()
        {
            string word;
            if(line == null) 
            {
                line = lines[lineNo].Trim().Split(' ');
            } else if (wordNo >= line.Length)
            {
                wordNo = 0;
                if(lineNo < lines.Length - 1)
                {
                    line = lines[++lineNo].Trim().Split(' ');
                }
                else
                {
                    return null;
                }
            }
            word = line[wordNo++];
            int wordEnum;

            if(Char.IsDigit(word[0]) || word[0] == '-' || word[0] == '+')
            {
                for(wordEnum = 1; wordEnum < word.Length; wordEnum++)
                {
                    char c = word[wordEnum];
                    if(!Char.IsDigit(c))
                    {
                        Console.Error.WriteLine("Error: token {0} at line {1}: token of type int cannot contain nondigit character: '{2}'", word, lineNo, c);
                        Environment.Exit(1);
                    }
                }
                if(wordEnum == 1)
                {
                    return new Token(word, TokenType.OPERATOR, lineNo);
                } else
                {
                    return new Token(word, TokenType.INT, lineNo);
                }
            } else if(Char.IsLetter(word[0]))
            {
                foreach(char c in word)
                {
                    if(!(Char.IsLetterOrDigit(c) || c != '_' || c != '-'))
                    {
                        Console.Error.WriteLine("Error: token {0} at line {1}: Names can not contain character: '{2}'", word, lineNo, c);
                        Environment.Exit(1);
                    }
                }
                return new Token(word, TokenType.NAME, lineNo);
            } else
            {
                return new Token(word, TokenType.NONE, wordNo);
            }
        }

        public void PrintAllLines()
        {
            foreach(string line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
