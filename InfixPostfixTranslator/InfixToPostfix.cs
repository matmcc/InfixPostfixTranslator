using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace InfixPostfixTranslator
{
    class InfixToPostfix
    {
        bool _verbose;
        string operators = "*/+-";

        private string _infix = "";
        public string Infix { get => _infix; set => _infix = value; }

        private string _postfix = "";
        public string Postfix
        {
            get { return _postfix; }
            set {
                _postfix = value;
                if (_verbose) { Console.WriteLine("{0, -20}{1}", "Building postfix: ", Postfix); }
                }
        }

        public InfixToPostfix(string input)
        { Infix = input; }

        public InfixToPostfix(string input, bool verbosemode) : this(input)
        { _verbose = verbosemode; }

        public string Convert()
        {
            var _stack = _verbose ? new StackVerbose<string>() : new Stack_LinkedListBased<string>();
            //var tokens = infix.ToCharArray();   // Breaks with numbers >1 digit
            var tokens = Infix.Split();

            foreach (var c in tokens)
            {
                // if letter or number, push to stack
                if (Regex.IsMatch(c, @"(\w+)"))
                {
                    Postfix += c + " ";
                }

                // if opening bracket, push to stack
                if (c == "(")
                { _stack.Push(c); }
                
                // if closing bracket...
                else if (c == ")")
                {
                    while (_stack.Peek() != "(")
                    {Postfix += _stack.Pop() + " "; }    // append operators to string until "("
                    _stack.Pop();   // discard "("
                }

                // if "operator"...
                else if (operators.Contains(c))
                {
                    if (_stack.Count == 0)  // if stack empty, push to stack
                    { _stack.Push(c); }
                    else
                    {
                        while (_stack.Peek() != "(")    // traverse stack until "(" ...
                        {
                            if (Precedence(_stack.Peek(), c))   // if precedence of next item on stack >= this then pop ...
                            { Postfix += _stack.Pop() + " "; } // ... append to _postfix with trailing space
                            else { break; }         // ... else if precedence !>= then break
                            if (_stack.Count == 0)  // if stack empty then break
                                break;
                        }
                        _stack.Push(c); // ... now push new operator to stack
                    }
                }
                // else throw exception here ?
            }

            for (int i = _stack.Count; i > 0; i--)
            {
                Postfix += _stack.Pop() + " ";
            }
            Postfix.TrimEnd();
            Postfix += "\n";
            //_postfix = String.Join(" ", _postfix.ToCharArray()); // to space _postfix TODO: But Fucks Up e.g. 100 
            return Postfix;
        }

        private static bool Precedence(string this_, string other)
        {
            if (this_ == other  || this_ == "*" || this_ == "/") // quick escape
            { return true; }
            else if (this_ == "+" || this_ == "-")
            {
                if (other == "+" || other == "-")
                { return true; }
                else
                { return false; }
            }
            else { return false; }
        }

    }
}