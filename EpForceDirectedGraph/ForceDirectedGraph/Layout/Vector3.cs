/*! 
@file Vector3.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
@date August 08, 2013
@brief Vector3 Interface
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

An Interface for the Vector3 Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpForceDirectedGraph
{
    public class Vector3:AbstractVector
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


        public Vector3(float iX, float iY, float iZ):base()
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
            Vector3 p = obj as Vector3;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (x == p.x) && (y == p.y) && (z == p.z);
        }

        public bool Equals(Vector3 p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

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


        public override AbstractVector Add(AbstractVector v2)
        {
            Vector3 v32 = v2 as Vector3;
            x = x + v32.x;
            y = y + v32.y;
            z = z + v32.z;
            return this;
        }

        public override AbstractVector Subtract(AbstractVector v2)
        {
            Vector3 v32 = v2 as Vector3;
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
            return new Vector3(0.0f, 0.0f,0.0f);
        }

        public static AbstractVector Identity()
        {
            return new Vector3(1.0f, 1.0f,1.0f);
        }

        public static AbstractVector Random()
        {
            return new Vector3(10.0f * (Util.Random(random) - 0.5f), 10.0f * (Util.Random(random) - 0.5f), 10.0f * (Util.Random(random) - 0.5f));
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            Vector3 temp = new Vector3(a.x, a.y, a.z);
            temp.Add(b);
            return temp;
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            Vector3 temp = new Vector3(a.x, a.y,a.z);
            temp.Subtract(b);
            return temp;
        }
        public static Vector3 operator *(Vector3 a, float b)
        {
            Vector3 temp = new Vector3(a.x, a.y,a.z);
            temp.Multiply(b);
            return temp;
        }
        public static Vector3 operator *(float a, Vector3 b)
        {
            Vector3 temp = new Vector3(b.x, b.y, b.z);
            temp.Multiply(a);
            return temp;
        }

        public static Vector3 operator /(Vector3 a, float b)
        {
            Vector3 temp = new Vector3(a.x, a.y,a.z);
            temp.Divide(b);
            return temp;
        }

    }
}
