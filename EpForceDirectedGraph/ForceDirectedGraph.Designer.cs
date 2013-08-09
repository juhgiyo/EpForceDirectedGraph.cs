namespace EpForceDirectedGraph
{
    partial class ForceDirectedGraph
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbStiffness = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRepulsion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDamping = new System.Windows.Forms.TextBox();
            this.cbbFromNode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbNodeName = new System.Windows.Forms.TextBox();
            this.btnAddNode = new System.Windows.Forms.Button();
            this.cbbToNode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddEdge = new System.Windows.Forms.Button();
            this.lbNode = new System.Windows.Forms.ListBox();
            this.lbEdge = new System.Windows.Forms.ListBox();
            this.btnRemoveNode = new System.Windows.Forms.Button();
            this.btnRemoveEdge = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnChangeProperties = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbStiffness
            // 
            this.tbStiffness.Location = new System.Drawing.Point(92, 29);
            this.tbStiffness.Name = "tbStiffness";
            this.tbStiffness.Size = new System.Drawing.Size(100, 21);
            this.tbStiffness.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Stiffness";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Repulsion";
            // 
            // tbRepulsion
            // 
            this.tbRepulsion.Location = new System.Drawing.Point(265, 29);
            this.tbRepulsion.Name = "tbRepulsion";
            this.tbRepulsion.Size = new System.Drawing.Size(100, 21);
            this.tbRepulsion.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(371, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Damping";
            // 
            // tbDamping
            // 
            this.tbDamping.Location = new System.Drawing.Point(432, 29);
            this.tbDamping.Name = "tbDamping";
            this.tbDamping.Size = new System.Drawing.Size(100, 21);
            this.tbDamping.TabIndex = 4;
            // 
            // cbbFromNode
            // 
            this.cbbFromNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFromNode.FormattingEnabled = true;
            this.cbbFromNode.Location = new System.Drawing.Point(279, 2);
            this.cbbFromNode.Name = "cbbFromNode";
            this.cbbFromNode.Size = new System.Drawing.Size(121, 20);
            this.cbbFromNode.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "Node Name";
            // 
            // tbNodeName
            // 
            this.tbNodeName.Location = new System.Drawing.Point(92, 2);
            this.tbNodeName.Name = "tbNodeName";
            this.tbNodeName.Size = new System.Drawing.Size(100, 21);
            this.tbNodeName.TabIndex = 7;
            // 
            // btnAddNode
            // 
            this.btnAddNode.Location = new System.Drawing.Point(198, 0);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(75, 23);
            this.btnAddNode.TabIndex = 9;
            this.btnAddNode.Text = "Add Node";
            this.btnAddNode.UseVisualStyleBackColor = true;
            // 
            // cbbToNode
            // 
            this.cbbToNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbToNode.FormattingEnabled = true;
            this.cbbToNode.Location = new System.Drawing.Point(425, 2);
            this.cbbToNode.Name = "cbbToNode";
            this.cbbToNode.Size = new System.Drawing.Size(121, 20);
            this.cbbToNode.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(406, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "-";
            // 
            // btnAddEdge
            // 
            this.btnAddEdge.Location = new System.Drawing.Point(552, 0);
            this.btnAddEdge.Name = "btnAddEdge";
            this.btnAddEdge.Size = new System.Drawing.Size(75, 23);
            this.btnAddEdge.TabIndex = 12;
            this.btnAddEdge.Text = "Add Edge";
            this.btnAddEdge.UseVisualStyleBackColor = true;
            // 
            // lbNode
            // 
            this.lbNode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbNode.FormattingEnabled = true;
            this.lbNode.ItemHeight = 12;
            this.lbNode.Location = new System.Drawing.Point(12, 79);
            this.lbNode.Name = "lbNode";
            this.lbNode.Size = new System.Drawing.Size(120, 436);
            this.lbNode.TabIndex = 13;
            // 
            // lbEdge
            // 
            this.lbEdge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbEdge.FormattingEnabled = true;
            this.lbEdge.ItemHeight = 12;
            this.lbEdge.Location = new System.Drawing.Point(141, 79);
            this.lbEdge.Name = "lbEdge";
            this.lbEdge.Size = new System.Drawing.Size(120, 436);
            this.lbEdge.TabIndex = 14;
            // 
            // btnRemoveNode
            // 
            this.btnRemoveNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveNode.Location = new System.Drawing.Point(12, 526);
            this.btnRemoveNode.Name = "btnRemoveNode";
            this.btnRemoveNode.Size = new System.Drawing.Size(120, 23);
            this.btnRemoveNode.TabIndex = 15;
            this.btnRemoveNode.Text = "Remove Node";
            this.btnRemoveNode.UseVisualStyleBackColor = true;
            // 
            // btnRemoveEdge
            // 
            this.btnRemoveEdge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveEdge.Location = new System.Drawing.Point(141, 526);
            this.btnRemoveEdge.Name = "btnRemoveEdge";
            this.btnRemoveEdge.Size = new System.Drawing.Size(120, 23);
            this.btnRemoveEdge.TabIndex = 16;
            this.btnRemoveEdge.Text = "Remove Edge";
            this.btnRemoveEdge.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(268, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 470);
            this.panel1.TabIndex = 17;
            // 
            // btnChangeProperties
            // 
            this.btnChangeProperties.Location = new System.Drawing.Point(552, 27);
            this.btnChangeProperties.Name = "btnChangeProperties";
            this.btnChangeProperties.Size = new System.Drawing.Size(127, 23);
            this.btnChangeProperties.TabIndex = 18;
            this.btnChangeProperties.Text = "Change Properties";
            this.btnChangeProperties.UseVisualStyleBackColor = true;
            // 
            // ForceDirectedGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 561);
            this.Controls.Add(this.btnChangeProperties);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnRemoveEdge);
            this.Controls.Add(this.btnRemoveNode);
            this.Controls.Add(this.lbEdge);
            this.Controls.Add(this.lbNode);
            this.Controls.Add(this.btnAddEdge);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbbToNode);
            this.Controls.Add(this.btnAddNode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbNodeName);
            this.Controls.Add(this.cbbFromNode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbDamping);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbRepulsion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbStiffness);
            this.Name = "ForceDirectedGraph";
            this.Text = "EpForceDirectedGraph.cs Demo";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ForceDirectedGraph_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbStiffness;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbRepulsion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDamping;
        private System.Windows.Forms.ComboBox cbbFromNode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbNodeName;
        private System.Windows.Forms.Button btnAddNode;
        private System.Windows.Forms.ComboBox cbbToNode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddEdge;
        private System.Windows.Forms.ListBox lbNode;
        private System.Windows.Forms.ListBox lbEdge;
        private System.Windows.Forms.Button btnRemoveNode;
        private System.Windows.Forms.Button btnRemoveEdge;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnChangeProperties;
    }
}

