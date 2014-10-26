/*! 
@file IGraph.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
@date August 08, 2013
@brief Graph Interface
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

An Interface for the Graph.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpForceDirectedGraph.cs
{
    public interface IGraph
    {
        void Clear();
        Node AddNode(Node iNode);
        Edge AddEdge(Edge iEdge);
        void CreateNodes(List<NodeData> iDataList);
        void CreateNodes(List<string> iNameList);
        void CreateEdges(List<Triple<string, string, EdgeData>> iDataList);
        void CreateEdges(List<Pair<string, string>> iDataList);
        Node CreateNode(NodeData data);
        Node CreateNode(string name);
        Edge CreateEdge(Node iSource, Node iTarget, EdgeData iData = null);
        Edge CreateEdge(string iSource, string iTarget, EdgeData iData = null);
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
