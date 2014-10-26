/*! 
@file AbstractVector.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
@date August 08, 2013
@brief AbstractVector Interface
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

An Interface for the AbstractVector Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpForceDirectedGraph.cs
{
    public abstract class AbstractVector:IVector
    {

        public float x
        {
            get;
            set;
        }

        public float y
        {
            get;
            set;
        }

        public float z
        {
            get;
            set;
        }

        public AbstractVector()
        {
        }

        public abstract AbstractVector Add(AbstractVector v2);
        public abstract AbstractVector Subtract(AbstractVector v2);
        public abstract AbstractVector Multiply(float n);
        public abstract AbstractVector Divide(float n);
        public abstract float Magnitude();
        //public abstract public abstract AbstractVector Normal();
        public abstract AbstractVector Normalize();
        public abstract AbstractVector SetZero();
        public abstract AbstractVector SetIdentity();

        public static AbstractVector operator +(AbstractVector a, AbstractVector b)
        {
            if (a is FDGVector2 && b is FDGVector2)
                return (a as FDGVector2) + (b as FDGVector2);
            else if (a is FDGVector3 && b is FDGVector3)
                return (a as FDGVector3) + (b as FDGVector3);
            return null;
        }
        public static AbstractVector operator -(AbstractVector a, AbstractVector b)
        {
            if (a is FDGVector2 && b is FDGVector2)
                return (a as FDGVector2) - (b as FDGVector2);
            else if (a is FDGVector3 && b is FDGVector3)
                return (a as FDGVector3) - (b as FDGVector3);
            return null;
        }
        public static AbstractVector operator *(AbstractVector a, float b)
        {
            if (a is FDGVector2)
                return (a as FDGVector2) * b;
            else if (a is FDGVector3)
                return (a as FDGVector3) * b;
            return null;
        }
        public static AbstractVector operator *(float a, AbstractVector b)
        {
            if (b is FDGVector2)
                return a * (b as FDGVector2);
            else if (b is FDGVector3)
                return a * (b as FDGVector3);
            return null;
        }

        public static AbstractVector operator /(AbstractVector a, float b)
        {
            if (a is FDGVector2)
                return (a as FDGVector2) / b;
            else if (a is FDGVector3)
                return (a as FDGVector3) / b;
            return null;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(System.Object obj)
        {
            return this==(obj as AbstractVector);
        }
        public static bool operator ==(AbstractVector a, AbstractVector b)
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
            if (a is FDGVector2 && b is FDGVector2)
                return (a as FDGVector2) == (b as FDGVector2);
            else if (a is FDGVector3 && b is FDGVector3)
                return (a as FDGVector3) == (b as FDGVector3);
            return false;

        }

        public static bool operator !=(AbstractVector a, AbstractVector b)
        {
            return !(a == b);
        }



    }


}
