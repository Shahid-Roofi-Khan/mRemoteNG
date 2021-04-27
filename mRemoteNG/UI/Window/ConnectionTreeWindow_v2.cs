using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using mRemoteNG.App;
using mRemoteNG.Config.Connections;
using mRemoteNG.Connection;
using mRemoteNG.Container;
using mRemoteNG.Properties;
using mRemoteNG.Resources.Language;
using mRemoteNG.Themes;
using mRemoteNG.Tree;
using mRemoteNG.Tree.ClickHandlers;
using mRemoteNG.Tree.Root;
using mRemoteNG.UI.Controls.ConnectionTree;
using mRemoteNG.UI.TaskDialog;
using WeifenLuo.WinFormsUI.Docking;

// ReSharper disable ArrangeAccessorOwnerBody

namespace mRemoteNG.UI.Window
{
    public partial class ConnectionTreeWindow_v2
    {
        private readonly IConnectionInitiator _connectionInitiator = new ConnectionInitiator();
        private ThemeManager _themeManager;
        private bool _sortedAz = true;

        public ConnectionInfo SelectedNode => ConnectionTree.SelectedNode;

        public ConnectionTree ConnectionTree { get; set; }

        public ConnectionTreeWindow_v2() : this(new DockContent())
        {
        }

        public ConnectionTreeWindow_v2(DockContent panel)
        {
            WindowType = WindowType.Tree;
            DockPnl = panel;

            // -------------------------------------------- Shahid Changes: need to bring this code from designer to here because VS 2019 removing these lines from there

   

            InitializeComponent();

            this.ConnectionTree = new ConnectionTree();
            ((System.ComponentModel.ISupportInitialize)(this.ConnectionTree)).BeginInit();
          
            this.ConnectionTree.SuspendLayout();
            this.msMain.SuspendLayout();

            // ------------------------------- Shahid changes

            mRemoteNG.Tree.ConnectionTreeModel connectionTreeModel2 = new mRemoteNG.Tree.ConnectionTreeModel();
            TreeNodeCompositeClickHandler treeNodeCompositeClickHandler3 = new TreeNodeCompositeClickHandler();
            mRemoteNG.Tree.AlwaysConfirmYes alwaysConfirmYes2 = new mRemoteNG.Tree.AlwaysConfirmYes();
            TreeNodeCompositeClickHandler treeNodeCompositeClickHandler4 = new TreeNodeCompositeClickHandler();

            this.splitContainer1.Panel1.ClientSize = new System.Drawing.Size(204, 411);
            
            this.splitContainer1.Panel1.Controls.Add(ConnectionTree);
            this.splitContainer1.Panel1.Controls.Add(msMain);

            // 
            // olvConnections
            // 
            this.ConnectionTree.AllowDrop = true;
            this.ConnectionTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConnectionTree.CellEditUseWholeCell = false;
            this.ConnectionTree.ConnectionTreeModel = connectionTreeModel2;
            this.ConnectionTree.Cursor = System.Windows.Forms.Cursors.Default;
            this.ConnectionTree.Dock = System.Windows.Forms.DockStyle.Fill;
            treeNodeCompositeClickHandler3.ClickHandlers = new ITreeNodeClickHandler<mRemoteNG.Connection.ConnectionInfo>[0];
            this.ConnectionTree.DoubleClickHandler = treeNodeCompositeClickHandler3;
            this.ConnectionTree.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectionTree.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ConnectionTree.HideSelection = false;
            this.ConnectionTree.IsSimpleDragSource = true;
            this.ConnectionTree.LabelEdit = true;
            this.ConnectionTree.Location = new System.Drawing.Point(0, 24);
            this.ConnectionTree.MultiSelect = false;
            this.ConnectionTree.Name = "ConnectionTree";
            this.ConnectionTree.NodeDeletionConfirmer = alwaysConfirmYes2;
            this.ConnectionTree.PostSetupActions = new IConnectionTreeDelegate[0];
            this.ConnectionTree.SelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.ConnectionTree.SelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.ConnectionTree.ShowGroups = false;
            treeNodeCompositeClickHandler4.ClickHandlers = new ITreeNodeClickHandler<mRemoteNG.Connection.ConnectionInfo>[0];
            this.ConnectionTree.SingleClickHandler = treeNodeCompositeClickHandler4;
            this.ConnectionTree.Size = new System.Drawing.Size(204, 366);
            this.ConnectionTree.TabIndex = 20;
            this.ConnectionTree.UnfocusedSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.ConnectionTree.UnfocusedSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.ConnectionTree.UseCompatibleStateImageBehavior = false;
            this.ConnectionTree.UseOverlays = false;
            this.ConnectionTree.View = System.Windows.Forms.View.Details;
            this.ConnectionTree.VirtualMode = true;

            this.Icon = global::mRemoteNG.Properties.Resources.Root_Icon;

            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)(this.ConnectionTree)).EndInit();
            this.ConnectionTree.ResumeLayout(false);
            this.ConnectionTree.PerformLayout();

