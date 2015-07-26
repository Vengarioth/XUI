using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.TTF
{
    public class Table
    {
        public uint Tag { get; private set; }
        public string Name { get; private set; }
        public uint CheckSum { get; private set; }
        public uint Offset { get; private set; }
        public uint Length { get; private set; }
        
        public Table(uint tag, string name, uint checkSum, uint offset, uint length)
        {
            Tag = tag;
            Name = name;
            CheckSum = checkSum;
            Offset = offset;
            Length = length;
        }

        public string GetTagAsString()
        {
            var bytes = BitConverter.GetBytes(Tag);
            Array.Reverse(bytes);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
