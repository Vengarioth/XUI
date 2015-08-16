using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XUI.TTF.Interpreter
{
    class Interpreter
    {
        private bool instructionsLoaded;
        private Dictionary<int, Instruction> instructionMap;

        public Interpreter()
        {
            instructionMap = new Dictionary<int, Instruction>();
        }

        private void EnsureInstructionsLoaded()
        {
            if (!instructionsLoaded)
                LoadInstructions();
        }

        private void LoadInstructions()
        {
            if (instructionsLoaded)
                return;

            var type = typeof(Instructions);
            foreach(var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public))
            {
                var parameter = method.GetParameters();
                if (parameter.Length != 3)
                    continue;

                if (parameter[0].ParameterType != typeof(int))
                    continue;
                if (parameter[1].ParameterType != typeof(InstructionStream))
                    continue;
                if (parameter[2].ParameterType != typeof(InstructionStack))
                    continue;

                if (method.ReturnType != typeof(void))
                    continue;

                if (method.GetCustomAttribute<OpCode>() == null && method.GetCustomAttribute<OpCodeRange>() == null)
                    continue;

                var flagsParameter = Expression.Parameter(typeof(int), "flags");
                var streamParameter = Expression.Parameter(typeof(InstructionStream), "stream");
                var stackParameter = Expression.Parameter(typeof(InstructionStack), "stack");
                
                var action = Expression.Lambda<Action<int, InstructionStream, InstructionStack>>(
                        Expression.Call(method, flagsParameter, streamParameter, stackParameter),
                        flagsParameter,
                        streamParameter,
                        stackParameter
                    ).Compile();
                
                foreach (var opCode in method.GetCustomAttributes<OpCode>())
                {
                    instructionMap.Add(opCode.Code, new Instruction(opCode.Code, action));
                }
                
                foreach (var opCodeRange in method.GetCustomAttributes<OpCodeRange>())
                {
                    var instruction = new Instruction(opCodeRange.From, action);

                    for(int i = opCodeRange.From; i <= opCodeRange.To; i++)
                    {
                        instructionMap.Add(i, instruction);
                    }
                }
            }

            instructionsLoaded = true;
        }

        public void Interpret(byte[] instructions)
        {
            EnsureInstructionsLoaded();

            var stack = new InstructionStack();
            var stream = new InstructionStream(instructions);

            while (stream.Position < stream.Length)
            {
                var opCode = (int)stream.ReadByte();
                instructionMap[opCode].Execute(opCode, stream, stack);
            }
        }
    }
}
