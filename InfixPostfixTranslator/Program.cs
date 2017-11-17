using System;

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

namespace InfixPostfixTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            TestRun();
            //TestStack();

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
                new GenStack<int>(-500);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Hooray, couldn't create a negative sized stack");
            }

            var myStack = new GenStack<int>(3);

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
            //myStack.Pop(); myStack.Pop(); myStack.Pop();

            //Console.WriteLine(String.Format("My final write: pop={0}, size={1}", myStack.Pop(), myStack.StackSize));
            for (int i = myStack.Count; i > 0; i--)
            {
                Console.WriteLine(i + " : " + myStack.Pop());
            }
        }
    }
}
