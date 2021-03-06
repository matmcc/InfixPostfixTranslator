﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfixPostfixTranslator
{
    /// <summary>
    /// Converts Infix string to Postfix string.
    /// Call Convert() to update and return Postfix.
    /// If verbosemode set in ctor, prints infix to postfix conversion process.
    /// </summary>
    public class InfixToPostfixOO : IInfixToPostfix
    {
        private string operators = "*/+-";

        public bool VerboseMode { get { return _verbose; } set { _verbose = value; } }
        private bool _verbose;

        /// <summary>
        /// Postfix string property.
        /// If verbosemode set in ctor, setter prints when called
        /// </summary>
        public string Postfix
        {
            get { return _postfix; }
            set
            {
                _postfix = value;
                if (VerboseMode) { Console.WriteLine("{0, -20}{1}", "Building postfix: ", Postfix); }
            }
        }
        private string _postfix = "";

        public string Infix
        {
            get { return _symbols.ToString(); }
            set
            {
                string[] _infix = value.Split();
                _symbols = new Symbol[_infix.Length];

                for (int i = 0; i < _infix.Length; i++)
                { _symbols[i] = new Symbol(_infix[i]); }

                Postfix = "";   //TODO: Note setting Infix resets postfix
            }
        }
        private Symbol[] _symbols;
        
#region Constructors
        public InfixToPostfixOO(string input = "")
        { Infix = input; }

        /// <summary>
        /// if verbosemode == true, will print Infix to Postfix conversion process
        /// </summary>
        /// <param name="input"></param>
        /// <param name="verbosemode">if true, Convert() will print conversion process</param>
        public InfixToPostfixOO(string input, bool verbosemode) : this(input)
        { VerboseMode = verbosemode; }
        #endregion

        public string Convert(string input)
        {
            Infix = input;
            return this.Convert();
        }

        /// <summary>
        /// Converts Infix into postfix notation, sets and returns Postfix.
        /// If verbosemode set in ctor, prints conversion process
        /// </summary>
        /// <returns></returns>
        public string Convert()
        {
            var _stack = VerboseMode ? new StackVerbose<Symbol>(): new Stack_LinkedListBased<Symbol>();

            foreach (var s in _symbols)
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
                        // TODO: surround next with try except block in case _stack.Count == 0 
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
                            { Postfix += _stack.Pop() + " "; } // ... append to Postfix with trailing space
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
        
    }
}
