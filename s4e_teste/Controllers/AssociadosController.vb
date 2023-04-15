Imports System.Web.Http
Imports s4e_teste.Dal

Namespace Controllers
    Public Class AssociadosController
        Inherits ApiController

        Private associados As New Dal.Associados
        Public Function GetAssociadosByNome(nome As String) As List(Of Models.Associados)
            Return associados.GetAssociadosByNome(nome)
        End Function

        Public Function GetAssociadosByData(data As DateTime) As List(Of Models.Associados)
            Return associados.GetAssociadosByData(data)
        End Function

        Public Function GetAssociadosEmpresa(id As Integer) As List(Of Models.Associados)
            Return associados.GetAssociadosEmpresa(id)
        End Function

        Public Function GetAssociados(id As Integer) As List(Of Models.Associados)
            Return associados.GetAssociados(id)
        End Function

        Public Function GetAssociadosByCpf(cpf As String) As List(Of Models.Associados)
            Return associados.GetAssociadosByCpf(cpf)
        End Function

        Public Function GetComboEmpresas() As List(Of Models.Empresas)
            Return associados.GetComboEmpresas()
        End Function

        Public Function GetAssociacaoEmpresas(idAssociado As Integer) As List(Of Integer)
            Return associados.GetAssociacaoEmpresas(idAssociado)
        End Function

        Public Sub AssociarEmpresa(listEmpresas As ListBox)

            Dim selEmpresa As New List(Of Integer)
            For Each item In listEmpresas.GetSelectedIndices()
                selEmpresa.Add(listEmpresas.Items.Item(item).Value)
            Next

            associados.AssociarEmpresa(selEmpresa)
        End Sub

        Public Sub RemoverAssociacaoEmpresa(listEmpresas As ListBox)

            Dim selEmpresa As New List(Of Integer)
            For Each item As ListItem In listEmpresas.Items
                If Not item.Selected Then
                    selEmpresa.Add(item.Value)
                End If
            Next
            If selEmpresa.Count > 0 Then
                associados.RemoverAssociacaoEmpresa(selEmpresa)
            End If
        End Sub

        Public Sub PostAssociados(nome As String, cpf As String, dtNascimento As String, id As Integer)

            If Not (String.IsNullOrEmpty(nome) AndAlso String.IsNullOrEmpty(cpf) AndAlso String.IsNullOrEmpty(dtNascimento)) Then

                associados.DadosAssociados.Nome = nome
                associados.DadosAssociados.Cpf = cpf
                associados.DadosAssociados.DtNascimento = dtNascimento
                If id > 0 Then
                    associados.AlterarAssociados(id)
                    associados.DadosAssociados.IdAssociado = id
                Else
                    associados.AddAssociados()
                    associados.DadosAssociados.IdAssociado = associados.GetAssociadosByCpf(cpf).Item(0).IdAssociado
                End If

            End If
        End Sub

        Public Sub DeleteAssociados(id As Integer)

            If id > 0 Then
                associados.DeleteAssociados(id)
            End If
        End Sub


    End Class
End Namespace