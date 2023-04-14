Imports System.Data.SqlClient
Imports System.Reflection
Imports System.ServiceModel
Imports System.ServiceModel.Activation

Namespace Dal

    Public Class Associados


        Private Const ConString As String = "Server=127.0.0.1;Database=master;User Id=sa;Password=AIVccn067"

        'Public Property Nome As String
        'Public Property Cpf As String
        'Public Property DtNascimento As String

        Public Property dadosAssociados As New Models.Associados()

        Public Sub AddAssociados()
            Dim db As New SqlConnection(ConString)
            Dim strSql As String = "INSERT INTO cadAssociados (nome,cpf,dtNascimento) VALUES (@nome,@cpf,@dtNascimento)"

            Using cmd As New SqlCommand()
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = dadosAssociados.Nome
                cmd.Parameters.Add("@cpf", SqlDbType.VarChar).Value = dadosAssociados.Cpf
                cmd.Parameters.Add("@dtNascimento", SqlDbType.DateTime).Value = dadosAssociados.DtNascimento

                cmd.CommandText = strSql
                cmd.Connection = db
                Try
                    db.Open()
                    cmd.ExecuteReader()
                Catch ex As Exception
                    Console.WriteLine(ex.ToString)
                Finally
                    db.Close()
                End Try
            End Using

        End Sub

        Public Function GetAssociados(id As Integer?) As IEnumerable(Of Models.Associados)
            Dim db As New SqlConnection(ConString)

            Dim strSql As String = "SELECT * FROM associadosXempresa ae
                                    INNER JOIN cadEmpresa ce ON ce.idEmpresa = ae.idEmpresa
                                    INNER JOIN cadAssociados ca ON ca.idAssociado = ae.idAssociado"

            Using cmd As New SqlCommand()

                If id > 0 Then
                    strSql &= " WHERE ca.idAssociado=@idAssociado "
                    cmd.Parameters.Add("@idAssociado", SqlDbType.Int).Value = id
                End If

                cmd.CommandText = strSql
                cmd.Connection = db
                Try

                    Dim retAssociados As New List(Of Models.Associados)
                    Dim retEmpresas As New List(Of Models.Empresas)

                    db.Open()
                    Using dr As SqlDataReader = cmd.ExecuteReader()


                        While dr.Read


                            retEmpresas.Add(New Models.Empresas With {
                                    .Cnpj = dr("cnpj"),
                                    .IdEmpresa = dr("idEmpresa"),
                                    .NomeEmpresa = dr("nomeEmpresa")
                                    })

                            retAssociados.Add(New Models.Associados With {
                                   .IdAssociado = dr("idAssociado"),
                                   .Cpf = dr("cpf"),
                                   .Nome = dr("nome"),
                                   .DtNascimento = dr("dtNascimento"),
                                   .Empresas = retEmpresas
                                   })

                        End While
                    End Using

                    Return retAssociados
                Catch ex As Exception
                    Console.WriteLine(ex.ToString)
                Finally
                    db.Close()
                End Try

            End Using

        End Function

    End Class
End Namespace