/*! 
@file Point.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
@date August 08, 2013
@brief Point Interface
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

An Interface for the Point Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpForceDirectedGraph
{
    public class Point
    {
        public Point(AbstractVector iPosition, AbstractVector iVelocity, AbstractVector iAcceleration,Node iNode)
        {
            position=iPosition;
            node = iNode;
            velocity = iVelocity;
            acceleration = iAcceleration;
        }

        public override int GetHashCode()
        {
            return position.GetHashCode();
        }
        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Point p = obj as Point;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return position==p.position;
        }

        public bool Equals(Point p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return position==p.position;
        }

        public static bool operator ==(Point a, Point b)
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
            return (a.position == b.position);
        }

        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        public void ApplyForce(AbstractVector force)
        {
            acceleration.Add(force/mass);
        }

        public AbstractVector position { get; set; }
        public Node node { get; private set; }
        public float mass
        {
            get
            {
                return node.Data.mass;
            }
        }
        public AbstractVector velocity { get; private set; }
        public AbstractVector acceleration { get; private set; }
     }
}
