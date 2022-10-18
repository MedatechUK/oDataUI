Option Strict Off

Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Xml.Xsl
Imports MedatechUK.Deserialiser
Imports MedatechUK.Logging
Imports metadata
Imports Newtonsoft.Json
Imports Formatting = Newtonsoft.Json.Formatting
Imports System.Text.RegularExpressions
Imports System.Web
Imports ScintillaNET
Imports System.ComponentModel

Public Class Form1

    Private lastURL As String = ""
    Private parsingURL As Boolean = False
    Private ed As Edmx

    Private _envfile As String = Nothing
    Public Property envfile As String
        Get
            If _envfile Is Nothing Then
                Return ""
            Else
                Return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _envfile)
            End If
        End Get
        Set(value As String)
            _envfile = value
        End Set
    End Property

    Public ReadOnly Property FriendlyName As String
        Get
            Return _envfile.Substring(0, _envfile.Length - 5)
        End Get
    End Property

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        SettxtFilter()
        settxtResult()

        autocompleteMenu1.TargetControlWrapper = New ScintillaWrapper(txtFilter)
        autocompleteMenu1.Colors.HighlightingColor = Color.LightBlue


    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = Not MsgBox("Close the application?", vbOKCancel, "Close?") = MsgBoxResult.Ok

    End Sub

