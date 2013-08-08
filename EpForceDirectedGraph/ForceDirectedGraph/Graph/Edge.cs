/*! 
@file Edge.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
@date August 08, 2013
@brief Edge Interface
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

An Interface for the Graph Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpForceDirectedGraph
{
    public class Edge
    {
        public Edge(string iId, Node iSource, Node iTarget, PhysicsData iData)
        {
            ID = iId;
            Source = iSource;
            Target = iTarget;
            Data = (iData!=null)? iData : new PhysicsData();
            Directed = false;
        }

        public string ID
        {
            get;
            private set;
        }
        public PhysicsData Data
        {
            get;
            private set;
        }

        public Node Source
        {
            get;
            private set;
        }

        public Node Target
        {
            get;
            private set;
        }

        public bool Directed
        {
            get;
            set;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Edge p = obj as Edge;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (ID == p.ID);
        }

        public bool Equals(Edge p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (ID == p.ID);
        }

        public static bool operator ==(Edge a, Edge b)
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
            return a.ID == b.ID;
        }

        public static bool operator !=(Edge a, Edge b)
        {
            return !(a == b);
        }
    }
}
