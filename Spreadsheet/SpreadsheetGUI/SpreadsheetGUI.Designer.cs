namespace SpreadsheetGUI
{
	partial class SpreadsheetGUI
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveASToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.ContentLabel = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.CellValueTextBox = new System.Windows.Forms.TextBox();
			this.CellContentTextBox = new System.Windows.Forms.TextBox();
			this.CellNameTextBox = new System.Windows.Forms.TextBox();
			this.CellValueLabel = new System.Windows.Forms.Label();
			this.CellContentLabel = new System.Windows.Forms.Label();
			this.CellNameLabel = new System.Windows.Forms.Label();
			this.PanelContainEditAndOutput = new System.Windows.Forms.Panel();
			this.spreadsheetPanel1 = new SSGui.SpreadsheetPanel();
			this.menuStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.PanelContainEditAndOutput.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(89, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// menuToolStripMenuItem
			// 
			this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.newToolStripMenuItem1,
            this.saveToolStripMenuItem,
            this.saveASToolStripMenuItem,
            this.closeToolStripMenuItem});
			this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
			this.menuToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.menuToolStripMenuItem.Text = "File";
			this.menuToolStripMenuItem.Click += new System.EventHandler(this.menuToolStripMenuItem_Click);
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
			this.newToolStripMenuItem.Text = "New";
			// 
			// newToolStripMenuItem1
			// 
			this.newToolStripMenuItem1.Name = "newToolStripMenuItem1";
			this.newToolStripMenuItem1.Size = new System.Drawing.Size(115, 22);
			this.newToolStripMenuItem1.Text = "Open";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
			this.saveToolStripMenuItem.Text = "Save";
			// 
			// saveASToolStripMenuItem
			// 
			this.saveASToolStripMenuItem.Name = "saveASToolStripMenuItem";
			this.saveASToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
			this.saveASToolStripMenuItem.Text = "Save AS";
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
			this.closeToolStripMenuItem.Text = "Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.menuToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.ContentLabel);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(699, 152);
			this.panel1.TabIndex = 1;
			// 
			// ContentLabel
			// 
			this.ContentLabel.AutoSize = true;
			this.ContentLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ContentLabel.Location = new System.Drawing.Point(0, 119);
			this.ContentLabel.Name = "ContentLabel";
			this.ContentLabel.Size = new System.Drawing.Size(64, 13);
			this.ContentLabel.TabIndex = 1;
			this.ContentLabel.Text = "Context Edit";
			this.ContentLabel.Click += new System.EventHandler(this.label1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBox1.Location = new System.Drawing.Point(0, 132);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(699, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.CellValueTextBox);
			this.panel2.Controls.Add(this.CellContentTextBox);
			this.panel2.Controls.Add(this.CellNameTextBox);
			this.panel2.Controls.Add(this.CellValueLabel);
			this.panel2.Controls.Add(this.CellContentLabel);
			this.panel2.Controls.Add(this.CellNameLabel);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel2.Location = new System.Drawing.Point(705, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(263, 152);
			this.panel2.TabIndex = 2;
			this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
			// 
			// CellValueTextBox
			// 
			this.CellValueTextBox.Location = new System.Drawing.Point(100, 75);
			this.CellValueTextBox.Name = "CellValueTextBox";
			this.CellValueTextBox.Size = new System.Drawing.Size(143, 20);
			this.CellValueTextBox.TabIndex = 5;
			this.CellValueTextBox.TextChanged += new System.EventHandler(this.CellValueTextBox_TextChanged);
			// 
			// CellContentTextBox
			// 
			this.CellContentTextBox.Location = new System.Drawing.Point(100, 44);
			this.CellContentTextBox.Name = "CellContentTextBox";
			this.CellContentTextBox.Size = new System.Drawing.Size(143, 20);
			this.CellContentTextBox.TabIndex = 4;
			this.CellContentTextBox.TextChanged += new System.EventHandler(this.CellContentTextBox_TextChanged);
			// 
			// CellNameTextBox
			// 
			this.CellNameTextBox.Location = new System.Drawing.Point(99, 14);
			this.CellNameTextBox.Name = "CellNameTextBox";
			this.CellNameTextBox.Size = new System.Drawing.Size(144, 20);
			this.CellNameTextBox.TabIndex = 3;
			this.CellNameTextBox.TextChanged += new System.EventHandler(this.CellNameTextBox_TextChanged);
			// 
			// CellValueLabel
			// 
			this.CellValueLabel.AutoSize = true;
			this.CellValueLabel.Location = new System.Drawing.Point(31, 75);
			this.CellValueLabel.Name = "CellValueLabel";
			this.CellValueLabel.Size = new System.Drawing.Size(54, 13);
			this.CellValueLabel.TabIndex = 2;
			this.CellValueLabel.Text = "Cell Value";
			// 
			// CellContentLabel
			// 
			this.CellContentLabel.AutoSize = true;
			this.CellContentLabel.Location = new System.Drawing.Point(30, 47);
			this.CellContentLabel.Name = "CellContentLabel";
			this.CellContentLabel.Size = new System.Drawing.Size(64, 13);
			this.CellContentLabel.TabIndex = 1;
			this.CellContentLabel.Text = "Cell Content";
			// 
			// CellNameLabel
			// 
			this.CellNameLabel.AutoSize = true;
			this.CellNameLabel.Location = new System.Drawing.Point(31, 17);
			this.CellNameLabel.Name = "CellNameLabel";
			this.CellNameLabel.Size = new System.Drawing.Size(55, 13);
			this.CellNameLabel.TabIndex = 0;
			this.CellNameLabel.Text = "Cell Name";
			// 
			// PanelContainEditAndOutput
			// 
			this.PanelContainEditAndOutput.Controls.Add(this.panel1);
			this.PanelContainEditAndOutput.Controls.Add(this.panel2);
			this.PanelContainEditAndOutput.Dock = System.Windows.Forms.DockStyle.Top;
			this.PanelContainEditAndOutput.Location = new System.Drawing.Point(0, 0);
			this.PanelContainEditAndOutput.Name = "PanelContainEditAndOutput";
			this.PanelContainEditAndOutput.Size = new System.Drawing.Size(968, 152);
			this.PanelContainEditAndOutput.TabIndex = 3;
			this.PanelContainEditAndOutput.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
			// 
			// spreadsheetPanel1
			// 
			this.spreadsheetPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spreadsheetPanel1.Location = new System.Drawing.Point(0, 152);
			this.spreadsheetPanel1.Name = "spreadsheetPanel1";
			this.spreadsheetPanel1.Size = new System.Drawing.Size(968, 434);
			this.spreadsheetPanel1.TabIndex = 4;
			// 
			// SpreadsheetGUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(968, 586);
			this.Controls.Add(this.spreadsheetPanel1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.PanelContainEditAndOutput);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "SpreadsheetGUI";
			this.Text = "SpreadsheetGUI";
			this.Load += new System.EventHandler(this.SpreadsheetGUI_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.PanelContainEditAndOutput.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label ContentLabel;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TextBox CellValueTextBox;
		private System.Windows.Forms.TextBox CellContentTextBox;
		private System.Windows.Forms.TextBox CellNameTextBox;
		private System.Windows.Forms.Label CellValueLabel;
		private System.Windows.Forms.Label CellContentLabel;
		private System.Windows.Forms.Label CellNameLabel;
		private System.Windows.Forms.Panel PanelContainEditAndOutput;
		private SSGui.SpreadsheetPanel spreadsheetPanel1;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveASToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
	}
}

