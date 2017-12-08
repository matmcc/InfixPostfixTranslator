using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace InfixPostfixTranslator
{
    /// <summary>
    /// Converts Infix string to Postfix string.
    /// Call Convert() to update and return Postfix.
    /// If verbosemode set in ctor, prints infix to postfix conversion process.
    /// </summary>
    public class InfixToPostfix : IInfixToPostfix
    {
        private string operators = "*/+-";

        public bool VerboseMode { get { return _verbose; } set { _verbose = value; } }
        private bool _verbose;

        public string Infix { get => _infix; set { _infix = value; Postfix = ""; } }    //TODO: Note setting infix resets postfix
        private string _infix = "";

        /// <summary>
        /// Postfix string property.
        /// If verbosemode set in ctor, setter prints when called
        /// </summary>
        public string Postfix
        {
            get { return _postfix; }
            set {
                _postfix = value;
                if (VerboseMode) { Console.WriteLine("{0, -20}{1}", "Building postfix: ", Postfix); }
                }
        }
        private string _postfix = "";


        #region Constructors
        public InfixToPostfix(string input = "")
        { Infix = input; }

        /// <summary>
        /// If verbosemode == true, prints infix to postfix conversion process
        /// </summary>
        /// <param name="input"></param>
        /// <param name="verbosemode">if true, prints infix to postfix conversion process</param>
        public InfixToPostfix(string input, bool verbosemode) : this(input)
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
            var _stack = VerboseMode ? new StackVerbose<string>() : new Stack_LinkedListBased<string>();
            //OLD: var tokens = infix.ToCharArray();   // Breaks with numbers >1 digit
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

            // Pop remaining stack items
            for (int i = _stack.Count; i > 0; i--)
            {
                Postfix += _stack.Pop() + " ";
            }
            Postfix.TrimEnd();
            Postfix += "\n";
            //OLD: _postfix = String.Join(" ", _postfix.ToCharArray()); // to space _postfix TODO: But Fucks Up e.g. 100 
            return Postfix;
        }

        /// <summary>
        /// Compares symbols: this_ to other. Returns true if precedence >= else false
        /// </summary>
        /// <param name="this_"></param>
        /// <param name="other"></param>
        /// <returns></returns>
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