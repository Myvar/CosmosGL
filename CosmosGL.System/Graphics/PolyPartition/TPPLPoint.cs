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

using tppl_float = System.Single;

namespace PolyPartition
{
    public sealed class TPPLPoint : ICloneable
    {
        public TPPLPoint(tppl_float x, tppl_float y, int id)
        {
            X = x; Y = y;
        }

        public TPPLPoint(tppl_float x, tppl_float y)
            : this(x, y, 0)
        {
        }

        public TPPLPoint(TPPLPoint src)
        {
            X = src.X;
            Y = src.Y;
            Id = src.Id;
        }

        public TPPLPoint()
            : this(0.0f, 0.0f, 0)
        {
        }

        public tppl_float X
        {
            get;
            set;
        }

        public tppl_float Y
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public static TPPLPoint operator + (TPPLPoint lhs, TPPLPoint rhs)
        {
            return new TPPLPoint(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static TPPLPoint operator + (TPPLPoint lhs, tppl_float rhs)
        {
            return new TPPLPoint(lhs.X + rhs, lhs.Y + rhs);
        }

        public static TPPLPoint operator - (TPPLPoint lhs, TPPLPoint rhs)
        {
            return new TPPLPoint(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public static TPPLPoint operator - (TPPLPoint lhs, tppl_float rhs)
        {
            return new TPPLPoint(lhs.X - rhs, lhs.Y - rhs);
        }

        public static TPPLPoint operator * (TPPLPoint lhs, TPPLPoint rhs)
        {
            return new TPPLPoint(lhs.X * rhs.X, lhs.Y * rhs.Y);
        }

        public static TPPLPoint operator * (TPPLPoint lhs, tppl_float rhs)
        {
            return new TPPLPoint(lhs.X * rhs, lhs.Y * rhs);
        }

        public static TPPLPoint operator / (TPPLPoint lhs, TPPLPoint rhs)
        {
            return new TPPLPoint(lhs.X / rhs.X, lhs.Y / rhs.Y);
        }

        public static TPPLPoint operator / (TPPLPoint lhs, tppl_float rhs)
        {
            return new TPPLPoint(lhs.X / rhs, lhs.Y / rhs);
        }

        public static bool operator == (TPPLPoint lhs, TPPLPoint rhs)
        {
            return (lhs.X == rhs.X) && (lhs.Y == rhs.Y);
        }

        public static bool operator != (TPPLPoint lhs, TPPLPoint rhs)
        {
            return !(lhs == rhs);
        }

        public object Clone()
        {
            return new TPPLPoint(X, Y, Id);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", X, Y, Id);
        }
    }
}
