using System;

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
}
