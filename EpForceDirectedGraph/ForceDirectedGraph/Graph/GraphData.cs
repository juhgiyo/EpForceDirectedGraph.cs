/*! 
@file PhysicsData.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
@date August 08, 2013
@brief PhysicsData Interface
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

An Interface for the PhysicsData Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpForceDirectedGraph
{
    public class NodeData : GraphData
    {
        public NodeData():base()
        {
            mass = 1.0f;
            initialPostion = null;
            origID = ""; // for merging the graph
        }
        public float mass
        {
            get;
            set;
        }

        public AbstractVector initialPostion
        {
            get;
            set;
        }
        public string origID
        {
            get;
            set;
        }

    }
    public class EdgeData:GraphData
    {
        public EdgeData():base()
        {
            length = 1.0f;
        }
        public float length
        {
            get;
            set;
        }


    }
    public class GraphData
    {
        public GraphData()
        {
            label = "";
        }


        public string label
        {
            get;
            set;
        }


    }
}