#Region "Methods"

    Private Sub ReloadTree()

        Dim modnode As TreeNode

        txtUrl.Text = ""
        txtFilter.Text = ""
        ListView1.Items.Clear()

        With TreeView
            With .Nodes
                While .Count > 0
                    .RemoveAt(0)
                End While
                modnode = .Add(FriendlyName)

            End With
        End With

        PauseForm()

        Using ex As New AppExtension(AddressOf MedatechUK.Logging.Events.logHandler)
            With ex.LexByAssemblyName(GetType(Edmx).FullName)
                Using sr As New StreamReader(envfile)
                    ed = .Deserialise(sr)
                    With ToolStripProgressBar1
                        .Maximum = 1 + ed.DataServices.Schema.EntityType.Count * 2
                        .Value = 0
                    End With
                End Using
            End With
        End Using


        Dim m As New Dictionary(Of String, EdmxDataServicesSchemaEntityType)
        Dim ut = New EdmxDataServicesSchemaEntityType("Unknown")
        With ut.Node
            .ImageIndex = 1
            .SelectedImageIndex = 1
        End With
        m.Add("Unknown", ut)

        Dim modules As New Dictionary(Of String, String)
        Using sr As New StreamReader("MODULES.TXT")
            Do Until sr.EndOfStream
                Dim l As String = sr.ReadLine
                If InStr(l, vbTab) > 0 Then
                    Dim mn = Split(l, vbTab)
                    Dim t = New EdmxDataServicesSchemaEntityType(mn(1))
                    With t.Node
                        .ImageIndex = 1
                        .SelectedImageIndex = 1
                    End With
                    If Not m.Keys.Contains(mn(1)) Then
                        m.Add(mn(1), t)
                        ToolStripProgressBar1.Maximum += 1
                    End If
                    modules.Add(mn(0), mn(1))


                End If

            Loop

        End Using

        With ed.DataServices.Schema
            For Each et In .EntityType
                ToolStripProgressBar1.Value += 1
                With et.Node
                    .ImageIndex = 3
                    .SelectedImageIndex = 5
                End With

                For Each P In et.Property
                    P.Parent = et
                Next

                If modules.Keys.Contains(et.Name) Then
                    et.MODULENAME = modules(et.Name)
                End If

                et.Node.Text = et.Name

            Next

            For Each et In .EntityType
                ToolStripProgressBar1.Value += 1
                For Each nav In et.NavigationProperty
                    Dim child = .byType(nav.Type)
                    If Not child Is Nothing Then
                        Dim ch As TreeNode = child.Node.Clone
                        ch.Text = nav.Name
                        et.Node.Nodes.Add(ch)

                        child.isParent = False
                        child.Type = nav.Type

                    End If
                Next
            Next

            For Each et In .Parents
                With et.Node
                    .ImageIndex = 2
                    .SelectedImageIndex = 4

                End With
                m(et.MODULENAME).Node.Nodes.Add(et.Node)

            Next


            For Each mo In m.Values
                ToolStripProgressBar1.Value += 1
                If mo.Node.Nodes.Count > 0 And mo.MODULENAME <> "Unknown" Then _
                    modnode.Nodes.Add(mo.Node)
            Next
            modnode.Expand()

        End With

        ResumeForm()

    End Sub

    Public Sub RefreshURL(sender As Object, e As Object)

        If TreeView.Nodes.Count > 0 And Not parsingURL Then
            For Each n As TreeNode In TreeView.Nodes(0).Nodes
                For Each n2 As TreeNode In n.Nodes
                    If n2.Checked Then
                        txtUrl.Text = String.Format("https://{0}/odata/priority/{1}/{2}/{3}", ed.URL, ed.TabulaINI, ed.Environment, n2.Tag.url)
                        Exit Sub

                    End If
                Next
            Next
        End If

    End Sub

    Public Sub refreshResult()

        If txtUrl.Text.Length > 0 Then

            TabControl1.SelectedIndex = 1

            With txtResult
                .Clear()
                .Text = "Loading..."
                .Refresh()
            End With

            With ToolStripProgressBar1
                .Maximum = 100
                .Value = 50

            End With

            PauseForm()

            Using webClient As New System.Net.WebClient
                With webClient
                    .Credentials = New NetworkCredential(ed.Username, ed.Password)

                    AddHandler .DownloadProgressChanged, AddressOf dlProgress
                    AddHandler .DownloadStringCompleted, AddressOf dlcomplete

                    .DownloadStringAsync(New Uri(txtUrl.Text))

                End With

            End Using

        End If

    End Sub

    Private Sub dlProgress(sender As Object, e As DownloadProgressChangedEventArgs)
        ToolStripProgressBar1.Value = e.ProgressPercentage

    End Sub

    Private Sub dlcomplete(sender As Object, e As DownloadStringCompletedEventArgs)

        ResumeForm()

        Dim dl As String = Nothing
        Try
            dl = e.Result
        Catch : End Try

        If Not dl Is Nothing Then
            Using stringReader = New StringReader(e.Result)
                Using stringWriter = New StringWriter()
                    Dim jsonReader = New JsonTextReader(stringReader)
                    Dim jsonWriter = New JsonTextWriter(stringWriter) With {
                        .Formatting = Formatting.Indented
                    }
                    txtResult.Clear()
                    Try
                        jsonWriter.WriteToken(jsonReader)
                        txtResult.Text = stringWriter.ToString()

                    Catch ex As Exception
                        txtResult.Text = e.Result

                    End Try

                End Using
            End Using

        Else

            Dim result As String = ""
            Dim readStream As New StreamReader(TryCast(e.Error, System.Net.WebException).Response.GetResponseStream, System.Text.Encoding.GetEncoding("utf-8"))
            Dim read(256) As [Char]

            ' Read 256 charcters at a time    .
            Dim count As Integer = readStream.Read(read, 0, 256)
            While count > 0
                ' Dump the 256 characters on a string and display the string onto the console.
                Dim str As New [String](read, 0, count)
                result += str
                count = readStream.Read(read, 0, 256)

            End While

            Using stringReader = New StringReader(result)
                Using stringWriter = New StringWriter()

                    Dim jsonReader = New JsonTextReader(stringReader)
                    Dim jsonWriter = New JsonTextWriter(stringWriter) With {
                        .Formatting = Formatting.Indented
                    }
                    txtResult.Clear()
                    Try
                        jsonWriter.WriteToken(jsonReader)
                        txtResult.Text = stringWriter.ToString()

                    Catch ex As Exception
                        txtResult.Text = result

                    End Try

                End Using
            End Using

        End If

    End Sub

    Public Sub ParseURL(Optional ByRef e As TreeNode = Nothing, Optional strVal As String = "")

        ListView1.Clear()
        txtUrl.Text = ""
        txtFilter.Text = ""
        TabControl1.SelectedIndex = 0

        Dim url As String = strVal
        Dim q As String = ""
        Dim frm As String = ""
        Dim a As TreeNode = Nothing

        If e Is Nothing Then
            url = strVal
            q = Split(url, "?")(1)
            frm = Split(Split(url, "?")(0), "/").Last

            With ToolStripProgressBar1
                .Maximum = q.Length
                .Value = 0
            End With

            PauseForm()

            For Each N In TreeView.Nodes(0).Nodes
                For Each C As TreeNode In N.nodes
                    If String.Compare(C.Tag.Name, frm) = 0 Then
                        a = C
                        untick(C)
                        C.Parent.Expand()
                        C.Expand()
                        C.Checked = True
                        parseChild(C, q)
                        Exit For
                    Else
                        untick(C)
                        C.Parent.Collapse()
                        C.Collapse()
                    End If
                Next
            Next

        Else

            frm = Split(Regex.Match(strVal, "^expand\=([A-Z_]+)").Value, "=")(1)
            q = strVal.Substring(String.Format("expand={0}", frm).Length)
            ToolStripProgressBar1.Value += String.Format("expand={0}", frm).Length

            For Each C As TreeNode In e.Nodes
                If String.Compare(C.Text, frm) = 0 Then
                    C.Checked = True
                    C.Expand()
                    parseChild(C, q)
                    Exit For
                Else
                    C.Collapse()
                End If
            Next

        End If

        If Not a Is Nothing Then
            TreeView.SelectedNode = a
            TreeView_AfterSelect(Me, New TreeViewEventArgs(a))

        End If

        ResumeForm()

    End Sub

    Sub PauseForm()
        Cursor = Cursors.WaitCursor
        MainPanel.Enabled = False

    End Sub

    Sub ResumeForm()
        MainPanel.Enabled = True
        Cursor = Cursors.Default
        ToolStripProgressBar1.Value = 0

    End Sub

    Sub parseChild(ByRef e As TreeNode, q As String)

