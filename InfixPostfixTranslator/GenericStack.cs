using System;

// Code from: https://codereview.stackexchange.com/questions/106004/stack-implementation-in-c
// Also: Sharp, John; Microsoft Visual C# 2013; pp.381-7

// Resize is expensive part
// Could configure this in ctor with crossover_size and ratio_to_grow

// Built to match interface of Stack<>
// Recommended to use Stack<> from library as it is safer and supported 
// Should be a simple swap from GenStack<> to Stack<>
// This is to demonstrate learning

namespace InfixPostfixTranslator
{
    public class GenStack<T>
    {
        private T[] _stack;
        private int _index;
        private int DEFAULT_SIZE = 5;   // Beware the magic number - TODO: There must be a better way

        public int StackSize { get { return _stack.Length; } }
        public int Count { get { return _index; } }

        public GenStack()
        {
            _stack = new T[DEFAULT_SIZE];
            _index = 0;
        }

        public GenStack(int stackSize)
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

            // Do no reset value to a default value, instead just return
            return returnValue;
        }
    }
}
