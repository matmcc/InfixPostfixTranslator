using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

//TODO: Change VerifyInput() to allow '.' ?
//... also consider noting other culture-specific separators, e.g. ','

namespace InfixPostfixTranslator
{
    public interface IInfixToPostfix
    {
        string Infix { get; set; }
        string Postfix { get; set; }
        bool VerboseMode { get; set; }
        string Convert();
        string Convert(string input);
    }

    public class Expression
    {
#region Properties
        /// <summary>
        /// Infix string property.
        /// Setter verifies input and cleans up whitespace.
        /// Setting Infix will set Postfix = "".
        /// </summary>
        public string Infix { get { return _infix; }
            set { if (VerifyInput(value)) { _infix = CleanInput(value); Postfix = ""; } } } // setting Infix resets Postfix = ""
        private string _infix = "";

        /// <summary>
        /// Postfix string property
        /// Updated by calling ConvertToPostfix()
        /// </summary>
        public string Postfix { get { return _postfix; } private set { _postfix = value; } }
        private string _postfix = "";

        /// <summary>
        /// Read-only AllowedSymbols string property: "()*/+-"
        /// </summary>
        public string AllowedSymbols { get { return _allowedSymbols; } }
        private string _allowedSymbols = "()*/+-";

        private IInfixToPostfix converter;
#endregion

#region Constructors
        public Expression()
        { this.converter = new InfixToPostfix(Infix); }

        public Expression(IInfixToPostfix converterObject)
        { this.converter = converterObject; }

        public Expression(string infix) : this()
        {
            if (VerifyInput(infix))
            { this.Infix = CleanInput(infix); }
            else { this.Infix = ""; }
        }

        public Expression(string[] infix) : this()
        {
            if (VerifyInput(infix))
            { this.Infix = CleanInput(infix); }
            else { this.Infix = ""; }
        }

        public Expression(IInfixToPostfix converterObject, string infix) : this(infix)
        { this.converter = converterObject; }

        public Expression(IInfixToPostfix converterObject, string[] infix) : this(infix)
        { this.converter = converterObject; }
        #endregion

        #region Methods
        /// <summary>
        /// Gets Infix expression from Console
        /// Verifies using VerifyInput() and cleans up whitespace
        /// </summary>
        public void GetInfix()
        {
            string input = "";
            do
            {
                Console.WriteLine("Enter infix expression: ");
                input = Console.ReadLine();
            } while (!VerifyInput(input)) ;

            Infix = CleanInput(input);
        }


        /// <summary>
        /// Verifies input string contains only letters, numbers, whitespace, allowed symbols
        /// </summary>
        /// <param name="input">string input to verify</param>
        /// <returns>True if verified</returns>
        public bool VerifyInput(string input)
        {
            bool checkMatchedParentheses = MatchedParentheses.IsBalanced(input);
            if (!checkMatchedParentheses)
            {
                Console.WriteLine("Invalid input: Parentheses: '(' and ')' must be balanced\n");
                return false;
            }

            bool _allCharsAllowed = false;
            foreach (var c in input)
            {
                // Currently allows letters, numbers, whitespace, AllowedSymbols as input
                if (Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c) || AllowedSymbols.Contains(c))
                { _allCharsAllowed = true; }
                else
                {
                    Console.WriteLine($"Invalid input: {c}\nOnly letters, numbers and symbols: {AllowedSymbols} are valid input");
                    return false;
                }
            }
            return _allCharsAllowed;
        }

        /// <summary>
        /// Verifies input string[] contains only letters, numbers, whitespace, allowed symbols
        /// </summary>
        /// <param name="input">string[] input to verify</param>
        /// <returns>True if verified</returns>
        private bool VerifyInput(string[] input)
        {
            return VerifyInput(String.Join(" ", input));
            //bool _allCharsAllowed = false;
            //foreach (var c in input)
            //{
            //    // Currently allows letters, numbers, whitespace, AllowedSymbols as input
            //    if (Regex.IsMatch(c, @"(\w+)|(\s+)") || AllowedSymbols.Contains(c))
            //    { _allCharsAllowed = true; }
            //    else
            //    {
            //        Console.WriteLine($"Invalid input: {c}. Only letters, numbers and symbols: {AllowedSymbols} are valid input");
            //        return false;
            //    }
            //}
            //return _allCharsAllowed;
        }


        /// <summary>
        /// Splits input at whitespace using Regex, returns string with " " between non-whitespace inputs
        /// </summary>
        /// <example>
        /// Fixes inconsistent user input so that "".Split() will separate out non-whitepsace parts of a string
        /// <c>CleanInput("1+2 * (3 / 4) + (  56 - 789)") == "1 + 2 * ( 3 / 4 ) + ( 56 - 789 )"</c>
        /// </example>
        /// <param name="input"></param>
        /// <returns></returns>
        private string CleanInput(string input)
        {
            string[] found = Regex.Split(input, @"(\W)");
            StringBuilder builder = new StringBuilder();
            foreach (var c in found)
            {
                if (!String.IsNullOrWhiteSpace(c))
                {
                    builder.Append(c).Append(" ");
                }
            }
            string cleanedString = builder.ToString().Trim();
            return cleanedString;
        }

        /// <summary>
        /// string[] overload to split input at whitespace using Regex, returns string with " " between non-whitespace inputs
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string CleanInput(string[] input)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var c in input)
            {
                if (!String.IsNullOrWhiteSpace(c))
                {
                    builder.Append(c).Append(" ");
                }
            }
            string cleanedString = builder.ToString().Trim();
            return cleanedString;
        }


        /// <summary>
        /// Calls instance of converter object : IInfixToPostfix
        /// Used to pass Infix prop to converter and then update Postfix prop
        /// </summary>
        /// <param name="verbosemode">if verbosemode == true, infix to postfix conversion process is printed to console</param>
        public void ConvertToPostfix(IInfixToPostfix converterObj, bool verbosemode = false)
        {
            converterObj.VerboseMode = verbosemode;
            Postfix = converterObj.Convert(Infix);
        }

        /// <summary>
        /// Calls Convert() of internal converter object
        /// Used to pass Infix prop to converter and then update Postfix prop
        /// </summary>
        /// <param name="verbosemode">if verbosemode == true, infix to postfix conversion process is printed to console</param>
        public void ConvertToPostfix(bool verbosemode = false)
        {
            converter.VerboseMode = verbosemode;
            Postfix = converter.Convert(Infix);
        }
        
#endregion

    }
}
