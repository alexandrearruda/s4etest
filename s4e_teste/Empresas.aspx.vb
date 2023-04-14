Imports System.Runtime.InteropServices.ComTypes

Public Class Contact
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RecuperarEmpresas(0)
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
        gvEmpresas.DataSource = Empresas.GetEmpresas(0)
        gvEmpresas.DataBind()

    End Sub

    Private Sub AlterarEmpresas(id As String)

        If String.IsNullOrWhiteSpace(id) OrElse id = "0" Then
            Return
        End If

        Dim empresas As New Controllers.EmpresaController
        empresas.PostEmpresas(txtNomeAlterar.Text, txtCnpjAlterar.Text, id)
        gvEmpresas.DataSource = Empresas.GetEmpresas(Convert.ToInt32(id))
        gvEmpresas.DataBind()

    End Sub

    Private Sub InserirEmpresas()
        Dim empresas As New Controllers.EmpresaController
        empresas.PostEmpresas(txtNome.Text, txtCnpj.Text, 0)
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

        gvEmpresas.DataSource = Empresas.GetEmpresas(Convert.ToInt32(id))
        gvEmpresas.DataBind()


    End Sub
End Class