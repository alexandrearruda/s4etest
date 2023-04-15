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
                    empresas.DadosEmpresas.IdEmpresa = id
                Else
                    empresas.AddEmpresas()
                    empresas.DadosEmpresas.IdEmpresa = empresas.GetEmpresasByCnpj(cnpj).Item(0).IdEmpresa
                End If
            End If
        End Sub

        Public Sub DeleteEmpresas(id As Integer)

            If id > 0 Then
                empresas.DeleteEmpresas(id)
            End If
        End Sub

        Public Function GetComboAssociados() As List(Of Models.Associados)
            Return empresas.GetComboAssociados()
        End Function

        Public Function GetRelacaoAssociados(idEmpresa As Integer) As List(Of Integer)
            Return empresas.GetRelacaoAssociados(idEmpresa)
        End Function

        Public Sub RelacionarAssociados(listAssociados As ListBox)

            Dim selAssociado As New List(Of Integer)
            For Each item In listAssociados.GetSelectedIndices()
                selAssociado.Add(listAssociados.Items.Item(item).Value)
            Next

            empresas.RelacionarAssociados(selAssociado)
        End Sub

        Public Sub RemoverRelacaoAssociados(listAssociados As ListBox)

            Dim selAssociado As New List(Of Integer)
            For Each item As ListItem In listAssociados.Items
                If Not item.Selected Then
                    selAssociado.Add(item.Value)
                End If
            Next
            If selAssociado.Count > 0 Then
                empresas.RemoverRelacaoAssociados(selAssociado)
            End If
        End Sub
    End Class
End Namespace