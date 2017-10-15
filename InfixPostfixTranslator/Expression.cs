using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
It is recommended you design an ‘Expression’ class that stores the infix and postfix strings.
That class should include the following methods:
getInfix: Stores the infix expression.
showInfix: Outputs the infix expression.
showPostfix: Outputs the postfix expression
*/

namespace InfixPostfixTranslator
{
    class Expression
    {
        private string _infix = "";
        private string allowedSymbols = "";

        protected string Infix { get { return _infix; } private set { _infix = value; } }
        protected string AllowedSymbols { get => allowedSymbols; set => allowedSymbols = value; } // why different?

        public Expression(string infix = "", string symbols = "()*/+-")
        {
            this.Infix = infix;
            this.AllowedSymbols = symbols;
            Console.WriteLine(AllowedSymbols);
            Console.WriteLine(Infix);
        }

        public void getInfix()
        {
            Console.WriteLine("Enter infix expression: ");
            string input = Console.ReadLine();
            string[] tokens = input.Split(' ');
            bool _allCharsAllowed = false;
            foreach (string c in tokens)
            {
                double temp;
                if (double.TryParse(c, out temp) || AllowedSymbols.Contains(c))
                { _allCharsAllowed = true;
                    Console.WriteLine(c + " " + temp);
                }
                else { _allCharsAllowed = false; }
            }

            if (!_allCharsAllowed)
            {
                Console.WriteLine($"Only numbers and symbols: {AllowedSymbols} are valid");
            }
            else
            {
                Infix = input;
            }
        }

        public void showInfix()
        {
            Console.WriteLine(Infix);
        }

    }
}