rereg:

        Dim matchC As MatchCollection = Regex.Matches(q, "(?<=\$)(.*?)(?=\=)")

        For Each m As Match In matchC
            Select Case m.Value.ToUpper
                Case "SELECT"
                    Dim sel As String
                    If m.NextMatch.Index = 0 Then
                        sel = Split(q.Substring(0), "$SELECT=",, CompareMethod.Text)(1)
                    Else
                        sel = Split(q.Substring(0, m.NextMatch.Index - 1), "$SELECT=",, CompareMethod.Text)(1)

                    End If
                    ToolStripProgressBar1.Value += String.Format("$SELECT={0}", sel).Length
                    CleanString(sel)

                    Dim l As New List(Of String)
                    For Each col As String In Split(sel, ",")
                        l.Add(col)
                    Next
                    For Each p In TryCast(e.Tag, EdmxDataServicesSchemaEntityType).Property
                        p.lvi.Checked = l.Contains(p.Name)
                    Next

                Case "FILTER"
                    Dim sel As String
                    If m.NextMatch.Index = 0 Then
                        sel = Split(q.Substring(0), "$FILTER=",, CompareMethod.Text)(1)
                    Else
                        sel = Split(q.Substring(0, m.NextMatch.Index - 1), "$FILTER=",, CompareMethod.Text)(1)

                    End If
                    ToolStripProgressBar1.Value += String.Format("$FILTER={0}", sel).Length
                    CleanString(sel)
                    TryCast(e.Tag, EdmxDataServicesSchemaEntityType).filter = sel

                Case "ORDERBY"
                    Dim sel As String
                    If m.NextMatch.Index = 0 Then
                        sel = Split(q.Substring(0), "$ORDERBY=",, CompareMethod.Text)(1)

                    Else
                        sel = Split(q.Substring(0, m.NextMatch.Index - 1), "$ORDERBY=",, CompareMethod.Text)(1)

                    End If
                    ToolStripProgressBar1.Value += String.Format("$ORDERBY={0}", sel).Length
                    CleanString(sel)

                    Dim l As New Dictionary(Of String, String)
                    For Each col As String In Split(sel, ",")
                        l.Add(Split(col, " ")(0), Split(col, " ")(1))
                    Next
                    For Each p In TryCast(e.Tag, EdmxDataServicesSchemaEntityType).Property
                        If l.Keys.Contains(p.Name) Then
                            p.Sort = l(p.Name).ToUpper
                        End If

                    Next

                Case "EXPAND"
                    Dim r As String = q.Substring(m.Index)
                    If Regex.Match(r, "^expand\=[A-Z_]+\(").Success Then

                        Dim mi As Integer = 0
                        Dim mo As Integer = 0
                        Dim mc As Integer = 0
                        Do Until mo > 0 And mo = mc
                            Select Case r.Substring(mi, 1)
                                Case "("
                                    mo += 1
                                Case ")"
                                    mc += 1
                            End Select
                            mi += 1
                        Loop
                        ParseURL(e, r.Substring(0, mi - 1))
                        ToolStripProgressBar1.Value += mi - 1
                        q = r.Substring(mi)
                        If q.Length > 0 Then
                            If q.Substring(0, 1) = "," Then
                                q = String.Format("$expand={0}", q.Substring(1))

                            End If
                        End If

                        GoTo rereg

                    ElseIf Regex.Match(r, "^expand\=[A-Z_]+\,").Success Then
                        ParseURL(e, r.Substring(0, InStr(r, ",") - 1))
                        ToolStripProgressBar1.Value += InStr(r, ",")
                        q = r.Substring(InStr(r, ","))
                        q = String.Format("$expand={0}", q)

                        GoTo rereg

                    ElseIf Regex.Match(r, "^expand\=[A-Z_]+\&").Success Then
                        ParseURL(e, r.Substring(0, InStr(r, "&") - 1))
                        ToolStripProgressBar1.Value += InStr(r, ",")
                        q = r.Substring(InStr(r, "&"))
                        q = String.Format("$expand={0}", q)

                        GoTo rereg

                    End If

            End Select

        Next

    End Sub

    Private Sub CleanString(ByRef sel As String)
        Dim r As String = sel.Substring(sel.Length - 1, 1)
        While r = ";" Or r = "&"
            sel = sel.Substring(0, sel.Length - 1)
            r = sel.Substring(sel.Length - 1, 1)

        End While
    End Sub

    Private Function IsURL(str As String) As Boolean

        If Not Strings.Left(str, 4).ToLower = "http" Then Return False
        If Not str.Contains("?") Then Return False
        If Not str.Split("/").Length - 1 = 7 Then Return False

        Return True

    End Function

    Private Sub untick(ByRef c As TreeNode)
        c.Checked = False
        For Each n As TreeNode In c.Nodes
            If n.Nodes.Count > 0 Then
                untick(n)
            End If
        Next
    End Sub

#End Region

