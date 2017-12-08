using System;
using System.Text;

namespace InfixPostfixTranslator
{
    /// <summary>
    /// Tests for balanced parentheses: '(' and ')'
    /// Must be equal in number and in open-and-closing order
    /// </summary>
    public class MatchedParentheses
    {
        /// <summary>
        /// Returns true if '(' and ')' balance, else false
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsBalanced(string input)
        {
            Stack_LinkedListBased<char> stack = new Stack_LinkedListBased<char>();
            char[] tokens = input.ToCharArray();

            foreach (char c in tokens)
            {
                if (c == '(')
                { stack.Push(c); }
                else if (c == ')')
                {
                    if (stack.Count == 0)
                    { return false;  }
                    else { stack.Pop();  }
                }
            }
            if (stack.Count == 0)
            { return true; }
            else { return false; }
        }

        public static bool IsBalanced(string[] input)
        {
            return IsBalanced(String.Join(" ", input));
        }
    }

    class TestMatchedParentheses
    {
        
        public static void Test()
        {
            string pass1, pass2, pass3, fail1, fail2, fail3;
            pass1 = "()";
            pass2 = "((()))";
            pass3 = "()(()())";
            fail1 = "(";
            fail2 = ")";
            fail3 = "(()(())";
            string test1, test2, test3, test4;
            test1 = ""; // true
            test2 = "1 + 2*(3-4)/ (( 5 * 6) -(7=8))"; // true
            test3 = "1 + 2*(3-4)/ (( 5 * 6) -(7=8)))"; // false
            test4 = "))))))()"; // false
            string[] tests = new string[] { pass1, pass2, pass3, fail1, fail2, fail3, test1, test2, test3, test4 };

            foreach (var test in tests)
            { Console.WriteLine($"{test}: {MatchedParentheses.IsBalanced(test)}"); }

            foreach (var test in tests)
            { Console.WriteLine($"Testing array overload_ {test}: {MatchedParentheses.IsBalanced(test.Split())}"); }

        }
    
    }
}
