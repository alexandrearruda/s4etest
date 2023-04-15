Imports System.Runtime.InteropServices.ComTypes

Public Class Empresas
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RecuperarEmpresas(0)
            CarregarComboAssociados()
        End If
    End Sub

    Private Sub btnConsultarEmpresas_Click(sender As Object, e As EventArgs) Handles btnConsultarempresas.Click
        RecuperarEmpresas(String.Empty)
    End Sub

    Private Sub btnConsultarEmpresasId_Click(sender As Object, e As EventArgs) Handles btnConsultarempresasId.Click
        RecuperarEmpresas(txtCosultarId.Text)
    End Sub

    Private Sub btnConsultarEmpresasCnpj_Click(sender As Object, e As EventArgs) Handles btnConsultarEmpresasCnpj.Click
        RecuperarEmpresasCnpj(txtCosultarCnpj.Text)
    End Sub
    Private Sub btnInserir_Click(sender As Object, e As EventArgs) Handles btnInserir.Click
        InserirEmpresas()
    End Sub

    Private Sub btnRemover_Click(sender As Object, e As EventArgs) Handles btnRemover.Click
        RemoverEmpresas(txtRemoverId.Text)
    End Sub

    Private Sub btnAlterar_Click(sender As Object, e As EventArgs) Handles btnAlterar.Click
        AlterarEmpresas(txtIdAlterar.Text)
    End Sub

    Private Sub RemoverEmpresas(id As String)

        If String.IsNullOrWhiteSpace(id) OrElse id = "0" Then
            Return
        End If

        Dim empresas As New Controllers.EmpresaController
        empresas.DeleteEmpresas(Convert.ToInt32(id))
        gvEmpresas.DataSource = empresas.GetEmpresas(0)
        gvEmpresas.DataBind()

    End Sub

    Private Sub AlterarEmpresas(id As String)

        If String.IsNullOrWhiteSpace(id) OrElse id = "0" Then
            Return
        End If

        Dim empresas As New Controllers.EmpresaController
        empresas.PostEmpresas(txtNomeAlterar.Text, txtCnpjAlterar.Text, id)
        empresas.RemoverRelacaoAssociados(lstRelacionarAssocAlt)
        empresas.RelacionarAssociados(lstRelacionarAssocAlt)
        gvEmpresas.DataSource = empresas.GetEmpresas(Convert.ToInt32(id))
        gvEmpresas.DataBind()

    End Sub

    Private Sub InserirEmpresas()
        Dim empresas As New Controllers.EmpresaController
        empresas.PostEmpresas(txtNome.Text, txtCnpj.Text, 0)
        empresas.RelacionarAssociados(lstRelacionarAssocIns)
        gvEmpresas.DataSource = empresas.GetEmpresasByCnpj(txtCnpj.Text)
        gvEmpresas.DataBind()

    End Sub

    Private Sub RecuperarEmpresasCnpj(cnpj As String)
        Dim empresas As New Controllers.EmpresaController
        gvEmpresas.DataSource = empresas.GetEmpresasByCnpj(cnpj)
        gvEmpresas.DataBind()
    End Sub

    Private Sub RecuperarEmpresas(id As String)

        Dim empresas As New Controllers.EmpresaController

        If String.IsNullOrWhiteSpace(id) Then
            id = "0"
        End If

        gvEmpresas.DataSource = empresas.GetEmpresas(Convert.ToInt32(id))
        gvEmpresas.DataBind()


    End Sub
    Private Sub CarregarComboAssociados()
        Dim empresas As New Controllers.EmpresaController
        For Each item In empresas.GetComboAssociados
            lstRelacionarAssocAlt.Items.Add(New ListItem(item.Nome, item.IdAssociado))
            lstRelacionarAssocIns.Items.Add(New ListItem(item.Nome, item.IdAssociado))
        Next
    End Sub

    Private Sub gvEmpresas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvEmpresas.SelectedIndexChanged
        lstRelacionarAssocAlt.ClearSelection()
        Dim grid As GridView = CType(sender, GridView)
        Dim linha As Integer = grid.SelectedRow.RowIndex
        If linha >= 0 Then
            txtIdAlterar.Text = grid.Rows(linha).Cells(1).Text
            txtRemoverId.Text = grid.Rows(linha).Cells(1).Text
            txtNomeAlterar.Text = grid.Rows(linha).Cells(2).Text.ToString()
            txtCnpjAlterar.Text = grid.Rows(linha).Cells(3).Text
        End If
        GetRelacaoAssociados()
    End Sub

    Private Sub GetRelacaoAssociados()
        Dim empresas As New Controllers.EmpresaController
        Dim lstAssociacoes As List(Of Integer) = empresas.GetRelacaoAssociados(txtIdAlterar.Text)
        For Each item In lstAssociacoes
            lstRelacionarAssocAlt.Items.FindByValue(item).Selected = True
        Next
    End Sub
End Class