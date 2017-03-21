﻿using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using CommonNetTools.IO.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonNetTools.IO.Test.Parsers
{
    [TestClass]
    public class CsvParserTest
    {
        private CsvParser _parser;

        [TestInitialize]
        public void Setup()
        {
            _parser = new CsvParser();
        }

        [TestMethod]
        public void Test()
        {
            const string src = @",1, 2, abc, 'hello world',3,  'hello\' again' , 1  2 3";

            var result = _parser.Parse(Cvt(src));
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(",1,2,abc,hello world,3,hello\" again,1  2 3", string.Join(",", result[0]));
        }

        [TestMethod]
        public void TestEscape()
        {
            var result = _parser.Parse(Cvt(@"'\''"));
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("\"", string.Join(",", result[0]));
        }

        [TestMethod]
        public void TestReadCsvFile()
        {
            var data = Assembly.GetExecutingAssembly().GetManifestResourceStream(GetType(), "name-test.csv.gz");
            if (data == null)
                throw new FileNotFoundException("Unable to find resource");

            using (data)
            using (var gz = new GZipStream(data, CompressionMode.Decompress))
            using (var reader = new StreamReader(gz, Encoding.UTF8))
            {
                var text = reader.ReadToEnd();

                var t0 = DateTime.Now;
                var csv = _parser.Parse(text);
                Console.WriteLine((int)(DateTime.Now - t0).TotalMilliseconds + " ms");

                Assert.AreEqual(10002, csv.Count);
            }
        }

        private string Cvt(string text)
        {
            return text.Replace('\'', '"');
        }
    }
}
