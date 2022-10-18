Option Strict Off

Imports metadata

Public Class dlgOpenEnv

    Private ed As Edmx

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler lstENV.KeyUp, AddressOf ctrlKeyUp
        AddHandler txtEnv.KeyUp, AddressOf ctrlKeyUp
        AddHandler txtINI.KeyUp, AddressOf ctrlKeyUp
        AddHandler txtPass.KeyUp, AddressOf ctrlKeyUp
        AddHandler txtUrl.KeyUp, AddressOf ctrlKeyUp
        AddHandler txtUser.KeyUp, AddressOf ctrlKeyUp

    End Sub

    Private Sub ctrlKeyUp(sender As Object, e As KeyEventArgs)
        If e.KeyValue = Keys.Escape Then
            DialogResult = DialogResult.Cancel
        End If

    End Sub

    Public ReadOnly Property metafile As String
        Get
            Return String.Format("{0}.meta", Trim(lstENV.SelectedItem))

        End Get
    End Property

    Private Sub lstENV_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstENV.SelectedIndexChanged

        ClearForm(Me)
        With Me
            .chkRefresh.Enabled = True
            Cursor = Cursors.WaitCursor
            .TableLayoutPanel1.Enabled = False

            If Trim(lstENV.SelectedItem).Length > 0 Then
                ed = GetEDMX(Me)
                FillForm(Me, ed)

            End If

            Me.TableLayoutPanel1.Enabled = True
            Cursor = Cursors.Default

        End With

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel

    End Sub

    Private Sub Buttonok_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        TableLayoutPanel1.Enabled = False
        Cursor = Cursors.WaitCursor
        Try
            If chkRefresh.Checked Then
                If GetMeta(Me) Then
                    DialogResult = DialogResult.OK

                End If
            Else
                If VerifySettings(Me) Then
                    SaveChanges(Me, ed)
                    DialogResult = DialogResult.OK
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message, vbOK + vbExclamation, "Error.")

        End Try
        TableLayoutPanel1.Enabled = True
        Cursor = Cursors.Default

    End Sub

End Class