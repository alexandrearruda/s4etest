Imports System.Data.SqlClient
Namespace Dal

    Public Class Empresas
        Private Const ConString As String = "Server=127.0.0.1;Database=master;User Id=sa;Password=AIVccn067"
        Private ReadOnly db As New SqlConnection(ConString)

        Public Property DadosEmpresas As New Models.Empresas()

        Public Sub AlterarEmpresas(id As Integer)

            Dim strSql As String = "UPDATE cadEmpresa SET nomeEmpresa=@nomeEmpresa,cnpj=@cnpj WHERE idEmpresa=@idEmpresa"

            Using cmd As New SqlCommand()
                cmd.Parameters.Add("@idEmpresa", SqlDbType.Int).Value = id
                cmd.Parameters.Add("@nomeEmpresa", SqlDbType.VarChar).Value = DadosEmpresas.NomeEmpresa
                cmd.Parameters.Add("@cnpj", SqlDbType.VarChar).Value = DadosEmpresas.Cnpj

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

            Dim strSql As String = "DELETE FROM associadosXempresa WHERE idEmpresa=@idEmpresa;DELETE FROM cadEmpresa WHERE idEmpresa=@idEmpresa"

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

            Dim strSql As String = "INSERT INTO cadEmpresa (nomeEmpresa,cnpj) VALUES (@nomeEmpresa,@cnpj)"

            Using cmd As New SqlCommand()
                cmd.Parameters.Add("@nomeEmpresa", SqlDbType.VarChar).Value = DadosEmpresas.NomeEmpresa
                cmd.Parameters.Add("@cnpj", SqlDbType.VarChar).Value = DadosEmpresas.Cnpj

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

        Public Function GetEmpresasAssociados(id As Integer?) As IEnumerable(Of Models.Empresas)

            Dim strSql As String = "SELECT * FROM associadosXempresa ae
                                    INNER JOIN cadEmpresa ce ON ce.idEmpresa = ae.idEmpresa
                                    INNER JOIN cadAssociados ca ON ca.idAssociado = ae.idAssociado"

            Using cmd As New SqlCommand()

                If id > 0 Then
                    strSql &= " WHERE ce.idEmpresa=@idEmpresa "
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

        Public Function GetEmpresas(id As Integer?) As IEnumerable(Of Models.Empresas)

            Dim strSql As String = "SELECT * FROM cadEmpresa "

            Using cmd As New SqlCommand()

                If id > 0 Then
                    strSql &= " WHERE idEmpresa=@idEmpresa "
                    cmd.Parameters.Add("@idEmpresa", SqlDbType.Int).Value = id
                End If

                cmd.CommandText = strSql
                cmd.Connection = db
                Try

                    Dim retEmpresas As New List(Of Models.Empresas)

                    db.Open()
                    Using dr As SqlDataReader = cmd.ExecuteReader()


                        While dr.Read

                            retEmpresas.Add(New Models.Empresas With {
                                   .IdEmpresa = dr("idEmpresa"),
                                   .Cnpj = dr("cnpj"),
                                   .NomeEmpresa = dr("nomeEmpresa")
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

        Public Function GetEmpresasByCnpj(cnpj As String) As IEnumerable(Of Models.Empresas)

            If String.IsNullOrEmpty(cnpj) Then
                Return New List(Of Models.Empresas)
            End If


            Dim strSql As String = "SELECT * FROM cadEmpresa WHERE cnpj=@cnpj "

            Using cmd As New SqlCommand()

                cmd.Parameters.Add("@cnpj", SqlDbType.VarChar).Value = cnpj
                cmd.CommandText = strSql
                cmd.Connection = db
                Try

                    Dim retEmpresas As New List(Of Models.Empresas)

                    db.Open()
                    Using dr As SqlDataReader = cmd.ExecuteReader()


                        While dr.Read

                            retEmpresas.Add(New Models.Empresas With {
                                   .IdEmpresa = dr("idEmpresa"),
                                   .Cnpj = dr("cnpj"),
                                   .NomeEmpresa = dr("nomeEmpresa")
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