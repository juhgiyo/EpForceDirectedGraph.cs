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

namespace EpForceDirectedGraph.cs
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
        protected Dictionary<string, Point> m_nodePoints;
        protected Dictionary<string, Spring> m_edgeSprings;
        public IGraph graph
        {
            get;
            protected set;
        }
        public void Clear()
        {
            m_nodePoints.Clear();
            m_edgeSprings.Clear();
            graph.Clear();
        }

        public ForceDirected(IGraph iGraph, float iStiffness, float iRepulsion, float iDamping)
        {
            graph=iGraph;
            Stiffness=iStiffness;
            Repulsion=iRepulsion;
            Damping=iDamping;
            m_nodePoints = new Dictionary<string, Point>();
            m_edgeSprings = new Dictionary<string, Spring>();

            Threadshold = 0.01f;
        }

        public abstract Point GetPoint(Node iNode);



        public Spring GetSpring(Edge iEdge)
        {
            if(!(m_edgeSprings.ContainsKey(iEdge.ID)))
            {
                float length = iEdge.Data.length;
                Spring existingSpring = null;

                List<Edge> fromEdges= graph.GetEdges(iEdge.Source,iEdge.Target);
                if (fromEdges != null)
                {
                    foreach (Edge e in fromEdges)
                    {
                        if (existingSpring == null && m_edgeSprings.ContainsKey(e.ID))
                        {
                            existingSpring = m_edgeSprings[e.ID];
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
                        if (existingSpring == null && m_edgeSprings.ContainsKey(e.ID))
                        {
                            existingSpring = m_edgeSprings[e.ID];
                            break;
                        }
                    }
                }
                
                if(existingSpring!=null)
                {
                    return new Spring(existingSpring.point2, existingSpring.point1, 0.0f, 0.0f);
                }
                 m_edgeSprings[iEdge.ID] = new Spring(GetPoint(iEdge.Source), GetPoint(iEdge.Target), length, Stiffness);

            }
            return m_edgeSprings[iEdge.ID];
        }

        // TODO: change this for group only after node grouping
        protected void applyCoulombsLaw()
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

        protected void applyHookesLaw()
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

        protected void attractToCentre()
        {
            foreach(Node n in graph.nodes)
            {
                Point point = GetPoint(n);
                if (!point.node.Pinned)
                {
                    AbstractVector direction = point.position*-1.0f;
                    //point.ApplyForce(direction * ((float)Math.Sqrt((double)(Repulsion / 100.0f))));

                    
                    float displacement = direction.Magnitude();
                    direction = direction.Normalize();
                    point.ApplyForce(direction * (Stiffness * displacement * 0.4f));
                }
             }
        }

        protected void updateVelocity(float iTimeStep)
        {
            foreach(Node n in graph.nodes)
            {
                Point point = GetPoint(n);
                point.velocity.Add(point.acceleration*iTimeStep);
                point.velocity.Multiply(Damping);
                point.acceleration.SetZero();
            }
        }

        protected void updatePosition(float iTimeStep)
        {
            foreach(Node n in graph.nodes)
            {
                Point point = GetPoint(n);
                point.position.Add(point.velocity*iTimeStep);
            }
        }

        protected float getTotalEnergy()
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
            applyCoulombsLaw();
            applyHookesLaw();
            attractToCentre();
            updateVelocity(iTimeStep);
            updatePosition(iTimeStep);
            if (getTotalEnergy() < Threadshold)
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

    public class ForceDirected2D : ForceDirected<FDGVector2>
    {
        public ForceDirected2D(IGraph iGraph, float iStiffness, float iRepulsion, float iDamping)
            : base(iGraph, iStiffness, iRepulsion, iDamping)
        {

        }

        public override Point GetPoint(Node iNode)
        {
            if (!(m_nodePoints.ContainsKey(iNode.ID)))
            {
                FDGVector2 iniPosition = iNode.Data.initialPostion as FDGVector2;
                if (iniPosition == null)
                    iniPosition = FDGVector2.Random() as FDGVector2;
                m_nodePoints[iNode.ID] = new Point(iniPosition, FDGVector2.Zero(), FDGVector2.Zero(), iNode);
            }
            return m_nodePoints[iNode.ID];
        }

        public override BoundingBox GetBoundingBox()
        {
            BoundingBox boundingBox = new BoundingBox();
            FDGVector2 bottomLeft = FDGVector2.Identity().Multiply(BoundingBox.defaultBB * -1.0f) as FDGVector2;
            FDGVector2 topRight = FDGVector2.Identity().Multiply(BoundingBox.defaultBB) as FDGVector2;
            foreach (Node n in graph.nodes)
            {
                FDGVector2 position = GetPoint(n).position as FDGVector2;

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

    public class ForceDirected3D : ForceDirected<FDGVector3>
    {
        public ForceDirected3D(IGraph iGraph, float iStiffness, float iRepulsion, float iDamping)
            : base(iGraph, iStiffness, iRepulsion, iDamping)
        {

        }

        public override Point GetPoint(Node iNode)
        {
            if (!(m_nodePoints.ContainsKey(iNode.ID)))
            {
                FDGVector3 iniPosition = iNode.Data.initialPostion as FDGVector3;
                if (iniPosition == null)
                    iniPosition = FDGVector3.Random() as FDGVector3;
                m_nodePoints[iNode.ID] = new Point(iniPosition, FDGVector3.Zero(), FDGVector3.Zero(), iNode);
            }
            return m_nodePoints[iNode.ID];
        }

        public override BoundingBox GetBoundingBox()
        {
            BoundingBox boundingBox = new BoundingBox();
            FDGVector3 bottomLeft = FDGVector3.Identity().Multiply(BoundingBox.defaultBB * -1.0f) as FDGVector3;
            FDGVector3 topRight = FDGVector3.Identity().Multiply(BoundingBox.defaultBB) as FDGVector3;
            foreach (Node n in graph.nodes)
            {
                FDGVector3 position = GetPoint(n).position as FDGVector3;
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
