/*! 
@file Renderer.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
@date August 08, 2013
@brief Renderer Interface
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

An Interface for the Renderer Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EpForceDirectedGraph
{
    class Renderer:AbstractRenderer
    {
        ForceDirectedGraph form;
        public Renderer(ForceDirectedGraph iForm,IForceDirected iForceDirected)
            : base(iForceDirected)
        {
            form = iForm;
        }

        public override void Clear()
        {

        }

        protected override void DrawEdge(Edge iEdge, AbstractVector iPosition1, AbstractVector iPosition2)
        {
            //TODO: Change positions of line
            form.DrawLine(iEdge);
        }

        protected override void DrawNode(Node iNode, AbstractVector iPosition)
        {
            //TODO: Change positions of line
            form.DrawBox(iNode);
        }


    }
}
