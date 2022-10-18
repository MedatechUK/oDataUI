Imports System.Windows.Forms

Public Class PasteURL

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Timer1.Enabled = True
        AddHandler txtPaste.KeyUp, AddressOf ctrlKeyUp

    End Sub

    Private Sub ctrlKeyUp(sender As Object, e As KeyEventArgs)
        If e.KeyValue = Keys.Escape Then
            DialogResult = DialogResult.Cancel
        End If

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If Len(txtPaste.Text) > 0 Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        txtPaste.Focus()
    End Sub

    Private Sub txtPaste_KeyPress(sender As Object, e As KeyEventArgs) Handles txtPaste.KeyUp

        If e.KeyData = Keys.Enter Then
            If Len(txtPaste.Text) > 0 Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
                Me.Close()
            End If
        End If

        If e.KeyData = Keys.Escape Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If

    End Sub

End Class
