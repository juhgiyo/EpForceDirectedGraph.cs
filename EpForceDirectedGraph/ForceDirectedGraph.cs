using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EpForceDirectedGraph
{
    public partial class ForceDirectedGraph : Form
    {
        const int width = 64;
        const int height = 32;
        Graphics paper;
         

        Dictionary<Node,GridBox> m_fdgBoxes;
        Dictionary<Edge, GridLine> m_fdgLines;
        Graph m_fdgGraph;
        ForceDirected2D m_fdgPhysics;
        Renderer m_fdgRenderer;

        public ForceDirectedGraph()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.Width = (width + 1) * 20;
            this.Height = (height + 1) * 20 + 100;
            this.MaximumSize = new Size(this.Width, this.Height);
            this.MaximizeBox = false;

            tbStiffness.Text = "640.0f";
            tbRepulsion.Text = "480.0f";
            tbDamping.Text = "0.5f";
            
            m_fdgBoxes = new Dictionary<Node, GridBox>();
            m_fdgLines = new Dictionary<Edge, GridLine>();
            m_fdgGraph = new Graph();
            m_fdgPhysics = new ForceDirected2D(m_fdgGraph, 640.0f, 480.0f, 0.5f);
            m_fdgRenderer = new Renderer(this, m_fdgPhysics);
        }

        private void ForceDirectedGraph_Paint(object sender, PaintEventArgs e)
        {
            paper = e.Graphics;
            m_fdgRenderer.Draw(0.3f);
        }

        public void DrawLine(Edge iEdge)
        {
            m_fdgLines[iEdge].DrawLine(paper);
        }

        public void DrawBox(Node iNode)
        {
            m_fdgBoxes[iNode].DrawBox(paper);
        }
    }
}
