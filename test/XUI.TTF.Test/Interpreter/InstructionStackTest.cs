using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XUI.TTF.Interpreter;

namespace XUI.TTF.Test.Interpreter
{
    [TestClass]
    public class InstructionStackTest
    {
        [TestMethod]
        public void PushPopInt32()
        {
            var stack = new InstructionStack();

            byte value = 0x3;

            stack.PushAsInt32(value);

            Assert.AreEqual(value, stack.PopInt32());
        }
    }
}