#Region "Scintilla Controls"

    Private txtResult As ScintillaNET.Scintilla = New ScintillaNET.Scintilla()
    Private txtFilter As ScintillaNET.Scintilla = New ScintillaNET.Scintilla()

    Private NUMBER_MARGIN As Integer = 1
    Private BOOKMARK_MARGIN As Integer = 2
    Private FOLDING_MARGIN As Integer = 3

    Private BACK_COLOR As Color = Color.FromArgb(42, 33, 28)
    Private FORE_COLOR As Color = Color.FromArgb(183, 183, 183)

    Private Sub SettxtFilter()

        Me.splitColumns.Panel2.Controls.Add(txtFilter)

        With txtFilter
            .Dock = System.Windows.Forms.DockStyle.Fill
            .WrapMode = WrapMode.Whitespace
            .IndentationGuides = IndentView.LookBoth
            .SetSelectionBackColor(True, Color.Black)
            .StyleResetDefault()
            .Styles(Style.Default).Font = "Consolas"
            .Styles(Style.Default).Size = 12
            .Styles(Style.Default).BackColor = Color.White
            .Styles(Style.Default).ForeColor = Color.Black
            .SetSelectionBackColor(True, Color.LightBlue)

            .StyleClearAll()
            .AllowDrop = True

            .Lexer = Lexer.Cpp
            .Styles(Style.Cpp.Default).ForeColor = Color.Silver
            .Styles(Style.Cpp.Comment).ForeColor = Color.FromArgb(0, 128, 0) '// Green
            .Styles(Style.Cpp.CommentLine).ForeColor = Color.FromArgb(0, 128, 0) '// Green
            .Styles(Style.Cpp.CommentLineDoc).ForeColor = Color.FromArgb(128, 128, 128) '// Gray
            .Styles(Style.Cpp.Number).ForeColor = Color.Olive
            .Styles(Style.Cpp.Word).ForeColor = Color.Blue
            .Styles(Style.Cpp.Word2).ForeColor = Color.Purple
            .Styles(Style.Cpp.String).ForeColor = Color.FromArgb(163, 21, 21) '// Red
            .Styles(Style.Cpp.Character).ForeColor = Color.FromArgb(163, 21, 21) '// Red
            .Styles(Style.Cpp.Verbatim).ForeColor = Color.FromArgb(163, 21, 21) '// Red
            .Styles(Style.Cpp.StringEol).BackColor = Color.Pink
            .Styles(Style.Cpp.Operator).ForeColor = Color.Purple
            .Styles(Style.Cpp.Preprocessor).ForeColor = Color.Maroon


            '.SetKeywords(0, "")
            .SetKeywords(0, "lt gt eq ne ")
            .SetKeywords(1, "and or ")

            AddHandler .TextChanged, AddressOf txtFilter_changed
            AddHandler .LostFocus, AddressOf txtFilter_LostFocus
            AddHandler .DragEnter, AddressOf txtFilter_DragEnter
            AddHandler .DragDrop, AddressOf txtFilter_DragDrop
            AddHandler .MouseEnter, AddressOf txtFilter_MouseEnter
            AddHandler .KeyDown, AddressOf txtFilter_KeyDown
            AddHandler .KeyUp, AddressOf txtFilter_Keyup

        End With

    End Sub

    Private Sub settxtResult()

        Me.TabPage2.Controls.Add(txtResult)

        With txtResult
            .Dock = System.Windows.Forms.DockStyle.Fill
            .WrapMode = WrapMode.None
            .IndentationGuides = IndentView.LookBoth
            .SetSelectionBackColor(True, Color.Black)
            .StyleResetDefault()
            .Styles(Style.Default).Font = "Consolas"
            .Styles(Style.Default).Size = 12
            .Styles(Style.Default).BackColor = Color.White
            .Styles(Style.Default).ForeColor = Color.Black
            .StyleClearAll()

            .Styles(Style.Json.Default).ForeColor = Color.Blue
            .Styles(Style.Json.BlockComment).ForeColor = Color.Green
            .Styles(Style.Json.LineComment).ForeColor = Color.Green
            .Styles(Style.Json.Keyword).ForeColor = Color.FromArgb(163, 21, 21)
            .Styles(Style.Json.PropertyName).ForeColor = Color.FromArgb(46, 117, 182)
            .Styles(Style.Json.String).ForeColor = Color.Red
            .Styles(Style.Json.Uri).ForeColor = Color.Blue

            .SetSelectionBackColor(True, Color.LightBlue)

            .Lexer = Lexer.Json

            Dim nums = .Margins(1)
            nums.Width = 30
            nums.Type = MarginType.Number
            nums.Sensitive = True
            nums.Mask = 0

            .Styles(Style.LineNumber).BackColor = BACK_COLOR
            .Styles(Style.LineNumber).ForeColor = FORE_COLOR
            .Styles(Style.IndentGuide).ForeColor = FORE_COLOR
            .Styles(Style.IndentGuide).BackColor = BACK_COLOR

            .SetFoldMarginColor(True, BACK_COLOR)
            .SetFoldMarginHighlightColor(True, BACK_COLOR)

            ''// Enable code folding
            .SetProperty("fold", "1")
            .SetProperty("fold.compact", "1")
            .SetProperty("fold.html", "1")

            ''// Configure a margin to display folding symbols
            .Margins(FOLDING_MARGIN).Type = MarginType.Symbol
            .Margins(FOLDING_MARGIN).Mask = Marker.MaskFolders
            .Margins(FOLDING_MARGIN).Sensitive = True
            .Margins(FOLDING_MARGIN).Width = 20

            ''// Set colors for all folding markers
            For i As Integer = 25 To 31
                .Markers(i).SetForeColor(BACK_COLOR) ''// styles For (+) And (-)
                .Markers(i).SetBackColor(FORE_COLOR) ''// styles For (+) And (-)
            Next

            ''// Configure folding markers with respective symbols
            .Markers(Marker.Folder).Symbol = MarkerSymbol.CirclePlus 'CODEFOLDING_CIRCULAR ?  : MarkerSymbol.BoxPlus
            .Markers(Marker.FolderOpen).Symbol = MarkerSymbol.CircleMinus 'CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus
            .Markers(Marker.FolderEnd).Symbol = MarkerSymbol.CirclePlusConnected 'CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected
            .Markers(Marker.FolderMidTail).Symbol = MarkerSymbol.TCorner
            .Markers(Marker.FolderOpenMid).Symbol = MarkerSymbol.CircleMinusConnected 'CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected
            .Markers(Marker.FolderSub).Symbol = MarkerSymbol.VLine
            .Markers(Marker.FolderTail).Symbol = MarkerSymbol.LCorner

            ''// Enable automatic folding
            .AutomaticFold = AutomaticFold.Show + AutomaticFold.Click + AutomaticFold.Change

            AddHandler .MouseEnter, AddressOf txtResult_MouseEnter
            AddHandler .KeyUp, AddressOf urlResult_KeyUp

        End With

    End Sub

#End Region

