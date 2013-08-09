/*! 
@file ForceDirected.cs
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

An Interface for the ForceDirected Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace EpForceDirectedGraph
{

    public class NearestPoint{
            public NearestPoint()
            {
                node=null;
                point=null;
                distance=null;
            }
            public Node node;
            public Point point;
            public float? distance;
        }

    public class BoundingBox
    {
        public static float defaultBB= 2.0f;
        public static float defaultPadding = 0.07f; // ~5% padding
        
        public BoundingBox()
        {
            topRightBack = null;
            bottomLeftFront = null;
        }
        public AbstractVector topRightBack;
        public AbstractVector bottomLeftFront;
    }

    public abstract class ForceDirected<Vector> : IForceDirected where Vector : IVector
    {
    	public float Stiffness
        {
            get;
            set;
        }

        public float Repulsion
        {
            get;
            set;
        }

        public float Damping
        {
            get;
            set;
        }

        public float Threadshold
        {
            get;
            set;
        }

        public bool WithinThreashold
        {
            get;
            private set;
        }
        protected Dictionary<string, Point> nodePoints;
        protected Dictionary<string, Spring> edgeSprings;
        public IGraph graph
        {
            get;
            protected set;
        }

        public ForceDirected(IGraph iGraph, float iStiffness, float iRepulsion, float iDamping)
        {
            graph=iGraph;
            Stiffness=iStiffness;
            Repulsion=iRepulsion;
            Damping=iDamping;
            nodePoints = new Dictionary<string, Point>();
            edgeSprings = new Dictionary<string, Spring>();

            Threadshold = 0.01f;
        }

        protected abstract Point GetPoint(Node iNode);
        
        

        protected Spring GetSpring(Edge iEdge)
        {
            if(!(edgeSprings.ContainsKey(iEdge.ID)))
            {
                float length = iEdge.Data.length;
                Spring existingSpring = null;

                List<Edge> fromEdges= graph.GetEdges(iEdge.Source,iEdge.Target);
                if (fromEdges != null)
                {
                    foreach (Edge e in fromEdges)
                    {
                        if (existingSpring == null && edgeSprings.ContainsKey(e.ID))
                        {
                            existingSpring = edgeSprings[e.ID];
                            break;
                        }
                    }
                
                }
                if(existingSpring!=null)
                {
                    return new Spring(existingSpring.point1, existingSpring.point2, 0.0f, 0.0f);
                }

                List<Edge> toEdges = graph.GetEdges(iEdge.Target,iEdge.Source);
                if (toEdges != null)
                {
                    foreach (Edge e in toEdges)
                    {
                        if (existingSpring == null && edgeSprings.ContainsKey(e.ID))
                        {
                            existingSpring = edgeSprings[e.ID];
                            break;
                        }
                    }
                }
                
                if(existingSpring!=null)
                {
                    return new Spring(existingSpring.point2, existingSpring.point1, 0.0f, 0.0f);
                }
                 edgeSprings[iEdge.ID] = new Spring(GetPoint(iEdge.Source), GetPoint(iEdge.Target), length, Stiffness);

            }
            return edgeSprings[iEdge.ID];
        }

        // TODO: change this for group only after node grouping
        protected void ApplyCoulombsLaw()
        {
            foreach(Node n1 in graph.nodes)
            {
                Point point1 = GetPoint(n1);
                foreach(Node n2 in graph.nodes)
                {
                    Point point2 = GetPoint(n2);
                    if(point1!=point2)
                    {
                        AbstractVector d=point1.position-point2.position;
                        float distance = d.Magnitude() +0.1f;
                        AbstractVector direction = d.Normalize();
                        if (n1.Pinned && n2.Pinned)
                        {
                            point1.ApplyForce(direction * 0.0f);
                            point2.ApplyForce(direction * 0.0f);
                        }
                        else if (n1.Pinned)
                        {
                            point1.ApplyForce(direction*0.0f);
                            //point2.ApplyForce((direction * Repulsion) / (distance * distance * -1.0f));
                            point2.ApplyForce((direction * Repulsion) / (distance * -1.0f));
                        }
                        else if (n2.Pinned)
                        {
                            //point1.ApplyForce((direction * Repulsion) / (distance * distance));
                            point1.ApplyForce((direction * Repulsion) / (distance));
                            point2.ApplyForce(direction * 0.0f);
                        }
                        else
                        {
//                             point1.ApplyForce((direction * Repulsion) / (distance * distance * 0.5f));
//                             point2.ApplyForce((direction * Repulsion) / (distance * distance * -0.5f));
                            point1.ApplyForce((direction * Repulsion) / (distance * 0.5f));
                            point2.ApplyForce((direction * Repulsion) / (distance * -0.5f));
                        }

                    }
                }
            }
        }

        protected void ApplyHookesLaw()
        {
            foreach(Edge e in graph.edges)
            {
                Spring spring = GetSpring(e);
                AbstractVector d = spring.point2.position-spring.point1.position;
                float displacement = spring.Length-d.Magnitude();
                AbstractVector direction = d.Normalize();

                if (spring.point1.node.Pinned && spring.point2.node.Pinned)
                {
                    spring.point1.ApplyForce(direction * 0.0f);
                    spring.point2.ApplyForce(direction * 0.0f);
                }
                else if (spring.point1.node.Pinned)
                {
                    spring.point1.ApplyForce(direction * 0.0f);
                    spring.point2.ApplyForce(direction * (spring.K * displacement));
                }
                else if (spring.point2.node.Pinned)
                {
                    spring.point1.ApplyForce(direction * (spring.K * displacement * -1.0f));
                    spring.point2.ApplyForce(direction * 0.0f);
                }
                else
                {
                    spring.point1.ApplyForce(direction * (spring.K * displacement * -0.5f));
                    spring.point2.ApplyForce(direction * (spring.K * displacement * 0.5f));
                }

                
            }
        }

        protected void AttractToCentre()
        {
//             foreach(Node n in graph.nodes)
//             {
//                 Point point = GetPoint(n);
//                 AbstractVector direction = point.position*-1.0f;
//                 if(!point.node.Pinned)
//                     point.ApplyForce(direction*(Repulsion/50.0f));
//             }
        }

        protected void UpdateVelocity(float iTimeStep)
        {
            foreach(Node n in graph.nodes)
            {
                Point point = GetPoint(n);
                point.velocity.Add(point.acceleration*iTimeStep);
                point.velocity.Multiply(Damping);
                point.acceleration.SetZero();
            }
        }

        protected void UpdatePosition(float iTimeStep)
        {
            foreach(Node n in graph.nodes)
            {
                Point point = GetPoint(n);
                point.position.Add(point.velocity*iTimeStep);
            }
        }

        protected float TotalEnergy()
        {
            float energy=0.0f;
            foreach(Node n in graph.nodes)
            {
                Point point = GetPoint(n);
                float speed = point.velocity.Magnitude();
                energy+=0.5f *point.mass *speed*speed;
            }
            return energy;
        }

        public void Calculate(float iTimeStep) // time in second
        {
            ApplyCoulombsLaw();
            ApplyHookesLaw();
            AttractToCentre();
            UpdateVelocity(iTimeStep);
            UpdatePosition(iTimeStep);
            if (TotalEnergy() < Threadshold)
            {
                WithinThreashold = true;
            }
            else
                WithinThreashold = false;
        }


        public void EachEdge(EdgeAction del)
        {
            foreach(Edge e in graph.edges)
            {
                del(e, GetSpring(e));
            }
        }

        public void EachNode(NodeAction del)
        {
            foreach (Node n in graph.nodes)
            {
                del(n, GetPoint(n));
            }
        }

        public NearestPoint Nearest(AbstractVector position)
        {
            NearestPoint min = new NearestPoint();
            foreach(Node n in graph.nodes)
            {
                Point point = GetPoint(n);
                float distance = (point.position-position).Magnitude();
                if(min.distance==null || distance<min.distance)
                {
                    min.node=n;
                    min.point=point;
                    min.distance=distance;
                }
            }
            return min;
        }

        public abstract BoundingBox GetBoundingBox();
	
    }

    public class ForceDirected2D : ForceDirected<Vector2>
    {
        public ForceDirected2D(IGraph iGraph, float iStiffness, float iRepulsion, float iDamping)
            : base(iGraph, iStiffness, iRepulsion, iDamping)
        {

        }

        protected override Point GetPoint(Node iNode)
        {
            if (!(nodePoints.ContainsKey(iNode.ID)))
            {
                Vector2 iniPosition = iNode.Data.initialPostion as Vector2;
                if (iniPosition == null)
                    iniPosition = Vector2.Random() as Vector2;
                nodePoints[iNode.ID] = new Point(iniPosition, Vector2.Zero(), Vector2.Zero(), iNode);
            }
            return nodePoints[iNode.ID];
        }

        public override BoundingBox GetBoundingBox()
        {
            BoundingBox boundingBox = new BoundingBox();
            Vector2 bottomLeft = Vector2.Identity().Multiply(BoundingBox.defaultBB*-1.0f) as Vector2;
            Vector2 topRight= Vector2.Identity().Multiply(BoundingBox.defaultBB) as Vector2;
            foreach (Node n in graph.nodes)
            {
                Vector2 position=GetPoint(n).position as Vector2;

                if(position.x < bottomLeft.x)
                    bottomLeft.x=position.x;
                if(position.y<bottomLeft.y)
                    bottomLeft.y=position.y;
                if(position.x>topRight.x)
                    topRight.x=position.x;
                if(position.y>topRight.y)
                    topRight.y=position.y;
            }
            AbstractVector padding = (topRight-bottomLeft).Multiply(BoundingBox.defaultPadding);
            boundingBox.bottomLeftFront=bottomLeft.Subtract(padding);
            boundingBox.topRightBack=topRight.Add(padding);
            return boundingBox;

        }
    }

    public class ForceDirected3D : ForceDirected<Vector3>
    {
        public ForceDirected3D(IGraph iGraph, float iStiffness, float iRepulsion, float iDamping)
            : base(iGraph, iStiffness, iRepulsion, iDamping)
        {

        }

        protected override Point GetPoint(Node iNode)
        {
            if (!(nodePoints.ContainsKey(iNode.ID)))
            {
                Vector3 iniPosition = iNode.Data.initialPostion as Vector3;
                if (iniPosition == null)
                    iniPosition = Vector3.Random() as Vector3;
                nodePoints[iNode.ID] = new Point(iniPosition, Vector3.Zero(), Vector3.Zero(), iNode);
            }
            return nodePoints[iNode.ID];
        }

        public override BoundingBox GetBoundingBox()
        {
            BoundingBox boundingBox = new BoundingBox();
            Vector3 bottomLeft = Vector3.Identity().Multiply(BoundingBox.defaultBB * -1.0f) as Vector3;
            Vector3 topRight = Vector3.Identity().Multiply(BoundingBox.defaultBB) as Vector3;
            foreach (Node n in graph.nodes)
            {
                Vector3 position = GetPoint(n).position as Vector3;
                if (position.x < bottomLeft.x)
                    bottomLeft.x = position.x;
                if (position.y < bottomLeft.y)
                    bottomLeft.y = position.y;
                if (position.z<bottomLeft.z)
                    bottomLeft.z = position.z;
                if (position.x > topRight.x)
                    topRight.x = position.x;
                if (position.y > topRight.y)
                    topRight.y = position.y;
                if (position.z > topRight.z)
                    topRight.z = position.z;
            }
            AbstractVector padding = (topRight - bottomLeft).Multiply(BoundingBox.defaultPadding);
            boundingBox.bottomLeftFront = bottomLeft.Subtract(padding);
            boundingBox.topRightBack = topRight.Add(padding);
            return boundingBox;

        }
    }
}
