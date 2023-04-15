Imports System.Data.SqlClient
Namespace Dal

    Public Class Empresas
        Private Const ConString As String = "Server=127.0.0.1;Database=master;User Id=sa;Password=AIVccn067"
        Private ReadOnly db As New SqlConnection(ConString)

        Public Property DadosEmpresas As New Models.Empresas()

        Public Sub AlterarEmpresas(id As Integer)
            db.Open()

            Dim strSql As String = "UPDATE cadEmpresa SET nomeEmpresa=@nomeEmpresa,cnpj=@cnpj WHERE idEmpresa=@idEmpresa"

            Using cmd As New SqlCommand()
                cmd.Parameters.Add("@idEmpresa", SqlDbType.Int).Value = id
                cmd.Parameters.Add("@nomeEmpresa", SqlDbType.VarChar).Value = DadosEmpresas.NomeEmpresa
                cmd.Parameters.Add("@cnpj", SqlDbType.VarChar).Value = DadosEmpresas.Cnpj

                cmd.CommandText = strSql
                cmd.Connection = db
                Try
                    cmd.ExecuteReader()
                Catch ex As Exception
                    Console.WriteLine(ex.ToString)

                End Try
            End Using
            db.Close()
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

            If Not VerificarEmpresaCnpj(DadosEmpresas.Cnpj) Then

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

            End If
        End Sub

        Private Function VerificarEmpresaCnpj(cnpj As String) As Boolean

            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT COUNT(1) FROM cadEmpresa WHERE cnpj=@cnpj"
                cmd.Parameters.Add("@cnpj", SqlDbType.VarChar).Value = cnpj
                cmd.Connection = db
                db.Open()
                If Convert.ToInt32(cmd.ExecuteScalar()) >= 1 Then

                    Return True

                End If
                db.Close()
            End Using

            Return False

        End Function

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

        Public Function GetEmpresasByCnpj(cnpj As String) As List(Of Models.Empresas)

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

        Public Function GetEmpresasByNome(nome As String) As List(Of Models.Empresas)

            If String.IsNullOrEmpty(nome) Then
                Return New List(Of Models.Empresas)
            End If


            Dim strSql As String = "SELECT * FROM cadEmpresa WHERE nomeEmpresa=@nomeEmpresa "

            Using cmd As New SqlCommand()

                cmd.Parameters.Add("@nomeEmpresa", SqlDbType.VarChar).Value = nome
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

        Public Function GetRelacaoAssociados(idEmpresa As Integer) As List(Of Integer)

            Dim strSql As String = "SELECT idAssociado FROM associadosXempresa WHERE idEmpresa = @idEmpresa"
            Dim retRelacaoAssociados As New List(Of Integer)
            Using cmd As New SqlCommand()
                cmd.CommandText = strSql
                cmd.Parameters.Add("@idEmpresa", SqlDbType.Int).Value = idEmpresa
                cmd.Connection = db
                db.Open()
                Using dr As SqlDataReader = cmd.ExecuteReader()
                    While dr.Read
                        retRelacaoAssociados.Add(dr("idAssociado"))
                    End While
                End Using
                db.Close()
            End Using

            Return retRelacaoAssociados

        End Function
        Public Function GetComboAssociados() As List(Of Models.Associados)

            Dim strSql As String = "SELECT idAssociado,nome FROM cadAssociados"
            Dim retAssociados As New List(Of Models.Associados)
            Using cmd As New SqlCommand()
                cmd.CommandText = strSql
                cmd.Connection = db
                db.Open()
                Using dr As SqlDataReader = cmd.ExecuteReader()
                    While dr.Read
                        retAssociados.Add(New Models.Associados With {
                                .IdAssociado = dr("idAssociado"),
                                .Nome = dr("nome")
                                })
                    End While
                End Using
                db.Close()
            End Using

            Return retAssociados
        End Function

        Public Sub RelacionarAssociados(selAssociados As List(Of Integer))

            Dim strSql As String = "INSERT INTO associadosXempresa (idAssociado,idEmpresa) VALUES (@idAssociado,@idEmpresa)"
            Using cmd As New SqlCommand()
                cmd.CommandText = strSql
                cmd.Connection = db
                db.Open()
                For Each item In selAssociados
                    cmd.Parameters.Add("@idAssociado", SqlDbType.Int).Value = item
                    cmd.Parameters.Add("@idEmpresa", SqlDbType.Int).Value = DadosEmpresas.IdEmpresa
                    If validarAssociacao(item, DadosEmpresas.IdEmpresa) Then
                        cmd.ExecuteScalar()
                    End If
                    cmd.Parameters.Clear()
                Next

                db.Close()
            End Using

        End Sub

        Public Sub RemoverRelacaoAssociados(selAssociados As List(Of Integer))

            Dim strSql As String = "DELETE FROM associadosXempresa WHERE idAssociado=@idAssociado AND idEmpresa=@idEmpresa"
            Using cmd As New SqlCommand()
                cmd.CommandText = strSql
                cmd.Connection = db
                db.Open()
                For Each item In selAssociados
                    cmd.Parameters.Add("@idAssociado", SqlDbType.Int).Value = item
                    cmd.Parameters.Add("@idEmpresa", SqlDbType.Int).Value = DadosEmpresas.IdEmpresa
                    If Not validarAssociacao(item, DadosEmpresas.IdEmpresa) Then
                        cmd.ExecuteScalar()
                    End If
                    cmd.Parameters.Clear()
                Next

                db.Close()
            End Using

        End Sub

        Private Function validarAssociacao(idAssociado As Integer, idEmpresa As Integer) As Boolean

            Dim strSql As String = "SELECT COUNT(1) FROM associadosXempresa WHERE idAssociado=@idAssociado AND idEmpresa=@idEmpresa"
            Using cmd As New SqlCommand()
                cmd.CommandText = strSql
                cmd.Connection = db

                cmd.Parameters.Add("@idAssociado", SqlDbType.Int).Value = idAssociado
                cmd.Parameters.Add("@idEmpresa", SqlDbType.Int).Value = idEmpresa

                If Convert.ToInt32(cmd.ExecuteScalar()) >= 1 Then
                    Return False
                End If

                Return True
            End Using
        End Function
    End Class
End Namespace