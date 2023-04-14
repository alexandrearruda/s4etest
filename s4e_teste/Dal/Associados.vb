Imports System.Data.SqlClient
Imports System.Reflection
Imports System.ServiceModel
Imports System.ServiceModel.Activation

Namespace Dal

    Public Class Associados

        Private Const ConString As String = "Server=127.0.0.1;Database=master;User Id=sa;Password=AIVccn067"
        Private ReadOnly db As New SqlConnection(ConString)

        Public Property DadosAssociados As New Models.Associados()

        Public Sub AlterarAssociados(id As Integer)

            If ValidarCpf(DadosAssociados.Cpf) Then

                Dim strSql As String = "UPDATE cadAssociados SET nome=@nome,cpf=@cpf,dtNascimento=@dtNascimento WHERE idAssociado=@idAssociado"

                Using cmd As New SqlCommand()
                    cmd.Parameters.Add("@idAssociado", SqlDbType.Int).Value = id
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = DadosAssociados.Nome
                    cmd.Parameters.Add("@cpf", SqlDbType.VarChar).Value = DadosAssociados.Cpf
                    cmd.Parameters.Add("@dtNascimento", SqlDbType.DateTime).Value = DadosAssociados.DtNascimento

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

        Public Sub DeleteAssociados(id As Integer)

            Dim strSql As String = "DELETE FROM associadosXempresa WHERE idAssociado=@idAssociado;DELETE FROM cadAssociados WHERE idAssociado=@idAssociado"

            Using cmd As New SqlCommand()
                cmd.Parameters.Add("@idAssociado", SqlDbType.Int).Value = id

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


        Public Sub AddAssociados()

            If ValidarCpf(DadosAssociados.Cpf) Then

                Dim strSql As String = "INSERT INTO cadAssociados (nome,cpf,dtNascimento) VALUES (@nome,@cpf,@dtNascimento)"

                Using cmd As New SqlCommand()
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = DadosAssociados.Nome
                    cmd.Parameters.Add("@cpf", SqlDbType.VarChar).Value = DadosAssociados.Cpf
                    cmd.Parameters.Add("@dtNascimento", SqlDbType.DateTime).Value = DadosAssociados.DtNascimento

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

        Public Function GetAssociadosEmpresa(id As Integer?) As IEnumerable(Of Models.Associados)

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
                    Return New List(Of Models.Associados)
                Finally
                    db.Close()
                End Try

            End Using

        End Function

        Public Function GetAssociados(id As Integer?) As IEnumerable(Of Models.Associados)

            Dim strSql As String = "SELECT * FROM cadAssociados "

            Using cmd As New SqlCommand()

                If id > 0 Then
                    strSql &= " WHERE idAssociado=@idAssociado "
                    cmd.Parameters.Add("@idAssociado", SqlDbType.Int).Value = id
                End If

                cmd.CommandText = strSql
                cmd.Connection = db
                Try

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

                        End While
                    End Using

                    Return retAssociados

                Catch ex As Exception
                    Console.WriteLine(ex.ToString)
                    Return New List(Of Models.Associados)
                Finally
                    db.Close()
                End Try

            End Using

        End Function

        Public Function GetAssociadosByCpf(cpf As String) As IEnumerable(Of Models.Associados)

            If String.IsNullOrEmpty(cpf) Then
                Return New List(Of Models.Associados)
            End If


            Dim strSql As String = "SELECT * FROM cadAssociados WHERE cpf=@cpf "

            Using cmd As New SqlCommand()

                cmd.Parameters.Add("@cpf", SqlDbType.VarChar).Value = cpf
                cmd.CommandText = strSql
                cmd.Connection = db
                Try

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

                        End While
                    End Using

                    Return retAssociados

                Catch ex As Exception
                    Console.WriteLine(ex.ToString)
                    Return New List(Of Models.Associados)
                Finally
                    db.Close()
                End Try

            End Using

        End Function
        Private Function ValidarCpf(cpf As String) As Boolean

            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT COUNT(1) FROM cadAssociados WHERE cpf=@cpf"
                cmd.Parameters.Add("@cpf", SqlDbType.VarChar).Value = cpf
                cmd.Connection = db
                db.Open()

                If Convert.ToInt32(cmd.ExecuteScalar()) >= 1 Then

                    Return False

                End If

                db.Close()
            End Using

            Return True

        End Function
    End Class
End Namespace