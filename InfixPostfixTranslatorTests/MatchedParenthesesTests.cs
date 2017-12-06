using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfixPostfixTranslator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixPostfixTranslator.Tests
{
    [TestClass()]
    public class MatchedParenthesesTests
    {
        [TestMethod()]
        public void IsBalancedTest_True()
        {
            // arrange
            string pass1 = "()", pass2 = "((()))", pass3 = "()(()())",
                pass4 = "", pass5 = "1 + 2*(3-4)/ (( 5 * 6) -(7=8))",
                pass6 = "{{[[()";   // only tests for balanced ()
            string[] tests = new string[] { pass1, pass2, pass3, pass4, pass5, pass6 };
            bool[] results = new bool[tests.Length];
            // act
            for (int i = 0; i < tests.Length; i++)
            { results[i] = MatchedParentheses.IsBalanced(tests[i]); }
            // assert
            foreach (var test_result in results)
                Assert.IsTrue(test_result);
        }

        [TestMethod()]
        public void IsBalancedTest_False()
        {
            // arrange
            string fail1 = "(", fail2 = ")", fail3 = "(()(())", fail4 = "))))))()",
                fail5 = "1 + 2*(3-4)/ (( 5 * 6) -(7=8)))", 
                fail6 = ")(";   // order important
            string[] tests = new string[] { fail1, fail2, fail3, fail4, fail5, fail6 };
            bool[] results = new bool[tests.Length];
            // act
            for (int i = 0; i < tests.Length; i++)
            { results[i] = MatchedParentheses.IsBalanced(tests[i]); }
            // assert
            foreach (var test_result in results)
                Assert.IsFalse(test_result);
        }
    }
}