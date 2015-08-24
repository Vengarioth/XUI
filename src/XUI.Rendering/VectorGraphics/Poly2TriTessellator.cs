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
                    AddQuadraticCurveSegment(quadraticCurveSegment, points, vertexBufferGenerator, indexBufferGenerator, ref indexCount, r, g, b);
                }
                else if(segment is LineSegment)
                {
                    var lineSegment = segment as LineSegment;
                    points.Add(new PolygonPoint(lineSegment.End.X, lineSegment.End.Y));

                    var ext = 0.01;
                    var normal = path.CompositMode == CompositMode.Subtract ? lineSegment.RightNormal : lineSegment.LeftNormal;
                    var p1 = (normal * ext) + lineSegment.Start;
                    var p2 = (normal * ext) + lineSegment.End;

                    AddVertex(vertexBufferGenerator, (float)lineSegment.Start.X, (float)lineSegment.Start.Y, 0f, 1f, r, g, b, 1f);
                    AddVertex(vertexBufferGenerator, (float)lineSegment.End.X, (float)lineSegment.End.Y, 0f, 1f, r, g, b, 1f);
                    AddVertex(vertexBufferGenerator, (float)p1.X, (float)p1.Y, 1f, 1f, r, g, b, 1f);
                    indexBufferGenerator.WriteUInt((uint)indexCount++, (uint)indexCount++, (uint)indexCount++);

                    AddVertex(vertexBufferGenerator, (float)lineSegment.Start.X, (float)lineSegment.Start.Y, 0f, 1f, r, g, b, 1f);
                    AddVertex(vertexBufferGenerator, (float)p1.X, (float)p1.Y, 0f, 0f, r, g, b, 1f);
                    AddVertex(vertexBufferGenerator, (float)p2.X, (float)p2.Y, 1f, 1f, r, g, b, 1f);
                    indexBufferGenerator.WriteUInt((uint)indexCount++, (uint)indexCount++, (uint)indexCount++);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            Polygon polygon;

            try
            {
                polygon = new Polygon(points);
                P2T.Triangulate(polygon);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return;
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

        private static void AddQuadraticCurveSegment(QuadraticCurveSegment quadraticCurveSegment, List<PolygonPoint> points, BufferGenerator vertexBufferGenerator, BufferGenerator indexBufferGenerator, ref int indexCount, float r, float g, float b, int subDiffs = 0)
        {
            if(subDiffs < 2)
            {
                var splits = quadraticCurveSegment.Split(0.5);
                AddQuadraticCurveSegment(splits[0], points, vertexBufferGenerator, indexBufferGenerator, ref indexCount, r, g, b, subDiffs + 1);
                AddQuadraticCurveSegment(splits[1], points, vertexBufferGenerator, indexBufferGenerator, ref indexCount, r, g, b, subDiffs + 1);
                return;
            }

            float sign;

            quadraticCurveSegment.Split(0.5);

            if (quadraticCurveSegment.Convex)
            {
                sign = 1f;
                points.Add(new PolygonPoint(quadraticCurveSegment.End.X, quadraticCurveSegment.End.Y));
            }
            else
            {
                sign = -1f;
                points.Add(new PolygonPoint(quadraticCurveSegment.ControlPoint.X, quadraticCurveSegment.ControlPoint.Y));
                points.Add(new PolygonPoint(quadraticCurveSegment.End.X, quadraticCurveSegment.End.Y));
            }

            AddVertex(vertexBufferGenerator, (float)quadraticCurveSegment.Start.X, (float)quadraticCurveSegment.Start.Y, 0f, 0f, r, g, b, sign);
            AddVertex(vertexBufferGenerator, (float)quadraticCurveSegment.End.X, (float)quadraticCurveSegment.End.Y, 1f, 1f, r, g, b, sign);
            AddVertex(vertexBufferGenerator, (float)quadraticCurveSegment.ControlPoint.X, (float)quadraticCurveSegment.ControlPoint.Y, 0.5f, 0f, r, g, b, sign);

            indexBufferGenerator.WriteUInt((uint)indexCount++, (uint)indexCount++, (uint)indexCount++);
        }
    }
}
