using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixPostfixTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            var testRun = new Expression();
            testRun.GetInfix();
            Console.WriteLine("The Infix you have entered is :");
            testRun.ShowInfix();
            Console.WriteLine("The Postfix equivalent is :");
            testRun.ConvertToPostfix(testRun.ReturnInfix());
            testRun.ShowPostfix();
        }
    }
}
