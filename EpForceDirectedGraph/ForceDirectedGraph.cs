using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;



namespace EpForceDirectedGraph
{
    public partial class ForceDirectedGraphForm : Form
    {
        const int width = 64;
        const int height = 32;
        Stopwatch stopwatch = new Stopwatch();

        Graphics paper;

        int panelTop;
        int panelBottom;
        int panelLeft;
        int panelRight;
         

        Dictionary<Node,GridBox> m_fdgBoxes;
        Dictionary<Edge, GridLine> m_fdgLines;
        Graph m_fdgGraph;
        ForceDirected2D m_fdgPhysics;
        Renderer m_fdgRenderer;

        System.Timers.Timer timer = new System.Timers.Timer(30);


        public ForceDirectedGraphForm()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.Width = (width + 1) * 20;
            this.Height = (height + 1) * 20 + 100;
            this.MaximumSize = new Size(this.Width, this.Height);
            this.MaximizeBox = false;

            tbStiffness.Text = "81.76";
            tbRepulsion.Text = "1000.0";
            tbDamping.Text = "0.5";
            panelTop = 0;
            panelBottom = pDrawPanel.Size.Height;
            panelLeft = 0;
            panelRight = pDrawPanel.Size.Width;
            
            m_fdgBoxes = new Dictionary<Node, GridBox>();
            m_fdgLines = new Dictionary<Edge, GridLine>();
            m_fdgGraph = new Graph();
            m_fdgPhysics = new ForceDirected2D(m_fdgGraph,81.76f,1000.0f, 0.5f);
            m_fdgRenderer = new Renderer(this, m_fdgPhysics);
           

            pDrawPanel.Paint += new PaintEventHandler(DrawPanel_Paint);
           
            timer.Interval = 100;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Start();

        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            pDrawPanel.Invalidate();
        }
        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            stopwatch.Stop();
            var p = sender as Panel;
            paper = e.Graphics;

            GridBox box = new GridBox((panelRight - panelLeft) / 2, (panelBottom - panelTop) / 2, BoxType.Pinned);
            box.DrawBox(paper);

            m_fdgRenderer.Draw(0.1f); // TODO: Check Time

