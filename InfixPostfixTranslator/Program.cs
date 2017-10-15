using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixPostfixTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            var _ = new Expression();
            _.getInfix();
            _.showInfix();
        }
    }
}
