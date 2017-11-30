using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace InfixPostfixTranslator
{
    internal class InfixToPostfix
    {
        internal static string Convert(string infix)
        {
            string _postfix = "";
            string operators = "*/+-";
            
            var _stack = new Stack_LinkedListBased<string>();
            //var tokens = infix.ToCharArray();   // Breaks with numbers >1 digit
            var tokens = infix.Split();

            foreach (var c in tokens)
            {
                // if letter or number, push to stack
                if (Regex.IsMatch(c, @"(\w+)"))
                {
                    _postfix += c + " ";
                }

                // if opening bracket, push to stack
                if (c == "(")
                { _stack.Push(c); }
                
                // if closing bracket...
                else if (c == ")")
                {
                    while (_stack.Peek() != "(")
                    {_postfix += _stack.Pop() + " "; }    // append operators to string until "("
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
                            { _postfix += _stack.Pop() + " "; } // ... append to _postfix with trailing space
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
                _postfix += _stack.Pop() + " ";
            }
            _postfix.TrimEnd();
            //_postfix = String.Join(" ", _postfix.ToCharArray()); // to space _postfix TODO: But Fucks Up e.g. 100 
            return _postfix;
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