using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;
using System.IO;
using EpForceDirectedGraph.cs;

namespace EpForceDirectedGraphDemo
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
            tbRepulsion.Text = "40000.0";
            tbDamping.Text = "0.5";
            panelTop = 0;
            panelBottom = pDrawPanel.Size.Height;
            panelLeft = 0;
            panelRight = pDrawPanel.Size.Width;
            
            m_fdgBoxes = new Dictionary<Node, GridBox>();
            m_fdgLines = new Dictionary<Edge, GridLine>();
            m_fdgGraph = new Graph();
            m_fdgPhysics = new ForceDirected2D(m_fdgGraph,81.76f,40000.0f, 0.5f);
            m_fdgRenderer = new Renderer(this, m_fdgPhysics);
           

            pDrawPanel.Paint += new PaintEventHandler(DrawPanel_Paint);

            timer.Interval = 30;
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

            m_fdgRenderer.Draw(0.05f); // TODO: Check Time

            stopwatch.Reset();
            stopwatch.Start();

           
        }

        private void ForceDirectedGraph_Paint(object sender, PaintEventArgs e)
        {
 
         
        }
        public Pair<int, int> GraphToScreen(FDGVector2 iPos)
        {
            Pair<int, int> retPair = new Pair<int, int>();
            retPair.first = (int)(iPos.x +(((float)(panelRight - panelLeft)) / 2.0f));
            retPair.second = (int)(iPos.y + (((float)(panelBottom - panelTop)) / 2.0f));
            return retPair;
        }

        public FDGVector2 ScreenToGraph(Pair<int, int> iScreenPos)
        {
            FDGVector2 retVec = new FDGVector2();
            retVec.x= ((float)iScreenPos.first)-(((float)(panelRight-panelLeft))/2.0f);
            retVec.y = ((float)iScreenPos.second) - (((float)(panelBottom - panelTop)) / 2.0f);
            return retVec;
        }

        public void DrawLine(Edge iEdge, AbstractVector iPosition1, AbstractVector iPosition2)
        {
            Pair<int, int> pos1 = GraphToScreen(iPosition1 as FDGVector2);
            Pair<int, int> pos2 = GraphToScreen(iPosition2 as FDGVector2);
            m_fdgLines[iEdge].Set(pos1.first, pos1.second, pos2.first, pos2.second);
            m_fdgLines[iEdge].DrawLine(paper);
            
        }

        public void DrawBox(Node iNode, AbstractVector iPosition)
        {
            Pair<int, int> pos = GraphToScreen(iPosition as FDGVector2);
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

        private bool addNode(string nodeName)
        {
            nodeName = nodeName.Trim();
            if (m_fdgGraph.GetNode(tbNodeName.Text) != null)
            {
                return false;
            }
            Node newNode = m_fdgGraph.CreateNode(nodeName);
            m_fdgBoxes[newNode] = new GridBox(0, 0, BoxType.Normal);

            cbbFromNode.Items.Add(nodeName);
            cbbToNode.Items.Add(nodeName);
            lbNode.Items.Add(nodeName);
            return true;
        }
        private void btnAddNode_Click(object sender, EventArgs e)
        {
            tbNodeName.Text=tbNodeName.Text.Trim();
            if (tbNodeName.Text == "")
            {
                MessageBox.Show("Please type in the node name to insert!");
                return;
            }
            if (!addNode(tbNodeName.Text))
            {
                MessageBox.Show("Node already exists in the graph!");
                return;
            }
        }
        private bool addEdge(string nodeName1, string nodeName2)
        {
            nodeName1 = nodeName1.Trim();
            nodeName2 = nodeName2.Trim();
            if (nodeName1 == nodeName2)
            {
                return false;
            }
            Node node1 = m_fdgGraph.GetNode(nodeName1);
            Node node2 = m_fdgGraph.GetNode(nodeName2);
            EdgeData data = new EdgeData();

            string label = nodeName1 + "-" + nodeName2;
            data.label = label;
            data.length = 60.0f;

            Edge newEdge = m_fdgGraph.CreateEdge(node1, node2, data);
            m_fdgLines[newEdge] = new GridLine(0, 0, 0, 0);

            lbEdge.Items.Add(label);
            return true;
        }
        private void btnAddEdge_Click(object sender, EventArgs e)
        {
            string nodeName1 = cbbFromNode.Text;
            string nodeName2 = cbbToNode.Text;
            if (!addEdge(nodeName1,  nodeName2))
            {
                MessageBox.Show("Edge cannot be connected to same node!");
                return;
            }
        }

        private void btnRemoveNode_Click(object sender, EventArgs e)
        {
            if (lbNode.SelectedIndex != -1)
            {
                int selectedIdx = lbNode.SelectedIndex;
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
                if (selectedIdx != 0)
                    lbNode.SelectedIndex = selectedIdx-1;
                lbNode.Focus();
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
                int selectedIdx = lbEdge.SelectedIndex;
                string edgeName = (string)lbEdge.SelectedItem;
                Edge removeEdge= m_fdgGraph.GetEdge(edgeName);
                m_fdgLines.Remove(removeEdge);
                m_fdgGraph.RemoveEdge(removeEdge);
                lbEdge.Items.RemoveAt(lbEdge.SelectedIndex);
                if (selectedIdx != 0)
                {
                    lbEdge.SelectedIndex = selectedIdx - 1;
                }
                lbEdge.Focus();
            }
            else
            {
                MessageBox.Show("Please select an edge to remove!");
            }
        }

        Node clickedNode;
        GridBox clickedGrid;
        private void pDrawPanel_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (KeyValuePair<Node, GridBox> keyPair in m_fdgBoxes)
            {
                if(keyPair.Value.boxRec.IntersectsWith(new Rectangle(e.Location,new Size(1,1))))
                {
                    clickedNode = keyPair.Key;
                    clickedGrid = keyPair.Value;
                    clickedGrid.boxType = BoxType.Pinned;
                }
            }
        }

        private void pDrawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickedNode != null)
            {

                FDGVector2 vec = ScreenToGraph(new Pair<int, int>(e.Location.X, e.Location.Y));
                clickedNode.Pinned = true;
                m_fdgPhysics.GetPoint(clickedNode).position = vec;
            }
            else
            {
                foreach (KeyValuePair<Node, GridBox> keyPair in m_fdgBoxes)
                {
                    if(keyPair.Value.boxRec.IntersectsWith(new Rectangle(e.Location,new Size(1,1))))
                    {
                        keyPair.Value.boxType = BoxType.Pinned;
                    }
                    else
                    {
                        keyPair.Value.boxType = BoxType.Normal;
                    }
                }

            }
        }

        private void pDrawPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (clickedNode != null)
            {
                clickedGrid.boxType = BoxType.Normal;
                clickedNode.Pinned = false;
                clickedNode = null;
                clickedGrid = null;
            }
        }

        private void tbNodeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddNode_Click(sender, e);
                tbNodeName.Focus();
            }
        }

        private void tbStiffness_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnChangeProperties_Click(sender, e);
                tbStiffness.Focus();
            }
        }

        private void tbRepulsion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnChangeProperties_Click(sender, e);
                tbRepulsion.Focus();
            }
        }

        private void tbDamping_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnChangeProperties_Click(sender, e);
                tbDamping.Focus();
            }
        }

        
        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = fileDialog.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    Int32 size = text.Length;

                    StringReader mapXML = new StringReader(text);
                    XmlTextReader xmlReader = new XmlTextReader(mapXML);
                    while (xmlReader.Read())
                    {
                        switch (xmlReader.NodeType)
                        {
                            case XmlNodeType.Element: // The node is an Element.
                                if (xmlReader.Name == "Node")
                                {
                                    loadNode(xmlReader);
                                }
                                else if (xmlReader.Name == "Edge")
                                {
                                    loadEdge(xmlReader);
                                }
                                break;

                            case XmlNodeType.Text: //Display the text in each element.
                                break;
                            case XmlNodeType.EndElement: //Display end of element.
                                break;
                        }
                    }
                }
                catch (IOException)
                {
                }
            }


        }

        private void loadNode(XmlTextReader iXmlReader)
        {
            while (iXmlReader.MoveToNextAttribute()) // Read attributes.
            {
                if (iXmlReader.Name == "nodeName")
                    addNode(iXmlReader.Value);
            }
        }
        private void loadEdge(XmlTextReader iXmlReader)
        {
            string nodeName1 = null;
            string nodeName2 = null;
            while (iXmlReader.MoveToNextAttribute()) // Read attributes.
            {
                if (iXmlReader.Name == "nodeName1")
                    nodeName1=iXmlReader.Value;
                if (iXmlReader.Name == "nodeName2")
                    nodeName2 = iXmlReader.Value;
            }
            addEdge(nodeName1, nodeName2);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            m_fdgPhysics.Clear();
            m_fdgGraph.Clear();
            m_fdgBoxes.Clear();
            m_fdgLines.Clear();
            lbEdge.Items.Clear();
            lbNode.Items.Clear();
            cbbFromNode.Items.Clear();
            cbbToNode.Items.Clear();
        }
    }
}
