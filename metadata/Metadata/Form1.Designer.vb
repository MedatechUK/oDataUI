<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SortAscendingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SortDescendingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NoSortToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ParseURLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyURLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.RefreshF5ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip3 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.MainPanel = New System.Windows.Forms.Panel()
        Me.TreeSplit = New System.Windows.Forms.SplitContainer()
        Me.TreeView = New System.Windows.Forms.TreeView()
        Me.ContextMenuStrip4 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.FindToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitURL = New System.Windows.Forms.SplitContainer()
        Me.txtUrl = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.splitColumns = New System.Windows.Forms.SplitContainer()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.EnvironmentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadEnvironmentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateEnvironmentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FindToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.CloseToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.autocompleteMenu1 = New AutocompleteMenuNS.AutocompleteMenu()
        Me.OpenToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.ContextMenuStrip3.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.MainPanel.SuspendLayout()
        CType(Me.TreeSplit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TreeSplit.Panel1.SuspendLayout()
        Me.TreeSplit.Panel2.SuspendLayout()
        Me.TreeSplit.SuspendLayout()
        Me.ContextMenuStrip4.SuspendLayout()
        CType(Me.SplitURL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitURL.Panel1.SuspendLayout()
        Me.SplitURL.Panel2.SuspendLayout()
        Me.SplitURL.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.splitColumns, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitColumns.Panel1.SuspendLayout()
        Me.splitColumns.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "priority.ico")
        Me.ImageList1.Images.SetKeyName(1, "l1.png")
        Me.ImageList1.Images.SetKeyName(2, "l2.png")
        Me.ImageList1.Images.SetKeyName(3, "l3.png")
        Me.ImageList1.Images.SetKeyName(4, "l4.png")
        Me.ImageList1.Images.SetKeyName(5, "l5.png")
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SortAscendingToolStripMenuItem, Me.SortDescendingToolStripMenuItem, Me.NoSortToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(161, 70)
        '
        'SortAscendingToolStripMenuItem
        '
        Me.SortAscendingToolStripMenuItem.Name = "SortAscendingToolStripMenuItem"
        Me.SortAscendingToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.SortAscendingToolStripMenuItem.Text = "Sort Ascending"
        '
        'SortDescendingToolStripMenuItem
        '
        Me.SortDescendingToolStripMenuItem.Name = "SortDescendingToolStripMenuItem"
        Me.SortDescendingToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.SortDescendingToolStripMenuItem.Text = "Sort Descending"
        '
        'NoSortToolStripMenuItem
        '
        Me.NoSortToolStripMenuItem.Name = "NoSortToolStripMenuItem"
        Me.NoSortToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.NoSortToolStripMenuItem.Text = "No Sort"
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem1, Me.OpenToolStripMenuItem1, Me.ToolStripMenuItem3, Me.ParseURLToolStripMenuItem, Me.CopyURLToolStripMenuItem, Me.ToolStripMenuItem1, Me.RefreshF5ToolStripMenuItem})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(181, 148)
        '
        'ParseURLToolStripMenuItem
        '
        Me.ParseURLToolStripMenuItem.Name = "ParseURLToolStripMenuItem"
        Me.ParseURLToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.ParseURLToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.ParseURLToolStripMenuItem.Text = "Paste URL"
        '
        'CopyURLToolStripMenuItem
        '
        Me.CopyURLToolStripMenuItem.Name = "CopyURLToolStripMenuItem"
        Me.CopyURLToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyURLToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.CopyURLToolStripMenuItem.Text = "Copy URL"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(165, 6)
        '
        'RefreshF5ToolStripMenuItem
        '
        Me.RefreshF5ToolStripMenuItem.Name = "RefreshF5ToolStripMenuItem"
        Me.RefreshF5ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefreshF5ToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.RefreshF5ToolStripMenuItem.Text = "Refresh"
        '
        'ContextMenuStrip3
        '
        Me.ContextMenuStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshToolStripMenuItem})
        Me.ContextMenuStrip3.Name = "ContextMenuStrip3"
        Me.ContextMenuStrip3.Size = New System.Drawing.Size(114, 26)
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.MainPanel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.StatusStrip1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.MenuStrip1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1145, 648)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'MainPanel
        '
        Me.MainPanel.Controls.Add(Me.TreeSplit)
        Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainPanel.Location = New System.Drawing.Point(3, 27)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.Padding = New System.Windows.Forms.Padding(2)
        Me.MainPanel.Size = New System.Drawing.Size(1139, 596)
        Me.MainPanel.TabIndex = 6
        '
        'TreeSplit
        '
        Me.TreeSplit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeSplit.Location = New System.Drawing.Point(2, 2)
        Me.TreeSplit.Name = "TreeSplit"
        '
        'TreeSplit.Panel1
        '
        Me.TreeSplit.Panel1.Controls.Add(Me.TreeView)
        '
        'TreeSplit.Panel2
        '
        Me.TreeSplit.Panel2.Controls.Add(Me.SplitURL)
        Me.TreeSplit.Size = New System.Drawing.Size(1135, 592)
        Me.TreeSplit.SplitterDistance = 373
        Me.TreeSplit.TabIndex = 2
        '
        'TreeView
        '
        Me.TreeView.CheckBoxes = True
        Me.TreeView.ContextMenuStrip = Me.ContextMenuStrip4
        Me.TreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!)
        Me.TreeView.ForeColor = System.Drawing.Color.Blue
        Me.TreeView.ImageIndex = 0
        Me.TreeView.ImageList = Me.ImageList1
        Me.TreeView.LineColor = System.Drawing.Color.Blue
        Me.TreeView.Location = New System.Drawing.Point(0, 0)
        Me.TreeView.Name = "TreeView"
        Me.TreeView.SelectedImageIndex = 0
        Me.TreeView.Size = New System.Drawing.Size(373, 592)
        Me.TreeView.TabIndex = 1
        '
        'ContextMenuStrip4
        '
        Me.ContextMenuStrip4.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FindToolStripMenuItem})
        Me.ContextMenuStrip4.Name = "ContextMenuStrip4"
        Me.ContextMenuStrip4.Size = New System.Drawing.Size(138, 26)
        '
        'FindToolStripMenuItem
        '
        Me.FindToolStripMenuItem.Name = "FindToolStripMenuItem"
        Me.FindToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.FindToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.FindToolStripMenuItem.Text = "Find"
        '
        'SplitURL
        '
        Me.SplitURL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitURL.Location = New System.Drawing.Point(0, 0)
        Me.SplitURL.Name = "SplitURL"
        Me.SplitURL.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitURL.Panel1
        '
        Me.SplitURL.Panel1.Controls.Add(Me.txtUrl)
        '
        'SplitURL.Panel2
        '
        Me.SplitURL.Panel2.Controls.Add(Me.TabControl1)
        Me.SplitURL.Size = New System.Drawing.Size(758, 592)
        Me.SplitURL.SplitterDistance = 93
        Me.SplitURL.TabIndex = 5
        '
        'txtUrl
        '
        Me.txtUrl.AllowDrop = True
        Me.autocompleteMenu1.SetAutocompleteMenu(Me.txtUrl, Nothing)
        Me.txtUrl.ContextMenuStrip = Me.ContextMenuStrip2
        Me.txtUrl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtUrl.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUrl.ForeColor = System.Drawing.Color.Blue
        Me.txtUrl.Location = New System.Drawing.Point(0, 0)
        Me.txtUrl.Multiline = True
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.ReadOnly = True
        Me.txtUrl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtUrl.Size = New System.Drawing.Size(758, 93)
        Me.txtUrl.TabIndex = 2
        '
        'TabControl1
        '
        Me.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(758, 495)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.splitColumns)
        Me.TabPage1.Location = New System.Drawing.Point(4, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(750, 464)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Query"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'splitColumns
        '
        Me.splitColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitColumns.Location = New System.Drawing.Point(3, 3)
        Me.splitColumns.Name = "splitColumns"
        Me.splitColumns.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitColumns.Panel1
        '
        Me.splitColumns.Panel1.Controls.Add(Me.ListView1)
        Me.splitColumns.Size = New System.Drawing.Size(744, 458)
        Me.splitColumns.SplitterDistance = 344
        Me.splitColumns.TabIndex = 5
        '
        'ListView1
        '
        Me.ListView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView1.ForeColor = System.Drawing.Color.Blue
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(744, 344)
        Me.ListView1.TabIndex = 1
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'TabPage2
        '
        Me.TabPage2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.TabPage2.Location = New System.Drawing.Point(4, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(750, 464)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Result"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 626)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1145, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(100, 16)
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EnvironmentToolStripMenuItem, Me.FileToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1145, 24)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'EnvironmentToolStripMenuItem
        '
        Me.EnvironmentToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadEnvironmentToolStripMenuItem, Me.CreateEnvironmentToolStripMenuItem, Me.SettingsToolStripMenuItem, Me.FindToolStripMenuItem1, Me.ToolStripMenuItem2, Me.CloseToolStripMenuItem1})
        Me.EnvironmentToolStripMenuItem.Name = "EnvironmentToolStripMenuItem"
        Me.EnvironmentToolStripMenuItem.Size = New System.Drawing.Size(87, 20)
        Me.EnvironmentToolStripMenuItem.Text = "&Environment"
        '
        'LoadEnvironmentToolStripMenuItem
        '
        Me.LoadEnvironmentToolStripMenuItem.Name = "LoadEnvironmentToolStripMenuItem"
        Me.LoadEnvironmentToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.LoadEnvironmentToolStripMenuItem.Text = "&Open"
        '
        'CreateEnvironmentToolStripMenuItem
        '
        Me.CreateEnvironmentToolStripMenuItem.Name = "CreateEnvironmentToolStripMenuItem"
        Me.CreateEnvironmentToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.CreateEnvironmentToolStripMenuItem.Text = "&New"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Enabled = False
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'FindToolStripMenuItem1
        '
        Me.FindToolStripMenuItem1.Name = "FindToolStripMenuItem1"
        Me.FindToolStripMenuItem1.Size = New System.Drawing.Size(180, 22)
        Me.FindToolStripMenuItem1.Text = "F&ind"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(177, 6)
        '
        'CloseToolStripMenuItem1
        '
        Me.CloseToolStripMenuItem1.Name = "CloseToolStripMenuItem1"
        Me.CloseToolStripMenuItem1.Size = New System.Drawing.Size(180, 22)
        Me.CloseToolStripMenuItem1.Text = "&Close"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.PasteToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(51, 20)
        Me.FileToolStripMenuItem.Text = "&Query"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Enabled = False
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.OpenToolStripMenuItem.Text = "&Open"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Enabled = False
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.SaveToolStripMenuItem.Text = "&Save"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Enabled = False
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.PasteToolStripMenuItem.Text = "Paste"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'autocompleteMenu1
        '
        Me.autocompleteMenu1.Colors = CType(resources.GetObject("autocompleteMenu1.Colors"), AutocompleteMenuNS.Colors)
        Me.autocompleteMenu1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.autocompleteMenu1.ImageList = Nothing
        Me.autocompleteMenu1.Items = New String() {"abc", "abcd", "abcde", "abcdef"}
        Me.autocompleteMenu1.TargetControlWrapper = Nothing
        '
        'OpenToolStripMenuItem1
        '
        Me.OpenToolStripMenuItem1.Name = "OpenToolStripMenuItem1"
        Me.OpenToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenToolStripMenuItem1.Size = New System.Drawing.Size(180, 22)
        Me.OpenToolStripMenuItem1.Text = "Open"
        '
        'SaveToolStripMenuItem1
        '
        Me.SaveToolStripMenuItem1.Name = "SaveToolStripMenuItem1"
        Me.SaveToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem1.Size = New System.Drawing.Size(168, 22)
        Me.SaveToolStripMenuItem1.Text = "Save"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(165, 6)
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1145, 648)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "oData Query UI"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ContextMenuStrip3.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.MainPanel.ResumeLayout(False)
        Me.TreeSplit.Panel1.ResumeLayout(False)
        Me.TreeSplit.Panel2.ResumeLayout(False)
        CType(Me.TreeSplit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TreeSplit.ResumeLayout(False)
        Me.ContextMenuStrip4.ResumeLayout(False)
        Me.SplitURL.Panel1.ResumeLayout(False)
        Me.SplitURL.Panel1.PerformLayout()
        Me.SplitURL.Panel2.ResumeLayout(False)
        CType(Me.SplitURL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitURL.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.splitColumns.Panel1.ResumeLayout(False)
        CType(Me.splitColumns, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitColumns.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SortAscendingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SortDescendingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NoSortToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents CopyURLToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip3 As ContextMenuStrip
    Friend WithEvents RefreshToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ParseURLToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MainPanel As Panel
    Friend WithEvents TreeSplit As SplitContainer
    Friend WithEvents TreeView As TreeView
    Friend WithEvents SplitURL As SplitContainer
    Friend WithEvents txtUrl As TextBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents splitColumns As SplitContainer
    Friend WithEvents ListView1 As ListView
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Private WithEvents autocompleteMenu1 As AutocompleteMenuNS.AutocompleteMenu
    Friend WithEvents ToolStripProgressBar1 As ToolStripProgressBar
    Friend WithEvents EnvironmentToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CreateEnvironmentToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadEnvironmentToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents CloseToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents RefreshF5ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip4 As ContextMenuStrip
    Friend WithEvents FindToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FindToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As ToolStripSeparator
End Class
