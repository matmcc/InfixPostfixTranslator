using System;
using System.Collections.Generic;

// TODO: Currently full of horrible verbose hacks - FIX THIS

// Built to resemble functionality and interface of Stack<T>
// Made generic as this would be useful in a collection class
// Recommended to use Stack<T> from library as it is safer, more functional, supported
// List-based stack has more functionality implemented to more closely match that in .NET framework...
// ...but both have necessary functionality for purposes of InfixToPostfix class.
// Should be a simple swap from GenStack<T> to Stack<T>
// This is to demonstrate learning

// Code/info from:
// Sharp, John; Microsoft Visual C# 2013; pp.381-7
// Michaelis, Mark & Lippert, Eric; Essential C# 6.0; pp.455-504
// MSDN .NET Framework Class Library - Stack<T> Class: https://msdn.microsoft.com/en-us/library/3278tedw(v=vs.110).aspx
// Linked-list-based stack: week 9 lecture (not publically accessible) ...
// ... https://canvas.anglia.ac.uk/courses/724/files/204804?module_item_id=96350
// Array-based stack: https://codereview.stackexchange.com/questions/106004/stack-implementation-in-c

// Both have similar Big-O cost
// TODO: Neaither currently implement interfaces.

// Stack_ArrayBased<T>
// Array-based stack
// Resize is expensive part
// Could configure this in ctor with crossover_size and ratio_to_grow

// Stack_LinkedListBased<T>
// SinglyLinkedList-based stack
// Implemented more functionality
// Does not have expense of array-resize
// Uncertain if Garbage Collector will deal less effectively with this than with array-based stack ...
// ... may hog memory (IDisposable not implemented)

namespace InfixPostfixTranslator
{
    public class Stack_ArrayBased<T>
    {
        private T[] _stack;
        private int _index;
        private int DEFAULT_SIZE = 5;   // Beware the magic number - TODO: There must be a better way
        
        public int StackSize { get { return _stack.Length; } }
        public int Count { get { return _index; } }

        public Stack_ArrayBased()
        {
            _stack = new T[DEFAULT_SIZE];
            _index = 0;
        }

        public Stack_ArrayBased(int stackSize)
        {
            if (stackSize < 0)
                throw new ArgumentOutOfRangeException("Requires a positive stack size");

            _stack = new T[stackSize];
            _index = 0;
        }
        
        public void Push(T value)
        {
            // Don't reallocate before we actually want to push to it
            if (_index == _stack.Length)
            {
                // Double for small stacks, and increase by 20% for larger stacks
                int oldLength = _stack.Length;
                Array.Resize(ref _stack, _stack.Length < 100
                                             ? 2 * _stack.Length
                                             : (int)(_stack.Length * 1.2)); // Magic numbers - could be set in ctor at top
            }

            // Store the value, and increase reference afterwards
            _stack[_index++] = value;
        }

        public T Pop()
        {
            if (_index == 0)
                throw new InvalidOperationException("The stack is empty");

            // Decrease the reference before fetching the value as
            // the reference points to the next free place
            T returnValue = _stack[--_index];

            // As a safety/security measure, reset value to a default value
            _stack[_index] = default(T);

            return returnValue;
        }

        public T Peek()
        {
            if (_index == 0)
                throw new InvalidOperationException("The stack is empty");

            // Look at value at _index - 1
            // Do not alter _index
            T returnValue = _stack[_index - 1];

            // Do not reset value to a default value, instead just return
            return returnValue;
        }
    }

    class Stack_LinkedListBased<T>
    {
        private class Node<T>
        {
            public Node<T> Next { get; set; }
            public T Data { get; set; }

            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }

        private Node<T> start, end;
        public int Count { get; set; }

        public Stack_LinkedListBased()
        {
            start = null;
            end = null;
            Count = 0;
        }

        public Stack_LinkedListBased(IEnumerable<T> collection)
        {
            if (collection == null)
            { throw new ArgumentNullException("collection is null"); }
            else
	        { 
                foreach (var item in collection)
                {
                    this.Push(item);
                } 
            }
        }

        public void Push(T data)
        {
            Node<T> current = new Node<T>(data);
            if (end == null)
            {
                start = current;
                end = current;
            }
            else
            {
                current.Next = start;
                start = current;
            }
            Count++;
        }
        
        public T Pop()
        {
            Node<T> current = start;
            if (start == null)
            { throw new InvalidOperationException("The stack is empty"); }
            else
            {
                start = current.Next;
                Count--;
                return current.Data;
            }
        }

        public T Peek()
        {
            if (start == null)
            { throw new InvalidOperationException("The stack is empty"); }
            else
            { return start.Data; }
        }

        public bool Contains(T item)
        {
            if (start == null)
            { throw new InvalidOperationException("The stack is empty"); }
            else
            {
                foreach (var _node in this)
                {
                    if (_node.Equals(item))
                    { return true; }
                }
                return false;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = start;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        public T[] ToArray()
        {
            T[] ret = new T[Count];
            if (start == null)
            { throw new InvalidOperationException("The stack is empty"); }
            else
            {
                var resetStart = start;
                var resetCount = this.Count;
                for (int i = 0; i < ret.Length; i++)
                {
                    ret[i] = this.Pop();
                }
                start = resetStart;
                this.Count = resetCount;
            }
            return ret;
        }
        
    }
    
}
