/*! 
@file Spring.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
@date August 08, 2013
@brief Spring Interface
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

An Interface for the Spring Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpForceDirectedGraph
{
    public class Spring
    {
        public Spring(Point iPoint1, Point iPoint2, float iLength, float iK)
        {
            point1 = iPoint1;
            point2 = iPoint2;
            Length = iLength;
            K = iK;
        }

        public Point point1
        {
            get;
            private set;
        }
        public Point point2
        {
            get;
            private set;
        }

        public float Length
        {
            get;
            private set;
        }

        public float K
        {
            get;
            private set;
        }
    }
}
