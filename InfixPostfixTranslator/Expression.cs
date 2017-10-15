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


//Notes
//    allowing setting of AllowedSymbols at any time complicates other parts of prgoram - e.g. testing and pushing to stack
//    potential for more breaking errors
//    Also allowing setting these at ctor breaks validification in getINput

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
        }
        // TODO: input checking below does not apply to string arg's to constructor - inconsistent, should they be removed?

        public void GetInfix()
        {
            Console.WriteLine("Enter infix expression: ");
            string input = Console.ReadLine();

            // TODO: Below checks input - do we want this testing here? or later, e.g. in translator? Fail Fast - here
            var tokens = input.ToCharArray();   // tried splitting at whitespace, too fragile, e.g. 2 * (3 + 1) would fail
            bool _allCharsAllowed = false;
            foreach (char c in tokens)
            {
                double temp;    // double to enable more numerical input (not spec'd in problem)... but is double.TryParse(c.ToString() below efficient?
                if (double.TryParse(c.ToString(), out temp) || AllowedSymbols.Contains(c) || Char.IsWhiteSpace(c))
                { _allCharsAllowed = true; }
                else { _allCharsAllowed = false; }
            }

            if (!_allCharsAllowed)
            {
                Console.WriteLine($"Only numbers and symbols: {AllowedSymbols} are valid input");
            }
            else
            {
                Infix = input;
            }
        }

        public void ShowInfix()
        {
            Console.WriteLine(Infix);
        }

        public void ShowPostfix()
        {
            throw new NotImplementedException();
        }

    }
}
