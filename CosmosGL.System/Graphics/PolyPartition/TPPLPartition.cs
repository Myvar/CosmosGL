// ============================================================================================================
// PolyPartSharp: library for polygon partition and triangulation based on the PolyPartition C++ library 
// https://github.com/JamesK89/PolyPartSharp
// Original project: https://github.com/ivanfratric/polypartition
// ============================================================================================================
// Original work Copyright (C) 2011 by Ivan Fratric
// Derivative work Copyright (C) 2016 by James John Kelly Jr.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;

using tppl_float = System.Single;

namespace PolyPartition
{
    public class TPPLPartition
    {
        protected class PartitionVertex
        {
            public bool isActive;
            public bool isConvex;
            public bool isEar;

            public TPPLPoint p;
            public tppl_float angle;

            public PartitionVertex previous;
            public PartitionVertex next;
        }

        protected struct MonotoneVertex
        {
            public TPPLPoint p;
            public int previous;
            public int next;
        }

        protected class VertexSorter : IComparer<MonotoneVertex>
        {
            public List<MonotoneVertex> vertices;

            public int Compare(MonotoneVertex x, MonotoneVertex y)
            {
                throw new NotImplementedException();
            }
        }

        protected struct Diagonal
        {
            public int index1;
            public int index2;
        }

        protected struct DPState
        {
            public bool visible;
            public tppl_float weight;
            public int bestvertex;
        }

        protected struct ScanLineEdge
        {
            public int index;

            public TPPLPoint p1;
            public TPPLPoint p2;

            public static bool operator < (ScanLineEdge lhs, ScanLineEdge rhs)
            {
                if (rhs.p1.Y == rhs.p2.Y)
                {
                    if (lhs.p1.Y == lhs.p2.Y)
                    {
                        if (lhs.p1.Y < rhs.p1.Y) return true;
                        else return false;
                    }
                    if (IsConvex(lhs.p1, lhs.p2, rhs.p1)) return true;
                    else return false;
                }
                else if (lhs.p1.Y == lhs.p2.Y)
                {
                    if (IsConvex(rhs.p1, rhs.p2, lhs.p1)) return false;
                    else return true;
                }
                else if (lhs.p1.Y < rhs.p1.Y)
                {
                    if (IsConvex(rhs.p1, rhs.p2, lhs.p1)) return false;
                    else return true;
                }
                else
                {
                    if (IsConvex(lhs.p1, lhs.p2, rhs.p1)) return true;
                    else return false;
                }
            }
            
            public static bool operator > (ScanLineEdge lhs, ScanLineEdge rhs)
            {
                throw new NotImplementedException();
            }

