/*! 
@file IGraph.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
@date August 08, 2013
@brief Graph Interface
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

An Interface for the Graph.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpForceDirectedGraph
{
    public interface IGraph
    {
        Node AddNode(Node iNode);
        Edge AddEdge(Edge iEdge);
        void CreateNodes(List<PhysicsData> iDataList);
        void CreateNodes(List<string> iNameList);
        void CreateEdges(List<Triple<string, string, PhysicsData>> iDataList);
        void CreateEdges(List<Pair<string, string>> iDataList);
        Node CreateNode(PhysicsData data);
        Node CreateNode(string name);
        Edge CreateEdge(Node iSource, Node iTarget, PhysicsData iData = null);
        Edge CreateEdge(string iSource, string iTarget, PhysicsData iData = null);
        List<Edge> GetEdges(Node iNode1, Node iNode2);
        void RemoveNode(Node iNode);
        void DetachNode(Node iNode);
        void RemoveEdge(Edge iEdge);
        void Merge(Graph iMergeGraph);
        void FilterNodes(Predicate<Node> match);
        void FilterEdges(Predicate<Edge> match);
        void AddGraphListener(IGraphEventListener iListener);

        List<Node> nodes
        {
            get;
        }

        List<Edge> edges
        {
            get;
        }
    }
    public interface IGraphEventListener
    {
        void GraphChanged();
    }
}
