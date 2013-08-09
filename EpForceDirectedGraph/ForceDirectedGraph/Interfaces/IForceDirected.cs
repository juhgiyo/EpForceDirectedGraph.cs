/*! 
@file IForceDirected.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
@date August 08, 2013
@brief ForceDirected Interface
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

An Interface for the ForceDirected.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpForceDirectedGraph
{
    public delegate void EdgeAction(Edge edge, Spring spring);
    public delegate void NodeAction(Node edge, Point point);

    public interface IForceDirected
    {
        IGraph graph
        {
            get;
        }

        float Stiffness
        {
            get;
        }

        float Repulsion
        {
            get;
        }

        float Damping
        {
            get;
        }

        float Threadshold // NOT Using
        {
            get;
            set;
        }


        void Calculate(float iTimeStep);
        void EachEdge(EdgeAction del);
        void EachNode(NodeAction del);
        NearestPoint Nearest(AbstractVector position);
        BoundingBox GetBoundingBox();
    }
}
