Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class EmpresaController
        Inherits ApiController

        Private empresas As New Dal.Empresas

        Public Function GetEmpresas(id As Integer) As List(Of Models.Empresas)
            Return empresas.GetEmpresas(id)
        End Function
        Public Function GetEmpresaAssociados(id As Integer) As List(Of Models.Empresas)
            Return empresas.GetEmpresasAssociados(id)
        End Function


        Public Function GetEmpresasByCnpj(cnpj As String) As List(Of Models.Empresas)
            Return empresas.GetEmpresasByCnpj(cnpj)
        End Function

        Public Sub PostEmpresas(nomeempresa As String, cnpj As String, id As Integer)

            If Not (String.IsNullOrEmpty(nomeempresa) AndAlso String.IsNullOrEmpty(cnpj)) Then
                empresas.DadosEmpresas.NomeEmpresa = nomeempresa
                empresas.DadosEmpresas.Cnpj = cnpj
                If id > 0 Then
                    empresas.AlterarEmpresas(id)
                Else
                    empresas.AddEmpresas()
                End If
            End If
        End Sub

        Public Sub DeleteEmpresas(id As Integer)

            If id > 0 Then
                empresas.DeleteEmpresas(id)
            End If
        End Sub
    End Class
End Namespace