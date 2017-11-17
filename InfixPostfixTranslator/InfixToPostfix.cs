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
            var _operatorSet = new HashSet<char>(operators);

            var _stack = new GenStack<char>();
            var tokens = infix.ToCharArray();

            foreach (char c in tokens)
            {
                //if (Char.IsLetterOrDigit(c) || c == ' ')    // previously _operandSet.Contains(c)
                if (Char.IsLetterOrDigit(c))    // don't want whitespace?
                { _postfix += c; }
                else if (c == '(')
                { _stack.Push(c); }
                else if (c == ')')
                {
                    while (_stack.Peek() != '(')
                    {_postfix += _stack.Pop(); }    // append operators to string until '('
                    _stack.Pop();   // discard '('
                }
                else if (_operatorSet.Contains(c))
                {
                    if (_stack.Count == 0)
                    { _stack.Push(c); }
                    else
                    {
                        while (_stack.Peek() != '(')    // traverse stack until '('
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
                // else throw exception here ?
            }
            for (int i = 0; i < _stack.Count; i++)
            {
                _postfix += _stack.Pop();
            }

            _postfix = String.Join(" ", _postfix.ToCharArray()); // to space _postfix
            return _postfix;
        }

        private static bool Precedence(char this_, char other)
        {
            if (this_ == other) // quick escape
            { return true; }
            else if (this_ == '+' || this_ == '-')  // could ! this to escape earlier? since */ => true
            {
                if (other == '+' || other == '-')
                { return true; }
                else
                { return false; }
            }
            else // this_ == * or /
            { return true; }
        }

    }
}