using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.TTF
{
    public class TTFReader
    {
        public TrueTypeFont Read(Stream stream)
        {
            var little = BitConverter.IsLittleEndian;
            using (BinaryReader reader = new ByteOrderSwappingBinaryReader(stream))
            {
                var version = reader.ReadUInt32();
                var tableCount = reader.ReadUInt16();
                var searchRange = reader.ReadUInt16();
                var entrySelector = reader.ReadUInt16();
                var rangeShift = reader.ReadUInt16();

                var tables = new List<Table>(tableCount);

                for (int i = 0; i < tableCount; i++)
                {
                    var tag = reader.ReadUInt32();
                    var checkSum = reader.ReadUInt32();
                    var offset = reader.ReadUInt32();
                    var length = reader.ReadUInt32();

                    var bytes = BitConverter.GetBytes(tag);
                    Array.Reverse(bytes);
                    var name = Encoding.ASCII.GetString(bytes);

                    tables.Add(new Table(tag, name, checkSum, offset, length));
                }

                var header = ExtractAsHeader(reader, tables.FirstOrDefault(e => e.Name.ToLower() == "head"));
                var maxp = ExtractAsMaxp(reader, tables.FirstOrDefault(e => e.Name.ToLower() == "maxp"));
                var loca = ExtractAsLoca(reader, maxp.NumGlyphs, header.IndexToLocFormat, tables.FirstOrDefault(e => e.Name.ToLower() == "loca"));
                var glyf = ExtractAsGlyf(reader, maxp.NumGlyphs, loca, tables.FirstOrDefault(e => e.Name.ToLower() == "glyf"));

                return new TrueTypeFont(header, maxp, loca, glyf);
            }

        }
        
        private Glyf ExtractAsGlyf(BinaryReader reader, int numGlyphs, Loca loca, Table table)
        {
            //see: https://developer.apple.com/fonts/TrueType-Reference-Manual/RM06/Chap6glyf.html

            var glyphs = new Glyph[numGlyphs];

            for (int i = 0; i < numGlyphs; i++)
            {
                reader.BaseStream.Seek(table.Offset, SeekOrigin.Begin);
                reader.BaseStream.Seek(loca.Offsets[i], SeekOrigin.Current);
                var length = loca.Offsets[i + 1] - loca.Offsets[i];

                if (length > 0)
                {
                    var numberOfContours = reader.ReadInt16();
                    var xMin = reader.ReadInt16();
                    var yMin = reader.ReadInt16();
                    var xMax = reader.ReadInt16();
                    var yMax = reader.ReadInt16();
                    
                    if (numberOfContours >= 0)
                    {
                        //If the number of contours is positive or zero, it is a single glyph;
                        glyphs[i] = ReadSimpleGlyph(reader, numberOfContours, xMin, yMin, xMax, yMax);
                    }
                    else
                    {
                        //If the number of contours less than zero, the glyph is compound
                        glyphs[i] = ReadCompoundGlyph(reader, -numberOfContours, xMin, yMin, xMax, yMax);
                    }
                }
                else
                {
                    glyphs[i] = Glyph.Empty;
                }
            }

            return new Glyf(
                glyphs
            );
        }

        private Glyph ReadSimpleGlyph(BinaryReader reader, int numberOfContours, short xMin, short yMin, short xMax, short yMax)
        {
            var endPtsOfContours = new ushort[numberOfContours];
            for(int i = 0; i < numberOfContours; i++)
            {
                endPtsOfContours[i] = reader.ReadUInt16();
            }

            var instructionLength = reader.ReadUInt16();
            var instructions = reader.ReadBytes(instructionLength);

            var numberOfPoints = endPtsOfContours.Last() + 1;

            var flags = ReadGlyphFlags(reader, numberOfPoints);

            var xCoordinates = ReadGlyphCoordinates(reader, numberOfPoints, flags, GlyphFlag.XByte, GlyphFlag.XSignOrSame);
            var yCoordinates = ReadGlyphCoordinates(reader, numberOfPoints, flags, GlyphFlag.YByte, GlyphFlag.YSignOrSame);

            var points = new PointS[numberOfPoints];
            for(int i = 0; i < numberOfPoints; i++)
            {
                points[i] = new PointS(xCoordinates[i], yCoordinates[i]);
            }

            var bounds = new BoundsS(xMin, xMax, yMin, yMax);

            return new Glyph(points, flags, instructions, endPtsOfContours, bounds);
        }

        private Glyph ReadCompoundGlyph(BinaryReader reader, int numberOfContours, short xMin, short yMin, short xMax, short yMax)
        {
            // TODO: Parse composite glyphs
            return Glyph.Empty;
        }

        private short[] ReadGlyphCoordinates(BinaryReader reader, int numberOfPoints, GlyphFlag[] flags, GlyphFlag isByte, GlyphFlag signOrSame)
        {
            var coordinates = new short[numberOfPoints];
            int x = 0;
            for (int i = 0; i < numberOfPoints; i++)
            {
                int dx;
                if (flags[i].HasFlag(isByte))
                {
                    var b = reader.ReadByte();
                    dx = flags[i].HasFlag(signOrSame) ? b : -b;
                }
                else
                {
                    if (flags[i].HasFlag(signOrSame))
                    {
                        dx = 0;
                    }
                    else
                    {
                        dx = reader.ReadInt16();
                    }
                }
                x += dx;
                coordinates[i] = (short)x; // TODO: overflow?
            }

            return coordinates;
        }

        private GlyphFlag[] ReadGlyphFlags(BinaryReader reader, int numberOfPoints)
        {
            var flags = new GlyphFlag[numberOfPoints];

            int i = 0;
            int repeat = 0;
            GlyphFlag flag = (GlyphFlag)0;
            while(i < numberOfPoints)
            {
                if(repeat > 0)
                {
                    repeat--;
                }
                else
                {
                    flag = (GlyphFlag)reader.ReadByte();
                    if (flag.HasFlag(GlyphFlag.Repeat))
                    {
                        repeat = reader.ReadByte();
                    }
                }

                flags[i++] = flag;
            }

            return flags;
        }

        private Loca ExtractAsLoca(BinaryReader reader, int numGlyphs, short indexToLocFormat, Table table)
        {
            //see: https://developer.apple.com/fonts/TrueType-Reference-Manual/RM06/Chap6loca.html
            reader.BaseStream.Seek(table.Offset, SeekOrigin.Begin);

            int numOffsets = numGlyphs + 1;
            var offsets = new uint[numOffsets];

            if(indexToLocFormat == 0)
            {
                //short offsets
                for (int i = 0; i < numOffsets + 1; i++)
                {
                    offsets[i] = (uint)(reader.ReadUInt16() * 2);
                }
            }
            else if(indexToLocFormat == 1)
            {
                //long offsets
                for (int i = 0; i < numOffsets; i++)
                {
                    offsets[i] = reader.ReadUInt32();
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            return new Loca(
                offsets
            );
        }

        private Maxp ExtractAsMaxp(BinaryReader reader, Table table)
        {
            //see: https://developer.apple.com/fonts/TrueType-Reference-Manual/RM06/Chap6maxp.html
            reader.BaseStream.Seek(table.Offset, SeekOrigin.Begin);

            var version = reader.ReadUInt32();
            var numGlyphs = reader.ReadUInt16();
            var maxPoints = reader.ReadUInt16();
            var maxContours = reader.ReadUInt16();
            var maxComponentPoints = reader.ReadUInt16();
            var maxComponentContours = reader.ReadUInt16();
            var maxZones = reader.ReadUInt16();
            var maxTwilightPoints = reader.ReadUInt16();
            var maxStorage = reader.ReadUInt16();
            var maxFunctionDefs = reader.ReadUInt16();
            var maxInstructionDefs = reader.ReadUInt16();
            var maxStackElements = reader.ReadUInt16();
            var maxSizeOfInstructions = reader.ReadUInt16();
            var maxComponentElements = reader.ReadUInt16();
            var maxComponentDepth = reader.ReadUInt16();

            return new Maxp(
                version,
                numGlyphs,
                maxPoints,
                maxContours,
                maxComponentPoints,
                maxComponentContours,
                maxZones,
                maxTwilightPoints,
                maxStorage,
                maxFunctionDefs,
                maxInstructionDefs,
                maxStackElements,
                maxSizeOfInstructions,
                maxComponentElements,
                maxComponentDepth
            );
        }

        private void ExtractAsGlyf(BinaryReader reader, Table table)
        {
            //see: https://developer.apple.com/fonts/TrueType-Reference-Manual/RM06/Chap6glyf.html
            reader.BaseStream.Seek(table.Offset, SeekOrigin.Begin);


        }

        private Header ExtractAsHeader(BinaryReader reader, Table table)
        {
            //see: https://developer.apple.com/fonts/TrueType-Reference-Manual/RM06/Chap6head.html
            reader.BaseStream.Seek(table.Offset, SeekOrigin.Begin);

            var version = reader.ReadUInt32();
            var fontRevision = reader.ReadUInt32();
            var checkSumAdjustment = reader.ReadUInt32();
            var magicNumber = reader.ReadUInt32();

            if (magicNumber != 0x5F0F3CF5)
                throw new Exception("Magic Number does not match specification: " + magicNumber.ToString("x"));

            var flags = reader.ReadUInt16();
            var unitsPerEm = reader.ReadUInt16();
            var created = reader.ReadUInt64();
            var modified = reader.ReadUInt64();
            var xMin = reader.ReadInt16();
            var yMin = reader.ReadInt16();
            var xMax = reader.ReadInt16();
            var yMax = reader.ReadInt16();
            var macStyle = reader.ReadUInt16();
            var lowestRecPPEM = reader.ReadUInt16();
            var fontDirectionHint = reader.ReadInt16();
            var indexToLocFormat = reader.ReadInt16();
            var glyphDataFormat = reader.ReadInt16();

            return new Header(
                version,
                fontRevision,
                checkSumAdjustment,
                magicNumber,
                flags,
                unitsPerEm,
                created,
                modified,
                xMin,
                yMin,
                xMax,
                yMax,
                macStyle,
                lowestRecPPEM,
                fontDirectionHint,
                indexToLocFormat,
                glyphDataFormat
            );
        }
    }
}
