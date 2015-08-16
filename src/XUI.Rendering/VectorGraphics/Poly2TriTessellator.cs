using OpenTK.Graphics.OpenGL4;
using Poly2Tri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUI.Rendering.OpenGL;
using XUI.Rendering.VectorGraphics;
using XUI.Vector;

namespace XUI.Rendering.VectorGraphics
{
    class Poly2TriTessellator
    {

        public static VectorMesh TriangulateShape(Shape shape)
        {
            P2T.CreateContext(TriangulationAlgorithm.DTSweep);

            var vertexBufferGenerator = new BufferGenerator();
            var indexBufferGenerator = new BufferGenerator();
            int indexCount = 0;
            
            foreach(var path in shape.Paths)
            {
                AddPathToBuffers(path, vertexBufferGenerator, indexBufferGenerator, ref indexCount);
            }

            var vertexBuffer = BufferFactory.Allocate(BufferTarget.ArrayBuffer, BufferUsageHint.StaticDraw, vertexBufferGenerator.GetBuffer());
            var indexBuffer = BufferFactory.Allocate(BufferTarget.ElementArrayBuffer, BufferUsageHint.StaticDraw, indexBufferGenerator.GetBuffer());

            return new VectorMesh(vertexBuffer, indexBuffer, indexCount);
        }

        private static void AddPathToBuffers(Path path, BufferGenerator vertexBufferGenerator, BufferGenerator indexBufferGenerator, ref int indexCount)
        {
            List<PolygonPoint> points = new List<PolygonPoint>();

            float r;
            float g;
            float b;
            if (path.CompositMode == CompositMode.Subtract)
            {
                r = 1f;
                g = 0f;
                b = 0f;
            }
            else
            {
                r = 1f;
                g = 1f;
                b = 1f;
            }

            foreach (var segment in path.Segments)
            {
                if (segment is QuadraticCurveSegment)
                {
                    var quadraticCurveSegment = segment as QuadraticCurveSegment;
                    points.Add(new PolygonPoint(quadraticCurveSegment.End.X, quadraticCurveSegment.End.Y));
                    
                    float sign = quadraticCurveSegment.Convex ? 1f : -1f;
                    
                    AddVertex(vertexBufferGenerator, (float)quadraticCurveSegment.Start.X, (float)quadraticCurveSegment.Start.Y, 0f, 0f, r, g, b, sign);
                    AddVertex(vertexBufferGenerator, (float)quadraticCurveSegment.End.X, (float)quadraticCurveSegment.End.Y, 1f, 1f, r, g, b, sign);
                    AddVertex(vertexBufferGenerator, (float)quadraticCurveSegment.ControlPoint.X, (float)quadraticCurveSegment.ControlPoint.Y, 0.5f, 0f, r, g, b, sign);
                    
                    indexBufferGenerator.WriteUInt((uint)indexCount++, (uint)indexCount++, (uint)indexCount++);
                }
                else if(segment is LineSegment)
                {
                    var lineSegment = segment as LineSegment;
                    points.Add(new PolygonPoint(lineSegment.End.X, lineSegment.End.Y));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            var polygon = new Polygon(points);

            try
            {
                P2T.Triangulate(polygon);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            foreach (var triangle in polygon.Triangles)
            {
                AddVertex(vertexBufferGenerator, (float)triangle.Points[0].X, (float)triangle.Points[0].Y, 0f, 1f, r, g, b, 1f);
                AddVertex(vertexBufferGenerator, (float)triangle.Points[1].X, (float)triangle.Points[1].Y, 0f, 1f, r, g, b, 1f);
                AddVertex(vertexBufferGenerator, (float)triangle.Points[2].X, (float)triangle.Points[2].Y, 0f, 1f, r, g, b, 1f);
                indexBufferGenerator.WriteUInt((uint)indexCount++, (uint)indexCount++, (uint)indexCount++);
            }
        }

        private static void AddVertex(BufferGenerator vertexBufferGenerator, float x, float y, float u, float v, float r, float g, float b, float sign)
        {
            vertexBufferGenerator.WriteFloat(x, y, u, v, r, g, b, sign);
        }
    }
}