#Region "TreeView handlers"

    Private Sub TreeView_BeforeCheck(sender As Object, e As TreeViewCancelEventArgs) Handles TreeView.BeforeCheck
        Select Case e.Node.Level
            Case 0
                e.Cancel = True
            Case 1
                e.Cancel = True
                e.Node.Expand()

        End Select
        TabControl1.SelectedIndex = 0

    End Sub

    Private Sub TreeView_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TreeView.AfterCheck

        If e.Node.Level = 2 And e.Node.Checked Then
            For Each n As TreeNode In e.Node.Parent.Nodes
                If Not n Is e.Node Then
                    n.Checked = False
                End If
            Next
        End If

        If e.Node.Level > 2 And e.Node.Checked Then
            Dim p As TreeNode
            p = e.Node.Parent
            While p.Level > 1
                If Not p.Checked Then
                    p.Checked = True
                End If
                p = p.Parent
            End While

        End If

        If Not e.Node.Checked Then
            If e.Node.Level = 2 Then
                txtUrl.Text = ""
            End If
            For Each n As TreeNode In e.Node.Nodes
                If n.Checked Then
                    n.Checked = False
                End If
            Next
        End If

        RefreshURL(sender, Nothing)


    End Sub

    Private Sub TreeView_BeforeCollapse(sender As Object, e As TreeViewCancelEventArgs) Handles TreeView.BeforeCollapse
        If e.Node.Level = 0 Then
            e.Cancel = True

        End If
        TabControl1.SelectedIndex = 0

    End Sub

    Private Sub TreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView.AfterSelect

        If Not e.Node.Tag Is Nothing Then
            With TryCast(e.Node.Tag, EdmxDataServicesSchemaEntityType)
                If .Property.Count > 0 Then
                    .lview(Me.ListView1, ed)
                    Me.txtFilter.Text = .filter
                    ListView1.Items(0).Selected = True
                End If

            End With
            SetAutoList(ListType.Columns)

        End If
        TabControl1.SelectedIndex = 0

    End Sub

    Private Sub TreeView_DoubleClick(sender As Object, e As EventArgs) Handles TreeView.DoubleClick
        ' do nothing
    End Sub

    Private Sub TreeView_Click(sender As Object, e As EventArgs) Handles TreeView.Click
        TabControl1.SelectedIndex = 0

    End Sub

    Private Sub ContextMenuStrip4_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip4.Opening
        e.Cancel = TreeView.SelectedNode Is Nothing
        If Not e.Cancel Then e.Cancel = Not TreeView.SelectedNode.Level < 2

    End Sub

    Private Sub FindToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindToolStripMenuItem.Click

        Dim a As TreeNode = Nothing

        Using f As New dlgFind
            Dim Names As New List(Of String)
            subNames(TreeView.SelectedNode, Names)
            Names.Sort()
            f.autocompleteMenu1.SetAutocompleteItems(Names)
            f.Timer1.Enabled = True
            If f.ShowDialog = DialogResult.OK Then

                PauseForm()
                For Each N In TreeView.Nodes(0).Nodes
                    For Each C As TreeNode In N.nodes
                        If String.Compare(C.Tag.Name, f.txtFind.Text) = 0 Then
                            a = C
                            C.Parent.Expand()
                            C.Expand()
                            Exit For
                        Else
                            C.Parent.Collapse()
                            C.Collapse()
                        End If
                    Next
                Next
                If Not a Is Nothing Then
                    TreeView.SelectedNode = a
                    TreeView_AfterSelect(Me, New TreeViewEventArgs(a))

                End If
                ResumeForm()

            End If

        End Using


    End Sub

    Private Sub subNames(ByRef e As TreeNode, ByRef Names As List(Of String))
        If Not e.Tag Is Nothing Then Names.Add(e.Text)
        If e.Level < 2 Then
            For Each n As TreeNode In e.Nodes
                subNames(n, Names)
            Next
        End If
    End Sub

#End Region

#Region "ListView1 handlers"

    Private Sub ListView1_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles ListView1.ItemChecked
        RefreshURL(sender, Nothing)
    End Sub

    Private Sub ListView1_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        With e.Item
            If e.IsSelected Then
                .BackColor = Color.LightBlue
                .ForeColor = Color.DarkBlue

            Else
                .BackColor = Color.White
                .ForeColor = Color.Blue
            End If
        End With
    End Sub

#End Region

