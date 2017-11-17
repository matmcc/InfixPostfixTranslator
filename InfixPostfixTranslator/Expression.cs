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
//    allowing setting of AllowedSymbols at any time complicates other parts of program - e.g. testing and pushing to stack
//    potential for more breaking errors
//    Also allowing setting these at ctor breaks validification in getInput

// TODO: Overload ctor to take string or array

namespace InfixPostfixTranslator
{
    class Expression
    {
        private string _infix = "";
        private string _postfix = "";
        private string allowedSymbols = "";

        // TODO: Should setters be private?
        protected string Infix { get { return _infix; } set { _infix = value; } }
        protected string Postfix { get { return _postfix; } set { _postfix = value; } }
        protected string AllowedSymbols { get { return allowedSymbols; } set { allowedSymbols = value; } }

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
                // Currently allows letters, numbers, whitespace, AllowedSymbols as input
                if (Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c) || AllowedSymbols.Contains(c))
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

        public void ConvertToPostfix(string infix)
        {
            Postfix = InfixToPostfix.Convert(infix);
        }

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
