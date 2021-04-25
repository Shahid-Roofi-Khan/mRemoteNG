using mRemoteNG.Tree.ClickHandlers;
using mRemoteNG.UI.Controls;
using mRemoteNG.UI.Controls.ConnectionTree;

namespace mRemoteNG.UI.Window
{
    public partial class ConnectionTreeWindow_v2 : BaseWindow
	{
        #region  Windows Form Designer generated code
		internal System.Windows.Forms.MenuStrip msMain;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewExpandAllFolders;
		internal System.Windows.Forms.ToolStripMenuItem mMenViewCollapseAllFolders;
		internal System.Windows.Forms.ToolStripMenuItem mMenSort;
		internal System.Windows.Forms.ToolStripMenuItem mMenAddConnection;
		internal System.Windows.Forms.ToolStripMenuItem mMenAddFolder;
		public System.Windows.Forms.TreeView tvConnections;
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mMenAddConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenAddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewExpandAllFolders = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenViewCollapseAllFolders = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenSort = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenFavorites = new System.Windows.Forms.ToolStripMenuItem();
            this.vsToolStripExtender = new WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender(this.components);
            this.pbSearch = new mRemoteNG.UI.Controls.MrngPictureBox(this.components);
            this.txtSearch = new mRemoteNG.UI.Controls.MrngSearchBox();
            this.searchBoxLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).BeginInit();
            this.searchBoxLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenAddConnection,
            this.mMenAddFolder,
            this.mMenViewExpandAllFolders,
            this.mMenViewCollapseAllFolders,
            this.mMenSort,
            this.mMenFavorites});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.msMain.ShowItemToolTips = true;
            this.msMain.Size = new System.Drawing.Size(204, 24);
            this.msMain.TabIndex = 10;
            this.msMain.Text = "MenuStrip1";
            // 
            // mMenAddConnection
            // 
            this.mMenAddConnection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mMenAddConnection.Name = "mMenAddConnection";
            this.mMenAddConnection.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.mMenAddConnection.Size = new System.Drawing.Size(8, 20);
            this.mMenAddConnection.Click += new System.EventHandler(this.CMenTreeAddConnection_Click);

            // 
            // mMenAddFolder
            // 
            this.mMenAddFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mMenAddFolder.Name = "mMenAddFolder";
            this.mMenAddFolder.Size = new System.Drawing.Size(12, 20);
            this.mMenAddFolder.Click += new System.EventHandler(this.CMenTreeAddFolder_Click);
            // 
            // mMenViewExpandAllFolders
            // 
            this.mMenViewExpandAllFolders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mMenViewExpandAllFolders.Name = "mMenViewExpandAllFolders";
            this.mMenViewExpandAllFolders.Size = new System.Drawing.Size(12, 20);
            this.mMenViewExpandAllFolders.Text = "Expand all folders";
            // 
            // mMenViewCollapseAllFolders
            // 
            this.mMenViewCollapseAllFolders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mMenViewCollapseAllFolders.Name = "mMenViewCollapseAllFolders";
            this.mMenViewCollapseAllFolders.Size = new System.Drawing.Size(12, 20);
            this.mMenViewCollapseAllFolders.Text = "Collapse all folders";
            // 
            // mMenSort
            // 
            this.mMenSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mMenSort.Name = "mMenSort";
            this.mMenSort.Size = new System.Drawing.Size(12, 20);
            // 
            // mMenFavorites
            // 
            this.mMenFavorites.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mMenFavorites.Name = "mMenFavorites";
            this.mMenFavorites.Size = new System.Drawing.Size(12, 20);
            this.mMenFavorites.Text = "Favorites";
            // 
            // vsToolStripExtender
            // 
            this.vsToolStripExtender.DefaultRenderer = null;
            // 
            // pbSearch
            // 
            this.pbSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbSearch.Location = new System.Drawing.Point(0, 0);
            this.pbSearch.Margin = new System.Windows.Forms.Padding(0);
            this.pbSearch.Name = "pbSearch";
            this.pbSearch.Size = new System.Drawing.Size(26, 21);
            this.pbSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSearch.TabIndex = 1;
            this.pbSearch.TabStop = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtSearch.Location = new System.Drawing.Point(26, 3);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(178, 15);
            this.txtSearch.TabIndex = 30;
            this.txtSearch.TabStop = false;
            this.txtSearch.Text = "Search";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // searchBoxLayoutPanel
            // 
            this.searchBoxLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
            this.searchBoxLayoutPanel.ColumnCount = 2;
            this.searchBoxLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.searchBoxLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.searchBoxLayoutPanel.Controls.Add(this.pbSearch, 0, 0);
            this.searchBoxLayoutPanel.Controls.Add(this.txtSearch);
            this.searchBoxLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchBoxLayoutPanel.Location = new System.Drawing.Point(0, 490);
            this.searchBoxLayoutPanel.Name = "searchBoxLayoutPanel";
            this.searchBoxLayoutPanel.RowCount = 1;
            this.searchBoxLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.searchBoxLayoutPanel.Size = new System.Drawing.Size(204, 21);
            this.searchBoxLayoutPanel.TabIndex = 32;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.msMain);
            this.splitContainer1.Panel1MinSize = 200;
            this.splitContainer1.Size = new System.Drawing.Size(204, 511);
            this.splitContainer1.SplitterDistance = 255;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 33;
            // 
            // ConnectionTreeWindow_v2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(204, 511);
            this.Controls.Add(this.searchBoxLayoutPanel);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "ConnectionTreeWindow_v2";
            this.TabText = "Connections";
            this.Text = "Connections";
            this.Load += new System.EventHandler(this.Tree_Load);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).EndInit();
            this.searchBoxLayoutPanel.ResumeLayout(false);
            this.searchBoxLayoutPanel.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
        #endregion

        private System.ComponentModel.IContainer components;
        private WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender vsToolStripExtender;
        internal MrngPictureBox pbSearch;
        internal Controls.MrngSearchBox txtSearch;
        public System.Windows.Forms.TableLayoutPanel searchBoxLayoutPanel;
        internal System.Windows.Forms.ToolStripMenuItem mMenFavorites;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
