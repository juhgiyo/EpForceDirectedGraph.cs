/*! 
@file Vector2.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
@date August 08, 2013
@brief Vector2 Interface
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

An Interface for the Vector2 Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpForceDirectedGraph
{
    public class Vector2:AbstractVector
    {
        public Vector2()
            : base()
        {
            x = 0.0f;
            y = 0.0f;
            z = 0.0f;
        }
        public Vector2(float iX, float iY):base()
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
            Vector2 p = obj as Vector2;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (x == p.x) && (y==p.y);
        }

        public bool Equals(Vector2 p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (x == p.x) && (y == p.y);
        }

        public static bool operator ==(Vector2 a, Vector2 b)
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

        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !(a == b);
        }


        public override AbstractVector Add(AbstractVector v2)
        {
            Vector2 v22 = v2 as Vector2;
            x= x+v22.x;
            y=y+v22.y;
            return this;
        }

        public override AbstractVector Subtract(AbstractVector v2)
        {
            Vector2 v22 = v2 as Vector2;
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
            return new Vector2(y * -1.0f, x);
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
            return new Vector2(0.0f, 0.0f);
        }

        public static AbstractVector Identity()
        {
            return new Vector2(1.0f, 1.0f);
        }

        public static AbstractVector Random()
        {
            
            Vector2 retVec=new Vector2(10.0f * (Util.Random(random) - 0.5f), 10.0f * (Util.Random(random) - 0.5f));
            return retVec;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            Vector2 temp = new Vector2(a.x, a.y);
            temp.Add(b);
            return temp;
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            Vector2 temp = new Vector2(a.x, a.y);
            temp.Subtract(b);
            return temp;
        }
        public static Vector2 operator *(Vector2 a, float b)
        {
            Vector2 temp = new Vector2(a.x, a.y);
            temp.Multiply(b);
            return temp;
        }
        public static Vector2 operator *(float a, Vector2 b)
        {
            Vector2 temp = new Vector2(b.x, b.y);
            temp.Multiply(a);
            return temp;
        }

        public static Vector2 operator /(Vector2 a, float b)
        {
            Vector2 temp = new Vector2(a.x, a.y);
            temp.Divide(b);
            return temp;
        }
        public static Vector2 operator /(float a, Vector2 b)
        {
            Vector2 temp = new Vector2(b.x, b.y);
            temp.Divide(a);
            return temp;
        }

    }
}
