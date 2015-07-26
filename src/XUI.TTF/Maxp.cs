using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.TTF
{
    public class Maxp
    {
        public ushort MaxComponentContours { get; private set; }
        public ushort MaxComponentDepth { get; private set; }
        public ushort MaxComponentElements { get; private set; }
        public ushort MaxComponentPoints { get; private set; }
        public ushort MaxContours { get; private set; }
        public ushort MaxFunctionDefs { get; private set; }
        public ushort MaxInstructionDefs { get; private set; }
        public ushort MaxPoints { get; private set; }
        public ushort MaxSizeOfInstructions { get; private set; }
        public ushort MaxStackElements { get; private set; }
        public ushort MaxStorage { get; private set; }
        public ushort MaxTwilightPoints { get; private set; }
        public ushort MaxZones { get; private set; }
        public ushort NumGlyphs { get; private set; }
        public uint Version { get; private set; }

        public Maxp(uint version, ushort numGlyphs, ushort maxPoints, ushort maxContours, ushort maxComponentPoints, ushort maxComponentContours, ushort maxZones, ushort maxTwilightPoints, ushort maxStorage, ushort maxFunctionDefs, ushort maxInstructionDefs, ushort maxStackElements, ushort maxSizeOfInstructions, ushort maxComponentElements, ushort maxComponentDepth)
        {
            Version = version;
            NumGlyphs = numGlyphs;
            MaxPoints = maxPoints;
            MaxContours = maxContours;
            MaxComponentPoints = maxComponentPoints;
            MaxComponentContours = maxComponentContours;
            MaxZones = maxZones;
            MaxTwilightPoints = maxTwilightPoints;
            MaxStorage = maxStorage;
            MaxFunctionDefs = maxFunctionDefs;
            MaxInstructionDefs = maxInstructionDefs;
            MaxStackElements = maxStackElements;
            MaxSizeOfInstructions = maxSizeOfInstructions;
            MaxComponentElements = maxComponentElements;
            MaxComponentDepth = maxComponentDepth;
        }
    }
}
