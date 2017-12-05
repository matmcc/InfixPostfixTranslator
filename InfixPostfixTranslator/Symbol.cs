using System;

namespace InfixPostfixTranslator
{
    class Symbol
    {
        private string _data;
        private bool _isSymbol;
        private string _symbols = "()*/+-";

        public string Data { get { return _data; } private set { IsSymbolSetter(value); _data = value; } }
        public bool IsSymbol { get { return _isSymbol; } }

        public Symbol()
        { Data = ""; }

        public Symbol(string data)
        { Data = data; }

        public override string ToString() => this.Data;
        
        private void IsSymbolSetter(string data)
        {
            _data = data;
            if (_symbols.Contains(data))
            { _isSymbol = true; }
            else { _isSymbol = false; }
        }

        public int CompareTo(object obj)
        {
            if (obj is Symbol)
            {
                Symbol that = (Symbol)obj;
                if (!that.IsSymbol)
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
