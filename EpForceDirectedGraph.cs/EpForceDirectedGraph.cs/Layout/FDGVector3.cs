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

namespace EpForceDirectedGraph.cs
{
    public class FDGVector3:AbstractVector
    {

        public FDGVector3()
            : base()
        {
            x = 0.0f;
            y = 0.0f;
            z = 0.0f;
        }

        public FDGVector3(float iX, float iY, float iZ)
            : base()
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
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            FDGVector3 p = obj as FDGVector3;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (x == p.x) && (y == p.y) && (z == p.z);
        }

        public bool Equals(FDGVector3 p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (x == p.x) && (y == p.y) && (z == p.z);
        }

        public static bool operator ==(FDGVector3 a, FDGVector3 b)
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

        public static bool operator !=(FDGVector3 a, FDGVector3 b)
        {
            return !(a == b);
        }


        public override AbstractVector Add(AbstractVector v2)
        {
            FDGVector3 v32 = v2 as FDGVector3;
            x = x + v32.x;
            y = y + v32.y;
            z = z + v32.z;
            return this;
        }

        public override AbstractVector Subtract(AbstractVector v2)
        {
            FDGVector3 v32 = v2 as FDGVector3;
            x = x - v32.x;
            y = y - v32.y;
            z = z - v32.z;
            return this;
        }

        public override AbstractVector Multiply(float n)
        {
            x=x*n;
            y=y*n;
            z = z * n;
            return this;
        }

        public override AbstractVector Divide(float n)
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

        public override float Magnitude()
        {
            return (float)Math.Sqrt((double)(x*x) + (double)(y*y) +(double)(z*z));
        }


        public override AbstractVector Normalize()
        {
            return this/Magnitude();
        }

        public override AbstractVector SetZero()
        {
            x = 0.0f;
            y = 0.0f;
            z = 0.0f;
            return this;
        }
        public override AbstractVector SetIdentity()
        {
            x = 1.0f;
            y = 1.0f;
            z = 1.0f;
            return this;
        }
        public static AbstractVector Zero()
        {
            return new FDGVector3(0.0f, 0.0f, 0.0f);
        }

        public static AbstractVector Identity()
        {
            return new FDGVector3(1.0f, 1.0f, 1.0f);
        }

        public static AbstractVector Random()
        {
            return new FDGVector3(10.0f * (Util.Random() - 0.5f), 10.0f * (Util.Random() - 0.5f), 10.0f * (Util.Random() - 0.5f));
        }

        public static FDGVector3 operator +(FDGVector3 a, FDGVector3 b)
        {
            FDGVector3 temp = new FDGVector3(a.x, a.y, a.z);
            temp.Add(b);
            return temp;
        }
        public static FDGVector3 operator -(FDGVector3 a, FDGVector3 b)
        {
            FDGVector3 temp = new FDGVector3(a.x, a.y, a.z);
            temp.Subtract(b);
            return temp;
        }
        public static FDGVector3 operator *(FDGVector3 a, float b)
        {
            FDGVector3 temp = new FDGVector3(a.x, a.y, a.z);
            temp.Multiply(b);
            return temp;
        }
        public static FDGVector3 operator *(float a, FDGVector3 b)
        {
            FDGVector3 temp = new FDGVector3(b.x, b.y, b.z);
            temp.Multiply(a);
            return temp;
        }

        public static FDGVector3 operator /(FDGVector3 a, float b)
        {
            FDGVector3 temp = new FDGVector3(a.x, a.y, a.z);
            temp.Divide(b);
            return temp;
        }

    }
}
