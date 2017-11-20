using System;
using System.Collections.Generic;

namespace InfixPostfixTranslator
{
    internal class InfixToPostfix
    {
        internal static string Convert(string infix)
        {
            string _postfix = "";
            //string operands = "0123456789";
            string operators = "*/+-";
            //var _operandSet = new HashSet<char>(operands);
            var _operatorSet = new HashSet<char>(operators); // needed? String.Contains() would work?

            var _stack = new GenStack<string>();
            //var tokens = infix.ToCharArray();   // Breaks with numbers >1 digit
            var tokens = infix.Split();

            foreach (var c in tokens)
            {
                //if (Char.IsLetterOrDigit(c) || c == ' ')    // previously _operandSet.Contains(c)
                //if (Char.IsLetterOrDigit(c))    // don't want whitespace?
                //{ _postfix += c; }
                if (c == "(")
                { _stack.Push(c); }
                else if (c == ")")
                {
                    while (_stack.Peek() != "(")
                    {_postfix += _stack.Pop(); }    // append operators to string until '('
                    _stack.Pop();   // discard '('
                }
                else if (operators.Contains(c))
                {
                    if (_stack.Count == 0)
                    { _stack.Push(c); }
                    else
                    {
                        while (_stack.Peek() != "(")    // traverse stack until '('
                        {
                            if (Precedence(c, _stack.Peek()))   // if precedence >= then pop ...
                            { _postfix += _stack.Pop(); }
                            else { break; }         // ... else break
                            if (_stack.Count == 0)  // stack empty - break
                                break;
                        }
                        _stack.Push(c);
                    }
                }
                else
                {
                    _postfix += c;  //TODO 1: moving this to here has messed up flow, needs to go back up, probably use regex
                }
                // else throw exception here ?
            }

            for (int i = 0; i < _stack.Count; i++)
            {
                _postfix += _stack.Pop();   //TODO 2: Then += " " here and Trim() later
            }
            
            //_postfix = String.Join(" ", _postfix.ToCharArray()); // to space _postfix TODO: But Fucks Up e.g. 100 
            return _postfix;
        }

        private static bool Precedence(string this_, string other)
        {
            if (this_ == other) // quick escape
            { return true; }
            else if (this_ == "+" || this_ == "-")  // could ! this to escape earlier? since */ => true
            {
                if (other == "+" || other == "-")
                { return true; }
                else
                { return false; }
            }
            else // this_ == * or /
            { return true; }
        }

    }
}