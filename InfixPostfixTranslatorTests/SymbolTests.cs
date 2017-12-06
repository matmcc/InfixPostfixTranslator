using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfixPostfixTranslator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixPostfixTranslator.Tests
{
    [TestClass()]
    public class SymbolTests
    {
        [TestMethod()]
        public void SymbolTest_EmptyConstrutor()
        {
            // arrange
            Symbol empty = new Symbol();
            // act
            var actual = empty.Data;
            var isSymbol = empty.IsSymbol;
            // assert
            Assert.AreEqual(actual, "");
            Assert.IsFalse(isSymbol);
        }

        [TestMethod()]
        public void SymbolTest1_StringConstructor()
        {
            // arrange
            Symbol number = new Symbol("42");
            Symbol mult = new Symbol("*");
            // act
            var numberData = number.Data;
            var numIsSymbol = number.IsSymbol;
            var multData = mult.Data;
            var multIsSymbol = mult.IsSymbol;

            // assert
            Assert.AreEqual(numberData, "42");
            Assert.IsFalse(numIsSymbol);
            Assert.AreEqual(multData, "*");
            Assert.IsTrue(multIsSymbol);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            // arrange
            Symbol empty = new Symbol();
            Symbol number = new Symbol("42");
            Symbol mult = new Symbol("*");
            // act
            string emptyString = empty.ToString();
            string numberString = number.ToString();
            string multString = mult.ToString();
            // assert
            Assert.AreEqual(emptyString, "");
            Assert.AreEqual(numberString, "42");
            Assert.AreEqual(multString, "*");
        }

        [TestMethod()]
        public void CompareToTest()
        {
            var setA = "**//++--".ToArray();
            var setB = "*//*+--+".ToArray();
            var setC = "/-*+-*+/".ToArray();

            for (int i = 0; i < setA.Length; i++)
            {
                var a = new Symbol(setA[i].ToString());
                var b = new Symbol(setB[i].ToString());
                Assert.AreEqual(a.CompareTo(b), 0);
            }

            for (int i = 0; i < setA.Length; i += 2)
            {
                var a = new Symbol(setA[i].ToString());
                var c = new Symbol(setC[i].ToString());
                Assert.AreEqual(a.CompareTo(c), 0);
            }

            for (int i = 1; i < 4; i += 2)
            {
                var a = new Symbol(setA[i].ToString());
                var c = new Symbol(setC[i].ToString());
                Assert.AreEqual(a.CompareTo(c), 1);
            }

            for (int i = 5; i < setA.Length; i += 2)
            {
                var a = new Symbol(setA[i].ToString());
                var c = new Symbol(setC[i].ToString());
                Assert.AreEqual(a.CompareTo(c), -1);
            }
        }
    }
}