            this.ResumeLayout(false);
            this.PerformLayout();

            this.mMenAddConnection.Image = global::mRemoteNG.Properties.Resources.Connection_Add;
            this.mMenAddFolder.Image = global::mRemoteNG.Properties.Resources.Folder_Add;
            this.mMenViewExpandAllFolders.Image = global::mRemoteNG.Properties.Resources.Expand;
            this.mMenViewCollapseAllFolders.Image = global::mRemoteNG.Properties.Resources.Collapse;
            this.mMenSort.Image = global::mRemoteNG.Properties.Resources.Sort_AZ;
            this.mMenFavorites.Image = global::mRemoteNG.Properties.Resources.star;

            

            SetMenuEventHandlers();
            SetConnectionTreeEventHandlers();
            Settings.Default.PropertyChanged += OnAppSettingsChanged;
            ApplyLanguage();
        }

        private void OnAppSettingsChanged(object o, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(Settings.UseFilterSearch))
            {
                ConnectionTree.UseFiltering = Settings.Default.UseFilterSearch;
                ApplyFiltering();
            }

            PlaceSearchBar(Settings.Default.PlaceSearchBarAboveConnectionTree);
            SetConnectionTreeClickHandlers();
        }

        private void PlaceSearchBar(bool placeSearchBarAboveConnectionTree)
        {
            searchBoxLayoutPanel.Dock = placeSearchBarAboveConnectionTree ? DockStyle.Top : DockStyle.Bottom;
        }


        #region Form Stuff

        private void Tree_Load(object sender, EventArgs e)
        {
            //work on the theme change
            _themeManager = ThemeManager.getInstance();
            _themeManager.ThemeChanged += ApplyTheme;
            ApplyTheme();

            txtSearch.Multiline = true;
            txtSearch.MinimumSize = new Size(0, 14);
            txtSearch.Size = new Size(txtSearch.Size.Width, 14);
            txtSearch.Multiline = false;
        }

        private void ApplyLanguage()
        {
            Text = Language.Connections;
            TabText = Language.Connections;

            mMenAddConnection.ToolTipText = Language.NewConnection;
            mMenAddFolder.ToolTipText = Language.NewFolder;
            mMenViewExpandAllFolders.ToolTipText = Language.ExpandAllFolders;
            mMenViewCollapseAllFolders.ToolTipText = Language.CollapseAllFolders;
            mMenSort.ToolTipText = Language.Sort;
            mMenFavorites.ToolTipText = Language.Favorites;

            txtSearch.Text = Language.SearchPrompt;
        }

        private new void ApplyTheme()
        {
            if (!_themeManager.ThemingActive)
                return;

            var activeTheme = _themeManager.ActiveTheme;
            vsToolStripExtender.SetStyle(msMain, activeTheme.Version, activeTheme.Theme);
            vsToolStripExtender.SetStyle(ConnectionTree.ContextMenuStrip, activeTheme.Version,
                activeTheme.Theme);

            if (!_themeManager.ActiveAndExtended)
                return;

            // connection search area
            searchBoxLayoutPanel.BackColor = activeTheme.ExtendedPalette.getColor("Dialog_Background");
            searchBoxLayoutPanel.ForeColor = activeTheme.ExtendedPalette.getColor("Dialog_Foreground");
            txtSearch.BackColor = activeTheme.ExtendedPalette.getColor("TextBox_Background");
            txtSearch.ForeColor = activeTheme.ExtendedPalette.getColor("TextBox_Foreground");
            //Picturebox needs to be manually themed
            pbSearch.BackColor = activeTheme.ExtendedPalette.getColor("TreeView_Background");
        }

        #endregion

        #region ConnectionTree

        private void SetConnectionTreeEventHandlers()
        {
            ConnectionTree.NodeDeletionConfirmer =
                new SelectedConnectionDeletionConfirmer(prompt => CTaskDialog.MessageBox(
                    Application.ProductName,prompt,"",ETaskDialogButtons.YesNo,ESysIcons.Question));
            ConnectionTree.KeyDown += TvConnections_KeyDown;
            ConnectionTree.KeyPress += TvConnections_KeyPress;
            SetTreePostSetupActions();
            SetConnectionTreeClickHandlers();
            Runtime.ConnectionsService.ConnectionsLoaded += ConnectionsServiceOnConnectionsLoaded;
        }

        private void SetTreePostSetupActions()
        {
            var actions = new List<IConnectionTreeDelegate>
            {
                new PreviouslyOpenedFolderExpander(),
                new RootNodeExpander()
            };

            if (Settings.Default.OpenConsFromLastSession && !Settings.Default.NoReconnect)
                actions.Add(new PreviousSessionOpener(_connectionInitiator));

            ConnectionTree.PostSetupActions = actions;
        }

        private void SetConnectionTreeClickHandlers()
        {
            var singleClickHandlers = new List<ITreeNodeClickHandler<ConnectionInfo>>();
            var doubleClickHandlers = new List<ITreeNodeClickHandler<ConnectionInfo>>
            {
                new ExpandNodeClickHandler(ConnectionTree)
            };

            if (Settings.Default.SingleClickOnConnectionOpensIt)
                singleClickHandlers.Add(new OpenConnectionClickHandler(_connectionInitiator));
            else
                doubleClickHandlers.Add(new OpenConnectionClickHandler(_connectionInitiator));

            if (Settings.Default.SingleClickSwitchesToOpenConnection)
                singleClickHandlers.Add(new SwitchToConnectionClickHandler(_connectionInitiator));

            ConnectionTree.SingleClickHandler = new TreeNodeCompositeClickHandler {ClickHandlers = singleClickHandlers};
            ConnectionTree.DoubleClickHandler = new TreeNodeCompositeClickHandler {ClickHandlers = doubleClickHandlers};
        }

        private void ConnectionsServiceOnConnectionsLoaded(object o, ConnectionsLoadedEventArgs connectionsLoadedEventArgs)
        {
            if (ConnectionTree.InvokeRequired)
            {
                ConnectionTree.Invoke(() => ConnectionsServiceOnConnectionsLoaded(o, connectionsLoadedEventArgs));
                return;
            }

            ConnectionTree.ConnectionTreeModel = connectionsLoadedEventArgs.NewConnectionTreeModel;
            ConnectionTree.SelectedObject =
                connectionsLoadedEventArgs.NewConnectionTreeModel.RootNodes.OfType<RootNodeInfo>().FirstOrDefault();
        }

        #endregion

        #region Top Menu

        private void SetMenuEventHandlers()
        {
            mMenViewExpandAllFolders.Click += (sender, args) => ConnectionTree.ExpandAll();
            mMenViewCollapseAllFolders.Click += (sender, args) =>
            {
                ConnectionTree.CollapseAll();
                ConnectionTree.Expand(ConnectionTree.GetRootConnectionNode());
            };
            mMenSort.Click += (sender, args) =>
            {
                if (_sortedAz)
                {
                    ConnectionTree.SortRecursive(ConnectionTree.GetRootConnectionNode(), ListSortDirection.Ascending);
                    mMenSort.Image = Properties.Resources.Sort_ZA;
                    _sortedAz = false;
                }
                else
                {
                    ConnectionTree.SortRecursive(ConnectionTree.GetRootConnectionNode(), ListSortDirection.Descending);
                    mMenSort.Image = Properties.Resources.Sort_AZ;
                    _sortedAz = true;
                }
            };
            mMenFavorites.Click += (sender, args) =>
            {
                mMenFavorites.DropDownItems.Clear();
                var rootNodes = Runtime.ConnectionsService.ConnectionTreeModel.RootNodes;
                var favoritesList = new List<ToolStripMenuItem>();

                foreach (var node in rootNodes)
                {
                    foreach (var containerInfo in Runtime.ConnectionsService.ConnectionTreeModel.GetRecursiveFavoriteChildList(node))
                    {
                        var favoriteMenuItem = new ToolStripMenuItem
                        {
                            Text = containerInfo.Name,
                            Tag = containerInfo,
                            Image = containerInfo.OpenConnections.Count > 0 ? Properties.Resources.Play : Properties.Resources.Pause
                        };
                        favoriteMenuItem.MouseUp += FavoriteMenuItem_MouseUp;
                        favoritesList.Add(favoriteMenuItem);
                    }
                }

                mMenFavorites.DropDownItems.AddRange(favoritesList.ToArray());
                mMenFavorites.ShowDropDown();
            };
        }

        private void FavoriteMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (((ToolStripMenuItem)sender).Tag is ContainerInfo) return;
            _connectionInitiator.OpenConnection((ConnectionInfo)((ToolStripMenuItem)sender).Tag);
        }

        #endregion

        #region Tree Context Menu

        private void CMenTreeAddConnection_Click(object sender, EventArgs e)
        {
            ConnectionTree.AddConnection();
        }

        private void CMenTreeAddFolder_Click(object sender, EventArgs e)
        {
            ConnectionTree.AddFolder();
        }

        #endregion

        #region Search

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        e.Handled = true;
                        ConnectionTree.Focus();
                        break;
                    case Keys.Up:
                    {
                        var match = ConnectionTree.NodeSearcher.PreviousMatch();
                        JumpToNode(match);
                        e.Handled = true;
                        break;
                    }
                    case Keys.Down:
                    {
                        var match = ConnectionTree.NodeSearcher.NextMatch();
                        JumpToNode(match);
                        e.Handled = true;
                        break;
                    }
                    default:
                        TvConnections_KeyDown(sender, e);
                        break;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddExceptionStackTrace("txtSearch_KeyDown (UI.Window.ConnectionTreeWindow) failed", ex);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFiltering();
        }

        private void ApplyFiltering()
        {
            if (Settings.Default.UseFilterSearch)
            {
                if (txtSearch.Text == "" || txtSearch.Text == Language.SearchPrompt)
                {
                    ConnectionTree.RemoveFilter();
                    return;
                }

                ConnectionTree.ApplyFilter(txtSearch.Text);
            }
            else
            {
                if (txtSearch.Text == "") return;
                ConnectionTree.NodeSearcher?.SearchByName(txtSearch.Text);
                JumpToNode(ConnectionTree.NodeSearcher?.CurrentMatch);
            }
        }

        public void JumpToNode(ConnectionInfo connectionInfo)
        {
            if (connectionInfo == null)
            {
                ConnectionTree.SelectedObject = null;
                return;
            }

            ExpandParentsRecursive(connectionInfo);
            ConnectionTree.SelectObject(connectionInfo);
            ConnectionTree.EnsureModelVisible(connectionInfo);
        }

        private void ExpandParentsRecursive(ConnectionInfo connectionInfo)
        {
            while (true)
            {
                if (connectionInfo?.Parent == null) return;
                ConnectionTree.Expand(connectionInfo.Parent);
                connectionInfo = connectionInfo.Parent;
            }
        }

        private void TvConnections_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsLetterOrDigit(e.KeyChar)) return;
                txtSearch.Focus();
                txtSearch.Text = e.KeyChar.ToString();
                txtSearch.SelectionStart = txtSearch.TextLength;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddExceptionStackTrace("tvConnections_KeyPress (UI.Window.ConnectionTreeWindow) failed", ex);
            }
        }

        private void TvConnections_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    if (SelectedNode == null)
                        return;
                    _connectionInitiator.OpenConnection(SelectedNode);
                }
                else if (e.Control && e.KeyCode == Keys.F)
                {
                    txtSearch.Focus();
                    txtSearch.SelectAll();
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddExceptionStackTrace("tvConnections_KeyDown (UI.Window.ConnectionTreeWindow) failed", ex);
            }
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            string out1 = "";
            try
            {
                out1 = this.ConnectionTree.SelectedNode.ConnectionCurrentState.ToString();
            }
            catch
            {
                out1 = "exception";
            }

            textBox1.Text = out1;

            this.ConnectionTree.PerformLayout();
            this.ConnectionTree.Refresh();
            //this.PerformLayout();

        }
    }
}