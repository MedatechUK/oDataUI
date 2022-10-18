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

Module metafile

    Public Function GetMeta(ByRef f As Object) As Boolean

        f.TableLayoutPanel1.Enabled = False
        f.Cursor = Cursors.WaitCursor
        Try
            Using sr = XmlReader.Create(
                Req(f).GetResponse.GetResponseStream
            )

                Dim xslt = New XslCompiledTransform()
                Dim xslArg = New XsltArgumentList()

                xslt.Load("XSLTFile1.xslt", New XsltSettings(True, True), New XmlUrlResolver())

                Dim settings = xslt.OutputSettings.Clone()
                settings.IndentChars = "  "
                settings.Encoding = New UTF8Encoding(False)

                Using result = XmlWriter.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), f.metafile), settings)
                    xslt.Transform(sr, xslArg, result, New XmlUrlResolver())
                    result.Close()

                End Using

                Dim ed As Edmx
                Using ex As New AppExtension(AddressOf MedatechUK.Logging.Events.logHandler)
                    With ex.LexByAssemblyName(GetType(Edmx).FullName)
                        Using sr2 As New StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), f.metafile))
                            ed = .Deserialise(sr2)
                        End Using
                        With ed
                            .URL = f.txtUrl.Text
                            .Environment = f.txtEnv.Text
                            .TabulaINI = f.txtINI.Text
                            .Username = f.txtUser.Text
                            .Password = f.txtPass.Text

                        End With
                        Dim ser As XmlSerializer = New XmlSerializer(GetType(Edmx))
                        Using sw As New StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), f.metafile))
                            ser.Serialize(sw, ed)
                        End Using

                    End With
                End Using

            End Using
            Return True

        Catch ex As Exception
            MsgBox(ex.Message, vbOK + vbExclamation, "Error.")
            Return False

        Finally
            f.TableLayoutPanel1.Enabled = True
            f.Cursor = Cursors.Default
        End Try

    End Function

    Public Function VerifySettings(ByRef f As Object) As Boolean

        f.TableLayoutPanel1.Enabled = False
        f.Cursor = Cursors.WaitCursor
        Try
            Dim r As HttpWebResponse = Req(f).GetResponse()
            If Not r.StatusCode = HttpStatusCode.OK Then
                MsgBox(r.StatusDescription, vbOK + vbExclamation, "Error.")
            End If
            Return r.StatusCode = HttpStatusCode.OK

        Catch ex As Exception
            MsgBox(ex.Message, vbOK + vbExclamation, "Error.")
            Return False

        Finally
            f.TableLayoutPanel1.Enabled = True
            f.Cursor = Cursors.Default

        End Try

    End Function

    Public Sub ClearForm(ByRef f As Object)
        With f
            With .txtEnv
                .Text = ""
            End With

            With .txtUrl
                .Text = ""
            End With

            With .txtINI
                .Text = ""
            End With

            With .txtUser
                .Text = ""
            End With

            With .txtPass
                .Text = ""
            End With

        End With

    End Sub

    Public Sub FillForm(ByRef f As Object, ByRef ed As Edmx)
        With f
            With .txtEnv
                .Text = ed.Environment
                .Enabled = True
            End With

            With .txtUrl
                .Text = ed.URL
                .Enabled = False
            End With

            With .txtINI
                .Text = ed.TabulaINI
                .Enabled = True
            End With

            With .txtUser
                .Text = ed.Username
                .Enabled = True
            End With

            With .txtPass
                .Text = ed.Password
                .Enabled = True
            End With

            .chkRefresh.Enabled = True

        End With

    End Sub

    Public Function SaveChanges(ByRef f As Object, ByRef ed As Edmx) As Boolean
        Return _
        String.Compare(ed.TabulaINI, f.txtINI.Text) <> 0 Or
            String.Compare(ed.Environment, f.txtEnv.Text) <> 0 Or
            String.Compare(ed.Username, f.txtUser.Text) <> 0 Or
            String.Compare(ed.Password, f.txtPass.Text) <> 0

    End Function

    Private Function Req(ByRef f As Object) As WebRequest

        Dim ret As WebRequest = WebRequest.Create(
            String.Format("https://{0}/odata/priority/{1}/{2}/$metadata", f.txtUrl.Text, f.txtINI.Text, f.txtEnv.Text)
        )

        ret.Headers.Add(
            "Authorization",
            "Basic " + System.Convert.ToBase64String(
                Encoding.GetEncoding("ISO-8859-1").GetBytes(f.txtUser.Text + ":" + f.txtPass.Text)
            )
        )
        Return ret

    End Function

    Public Function GetEDMX(f As Object) As Edmx
        Using ex As New AppExtension(AddressOf MedatechUK.Logging.Events.logHandler)
            With ex.LexByAssemblyName(GetType(Edmx).FullName)
                Using sr As New StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), f.metafile))
                    Return .Deserialise(sr)
                End Using

            End With
        End Using

    End Function

End Module