            public static bool IsConvex(TPPLPoint p1, TPPLPoint p2, TPPLPoint p3)
            {
                tppl_float tmp;
                tmp = (p3.Y - p1.Y) * (p2.X - p1.X) - (p3.X - p1.X) * (p2.Y - p1.Y);

                if (tmp > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        protected bool IsConvex(TPPLPoint p1, TPPLPoint p2, TPPLPoint p3)
        {
            tppl_float tmp;
            tmp = (p3.Y - p1.Y) * (p2.X - p1.X) - (p3.X - p1.X) * (p2.Y - p1.Y);

            if (tmp > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool IsReflex(TPPLPoint p1, TPPLPoint p2, TPPLPoint p3)
        {
            tppl_float tmp;
            tmp = (p3.Y - p1.Y) * (p2.X - p1.X) - (p3.X - p1.X) * (p2.Y - p1.Y);

            if (tmp < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool IsInside(TPPLPoint p1, TPPLPoint p2, TPPLPoint p3, TPPLPoint p)
        {
            if (IsConvex(p1, p, p2)) return false;
            if (IsConvex(p2, p, p3)) return false;
            if (IsConvex(p3, p, p1)) return false;
            return true;
        }

        protected bool InCone(TPPLPoint p1, TPPLPoint p2, TPPLPoint p3, TPPLPoint p)
        {
            bool convex;

            convex = IsConvex(p1, p2, p3);

            if (convex)
            {
                if (!IsConvex(p1, p2, p)) return false;
                if (!IsConvex(p2, p3, p)) return false;
                return true;
            }
            else
            {
                if (IsConvex(p1, p2, p)) return true;
                if (IsConvex(p2, p3, p)) return true;
                return false;
            }
        }

        protected bool InCone(PartitionVertex v, TPPLPoint p)
        {
            TPPLPoint p1, p2, p3;

            p1 = v.previous.p;
            p2 = v.p;
            p3 = v.next.p;

            return InCone(p1, p2, p3, p);
        }

        protected int Intersects(TPPLPoint p11, TPPLPoint p12, TPPLPoint p21, TPPLPoint p22)
        {
            if ((p11.X == p21.X) && (p11.Y == p21.Y)) return 0;
            if ((p11.X == p22.X) && (p11.Y == p22.Y)) return 0;
            if ((p12.X == p21.X) && (p12.Y == p21.Y)) return 0;
            if ((p12.X == p22.X) && (p12.Y == p22.Y)) return 0;

            TPPLPoint v1ort = new TPPLPoint(), 
                      v2ort = new TPPLPoint(),
                      v = new TPPLPoint();

            tppl_float dot11, dot12, dot21, dot22;

            v1ort.X = p12.Y - p11.Y;
            v1ort.Y = p11.X - p12.X;

            v2ort.X = p22.Y - p21.Y;
            v2ort.Y = p21.X - p22.X;

            v = p21 - p11;
            dot21 = v.X * v1ort.X + v.Y * v1ort.Y;
            v = p22 - p11;
            dot22 = v.X * v1ort.X + v.Y * v1ort.Y;

            v = p11 - p21;
            dot11 = v.X * v2ort.X + v.Y * v2ort.Y;
            v = p12 - p21;
            dot12 = v.X * v2ort.X + v.Y * v2ort.Y;

            if (dot11 * dot12 > 0) return 0;
            if (dot21 * dot22 > 0) return 0;

            return 1;
        }

        TPPLPoint Normalize(TPPLPoint p)
        {
            TPPLPoint r = new TPPLPoint();
            tppl_float n = (tppl_float)Math.Sqrt(p.X * p.X + p.Y * p.Y);
            
            if (n != (tppl_float)0)
            {
                r = p / n;
            }
            else
            {
                r.X = 0;
                r.Y = 0;
            }

            return r;
        }

        protected tppl_float Distance(TPPLPoint p1, TPPLPoint p2)
        {
            tppl_float dx, dy;

            dx = p2.X - p1.X;
            dy = p2.Y - p1.Y;

            return (tppl_float)Math.Sqrt(dx * dx + dy * dy);
        }

        protected void UpdateVertexReflexity(PartitionVertex v)
        {
            PartitionVertex v1 = null, v3 = null;

            v1 = v.previous;
            v3 = v.next;

            v.isConvex = !IsReflex(v1.p, v.p, v3.p);
        }

        protected void UpdateVertex(PartitionVertex v, List<PartitionVertex> vertices)
        {
            int i;
            PartitionVertex v1 = null, v3 = null;
            TPPLPoint vec1, vec3;

            v1 = v.previous;
            v3 = v.next;

            v.isConvex = IsConvex(v1.p, v.p, v3.p);

            vec1 = Normalize(v1.p - v.p);
            vec3 = Normalize(v3.p - v.p);

            v.angle = vec1.X * vec3.X + vec1.Y * vec3.Y;

            if (v.isConvex)
            {
                v.isEar = true;

                int numvertices = vertices.Count;
                for (i = 0; i < numvertices; i++)
                {
                    if ((vertices[i].p.X == v.p.X) && (vertices[i].p.Y == v.p.Y)) continue;
                    if ((vertices[i].p.X == v1.p.X) && (vertices[i].p.Y == v1.p.Y)) continue;
                    if ((vertices[i].p.X == v3.p.X) && (vertices[i].p.Y == v3.p.Y)) continue;
                    if (IsInside(v1.p, v.p, v3.p, vertices[i].p))
                    {
                        v.isEar = false;
                        break;
                    }
                }
            }
            else
            {
                v.isEar = false;
            }
        }

        public int Triangulate_EC(TPPLPoly poly, List<TPPLPoly> triangles)
        {
            int numvertices;
            List<PartitionVertex> vertices = null;
            PartitionVertex ear = null;

            TPPLPoly triangle = new TPPLPoly();

            int i, j;
            bool earfound;

            if (poly.Count < 3) return 0;
            if (poly.Count == 3)
            {
                triangles.Add(poly);
                return 1;
            }

            numvertices = poly.Count;

            vertices = new List<PartitionVertex>(numvertices);
            
            for (i = 0; i < numvertices; i++)
            {
                vertices.Add(new PartitionVertex());
            }

            for (i = 0; i < numvertices; i++)
            {
                vertices[i].isActive = true;
                vertices[i].p = poly[i];
                if (i == (numvertices - 1)) vertices[i].next = vertices[0];
                else vertices[i].next = vertices[i + 1];
                if (i == 0) vertices[i].previous = vertices[numvertices - 1];
                else vertices[i].previous = vertices[i - 1];
            }

            for (i = 0; i < numvertices; i++)
            {
                UpdateVertex(vertices[i], vertices);
            }

            for (i = 0; i < numvertices - 3; i++)
            {
                earfound = false;
                //find the most extruded ear
                for (j = 0; j < numvertices; j++)
                {
                    if (!vertices[j].isActive) continue;
                    if (!vertices[j].isEar) continue;
                    if (!earfound)
                    {
                        earfound = true;
                        ear = vertices[j];
                    }
                    else
                    {
                        if (vertices[j].angle > ear.angle)
                        {
                            ear = vertices[j];
                        }
                    }
                }
                if (!earfound)
                {
                    vertices.Clear();
                    return 0;
                }

                triangle = new TPPLPoly(ear.previous.p, ear.p, ear.next.p);
                triangles.Add(triangle);

                ear.isActive = false;
                ear.previous.next = ear.next;
                ear.next.previous = ear.previous;

                if (i == numvertices - 4) break;

                UpdateVertex(ear.previous, vertices);
                UpdateVertex(ear.next, vertices);
            }

            for (i = 0; i < numvertices; i++)
            {
                if (vertices[i].isActive)
                {
                    triangle = new TPPLPoly(vertices[i].previous.p, vertices[i].p, vertices[i].next.p);
                    triangles.Add(triangle);
                    break;
                }
            }
            
            vertices.Clear();

            return 1;
        }
    }
}
