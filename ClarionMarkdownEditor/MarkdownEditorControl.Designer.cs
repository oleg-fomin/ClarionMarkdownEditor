namespace ClarionMarkdownEditor
{
    partial class MarkdownEditorControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnInsertToIDE;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblFileName;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnInsertToIDE = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblFileName = new System.Windows.Forms.ToolStripLabel();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            //
            // toolStrip
            //
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.btnNew,
                this.btnOpen,
                this.btnSave,
                this.btnSaveAs,
                this.toolStripSeparator1,
                this.btnInsertToIDE,
                this.toolStripSeparator2,
                this.lblFileName
            });
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(600, 25);
            this.toolStrip.TabIndex = 0;
            //
            // btnNew
            //
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(35, 22);
            this.btnNew.Text = "New";
            this.btnNew.ToolTipText = "New file";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            //
            // btnOpen
            //
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(40, 22);
            this.btnOpen.Text = "Open";
            this.btnOpen.ToolTipText = "Open file";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            //
            // btnSave
            //
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(35, 22);
            this.btnSave.Text = "Save";
            this.btnSave.ToolTipText = "Save file";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnSaveAs
            //
            this.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(52, 22);
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.ToolTipText = "Save file as...";
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            //
            // toolStripSeparator1
            //
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            //
            // btnInsertToIDE
            //
            this.btnInsertToIDE.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnInsertToIDE.Name = "btnInsertToIDE";
            this.btnInsertToIDE.Size = new System.Drawing.Size(75, 22);
            this.btnInsertToIDE.Text = "Insert to IDE";
            this.btnInsertToIDE.ToolTipText = "Insert markdown to active IDE editor";
            this.btnInsertToIDE.Click += new System.EventHandler(this.btnInsertToIDE_Click);
            //
            // toolStripSeparator2
            //
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            //
            // lblFileName
            //
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(50, 22);
            this.lblFileName.Text = "Untitled";
            this.lblFileName.ForeColor = System.Drawing.Color.Gray;
            //
            // webBrowser
            //
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 25);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(600, 375);
            this.webBrowser.TabIndex = 1;
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            //
            // MarkdownEditorControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.toolStrip);
            this.Name = "MarkdownEditorControl";
            this.Size = new System.Drawing.Size(600, 400);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
