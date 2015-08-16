using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XUI.TTF.Interpreter;
using System.IO;

namespace XUI.TTF.Test.Interpreter
{
    [TestClass]
    public class InterpreterTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //taken from a TTF file
            var streamData = new byte[] { 0x40, 0x0b, 0x07, 0x01, 0x03, 0x04, 0x00, 0x03, 0x05, 0x05, 0x09, 0x04, 0x00, 0x2f, 0xcd, 0x12, 0x39, 0x2f, 0xcd, 0x00, 0x2f, 0xcd, 0x3f, 0xcd, 0x31, 0x30 };
            
            var interpreter = new XUI.TTF.Interpreter.Interpreter();

            interpreter.Interpret(streamData);
        }
    }
}
