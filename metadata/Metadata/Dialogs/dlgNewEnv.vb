
Imports metadata

Public Class dlgNewEnv

    Private _loadedED As Edmx = Nothing
    Public Property loadedED As Edmx
        Get
            Return _loadedED
        End Get
        Set(value As Edmx)
            _loadedED = value
        End Set
    End Property
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        AddHandler txtEnv.KeyUp, AddressOf ctrlKeyUp
        AddHandler txtINI.KeyUp, AddressOf ctrlKeyUp
        AddHandler txtName.KeyUp, AddressOf ctrlKeyUp
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
            Return String.Format("{0}.meta", Trim(txtName.Text))

        End Get
    End Property

    Private _dlgType As Integer = 0
    Public Property dlgType As Integer
        Get
            Return _dlgType
        End Get
        Set(value As Integer)
            _dlgType = value
        End Set
    End Property

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        If dlgType = 1 Then
            If GetMeta(Me) Then
                DialogResult = DialogResult.OK

            End If

        Else
            If Not chkRefresh.Checked Then
                chkRefresh.Checked = SaveChanges(Me, loadedED)

            End If

            Select Case chkRefresh.Checked
                Case True
                    If GetMeta(Me) Then
                        DialogResult = DialogResult.OK

                    End If

                Case Else
                    If VerifySettings(Me) Then
                        DialogResult = DialogResult.OK

                    End If

            End Select

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel

    End Sub

End Class