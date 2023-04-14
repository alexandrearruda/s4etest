Public Class About
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RecuperarAssociados(0)
        End If
    End Sub

    Private Sub btnConsultarAssociados_Click(sender As Object, e As EventArgs) Handles btnConsultarAssociados.Click
        RecuperarAssociados(String.Empty)
    End Sub

    Private Sub btnConsultarAssociadosId_Click(sender As Object, e As EventArgs) Handles btnConsultarAssociadosId.Click
        RecuperarAssociados(txtCosultarId.Text)
    End Sub

    Private Sub btnConsultarAssociadosCpf_Click(sender As Object, e As EventArgs) Handles btnConsultarAssociadosCpf.Click
        RecuperarAssociadosCpf(txtCosultarCpf.Text)
    End Sub
    Private Sub btnInserir_Click(sender As Object, e As EventArgs) Handles btnInserir.Click
        InserirAssociados()
    End Sub

    Private Sub btnRemover_Click(sender As Object, e As EventArgs) Handles btnRemover.Click
        RemoverAssociados(txtRemoverId.Text)
    End Sub

    Private Sub btnAlterar_Click(sender As Object, e As EventArgs) Handles btnAlterar.Click
        AlterarAssociados(txtIdAlterar.Text)
    End Sub

    Private Sub RemoverAssociados(id As String)

        If String.IsNullOrWhiteSpace(id) OrElse id = "0" Then
            Return
        End If

        Dim associados As New Controllers.AssociadosController
        associados.DeleteAssociados(Convert.ToInt32(id))
        gvAssociados.DataSource = associados.GetAssociados(0)
        gvAssociados.DataBind()

    End Sub

    Private Sub AlterarAssociados(id As String)

        If String.IsNullOrWhiteSpace(id) OrElse id = "0" Then
            Return
        End If

        Dim associados As New Controllers.AssociadosController
        associados.PostAssociados(txtNomeAlterar.Text, txtCpfAlterar.Text, txtdtNascimentoAlterar.Text, id)
        gvAssociados.DataSource = associados.GetAssociados(Convert.ToInt32(id))
        gvAssociados.DataBind()

    End Sub

    Private Sub InserirAssociados()
        Dim associados As New Controllers.AssociadosController
        associados.PostAssociados(txtNome.Text, txtCpf.Text, txtData.Text, 0)
        gvAssociados.DataSource = associados.GetAssociadosByCpf(txtCpf.Text)
        gvAssociados.DataBind()

    End Sub

    Private Sub RecuperarAssociadosCpf(cpf As String)
        Dim associados As New Controllers.AssociadosController
        gvAssociados.DataSource = associados.GetAssociadosByCpf(cpf)
        gvAssociados.DataBind()
    End Sub

    Private Sub RecuperarAssociados(id As String)

        Dim associados As New Controllers.AssociadosController

        If String.IsNullOrWhiteSpace(id) Then
            id = "0"
        End If

        gvAssociados.DataSource = associados.GetAssociados(Convert.ToInt32(id))
        gvAssociados.DataBind()


    End Sub


End Class