using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBKStudio1.Models
{
    public class MathParser
    {
        public static bool IsDelimeter(string token)
        {
            return IsDelimeter(token.ToCharArray()[0]);
        }
        public static bool IsDelimeter(char token)
        {
            if (" =".IndexOf(token) != -1)
            {
                return true;
            }
            return false;
        }

        public static bool IsOperator(string token)
        {
            if (token == "-u")
            {
                return true;
            }else if (token.Length > 1)
            {
                return false;
            }
            return IsOperator(token.ToCharArray()[0]);
        }
        public static bool IsOperator(char token)
        {
            if ("+-−/÷*×^".IndexOf(token) != -1)
            {
                return true;
            }
            return false;
        }

        public static bool IsLeftAssociative(string token)
        {
            return IsOperator(token) && !(token == "^" || token == "-u");
        }

        public static bool IsRightAssociative(string token)
        {
            return token == "^" || token == "-u";
        }

        public static byte GetPriority(string token)
        {
            switch (token)
            {
                case "+": return 2;
                case "-": return 2;
                case "−": return 2;
                case "*": return 4;
                case "×": return 4;
                case "/": return 4;
                case "÷": return 4;
                case "^": return 6;
                case "-u": return 8;
                default: return 10;
            }
        }
    }
}
