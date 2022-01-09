﻿using System.Collections.Generic;
using DotNetCommons.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCommons.Test.Collections
{
    [TestClass]
    public class CollectionExtensionsTest
    {
        private List<string> _list;
        private Dictionary<string, int> _dictI;
        private Dictionary<string, decimal> _dictD;

        [TestInitialize]
        public void Setup()
        {
            _list = new List<string> { "A", "AB", "C", "D" };
            _dictI = new Dictionary<string, int> { ["A"] = 1, ["B"] = 0 };
            _dictD = new Dictionary<string, decimal> { ["A"] = 1, ["B"] = 0 };
        }

        [TestMethod]
        public void TestAddIfNotNull()
        {
            var list = new List<string>();

            ((List<string>)null).AddIfNotNull("hello");

            list.AddIfNotNull("hello");
            list.AddIfNotNull("");
            list.AddIfNotNull(null);

            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void TestAddRangeIfNotNull()
        {
            var list = new List<string>();

            ((List<string>)null).AddRangeIfNotNull(null);
            ((List<string>)null).AddRangeIfNotNull(list);

            list.AddRangeIfNotNull(null);
            list.AddRangeIfNotNull(new[] { "hello", "", null });

            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void TestExtractAt()
        {
            var item = _list.ExtractAt(1);

            Assert.AreEqual(item, "AB");
            Assert.AreEqual("A,C,D", string.Join(",", _list));
        }

        [TestMethod]
        public void TestExtractAll()
        {
            var items = _list.ExtractAll(x => x.StartsWith("A"));

            Assert.AreEqual("A,AB", string.Join(",", items));
            Assert.AreEqual("C,D", string.Join(",", _list));
        }

        [TestMethod]
        public void TestExtractFirst()
        {
            Assert.AreEqual("A", _list.ExtractFirst());
        }

        [TestMethod]
        public void TestExtractLast()
        {
            Assert.AreEqual("D", _list.ExtractLast());
        }

        [TestMethod]
        public void TestGetOrDefault()
        {
            Assert.AreEqual(1, _dictI.GetValueOrDefault("A"));
            Assert.AreEqual(0, _dictI.GetValueOrDefault("B"));
            Assert.AreEqual(0, _dictI.GetValueOrDefault("C"));
        }

        [TestMethod]
        public void TestIncrementInteger()
        {
            Assert.AreEqual(2, _dictI.Increment("A"));
            Assert.AreEqual(2, _dictI.Increment("Z", 2));
        }

        [TestMethod]
        public void TestIncrementDecimal()
        {
            Assert.AreEqual(2.99M, _dictD.Increment("A", 1.99M));
            Assert.AreEqual(2.45M, _dictD.Increment("Z", 2.45M));
        }
    }
}
