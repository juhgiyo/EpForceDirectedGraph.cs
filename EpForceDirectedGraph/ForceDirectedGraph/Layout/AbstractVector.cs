/*! 
@file AbstractVector.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
@date August 08, 2013
@brief AbstractVector Interface
@version 1.0

@section LICENSE

Copyright (C) 2013  Woong Gyu La <juhgiyo@gmail.com>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

@section DESCRIPTION

An Interface for the AbstractVector Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpForceDirectedGraph
{
    public abstract class AbstractVector:IVector
    {
        protected static Random random=new Random();

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
            if (a is Vector2 && b is Vector2)
                return (a as Vector2) + (b as Vector2);
            else if (a is Vector3 && b is Vector2)
                return (a as Vector3) + (b as Vector3);
            return null;
        }
        public static AbstractVector operator -(AbstractVector a, AbstractVector b)
        {
            if (a is Vector2 && b is Vector2)
                return (a as Vector2) - (b as Vector2);
            else if (a is Vector3 && b is Vector2)
                return (a as Vector3) - (b as Vector3);
            return null;
        }
        public static AbstractVector operator *(AbstractVector a, float b)
        {
            if (a is Vector2)
                return (a as Vector2) * b;
            else if (a is Vector3)
                return (a as Vector3) * b;
            return null;
        }
        public static AbstractVector operator *(float a, AbstractVector b)
        {
            if (b is Vector2)
                return a* (b as Vector2);
            else if (b is Vector3)
                return a* (b as Vector3);
            return null;
        }

        public static AbstractVector operator /(AbstractVector a, float b)
        {
            if (a is Vector2)
                return (a as Vector2) / b;
            else if (a is Vector3)
                return (a as Vector3) / b;
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
            if(a is Vector2 && b is Vector2)
                return (a as Vector2)==(b as Vector2);
            else if (a is Vector3 && b is Vector3)
                return (a as Vector3) == (b as Vector3);
            return false;

        }

        public static bool operator !=(AbstractVector a, AbstractVector b)
        {
            return !(a == b);
        }



    }


}
