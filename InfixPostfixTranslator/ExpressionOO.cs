using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InfixPostfixTranslator
{
    public class ExpressionOO
    {
#region Properties
        /// <summary>
        /// Infix string property
        /// Setter verifies input and cleans up whitespace
        /// </summary>
        public string Infix { get { return _infix; }
            set { if (VerifyInput(value)) { _infix = CleanInput(value); } } }
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
#endregion

#region Constructors
        public ExpressionOO()
        { this.Infix = ""; }

        public ExpressionOO(string infix)
        {
            if (VerifyInput(infix))
            { this.Infix = CleanInput(infix); }
            else { this.Infix = ""; }
        }

        public ExpressionOO(string[] infix)
        {
            if (VerifyInput(infix))
            { this.Infix = CleanInput(infix); }
            else { this.Infix = ""; }
        }
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
            bool _allCharsAllowed = false;
            foreach (var c in input)
            {
                // Currently allows letters, numbers, whitespace, AllowedSymbols as input
                if (Regex.IsMatch(c, @"(\w+)|(\s+)") || AllowedSymbols.Contains(c))
                { _allCharsAllowed = true; }
                else
                {
                    Console.WriteLine($"Invalid input: {c}. Only letters, numbers and symbols: {AllowedSymbols} are valid input");
                    return false;
                }
            }
            return _allCharsAllowed;
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


        //TODO: Fix separate Converter methods into one
        /// <summary>
        /// Creates and calls instance of InfixToPostfixOO.Convert()
        /// Used to pass Infix prop to converter and then update Postfix prop
        /// </summary>
        /// <param name="verbosemode">if verbosemode == true, infix to postfix conversion process is printed to console</param>
        public void ConvertToPostfix(bool verbosemode = false)
        {
            InfixToPostfixOO converter = new InfixToPostfixOO(Infix, verbosemode);
            Postfix = converter.Convert();
        }

        /// <summary>
        /// Creates and calls instance of InfixToPostfix.Convert()
        /// Used to pass Infix prop to converter and then update Postfix prop
        /// </summary>
        /// <param name="verbosemode">if verbosemode == true, infix to postfix conversion process is printed to console</param>
        public void ConvertToPostfix2(bool verbosemode = false)
        {
            InfixToPostfix converter = new InfixToPostfix(Infix, verbosemode);
            Postfix = converter.Convert();
        }
#endregion

    }
}