            stopwatch.Reset();
            stopwatch.Start();

           
        }

        private void ForceDirectedGraph_Paint(object sender, PaintEventArgs e)
        {
//             stopwatch.Stop();
//             paper = e.Graphics;
// 
//             GridBox box = new GridBox((panelRight - panelLeft) / 2, (panelBottom - panelTop) / 2, BoxType.Pinned);
//             box.DrawBox(paper);
// 
//             m_fdgRenderer.Draw(((float)stopwatch.ElapsedMilliseconds) / 1000.0f); // TODO: Check Time
// 
//             stopwatch.Reset();
//             stopwatch.Start();
 
            
            
        }
        public Pair<int, int> GraphToScreen(Vector2 iPos)
        {
            Pair<int, int> retPair = new Pair<int, int>();
            retPair.first = (int)(iPos.x +(((float)(panelRight - panelLeft)) / 2.0f));
            retPair.second = (int)(iPos.y + (((float)(panelBottom - panelTop)) / 2.0f));
            return retPair;
        }

        public Vector2 ScreenToGraph(Pair<int, int> iScreenPos)
        {
            Vector2 retVec = new Vector2();
            retVec.x= ((float)iScreenPos.first)-(((float)(panelRight-panelLeft))/2.0f);
            retVec.y = ((float)iScreenPos.second) - (((float)(panelBottom - panelTop)) / 2.0f);
            return retVec;
        }

        public void DrawLine(Edge iEdge, AbstractVector iPosition1, AbstractVector iPosition2)
        {
            Pair<int, int> pos1 = GraphToScreen(iPosition1 as Vector2);
            Pair<int, int> pos2 = GraphToScreen(iPosition2 as Vector2);
            m_fdgLines[iEdge].Set(pos1.first, pos1.second, pos2.first, pos2.second);
            m_fdgLines[iEdge].DrawLine(paper);
            
        }

        public void DrawBox(Node iNode, AbstractVector iPosition)
        {
            Pair<int, int> pos = GraphToScreen(iPosition as Vector2);
            m_fdgBoxes[iNode].Set(pos.first, pos.second);
            m_fdgBoxes[iNode].DrawBox(paper);
        }

        private void btnChangeProperties_Click(object sender, EventArgs e)
        {
            float stiffNess = System.Convert.ToSingle(tbStiffness.Text);
            m_fdgPhysics.Stiffness = stiffNess;
            float repulsion = System.Convert.ToSingle(tbRepulsion.Text);
            m_fdgPhysics.Repulsion = repulsion;
            float damping = System.Convert.ToSingle(tbDamping.Text);
            m_fdgPhysics.Damping = damping;
        }

        private void btnAddNode_Click(object sender, EventArgs e)
        {
            tbNodeName.Text=tbNodeName.Text.Trim();
            if (tbNodeName.Text == "")
            {
                MessageBox.Show("Please type in the node name to insert!");
                return;
            }
            if (m_fdgGraph.GetNode(tbNodeName.Text) != null)
            {
                MessageBox.Show("Node already exists in the graph!");
                return;
            }
           Node newNode= m_fdgGraph.CreateNode(tbNodeName.Text);
           m_fdgBoxes[newNode] = new GridBox(0, 0, BoxType.Normal);

           cbbFromNode.Items.Add(tbNodeName.Text);
           cbbToNode.Items.Add(tbNodeName.Text);
           lbNode.Items.Add(tbNodeName.Text);
        }

        private void btnAddEdge_Click(object sender, EventArgs e)
        {
            string nodeName1 = cbbFromNode.Text;
            string nodeName2 = cbbToNode.Text;
            if (nodeName1 == nodeName2)
            {
                MessageBox.Show("Edge cannot be connected to same node!");
                return;
            }
            Node node1 = m_fdgGraph.GetNode(nodeName1);
            Node node2 = m_fdgGraph.GetNode(nodeName2);
            PhysicsData data = new PhysicsData();
            
            string label = nodeName1 + "-" + nodeName2;
            data.label = label;
            data.length = 60.0f;

            Edge newEdge = m_fdgGraph.CreateEdge(node1, node2, data);
            m_fdgLines[newEdge] = new GridLine(0, 0, 0, 0);

            lbEdge.Items.Add(label);
        }

        private void btnRemoveNode_Click(object sender, EventArgs e)
        {
            if (lbNode.SelectedIndex != -1)
            {
                string nodeName=(string)lbNode.SelectedItem;
                Node removeNode=m_fdgGraph.GetNode(nodeName);

                m_fdgBoxes.Remove(removeNode);
                List<Edge> edgeList = m_fdgGraph.GetEdges(removeNode);
                foreach(Edge edge in edgeList)
                {
                    m_fdgLines.Remove(edge);
                    int edgeIndex=lbEdge.FindString(edge.Data.label);
                    lbEdge.Items.RemoveAt(edgeIndex);
                }
                m_fdgGraph.RemoveNode(removeNode);


                cbbFromNode.Items.RemoveAt(lbNode.SelectedIndex);
                cbbToNode.Items.RemoveAt(lbNode.SelectedIndex);

                lbNode.Items.RemoveAt(lbNode.SelectedIndex);                
            }
            else
            {
                MessageBox.Show("Please select a node to remove!");
            }
        }

        private void btnRemoveEdge_Click(object sender, EventArgs e)
        {
            if (lbEdge.SelectedIndex != -1)
            {
                string edgeName = (string)lbEdge.SelectedItem;
                Edge removeEdge= m_fdgGraph.GetEdge(edgeName);
                m_fdgLines.Remove(removeEdge);
                m_fdgGraph.RemoveEdge(removeEdge);
                lbEdge.Items.RemoveAt(lbEdge.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Please select an edge to remove!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pDrawPanel.Invalidate();
            //Invalidate();
        }
    }
}
