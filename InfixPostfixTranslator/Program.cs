using System;
using System.Text.RegularExpressions;

//  TODO: Keep in separate files?
//  TODO: Resolve issue re: symbols in Expression class constructor

//  TODO: Redesign InfixToPostfix.Precedence ?
//  TODO: Is everything as private as possible?
//  TODO: Are scopes as minimal as is practical?

//  TODO: Learn how to, then write unit tests
//  TODO: Learn how to, then write xml tagged comments/documentation

//  TODO: Is this structured as it should be?
//  TODO: Create class diagrams

//  TODO: Are all control-flow designed to exit early in the best way? i.e. most likely bool-test first?

// TODO: Sort out verbose mode

// TODO: Parentheses checker

// TODO: Does expression need refactoring into UserInput and Expression ?

namespace InfixPostfixTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestRun();
            //TestStack();
            //TestStackLinked();
            //Testing();
            //TestMatchedParentheses.Test();
        }

        public static void Testing()
        {
            string test = "1 + 2 * (3 / 4) + (  56 - 789)";
            Console.WriteLine("initial string: " + test);
            string[] test_split = test.Split();
            foreach (var c in test_split)
            { Console.WriteLine(c); }
            Console.WriteLine();

            string test2 = "1 + 2 * (3 / 4) + (  56 - 789) /12 ";
            string[] parts = Regex.Split(test2, @"(\s+)|(-)|(\+)|(\*)|(/)|(\()|(\))");
            foreach (var c in parts)
            { Console.Write(c + '.'); }
            Console.WriteLine();

            // This is The One!
            string test3 = "1 + 2 * (3 / 4) + (  56 - 789) /12 ";
            string[] found = Regex.Split(test3, @"(\W)");
            foreach (var c in found)
            {
                if (!String.IsNullOrWhiteSpace(c))
                { Console.WriteLine(c); }
            }
        }

        public static void TestRun()
        {
            var expressionInstance = new Expression();
            expressionInstance.GetInfix();
            Console.WriteLine("The Infix you have entered is :");
            expressionInstance.ShowInfix();
            Console.WriteLine("The Postfix equivalent is :");
            var returnedPostfix = expressionInstance.ReturnInfix();
            expressionInstance.ConvertToPostfix(returnedPostfix);
            expressionInstance.ShowPostfix();
        }

        public static void TestStack()
        {

            try
            {
                new Stack_ArrayBased<int>(-500);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Hooray, couldn't create a negative sized stack");
            }

            var myStack = new Stack_ArrayBased<int>(3);

            try
            {
                myStack.Pop();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Hooray, it was empty and failed. :-)");
            }

            myStack.Push(1);
            myStack.Push(2);
            var popped = myStack.Pop();
            if (popped == 2)
            {
                Console.WriteLine("Yuhu... Found the value I pushed! :-D ");
            }

            myStack.Push(0);
            myStack.Push(3);
            myStack.Push(4);
            myStack.Push(5);
            Console.WriteLine(myStack.Peek());

            for (int i = myStack.Count; i > 0; i--)
            {
                Console.WriteLine(i + " : " + myStack.Pop());
            }
        }

        public static void TestStackLinked()
        {
            var myStack = new Stack_LinkedListBased<int>();

            try
            {
                myStack.Pop();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Hooray, it was empty and failed. :-)");
            }

            myStack.Push(1);
            myStack.Push(2);
            var popped = myStack.Pop();
            if (popped == 2)
            {
                Console.WriteLine("Yuhu... Found the value I pushed! :-D ");
            }

            myStack.Push(0);
            myStack.Push(3);
            myStack.Push(4);
            myStack.Push(5);
            Console.WriteLine($"Peeking at top value: {myStack.Peek()}");

            Console.WriteLine("Testing ToArray() ...");
            int[] returnedArray = myStack.ToArray();
            foreach (var item in returnedArray)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine("\nTesting IEnumerable ...");
            foreach (var item in myStack)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine("\nTesting Contains()...");
            var last = myStack.ToArray()[myStack.Count-1];
            Console.WriteLine($"Last item in stack: {last}");
            Console.WriteLine($"Stack contains {last}: {myStack.Contains(last)}");

            Console.WriteLine("\nPopopopopop ...");
            for (int i = myStack.Count; i > 0; i--)
            {
                Console.WriteLine(i + " : " + myStack.Pop());
            }

            try
            {
                var myStackNull = new Stack_LinkedListBased<string>(null);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Cannot pass null to constructor\n");
            }

            var myStack2 = new Stack_LinkedListBased<string>(new string[] { "alice", "bradley", "claire", "daniel", "edith" });
            foreach (var item in myStack2)
            { Console.WriteLine(item); }

        }
    }
}
