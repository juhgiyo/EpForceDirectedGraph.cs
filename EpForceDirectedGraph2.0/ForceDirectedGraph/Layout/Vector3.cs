/*! 
@file Vector3.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
@date August 08, 2013
@brief Vector3 Interface
@version 1.0

@section LICENSE

The MIT License (MIT)

Copyright (c) 2013 Woong Gyu La <juhgiyo@gmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

@section DESCRIPTION

An Interface for the Vector3 Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using General;

namespace EpForceDirectedGraph
{
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;


        public Vector3(float iX, float iY, float iZ)
        {
            x = iX;
            y = iY;
            z = iZ;

        }

        public override int GetHashCode()
        {
            return (int)x^(int)y^(int)z;
        }
        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (!(obj is Vector3))
                return false;
            Vector3 p = (Vector3)obj;
            // Return true if the fields match:
            return (x == p.x) && (y == p.y) && (z == p.z);
        }

        public bool Equals(ref Vector3 p)
        {
            // Return true if the fields match:
            return (x == p.x) && (y == p.y) && (z == p.z);
        }

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return (a.x == b.x) && (a.y == b.y) && (a.z == b.z);
        }

        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return !(a == b);
        }


        public Vector3 Add( Vector3 v2)
        {
            Vector3 v32 = v2;
            x = x + v32.x;
            y = y + v32.y;
            z = z + v32.z;
            return this;
        }


        public Vector3 Subtract( Vector3 v2)
        {
            Vector3 v32 = v2;
            x = x - v32.x;
            y = y - v32.y;
            z = z - v32.z;
            return this;
        }

        public Vector3 Multiply(float n)
        {
            x=x*n;
            y=y*n;
            z = z * n;
            return this;
        }

        public Vector3 Divide(float n)
        {
            if (n==0.0f)
            {
                x=0.0f;
                y=0.0f;
                z = 0.0f;
            }
            else
            {
                x=x/n;
                y=y/n;
                z = z / n;
            }
            return this;
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt((double)(x*x) + (double)(y*y) +(double)(z*z));
        }


        public Vector3 Normalize()
        {
            return this/Magnitude();
        }

        public Vector3 SetZero()
        {
            x = 0.0f;
            y = 0.0f;
            z = 0.0f;
            return this;
        }
        public Vector3 SetIdentity()
        {
            x = 1.0f;
            y = 1.0f;
            z = 1.0f;
            return this;
        }
        public static Vector3 Zero()
        {
            return new Vector3(0.0f, 0.0f,0.0f);
        }

        public static Vector3 Identity()
        {
            return new Vector3(1.0f, 1.0f,1.0f);
        }

        public static Vector3 Random()
        {
            return new Vector3(10.0f * (Util.Random() - 0.5f), 10.0f * (Util.Random() - 0.5f), 10.0f * (Util.Random() - 0.5f));
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {

            return a.Add(b); ;
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {

            return a.Subtract(b); ;
        }
        public static Vector3 operator *(Vector3 a, float b)
        {

            return a.Multiply(b); ;
        }
        public static Vector3 operator *(float a, Vector3 b)
        {

            return b.Multiply(a); ;
        }

        public static Vector3 operator /(Vector3 a, float b)
        {

            return a.Divide(b); ;
        }

    }
}
