using System;
using System.Text;

namespace InfixPostfixTranslator
{
    class StackVerbose<T> : Stack_LinkedListBased<T>
    {
        public override void Push(T data)
        {
            Console.WriteLine("{0, -20}{1}", "Pushing to stack: ", data);
            base.Push(data);
            Console.WriteLine(this);
        }

        public override T Pop()
        {
            Console.WriteLine(this);
            Console.WriteLine("{0, -20}{1}", "Popping from stack: ", base.Peek());
            return base.Pop();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0, -20}", "Stack: ");
            foreach (var c in this)
            { builder.Append($"[ {c} ] "); }
            return builder.ToString().Trim();
        }
    }
}
