/*! 
@file FDGVector2.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
@date August 08, 2013
@brief FDGVector2 Interface
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

An Interface for the FDGVector2 Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EpForceDirectedGraph.cs
{
    public class FDGVector2:AbstractVector
    {
        public FDGVector2()
            : base()
        {
            x = 0.0f;
            y = 0.0f;
            z = 0.0f;
        }
        public FDGVector2(float iX, float iY)
            : base()
        {
            x = iX;
            y = iY;
            z = 0.0f;

        }

        public override int GetHashCode()
        {
            return (int)x^(int)y;
        }
        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            FDGVector2 p = obj as FDGVector2;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (x == p.x) && (y==p.y);
        }

        public bool Equals(FDGVector2 p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (x == p.x) && (y == p.y);
        }

        public static bool operator ==(FDGVector2 a, FDGVector2 b)
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
            return (a.x == b.x) && (a.y == b.y);
        }

        public static bool operator !=(FDGVector2 a, FDGVector2 b)
        {
            return !(a == b);
        }


        public override AbstractVector Add(AbstractVector v2)
        {
            FDGVector2 v22 = v2 as FDGVector2;
            x= x+v22.x;
            y=y+v22.y;
            return this;
        }

        public override AbstractVector Subtract(AbstractVector v2)
        {
            FDGVector2 v22 = v2 as FDGVector2;
            x=x-v22.x;
            y=y-v22.y;
            return this;
        }

        public override AbstractVector Multiply(float n)
        {
            x=x*n;
            y=y*n;
            return this;
        }

        public override AbstractVector Divide(float n)
        {
            if (n==0.0f)
            {
                x=0.0f;
                y=0.0f;
            }
            else
            {
                x=x/n;
                y=y/n;
            }
            return this;
        }

        public override float Magnitude()
        {
            return (float)Math.Sqrt((double)(x*x) + (double)(y*y));
        }

        public AbstractVector Normal()
        {
            return new FDGVector2(y * -1.0f, x);
        }

        public override AbstractVector Normalize()
        {
            return this/Magnitude();
        }

        public override AbstractVector SetZero()
        {
            x = 0.0f;
            y = 0.0f;
            return this;
        }
        public override AbstractVector SetIdentity()
        {
            x = 1.0f;
            y = 1.0f;
            return this;
        }
        public static AbstractVector Zero()
        {
            return new FDGVector2(0.0f, 0.0f);
        }

        public static AbstractVector Identity()
        {
            return new FDGVector2(1.0f, 1.0f);
        }

        public static AbstractVector Random()
        {

            FDGVector2 retVec = new FDGVector2(10.0f * (Util.Random() - 0.5f), 10.0f * (Util.Random() - 0.5f));
            return retVec;
        }

        public static FDGVector2 operator +(FDGVector2 a, FDGVector2 b)
        {
            FDGVector2 temp = new FDGVector2(a.x, a.y);
            temp.Add(b);
            return temp;
        }
        public static FDGVector2 operator -(FDGVector2 a, FDGVector2 b)
        {
            FDGVector2 temp = new FDGVector2(a.x, a.y);
            temp.Subtract(b);
            return temp;
        }
        public static FDGVector2 operator *(FDGVector2 a, float b)
        {
            FDGVector2 temp = new FDGVector2(a.x, a.y);
            temp.Multiply(b);
            return temp;
        }
        public static FDGVector2 operator *(float a, FDGVector2 b)
        {
            FDGVector2 temp = new FDGVector2(b.x, b.y);
            temp.Multiply(a);
            return temp;
        }

        public static FDGVector2 operator /(FDGVector2 a, float b)
        {
            FDGVector2 temp = new FDGVector2(a.x, a.y);
            temp.Divide(b);
            return temp;
        }
        public static FDGVector2 operator /(float a, FDGVector2 b)
        {
            FDGVector2 temp = new FDGVector2(b.x, b.y);
            temp.Divide(a);
            return temp;
        }

    }
}
