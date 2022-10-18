Imports System.IO
Imports System.Text.RegularExpressions

Module Module1

    Sub Main()

        Dim listPub As New List(Of String)
        Dim listPriPat As New List(Of String)
        Dim listPri As New List(Of String)
        Dim filestr As String
        Dim FN = New FileInfo(Path.Combine(Environment.CurrentDirectory, Replace(Environment.GetCommandLineArgs(1), "xsd", "vb",,, CompareMethod.Text)))

        If Not FN.Exists Then
            Console.Write("File not found.")
            End
        Else
            Console.Write(String.Format("Converting {0}.", FN.FullName))

        End If

        Using sr As New StreamReader(FN.FullName)
            filestr = sr.ReadToEnd

        End Using

        Dim pri As Regex = New Regex("(P|p)rivate .+Field\(\) (A|a)s .+")

        Dim MatchObj As Match = pri.Match(filestr)
        While MatchObj.Success
            Dim NOCR As String = Replace(MatchObj.Value, vbCr, "")
            If Not listPri.Contains(NOCR) Then
                Dim TY As String = Split(NOCR, " as ",, CompareMethod.Text)(1)
                listPri.Add(NOCR)
                listPriPat.Add(String.Format("(P|p)ublic (P|p)roperty .+\(\) (A|a)s {0}", TY))
            End If
            MatchObj = MatchObj.NextMatch()

        End While

        For Each pat In listPriPat
            Dim pub As Regex = New Regex(pat)
            MatchObj = pub.Match(filestr)
            While MatchObj.Success
                Dim NOCR As String = Replace(MatchObj.Value, vbCr, "")
                If Not listPub.Contains(NOCR) Then
                    listPub.Add(NOCR)
                End If
                MatchObj = MatchObj.NextMatch()

            End While
        Next

        For Each pubk As String In listPub
            filestr = filestr.Replace(pubk & "()", pubk)
            filestr = filestr.Replace(pubk, Replace(pubk, "() As ", " As list(of ") & ")")

        Next
        For Each prik As String In listPri
            filestr = filestr.Replace(prik & "()", prik)
            filestr = filestr.Replace(prik, Replace(prik, "() As ", " As new list(of ") & ")")

        Next

        Using sr As New StreamWriter(FN.FullName)
            sr.Write(filestr)

        End Using

    End Sub

End Module
