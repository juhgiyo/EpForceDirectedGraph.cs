/*! 
@file ForceDirected.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
@date August 08, 2013
@brief ForceDirected Interface
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
        public Vector3? topRightBack;
        public Vector3? bottomLeftFront;
    }

    public abstract class ForceDirected: IForceDirected
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
        public void Clear()
        {
            nodePoints.Clear();
            edgeSprings.Clear();
            graph.Clear();
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

        public abstract Point GetPoint(Node iNode);



        public Spring GetSpring(Edge iEdge)
        {
            if(!(edgeSprings.ContainsKey(iEdge.ID)))
            {
                float length = iEdge.Data.length;
                Spring ?existingSpring = null;

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
                    return new Spring(existingSpring.Value.point1, existingSpring.Value.point2, 0.0f, 0.0f);
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
                    return new Spring(existingSpring.Value.point2, existingSpring.Value.point1, 0.0f, 0.0f);
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
                        Vector3 d=point1.position-point2.position;
                        float distance = d.Magnitude() +0.1f;
                        Vector3 direction = d.Normalize();
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
                Vector3 d = spring.point2.position-spring.point1.position;
                float displacement = spring.length-d.Magnitude();
                Vector3 direction = d.Normalize();

                if (spring.point1.node.Pinned && spring.point2.node.Pinned)
                {
                    spring.point1.ApplyForce(direction * 0.0f);
                    spring.point2.ApplyForce(direction * 0.0f);
                }
                else if (spring.point1.node.Pinned)
                {
                    spring.point1.ApplyForce(direction * 0.0f);
                    spring.point2.ApplyForce(direction * (spring.k * displacement));
                }
                else if (spring.point2.node.Pinned)
                {
                    spring.point1.ApplyForce(direction * (spring.k * displacement * -1.0f));
                    spring.point2.ApplyForce(direction * 0.0f);
                }
                else
                {
                    spring.point1.ApplyForce(direction * (spring.k * displacement * -0.5f));
                    spring.point2.ApplyForce(direction * (spring.k * displacement * 0.5f));
                }

                
            }
        }

        protected void AttractToCentre()
        {
            foreach(Node n in graph.nodes)
            {
                Point point = GetPoint(n);
                if (!point.node.Pinned)
                {
                    Vector3 direction = point.position*-1.0f;
                    //point.ApplyForce(direction * ((float)Math.Sqrt((double)(Repulsion / 100.0f))));

                    
                    float displacement = direction.Magnitude();
                    direction = direction.Normalize();
                    point.ApplyForce(direction * (Stiffness * displacement * 0.4f));
                }
             }
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

        public NearestPoint Nearest(Vector3 position)
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

    public class ForceDirected2D : ForceDirected
    {
        public ForceDirected2D(IGraph iGraph, float iStiffness, float iRepulsion, float iDamping)
            : base(iGraph, iStiffness, iRepulsion, iDamping)
        {

        }

        public override Point GetPoint(Node iNode)
        {
            if (!(nodePoints.ContainsKey(iNode.ID)))
            {
                Vector3 ?iniPosition = iNode.Data.initialPostion;
                if (iniPosition == null)
                    iniPosition = Vector3.Random();
                nodePoints[iNode.ID] = new Point(iniPosition.Value, Vector3.Zero(), Vector3.Zero(), iNode);
            }
            return nodePoints[iNode.ID];
        }

        public override BoundingBox GetBoundingBox()
        {
            BoundingBox boundingBox = new BoundingBox();
            Vector3 bottomLeft = Vector3.Identity().Multiply(BoundingBox.defaultBB*-1.0f);
            Vector3 topRight= Vector3.Identity().Multiply(BoundingBox.defaultBB);
            foreach (Node n in graph.nodes)
            {
                Vector3 position=GetPoint(n).position;

                if(position.x < bottomLeft.x)
                    bottomLeft.x=position.x;
                if(position.y<bottomLeft.y)
                    bottomLeft.y=position.y;
                if(position.x>topRight.x)
                    topRight.x=position.x;
                if(position.y>topRight.y)
                    topRight.y=position.y;
            }
            Vector3 padding = (topRight-bottomLeft).Multiply(BoundingBox.defaultPadding);
            boundingBox.bottomLeftFront=bottomLeft.Subtract(padding);
            boundingBox.topRightBack=topRight.Add(padding);
            return boundingBox;

        }
    }

    public class ForceDirected3D : ForceDirected
    {
        public ForceDirected3D(IGraph iGraph, float iStiffness, float iRepulsion, float iDamping)
            : base(iGraph, iStiffness, iRepulsion, iDamping)
        {

        }

        public override Point GetPoint(Node iNode)
        {
            if (!(nodePoints.ContainsKey(iNode.ID)))
            {
                Vector3 ?iniPosition = iNode.Data.initialPostion;
                if (iniPosition == null)
                    iniPosition = Vector3.Random();
                nodePoints[iNode.ID] = new Point(iniPosition.Value, Vector3.Zero(), Vector3.Zero(), iNode);
            }
            return nodePoints[iNode.ID];
        }

        public override BoundingBox GetBoundingBox()
        {
            BoundingBox boundingBox = new BoundingBox();
            Vector3 bottomLeft = Vector3.Identity().Multiply(BoundingBox.defaultBB * -1.0f);
            Vector3 topRight = Vector3.Identity().Multiply(BoundingBox.defaultBB);
            foreach (Node n in graph.nodes)
            {
                Vector3 position = GetPoint(n).position;
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
            Vector3 padding = (topRight - bottomLeft).Multiply(BoundingBox.defaultPadding);
            boundingBox.bottomLeftFront = bottomLeft.Subtract(padding);
            boundingBox.topRightBack = topRight.Add(padding);
            return boundingBox;

        }
    }
}
