using System;
using System.Collections;
using System.Collections.Generic;

#region Comments
// Built to resemble functionality and interface of Stack<T>
// Made generic as this would be useful in a collection class
// Recommended to use Stack<T> from .NET library in general use - this is to demostrate learning
// Should be a simple swap from either class here to Stack<T>
// List-based stack has more functionality implemented to more closely match Stack<T> in .NET framework...
// ...but both have necessary functionality for purposes of InfixToPostfix class.
// Both have similar Big-O cost


// Code/info from:
// Sharp, John; Microsoft Visual C# 2013; pp.381-7
// Michaelis, Mark & Lippert, Eric; Essential C# 6.0; pp.455-504
// MSDN .NET Framework Class Library - Stack<T> Class: https://msdn.microsoft.com/en-us/library/3278tedw(v=vs.110).aspx
// Linked-list-based stack: week 9 lecture (not publically accessible) ...
// ... https://canvas.anglia.ac.uk/courses/724/files/204804?module_item_id=96350
// Array-based stack: https://codereview.stackexchange.com/questions/106004/stack-implementation-in-c


// Stack_LinkedListBased<T>
// SinglyLinkedList-based stack
// Implemented more functionality to achieve greater similarity with Stack<T>:
// Overloaded constructor, ToArray and CopyTo, iterator-based IEnumerable<T> ...
// ... but not Add(), Remove(), Clear(), IsReadOnly from ICollection<> - remove() does not fit with stack design.
// ... also not ICollection - no sync methods and CopyTo() is not explicitly implemented.
// Does not have expense of array-resize
// Uncertain if Garbage Collector will deal less effectively with this than with array-based stack ...

// Stack_ArrayBased<T>
// Array-based stack
// Resize is expensive part
// Could configure this in ctor with crossover_size and ratio_to_grow

#endregion

namespace InfixPostfixTranslator
{
    #region Stack_LinkedListBased
    /// <summary>
    /// Represents a variable size last-in-first-out (LIFO) collection of instances of the same specified type.
    /// Based on a singly-linked-list style collection of nodes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Stack_LinkedListBased<T> : IEnumerable<T>, IEnumerable
    {
        #region Node
        // Note: Encapsulating private Node<T> class inside Stack class causes Compiler notice about shared <T> name
        // ... could rename T in Node<T>
        /// <summary>
        /// Node object for a linked-list style collection
        /// Contains T Data field and Node<T> Next pointer to next node in structure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class Node<T>
        {
            public Node<T> Next { get; set; }
            public T Data { get; set; }

            /// <summary>
            /// Node object, contains Data field and Next pointer to next node in structure
            /// </summary>
            /// <param name="data"></param>
            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }
#endregion

        private Node<T> start, end;
        /// <summary>
        /// Gets the number of elements contained in the stack.
        /// </summary>
        public int Count { get; set; }

        #region Constructors
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
        #endregion

        #region Stack Methods
        /// <summary>
        /// Create and insert new node at top of stack
        /// </summary>
        /// <param name="data"></param>
        public virtual void Push(T data)
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
        
        /// <summary>
        /// Remove and return node.Data at top of stack
        /// </summary>
        /// <returns></returns>
        public virtual T Pop()
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

        /// <summary>
        /// Return Node.Data of node at top of stack without changing stack
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (start == null)
            { throw new InvalidOperationException("The stack is empty"); }
            else
            { return start.Data; }
        }
        #endregion

        #region IEnumerator
        /// <summary>
        /// Returns an enumerator for the stack
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = start;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        #region Contains, ToArray, CopyTo
        /// <summary>
        /// Determines whether an element is in the stack
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Copies the stack to a new array
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Copies the stack to an existing one-dimensional Array, starting at the specified array index.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (start == null)
            { throw new InvalidOperationException("The stack is empty"); }
            else
            {
                this.ToArray().CopyTo(array, arrayIndex);
            }
        }
#endregion
    }
    #endregion

#region Stack_ArrayBased
    /// <summary>
    /// Represents a variable size last-in-first-out (LIFO) collection of instances of the same specified type.
    /// Backed by an Array<T> that resizes as needed - init size can be set in ctor - resize most expensive operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Stack_ArrayBased<T>
    {
        private T[] _stack;
        private int _index;
        private int DEFAULT_SIZE = 50;   // Beware the magic number - TODO: There must be a better way

        public int StackSize { get { return _stack.Length; } }
        public int Count { get { return _index; } }

        #region Constructors
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
        #endregion

        #region Stack methods
        /// <summary>
        /// Inserts an object at the top of the Stack<T>
        /// </summary>
        /// <param name="value"></param>
        public void Push(T value)
        {
            // Check if array needs resizing
            if (_index == _stack.Length)
            {
                // Double for small stacks, and increase by 20% for larger stacks
                int oldLength = _stack.Length;
                Array.Resize(ref _stack, _stack.Length < 50
                                             ? 2 * _stack.Length
                                             : (int)(_stack.Length * 1.2)); // Magic numbers - could be set in constructor?
            }

            // Store the value, and increase reference afterwards
            _stack[_index++] = value;
        }

        /// <summary>
        /// Removes and returns the object at the top of the Stack<T>.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the object at the top of the Stack<T> without removing it.
        /// </summary>
        /// <returns></returns>
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
#endregion
    }
#endregion
}
