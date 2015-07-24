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
            Polygon polygon = null;
            List<PolygonPoint> points = new List<PolygonPoint>();

            var vertexBufferGenerator = new BufferGenerator();
            var indexBufferGenerator = new BufferGenerator();
            int indexCount = 0;

            foreach (var path in shape.Paths)
            {
                
                foreach (var segment in path.Segments)
                {
                    if(segment is QuadraticCurveSegment)
                    {
                        var quadraticCurveSegment = segment as QuadraticCurveSegment;
                        if(path.CompositMode == CompositMode.Subtract)
                        {
                            points.Add(new PolygonPoint(quadraticCurveSegment.ControlPoint.X, quadraticCurveSegment.ControlPoint.Y));
                            points.Add(new PolygonPoint(quadraticCurveSegment.End.X, quadraticCurveSegment.End.Y));
                        }
                        else
                        {
                            points.Add(new PolygonPoint(quadraticCurveSegment.End.X, quadraticCurveSegment.End.Y));
                        }

                        float sign = quadraticCurveSegment.Convex ? 1f : -1f;

                        //write vertex, uv, sign
                        vertexBufferGenerator.WriteFloat((float)quadraticCurveSegment.Start.X, (float)quadraticCurveSegment.Start.Y, 0f, 1f, 0f, 0f, sign);
                        vertexBufferGenerator.WriteFloat((float)quadraticCurveSegment.End.X, (float)quadraticCurveSegment.End.Y, 0f, 1f, 1f, 1f, sign);
                        vertexBufferGenerator.WriteFloat((float)quadraticCurveSegment.ControlPoint.X, (float)quadraticCurveSegment.ControlPoint.Y, 0f, 1f, 0.5f, 0f, sign);
                        indexBufferGenerator.WriteUInt((uint)indexCount++, (uint)indexCount++, (uint)indexCount++);
                    }
                    else
                    {
                        points.Add(new PolygonPoint(segment.End.X, segment.End.Y));
                    }
                }

                if(polygon == null)
                {
                    polygon = new Polygon(points);
                }
                else
                {
                    if (path.CompositMode == CompositMode.Subtract)
                    {
                        polygon.AddHole(new Polygon(points));
                    }
                    else
                    {
                        polygon.AddPoints(points);
                    }
                }

                points.Clear();
            }
            
            P2T.CreateContext(TriangulationAlgorithm.DTSweep);
            P2T.Triangulate(polygon);
            
            foreach(var triangle in polygon.Triangles)
            {
                vertexBufferGenerator.WriteFloat((float)triangle.Points[0].X, (float)triangle.Points[0].Y, 0f, 1f, 0f, 1f, 1f);
                vertexBufferGenerator.WriteFloat((float)triangle.Points[1].X, (float)triangle.Points[1].Y, 0f, 1f, 0f, 1f, 1f);
                vertexBufferGenerator.WriteFloat((float)triangle.Points[2].X, (float)triangle.Points[2].Y, 0f, 1f, 0f, 1f, 1f);
                indexBufferGenerator.WriteUInt((uint)indexCount++, (uint)indexCount++, (uint)indexCount++);
            }

            var vertexBuffer = BufferFactory.Allocate(BufferTarget.ArrayBuffer, BufferUsageHint.StaticDraw, vertexBufferGenerator.GetBuffer());
            var indexBuffer = BufferFactory.Allocate(BufferTarget.ElementArrayBuffer, BufferUsageHint.StaticDraw, indexBufferGenerator.GetBuffer());

            return new VectorMesh(vertexBuffer, indexBuffer, indexCount);
        }

    }
}
