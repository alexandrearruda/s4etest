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

    Public Sub PostAssociados(nome As String, cpf As String, dtNascimento As String)

        If Not (String.IsNullOrEmpty(nome) AndAlso String.IsNullOrEmpty(cpf) AndAlso String.IsNullOrEmpty(dtNascimento)) Then
            associados.dadosAssociados.Nome = nome
            associados.dadosAssociados.Cpf = cpf
            associados.dadosAssociados.DtNascimento = dtNascimento
            associados.AddAssociados()
        End If
    End Sub


End Class
