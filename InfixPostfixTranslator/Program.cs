using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  TODO: Kepp in separate files?
//  TODO: Resolve issue re: symbols in Expression class constructor

//  TODO: Redesign InfixToPostfix.Precedence ?
//  TODO: Is everything as private as possible?
//  TODO: Are scopes as minimal as is practical?

//  TODO: Learn how to, then write unit tests
//  TODO: Learn how to, then write xml tagged comments/documentation

//  TODO: Is this structured as it should be?
//  TODO: Create class diagrams

//  TODO: Are all control-flow designed to exit early in the best way? i.e. most likely bool-test first?

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
