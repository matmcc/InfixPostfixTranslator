using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfixPostfixTranslator
{
    class ExpressionOLD
    {
        private string _infix = "";
        private string _postfix = "";
        private string allowedSymbols = "()*/+-";
        
        public string Infix { get { return _infix; } private set { _infix = value; } }
        public string Postfix { get { return _postfix; } private set { _postfix = value; } }
        public string AllowedSymbols { get { return allowedSymbols; } }

        public ExpressionOLD()
        {
            this.Infix = "";
        }

        public ExpressionOLD(string infix)
        {
            if (VerifyInput(infix))
            {
                this.Infix = CleanInput(infix);
            }
            else
            {
                this.Infix = "";
            }
        }

        public ExpressionOLD(string[] infix)
        {
            if (VerifyInput(infix))
            {
                this.Infix = CleanInput(infix);
            }
            else
            {
                this.Infix = "";
            }
        }

        public void GetInfix()
        {
            string input = "";
            do
            {
                Console.WriteLine("Enter infix expression: ");
                input = Console.ReadLine();
            } while (!VerifyInput(input)) ;

            Infix = CleanInput(input);
        }

        private bool VerifyInput(string input)
        {
            bool _allCharsAllowed = false;
            foreach (var c in input)
            {
                // Currently allows letters, numbers, whitespace, AllowedSymbols as input
                if (Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c) || AllowedSymbols.Contains(c))
                { _allCharsAllowed = true; }
                else
                {
                    Console.WriteLine($"Invalid input: {c}. Only letters, numbers and symbols: {AllowedSymbols} are valid input");
                    return false;
                }
            }

            return _allCharsAllowed;
        }

        private bool VerifyInput(string[] input)
        {
            bool _allCharsAllowed = false;
            foreach (var c in input)
            {
                // Currently allows letters, numbers, whitespace, AllowedSymbols as input
                if (Regex.IsMatch(c, @"(\w+)|(\s+)") || AllowedSymbols.Contains(c))
                { _allCharsAllowed = true; }
                else
                {
                    Console.WriteLine($"Invalid input: {c}. Only letters, numbers and symbols: {AllowedSymbols} are valid input");
                    return false;
                }
            }

            return _allCharsAllowed;
        }

        private string CleanInput(string input)
        {
            string[] found = Regex.Split(input, @"(\W)");
            StringBuilder builder = new StringBuilder();
            foreach (var c in found)
            {
                if (!String.IsNullOrWhiteSpace(c))
                {
                    builder.Append(c).Append(" ");
                }
            }
            string cleanedString = builder.ToString().Trim();
            return cleanedString;
        }

        private string CleanInput(string[] input)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var c in input)
            {
                if (!String.IsNullOrWhiteSpace(c))
                {
                    builder.Append(c).Append(" ");
                }
            }
            string cleanedString = builder.ToString().Trim();
            return cleanedString;
        }

        //public void ConvertToPostfix(string infix)
        //{
        //    Postfix = InfixToPostfix.Convert(infix);
        //}

        public void ShowInfix()
        {
            Console.WriteLine(Infix);
        }

        public void ShowPostfix()
        {
            Console.WriteLine(Postfix);
        }

        public string ReturnInfix() => Infix;
        public string ReturnPostFix() => Postfix;
        
    }
}
