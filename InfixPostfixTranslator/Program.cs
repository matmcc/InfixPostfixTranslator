using System;
using System.Text.RegularExpressions;

// OLD COMMENTS - for write-up
//  TODO: Keep in separate files?
//  TODO: Resolve issue re: symbols in Expression class constructor

// CHECKLIST
//  TODO: Is everything as private as possible?
//  TODO: Are scopes as minimal as is practical?
//  TODO: Are all control-flow designed to exit early in the best way? i.e. most likely bool-test first?

// JOBS:
//  Separating out Expression and UserInterface would make this less fragile/linked
//  ... e.g. enable re-run, changes to one without other
//  ... but expression includes some verification which should belong there to work with ctor
//  Could refactor Regex out into methods so that this is more easily changed if required ...
//  ... this could help with ...
//  Creating a class that contains a symbol, and knows it's type, could be an alternative to using strings
//  ... e.g. symbol.data = "*"; symbol.type = "operator"; ... or data = "("; type = "("

//  TODO: Verbose mode for stack creation ? Using events ?
//  TODO: Does expression need refactoring into UserInput and Expression ?
//  TODO: Build RunAgain into UserInput ?
//  TODO: Redesign InfixToPostfix.Precedence ? ... Considered IComparable but this implies sortable - do not want
//  TODO: Interfaces?

//  TODO: Learn how to, then write unit tests
//  TODO: Learn how to, then write xml tagged comments/documentation

//  TODO: Create class diagrams


namespace InfixPostfixTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestRun();
            TestRunOO();
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

        //public static void TestRun()
        //{
        //    var expressionInstance = new Expression();
        //    expressionInstance.GetInfix();
        //    Console.WriteLine("The Infix you have entered is :");
        //    expressionInstance.ShowInfix();
        //    Console.WriteLine("The Postfix equivalent is :");
        //    var returnedPostfix = expressionInstance.ReturnInfix();
        //    expressionInstance.ConvertToPostfix(returnedPostfix);
        //    expressionInstance.ShowPostfix();
        //}

        public static void TestRunOO()
        {
            var UiInstance = new UserInterface();
            UiInstance.Run();
            //UiInstance.GetInfix();
            //string userInput = UiInstance.Infix;
            //var expressionOO = new ExpressionOO(userInput);
            //Console.WriteLine("\nThe Infix you have entered is :");
            //expressionOO.ShowInfix();
            //var converter = new InfixToPostfixOO(expressionOO.Infix, true);
            //converter.Convert();
            //Console.WriteLine("\nThe Postfix equivalent is :");
            //Console.WriteLine(converter._Postfix);
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
