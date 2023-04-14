Imports System.Security.Cryptography
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Web.Http
Imports s4e_teste.Dal

Public Class AssociadosController
    Inherits ApiController

    Private associados As New Dal.Associados

    Public Function GetAssociados(id As Integer) As List(Of Models.Associados)
        Return associados.GetAssociados(id)
    End Function

    Public Sub PostAssociados(nome As String, cpf As String, dtNascimento As String, id As Integer)

        If Not (String.IsNullOrEmpty(nome) AndAlso String.IsNullOrEmpty(cpf) AndAlso String.IsNullOrEmpty(dtNascimento)) Then
            associados.DadosAssociados.Nome = nome
            associados.DadosAssociados.Cpf = cpf
            associados.DadosAssociados.DtNascimento = dtNascimento
            If id > 0 Then
                associados.AlterarAssociados(id)
            Else
                associados.AddAssociados()
            End If
        End If
    End Sub

    Public Sub DeleteAssociados(id As Integer)

        If id > 0 Then
            associados.DeleteAssociados(id)
        End If
    End Sub


End Class
