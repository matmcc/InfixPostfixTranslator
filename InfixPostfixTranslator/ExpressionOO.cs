using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfixPostfixTranslator
{
    class UserInterface
    {
        public string Infix { get { return _infix; } private set { _infix = value; } }
        private string _infix;

        public string Postfix { get { return _postfix; } private set { _postfix = value; } }
        private string _postfix;
        
        private ExpressionOO expression;
        private bool runAgain;
        
        public UserInterface()
        {
            this.Infix = "";
            this.Postfix = "";
            this.expression = new ExpressionOO();
        }

        public UserInterface(bool runAgain) : this()
        { this.runAgain = runAgain; }

        public void Run()
        {
            string input = "";
            do
            {
                GetInfix();
                ShowInfix();
                GetPostfix();
                ShowInfix();
                ShowPostfix();
                do
                {
                    Console.WriteLine("Run again?: Y/N");
                    input = Console.ReadKey().Key.ToString().ToLower();
                    Console.WriteLine();
                } while (!(input == "y" || input == "n"));
                runAgain = (input == "y");
            } while (runAgain);
        }

        public void GetInfix()
        {
            string input = "";
            do
            {
                Console.WriteLine("Enter infix expression: ");
                input = Console.ReadLine();
            } while (!expression.VerifyInput(input));

            expression.Infix = input;
            Infix = expression.Infix;
        }

        public void GetPostfix()
        {
            string input;
            do
            {
                Console.WriteLine("Run (V)erbose or (S)ilent?");
                input = Console.ReadKey().Key.ToString().ToLower(); // Gets case-insensitive key input...
                Console.WriteLine();
            } while (!(input == "v" || input == "s"));  // ... to use here

            bool verbosemode = true ? (input == "v") : false;
            expression.ConvertToPostfix(verbosemode);
            Postfix = expression.Postfix;
        }

        public void ShowInfix()
        {
            Console.WriteLine($"\nThe Infix you entered is :\n{Infix}\n");
        }

        public void ShowPostfix()
        {
            Console.WriteLine($"\nThe Postfix equivalent is :\n{Postfix}\n");
        }
    }

    class ExpressionOO
    {
        private string _infix = "";
        public string Infix { get { return _infix; }
            set { if (VerifyInput(value)) { _infix = CleanInput(value); } } }

        private string _postfix = "";
        public string Postfix { get { return _postfix; } private set { _postfix = value; } }

        private string _allowedSymbols = "()*/+-";
        public string AllowedSymbols { get { return _allowedSymbols; } }

        public ExpressionOO()
        { this.Infix = ""; }

        public ExpressionOO(string infix)
        {
            if (VerifyInput(infix))
            { this.Infix = CleanInput(infix); }
            else { this.Infix = ""; }
        }

        public ExpressionOO(string[] infix)
        {
            if (VerifyInput(infix))
            { this.Infix = CleanInput(infix); }
            else { this.Infix = ""; }
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

        public bool VerifyInput(string input)
        {
            bool _allCharsAllowed = false;
            foreach (var c in input)
            {
                // Currently allows letters, numbers, whitespace, AllowedSymbols as input
                if (Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c) || AllowedSymbols.Contains(c))
                { _allCharsAllowed = true; }
                else
                {
                    Console.WriteLine($"Invalid input: {c}\nOnly letters, numbers and symbols: {AllowedSymbols} are valid input");
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

        public void ConvertToPostfix(bool verbosemode = false)
        {
            InfixToPostfixOO converter = new InfixToPostfixOO(Infix, verbosemode);
            Postfix = converter.Convert();
        }

        public void ConvertToPostfix2(bool verbosemode = false)
        {
            InfixToPostfix converter = new InfixToPostfix(Infix, verbosemode);
            Postfix = converter.Convert();
        }

    }
}
