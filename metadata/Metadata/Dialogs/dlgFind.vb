Imports System.ComponentModel

Public Class dlgFind
    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Timer1.Enabled = False
        txtFind.Focus()

    End Sub

    Private Sub txtFind_KeyUp(sender As Object, e As KeyEventArgs) Handles txtFind.KeyUp
        If e.KeyData = Keys.Escape Then
            autocompleteMenu1.Close()
            DialogResult = DialogResult.Cancel

        End If

    End Sub

End Class