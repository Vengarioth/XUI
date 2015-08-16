using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using XUI.TTF.Interpreter;

namespace XUI.TTF.Test.Interpreter.InstructionTests
{
    [TestClass]
    public class NPUSHB
    {
        [TestMethod]
        public void PushOntoStack()
        {
            var streamData = new byte[] { 0x0b, 0x07, 0x01, 0x03, 0x04, 0x00, 0x03, 0x05, 0x05, 0x09, 0x04, 0x00 };
            var stream = new InstructionStream(streamData);
            var stack = new InstructionStack();
            var flags = 0;

            Instructions.NPUSHB(flags, stream, stack);

            Assert.AreEqual(0x00, stack.PopInt32());
            Assert.AreEqual(0x04, stack.PopInt32());
            Assert.AreEqual(0x09, stack.PopInt32());
            Assert.AreEqual(0x05, stack.PopInt32());
            Assert.AreEqual(0x05, stack.PopInt32());
            Assert.AreEqual(0x03, stack.PopInt32());
            Assert.AreEqual(0x00, stack.PopInt32());
            Assert.AreEqual(0x04, stack.PopInt32());
            Assert.AreEqual(0x03, stack.PopInt32());
            Assert.AreEqual(0x01, stack.PopInt32());
            Assert.AreEqual(0x07, stack.PopInt32());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void StreamTooShort()
        {
            var streamData = new byte[] { 0x2 };
            var stream = new InstructionStream(streamData);
            var stack = new InstructionStack();
            var flags = 0;

            Instructions.NPUSHB(flags, stream, stack);
        }
    }
}
