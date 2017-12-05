using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfixPostfixTranslator
{
    class InfixToPostfixOO
    {
        bool _verbose;
        string _postfix = "";
        public string Postfix
        {
            get { return _postfix; }
            set
            {
                _postfix = value;
                if (_verbose) { Console.WriteLine("{0, -20}{1}", "Building postfix: ", Postfix); }
            }
        }

        string operators = "*/+-";
        Symbol[] symbols;

        public InfixToPostfixOO(string input)
        {
            string[] _infix = input.Split();
            symbols = new Symbol[_infix.Length];

            for (int i = 0; i < _infix.Length; i++)
            { symbols[i] = new Symbol(_infix[i]); }
        }

        public InfixToPostfixOO(string input, bool verbosemode) : this(input)
        {
            _verbose = verbosemode;
        }

        internal string Convert()
        {
            var _stack = _verbose ? new StackVerbose<Symbol>(): new Stack_LinkedListBased<Symbol>();

            foreach (var s in symbols)
            {
                // if letter or number, push to stack
                if (!s.IsSymbol)
                {
                    Postfix += s + " ";
                }

                // if opening bracket, push to stack
                if (s.Data == "(")
                { _stack.Push(s); }

                // if closing bracket...
                else if (s.Data == ")")
                {
                    while (_stack.Peek().Data != "(")
                    { Postfix += _stack.Pop() + " "; }    // append operators to string until "("
                    _stack.Pop();   // discard "("
                }

                // if "operator"...
                else if (operators.Contains(s.Data))
                {
                    if (_stack.Count == 0)  // if stack empty, push to stack
                    { _stack.Push(s); }
                    else
                    {
                        while (_stack.Peek().Data != "(")    // traverse stack until "(" ...
                        {
                            if (_stack.Peek() >= s)   // if precedence of next item on stack >= this then pop ...
                            { Postfix += _stack.Pop() + " "; } // ... append to _postfix with trailing space
                            else { break; }         // ... else if precedence !>= then break
                            if (_stack.Count == 0)  // if stack empty then break
                                break;
                        }
                        _stack.Push(s); // ... now push new operator to stack
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

        //private static bool Precedence(string this_, string other)
        //{
        //    if (this_ == other || this_ == "*" || this_ == "/") // quick escape
        //    { return true; }
        //    else if (this_ == "+" || this_ == "-")
        //    {
        //        if (other == "+" || other == "-")
        //        { return true; }
        //        else
        //        { return false; }
        //    }
        //    else { return false; }
        //}

    }
}