#Region "txtFilter handlers"

    Public Enum ListType
        Columns
        Comparitor
        Value
        Extender

    End Enum

    Private AU As ListType = ListType.Columns
    Private AUCancel As Boolean = False

    Private Sub autocompleteMenu1_Opening(sender As Object, e As CancelEventArgs) Handles autocompleteMenu1.Opening

        If ListView1.Items.Count = 0 Then
            e.Cancel = True
            Exit Sub
        End If

        Dim wrd As List(Of String) = LeftofCaret()
        With TryCast(ListView1.Items(0).Tag, EdmxDataServicesSchemaEntityTypeProperty).Parent
            Select Case wrd.Count
                Case 0
                    If Not AU = ListType.Columns Then
                        SetAutoList(ListType.Columns)
                        e.Cancel = True

                    End If

                Case Else
                    If Not AU = ListType.Comparitor Then
                        For Each col In .Property
                            If String.Compare(col.Name, wrd(wrd.Count - 1)) = 0 Then
                                SetAutoList(ListType.Comparitor)
                                e.Cancel = True
                                Exit For

                            End If
                        Next
                    End If
                    If Not e.Cancel And wrd.Count >= 3 And Not AU = ListType.Extender Then
                        For Each col In .Property
                            If String.Compare(col.Name, wrd(wrd.Count - 3)) = 0 Then
                                SetAutoList(ListType.Extender)
                                e.Cancel = True
                                Exit For

                            End If
                        Next
                    End If

                    If Not e.Cancel And Not AU = ListType.Columns Then
                        If New List(Of String)({"and", "or"}).Contains(wrd.Last) Then
                            SetAutoList(ListType.Columns)
                            e.Cancel = True
                        End If
                    End If

                    If Not e.Cancel And Not AU = ListType.Value Then
                        If New List(Of String)({"lt", "gt", "eq", "ne"}).Contains(wrd.Last) Then
                            SetAutoList(ListType.Value)
                            e.Cancel = True
                        End If
                    End If

            End Select

        End With

        If e.Cancel And Not AUCancel Then
            AUCancel = True
            autocompleteMenu1.Show(txtFilter, True)
        Else
            AUCancel = False
        End If

    End Sub

    Private Sub SetAutoList(t As ListType)
        Dim l As New List(Of String)
        Select Case t
            Case ListType.Columns
                With TryCast(ListView1.Items(0).Tag, EdmxDataServicesSchemaEntityTypeProperty).Parent
                    For Each col In .Property
                        l.Add(col.Name)
                    Next
                End With
                AU = ListType.Columns

            Case ListType.Comparitor
                l.Add("eq")
                l.Add("ne")
                l.Add("gt")
                l.Add("lt")
                AU = ListType.Comparitor

            Case ListType.Extender
                l.Add("and")
                l.Add("or")
                AU = ListType.Extender

            Case ListType.Value
                l.Add("'0'")
                Dim utc As String
                utc = String.Format("{0}-{1}-{2}T{3}:{4}:{5}Z",
        Now.Year,
        Strings.Right("00" & Now.Month, 2),
        Strings.Right("00" & Now.Day, 2),
        Strings.Right("00" & Now.Hour, 2),
        Strings.Right("00" & Now.Minute, 2),
        Strings.Right("00" & Now.Second, 2)
        )
                l.Add(utc)
                AU = ListType.Value

        End Select
        autocompleteMenu1.SetAutocompleteItems(l)

    End Sub

    Private Sub txtFilter_KeyDown(sender As Object, e As KeyEventArgs)
        Select Case e.KeyData
            Case Keys.Space
                e.SuppressKeyPress = SpaceBehindCaret()

            Case Else

        End Select

    End Sub

    Private Sub txtFilter_Keyup(sender As Object, e As KeyEventArgs)
        Select Case e.KeyData
            Case Keys.Space
                autocompleteMenu1.Show(txtFilter, True)

            Case Else

        End Select

    End Sub

    Private Function LeftofCaret() As List(Of String)
        Dim ret As New List(Of String)
        Try
            For Each STR As String In Split(Replace(Trim(txtFilter.Text.Substring(0, txtFilter.CurrentPosition - 1)), Chr(32) & Chr(32), Chr(32)), " ")
                If STR.Length > 0 Then ret.Add(STR)
            Next

        Catch : End Try

        Return ret

    End Function

    Private Function SpaceBehindCaret() As Boolean
        Try
            Return txtFilter.Text.Substring(txtFilter.CurrentPosition - 1, 1) = " "
        Catch
            Return True
        End Try

    End Function

    Private Sub txtFilter_changed(sender As Object, e As EventArgs)
        For Each i In ListView1.SelectedItems
            With TryCast(i.tag, EdmxDataServicesSchemaEntityTypeProperty).Parent
                .filter = Me.txtFilter.Text
            End With
        Next

    End Sub

    Private Sub txtFilter_LostFocus(sender As Object, e As EventArgs)
        RefreshURL(sender, Nothing)

    End Sub

#End Region

#Region "Tabcontrol1 handlers"

    Private Sub TabControl1_TabIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

        If TabControl1.SelectedIndex = 1 Then
            If txtUrl.Text.Length > 0 And Not (String.Compare(lastURL, txtUrl.Text) = 0) Then
                lastURL = txtUrl.Text
                refreshResult()

            End If

            With txtResult
                '.Select(0, 0)
                .Focus()
            End With

        End If

    End Sub

#End Region

