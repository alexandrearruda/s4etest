Imports System.Data.SqlClient
Namespace Dal

    Public Class Empresas

        Private Const ConString As String = "Server=127.0.0.1;Database=master;User Id=sa;Password=AIVccn067"
        Private ReadOnly db As New SqlConnection(ConString)

        Public Property DadosEmpresas As New Models.Empresas()

        Public Sub AlterarEmpresas(id As Integer)

            Dim strSql As String = "UPDATE cadEmpresa SET NomeEmpresa=@NomeEmpresa,Cnpj=@Cnpj WHERE idEmpresa=@idEmpresa"

            Using cmd As New SqlCommand()

                cmd.Parameters.Add("@idEmpresa", SqlDbType.Int).Value = id
                cmd.Parameters.Add("@NomeEmpresa", SqlDbType.VarChar).Value = DadosEmpresas.NomeEmpresa
                cmd.Parameters.Add("@Cnpj", SqlDbType.VarChar).Value = DadosEmpresas.Cnpj

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

        Public Sub DeleteEmpresas(id As Integer)

            Dim strSql As String = "DELETE FROM associadosXempresa WHERE idEmpresa=@idEmpresa;DELETE FROM cadEmpresas WHERE idEmpresa=@idEmpresa"

            Using cmd As New SqlCommand()
                cmd.Parameters.Add("@idEmpresa", SqlDbType.Int).Value = id

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


        Public Sub AddEmpresas()

            Dim strSql As String = "INSERT INTO cadEmpresas (NomeEmpresa,Cnpj) VALUES (@NomeEmpresa,@Cnpj)"

            Using cmd As New SqlCommand()
                cmd.Parameters.Add("@NomeEmpresa", SqlDbType.VarChar).Value = DadosEmpresas.NomeEmpresa
                cmd.Parameters.Add("@Cnpj", SqlDbType.VarChar).Value = DadosEmpresas.Cnpj

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

        Public Function GetEmpresas(id As Integer?) As IEnumerable(Of Models.Empresas)

            Dim strSql As String = "SELECT * FROM associadosXempresa ae
                                    INNER JOIN cadEmpresa ce ON ce.idEmpresa = ae.idEmpresa
                                    INNER JOIN cadAssociados ca ON ca.idAssociado = ae.idAssociado"

            Using cmd As New SqlCommand()

                If id > 0 Then
                    strSql &= " WHERE ae.idEmpresa=@idEmpresa "
                    cmd.Parameters.Add("@idEmpresa", SqlDbType.Int).Value = id
                End If

                cmd.CommandText = strSql
                cmd.Connection = db
                Try

                    Dim retEmpresas As New List(Of Models.Empresas)
                    Dim retAssociados As New List(Of Models.Associados)

                    db.Open()
                    Using dr As SqlDataReader = cmd.ExecuteReader()

                        While dr.Read

                            retAssociados.Add(New Models.Associados With {
                                       .IdAssociado = dr("idAssociado"),
                                       .Cpf = dr("cpf"),
                                       .Nome = dr("nome"),
                                       .DtNascimento = dr("dtNascimento")
                                       })

                            retEmpresas.Add(New Models.Empresas With {
                                        .Cnpj = dr("cnpj"),
                                        .IdEmpresa = dr("idEmpresa"),
                                        .NomeEmpresa = dr("nomeEmpresa"),
                                        .Associados = retAssociados
                                        })

                        End While
                    End Using

                    Return retEmpresas

                Catch ex As Exception
                    Console.WriteLine(ex.ToString)
                    Return New List(Of Models.Empresas)
                Finally
                    db.Close()
                End Try

            End Using
        End Function

    End Class
End Namespace