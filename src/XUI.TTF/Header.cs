using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.TTF
{
    public class Header
    {
        public uint Version { get; private set; }
        public uint FontRevision { get; private set; }
        public uint CheckSumAdjustment { get; private set; }
        public uint MagicNumber { get; private set; }
        public uint Flags { get; private set; }
        public uint UnitsPerEm { get; private set; }
        public ulong Created { get; private set; }
        public ulong Modified { get; private set; }
        public short XMin { get; private set; }
        public short YMin { get; private set; }
        public short XMax { get; private set; }
        public short YMax { get; private set; }
        public uint MacStyle { get; private set; }
        public uint LowestRecPPEM { get; private set; }
        public short FontDirectionHint { get; private set; }
        public short IndexToLocFormat { get; private set; }
        public short GlyphDataFormat { get; private set; }
        
        public Header(uint version, uint fontRevision, uint checkSumAdjustment, uint magicNumber, ushort flags, ushort unitsPerEm, ulong created, ulong modified, short xMin, short yMin, short xMax, short yMax, ushort macStyle, ushort lowestRecPPEM, short fontDirectionHint, short indexToLocFormat, short glyphDataFormat)
        {
            Version = version;
            FontRevision = fontRevision;
            CheckSumAdjustment = checkSumAdjustment;
            MagicNumber = magicNumber;
            Flags = flags;
            UnitsPerEm = unitsPerEm;
            Created = created;
            Modified = modified;
            XMin = xMin;
            YMin = yMin;
            XMax = xMax;
            YMax = yMax;
            MacStyle = macStyle;
            LowestRecPPEM = lowestRecPPEM;
            FontDirectionHint = fontDirectionHint;
            IndexToLocFormat = indexToLocFormat;
            GlyphDataFormat = glyphDataFormat;
        }
    }
}
