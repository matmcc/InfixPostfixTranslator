using System;

namespace InfixPostfixTranslator
{
    /// <summary>
    /// Symbol class: Contains string Data which sets IsSymbol bool true if in '()*/+-'
    /// </summary>
    /// <remarks>
    /// Built to enable a more OOP approach to InfixToPostfix conversion.
    /// Data contains a string - if this is in "()*/+-" then sets IsSymbol = true.
    /// IsSymbol can be used to update other code on Data contents - for conversion/stack algorithm.
    /// CompareTo implemented and comparison operators overloaded for use in InfixToPostfix conversion algorithm
    /// ... in the style ofIComparable Interface - but this is not implemented as this implies objects are sortable...
    /// ... which they are not. CompareTo will throw exception if either Symbol.IsSymbol != true
    /// ToString overloaded to make code in InfixToPostfix easier to follow.
    /// Note: Overriding == and != but not .Equals() or .GetHashCode() causes a compiler notice
    /// Warning: IsSymbolSetter tests if symbol by testing if "()*/+-".Contains(input) ...
    /// ... this assumes input is one char long, but would still be true for e.g. "*/"
    /// </remarks>
    /// 
    public class Symbol
    {
        private string _symbols = "()*/+-";

        /// <summary>
        /// Data string property. Setter also sets IsSymbol bool
        /// </summary>
        public string Data { get { return _data; } private set { IsSymbolSetter(value); _data = value; } }
        private string _data;

        public bool IsSymbol { get { return _isSymbol; } }
        private bool _isSymbol;

        public Symbol()
        { Data = ""; }

        public Symbol(string data)
        { Data = data; }

        public override string ToString() => this.Data; // override ToString to make code easier to read in InfixToPostfix
        
        private void IsSymbolSetter(string data)
        {
            if (data == "") {_isSymbol = false; }
            else if (_symbols.Contains(data)) { _isSymbol = true; } // Beware e.g. ")*/" == true
            else { _isSymbol = false; }
        }

        /// <summary>
        /// Compares other object to this. If gt return 1, if == return 0, if lt return -1
        /// </summary>
        /// <exception cref="InvalidOperationException">other object must be IsSymbol == true</exception>
        /// <exception cref="ArgumentException">other object mst be type Symbol</exception>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is Symbol)
            {
                Symbol that = (Symbol)obj;
                if (!that.IsSymbol || !this.IsSymbol)   // only compare for _symbols
                { throw new InvalidOperationException("Comparison is only valid if (Symbol)Object.IsSymbol == true"); }
                if (this.Data == that.Data)
                { return 0; }
                if (this.Data == "*" || this.Data == "/")
                {
                    if (that.Data == "*" || that.Data == "/")
                    { return 0; }
                    else { return 1; }
                }
                else if (that.Data == "*" || that.Data == "/")
                { return -1; }
                else { return 0; }
            }
            else
            {
                throw new ArgumentException("Object is not a Symbol");
            }
        }

        public static bool operator ==(Symbol SymbThis, Symbol SymbThat) => SymbThis.CompareTo(SymbThat) == 0;

        public static bool operator !=(Symbol SymbThis, Symbol SymbThat) => SymbThis.CompareTo(SymbThat) != 0;

        public static bool operator >(Symbol SymbThis, Symbol SymbThat) => SymbThis.CompareTo(SymbThat) > 0;

        public static bool operator <(Symbol SymbThis, Symbol SymbThat) => SymbThis.CompareTo(SymbThat) < 0;

        public static bool operator >=(Symbol SymbThis, Symbol SymbThat) => SymbThis.CompareTo(SymbThat) >= 0;

        public static bool operator <=(Symbol SymbThis, Symbol SymbThat) => SymbThis.CompareTo(SymbThat) <= 0;
        
    }
}