#Region "Drag Drop"

    Private MouseIsDown As Boolean = False
    Private Sub ListView1_MouseDown(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDown
        MouseIsDown = True
    End Sub

    Private Sub ListView1_MouseMove(sender As Object, e As MouseEventArgs) Handles ListView1.MouseMove
        If MouseIsDown Then

            ' Initiate dragging.
            If ListView1.SelectedIndices.Count > 0 Then _
    ListView1.DoDragDrop(ListView1.Items(ListView1.SelectedIndices(0)), DragDropEffects.Copy)

        End If

        MouseIsDown = False

    End Sub

    Private Sub txtFilter_DragEnter(sender As Object, e As DragEventArgs)
        If (e.Data.GetDataPresent(DataFormats.Serializable)) Then

            ' Display the copy cursor.
            If Not e.Data.GetData(GetType(ListViewItem)) Is Nothing Then

                e.Effect = DragDropEffects.Copy

            Else
                ' Display the no-drop cursor.
                e.Effect = DragDropEffects.None

            End If

        Else

            ' Display the no-drop cursor.
            e.Effect = DragDropEffects.None

        End If

    End Sub

    Private Sub txtFilter_DragDrop(sender As Object, e As DragEventArgs)
        With txtFilter
            .Focus()
            Dim l As Integer

            l = (txtFilter.Text.Substring(0, txtFilter.CurrentPosition) & e.Data.GetData(GetType(ListViewItem)).text).length
            .Text = .Text.Substring(0, .CurrentPosition) & e.Data.GetData(GetType(ListViewItem)).text & .Text.Substring(.CurrentPosition)
            .SelectionStart = l
            .CurrentPosition = l

        End With

    End Sub

    Private Sub txtUrl_DragEnter(sender As Object, e As DragEventArgs) Handles txtUrl.DragEnter
        If (e.Data.GetDataPresent(DataFormats.Text)) Then

            ' Display the copy cursor.
            e.Effect = DragDropEffects.Copy

        Else

            ' Display the no-drop cursor.
            e.Effect = DragDropEffects.None

        End If

    End Sub

    Private Sub txtUrl_DragDrop(sender As Object, e As DragEventArgs) Handles txtUrl.DragDrop

        If IsURL(HttpUtility.UrlDecode(e.Data.GetData(DataFormats.Text))) Then
            parsingURL = True
            MainPanel.Enabled = False
            Cursor = Cursors.WaitCursor
            ParseURL(Nothing, HttpUtility.UrlDecode(e.Data.GetData(DataFormats.Text)))
            parsingURL = False
            RefreshURL(sender, Nothing)
            MainPanel.Enabled = True
            Cursor = Cursors.Default

        End If

    End Sub

#End Region

#Region "Order by"

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If Not (ListView1.SelectedItems.Count > 0) Then
            e.Cancel = True
        Else
            With TryCast(ListView1.SelectedItems(0).Tag, EdmxDataServicesSchemaEntityTypeProperty)
                Select Case .Sort.ToUpper
                    Case "ASC"
                        SortAscendingToolStripMenuItem.Visible = False
                        SortDescendingToolStripMenuItem.Visible = True
                        NoSortToolStripMenuItem.Visible = True

                    Case "DESC"
                        SortAscendingToolStripMenuItem.Visible = True
                        SortDescendingToolStripMenuItem.Visible = False
                        NoSortToolStripMenuItem.Visible = True

                    Case Else
                        SortAscendingToolStripMenuItem.Visible = True
                        SortDescendingToolStripMenuItem.Visible = True
                        NoSortToolStripMenuItem.Visible = False

                End Select

            End With
        End If

    End Sub

    Private Sub SortAscendingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SortAscendingToolStripMenuItem.Click
        With TryCast(ListView1.SelectedItems(0).Tag, EdmxDataServicesSchemaEntityTypeProperty)
            .Sort = "ASC"
        End With
        RefreshURL(sender, Nothing)
    End Sub

    Private Sub SortDescendingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SortDescendingToolStripMenuItem.Click
        With TryCast(ListView1.SelectedItems(0).Tag, EdmxDataServicesSchemaEntityTypeProperty)
            .Sort = "DESC"
        End With
        RefreshURL(sender, Nothing)
    End Sub

    Private Sub NoSortToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoSortToolStripMenuItem.Click
        With TryCast(ListView1.SelectedItems(0).Tag, EdmxDataServicesSchemaEntityTypeProperty)
            .Sort = ""
        End With
        RefreshURL(sender, Nothing)
    End Sub


#End Region

#Region "Copy / paste / refresh URL"

    Private Sub ContextMenuStrip2_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip2.Opening
        e.Cancel = Not envfile.Length > 0
        ParseURLToolStripMenuItem.Enabled = envfile.Length > 0
        OpenToolStripMenuItem1.Enabled = envfile.Length > 0

        CopyURLToolStripMenuItem.Enabled = txtUrl.Text.Length > 0
        RefreshF5ToolStripMenuItem.Enabled = txtUrl.Text.Length > 0
        SaveToolStripMenuItem1.Enabled = txtUrl.Text.Length > 0

    End Sub

    Private Sub CopyURLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyURLToolStripMenuItem.Click
        My.Computer.Clipboard.SetText(txtUrl.Text)

    End Sub

    Private Sub ParseURLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParseURLToolStripMenuItem.Click, PasteToolStripMenuItem.Click

        Dim d As New PasteURL
        If IsURL(Clipboard.GetText(TextDataFormat.Text)) Then _
            d.txtPaste.Text = Clipboard.GetText(TextDataFormat.Text)

        If d.ShowDialog = DialogResult.OK Then
            If IsURL(HttpUtility.UrlDecode(d.txtPaste.Text)) Then
                parsingURL = True
                ParseURL(Nothing, HttpUtility.UrlDecode(d.txtPaste.Text))
                parsingURL = False
                RefreshURL(sender, Nothing)
            Else
                MsgBox("Invalid URL.", vbOKOnly + vbExclamation, "Error.")
            End If

        End If

    End Sub

    Private Sub RefreshF5ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshF5ToolStripMenuItem.Click
        refreshResult()

    End Sub

#End Region

#Region "Refresh Result"

    Private Sub urlResult_KeyUp(sender As Object, e As KeyEventArgs) Handles txtUrl.KeyUp

        If e.KeyData = Keys.F5 Then
            e.Handled = True
            refreshResult()

        End If

    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        refreshResult()
        With txtResult
            .Focus()

        End With

    End Sub

#End Region

#Region "Select on mouse enter"

    Private Sub TreeView_MouseEnter(sender As Object, e As EventArgs) Handles TreeView.MouseEnter
        TreeView.Focus()
    End Sub

    Private Sub ListView1_MouseEnter(sender As Object, e As EventArgs) Handles ListView1.MouseEnter
        ListView1.Focus()
    End Sub

    Private Sub txtResult_MouseEnter(sender As Object, e As EventArgs)
        txtResult.Focus()
    End Sub

    Private Sub txtFilter_MouseEnter(sender As Object, e As EventArgs)
        txtFilter.Focus()
    End Sub

#End Region

#Region "File menu"

    Private filename As String = ""

    Private Sub FileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileToolStripMenuItem.Click
        SaveToolStripMenuItem.Enabled = txtUrl.Text.Length > 0
        OpenToolStripMenuItem.Enabled = envfile.Length() > 0
        PasteToolStripMenuItem.Enabled = envfile.Length() > 0

    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click, OpenToolStripMenuItem1.Click

        With OpenFileDialog1

            .Title = "Open File"
            .FileName = "*.url"
            .Filter = "URL Shortcut (*.url)|*.url|All files (*.*)|*.*"

            If Not .ShowDialog = DialogResult.Cancel Then
                If File.Exists(.FileName) Then
                    Using sr As New StreamReader(.FileName)
                        While Not sr.EndOfStream
                            Dim str As String = sr.ReadLine
                            If InStr(str, "=") > 0 Then
                                If String.Compare(Split(str, "=")(0), "URL") = 0 Then
                                    If IsURL(HttpUtility.UrlDecode(str.Substring(InStr(str, "=")))) Then
                                        parsingURL = True
                                        MainPanel.Enabled = False
                                        Cursor = Cursors.WaitCursor
                                        ParseURL(Nothing, HttpUtility.UrlDecode(str.Substring(InStr(str, "="))))
                                        parsingURL = False
                                        RefreshURL(sender, Nothing)
                                        MainPanel.Enabled = True
                                        Cursor = Cursors.Default
                                    Else
                                        MsgBox("Invalid URL.", vbOKOnly + vbExclamation, "Error.")
                                    End If
                                    sr.ReadToEnd()

                                End If
                            End If
                        End While
                    End Using
                Else
                    MsgBox("File not found.", vbOKOnly + vbExclamation, "Error.")

                End If

            End If

        End With

    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click, SaveToolStripMenuItem1.Click
        With SaveFileDialog1

            .Title = "Save File"
            .DefaultExt = "url"
            .Filter = "URL Shortcut (*.url)|*.url|All files (*.*)|*.*" '

            .AddExtension = True
            .OverwritePrompt = True
            If filename.Length > 0 Then
                .InitialDirectory = New FileInfo(.FileName).DirectoryName
                .FileName = New FileInfo(.FileName).Name

            End If

            If Not .ShowDialog = DialogResult.Cancel Then
                filename = .FileName
                Using sw As New StreamWriter(.FileName)
                    sw.WriteLine("URL=" & txtUrl.Text)

                End Using

            End If

        End With

    End Sub

    Private Sub EnvironmentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnvironmentToolStripMenuItem.Click
        SettingsToolStripMenuItem.Enabled = envfile.Length > 0
        FindToolStripMenuItem1.Enabled = envfile.Length > 0

    End Sub

    Private Sub LoadEnvironmentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadEnvironmentToolStripMenuItem.Click

        Using f As New dlgOpenEnv
            With f
                With .chkRefresh
                    .Checked = False
                    .Enabled = False

                End With

                .txtUrl.Enabled = False
                .txtINI.Enabled = False
                .txtEnv.Enabled = False
                .txtUser.Enabled = False
                .txtPass.Enabled = False

                With .lstENV.Items
                    .Add("")
                    For Each env As FileInfo In New DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).GetFiles()
                        If String.Compare(env.Extension, ".meta") = 0 Then
                            .Add(env.Name.Substring(0, env.Name.Length - 5))
                        End If
                    Next

                End With

                If Not .lstENV.Items.Count = 1 Then
                    If .ShowDialog() = DialogResult.OK Then
                        envfile = .metafile
                        ReloadTree()

                    End If

                Else
                    CreateEnvironmentToolStripMenuItem_Click(Me, New EventArgs)

                End If

            End With

        End Using

    End Sub

    Private Sub CreateEnvironmentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateEnvironmentToolStripMenuItem.Click

        Using F As New dlgNewEnv
            With F
                .dlgType = 1
                With .chkRefresh
                    .Checked = True
                    .Enabled = False

                End With

                If .ShowDialog() = DialogResult.OK Then
                    envfile = .metafile
                    ReloadTree()

                End If

            End With

        End Using

    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click

        Using F As New dlgNewEnv
            With F
                .dlgType = 2

                .txtINI.Text = ed.TabulaINI
                .txtEnv.Text = ed.Environment
                .txtUser.Text = ed.Username
                .txtPass.Text = ed.Password

                With .chkRefresh
                    .Checked = False
                    .Enabled = True

                End With

                With .txtName
                    .Text = FriendlyName
                    .Enabled = False

                End With

                With .txtUrl
                    .Text = ed.URL
                    .Enabled = False

                End With

                .loadedED = ed
                If .ShowDialog() = DialogResult.OK Then
                    If .chkRefresh.Checked Then ReloadTree()

                End If

            End With

        End Using

    End Sub

    Private Sub FindToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FindToolStripMenuItem1.Click
        TreeView.SelectedNode = TreeView.Nodes(0)
        FindToolStripMenuItem_Click(Me, New EventArgs)

    End Sub

    Private Sub CloseToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem1.Click
        Me.Close()

    End Sub


#End Region

End Class
