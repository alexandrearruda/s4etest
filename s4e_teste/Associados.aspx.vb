Public Class Associados
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RecuperarAssociados(0)
            CarregarComboEmpresas()
        End If
    End Sub

    Private Sub btnConsultarNome_Click(sender As Object, e As EventArgs) Handles btnConsultarNome.Click
        RecuperarAssociadosNome(txtConsultarNome.Text)
    End Sub
    Private Sub btnConsultaData_Click(sender As Object, e As EventArgs) Handles btnConsultaData.Click
        RecuperarAssociadosData(txtConsultaData.Text)
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
        associados.RemoverAssociacaoEmpresa(lstAssociarEmpAlt)
        associados.AssociarEmpresa(lstAssociarEmpAlt)

        gvAssociados.DataSource = associados.GetAssociados(Convert.ToInt32(id))
        gvAssociados.DataBind()

    End Sub

    Private Sub InserirAssociados()
        Dim associados As New Controllers.AssociadosController
        associados.PostAssociados(txtNome.Text, txtCpf.Text, txtData.Text, 0)
        associados.AssociarEmpresa(lstAssociarEmpIns)
        gvAssociados.DataSource = associados.GetAssociadosByCpf(txtCpf.Text)
        gvAssociados.DataBind()

    End Sub
    Private Sub RecuperarAssociadosNome(nome As String)
        Dim associados As New Controllers.AssociadosController
        gvAssociados.DataSource = associados.GetAssociadosByNome(nome)
        gvAssociados.DataBind()
    End Sub
    Private Sub RecuperarAssociadosData(data As DateTime)
        Dim associados As New Controllers.AssociadosController
        gvAssociados.DataSource = associados.GetAssociadosByData(data)
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
    Private Sub CarregarComboEmpresas()
        Dim associados As New Controllers.AssociadosController
        For Each item In associados.GetComboEmpresas()
            lstAssociarEmpAlt.Items.Add(New ListItem(item.NomeEmpresa, item.IdEmpresa))
            lstAssociarEmpIns.Items.Add(New ListItem(item.NomeEmpresa, item.IdEmpresa))
        Next
    End Sub

    Private Sub gvAssociados_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAssociados.SelectedIndexChanged
        lstAssociarEmpAlt.ClearSelection()
        Dim grid As GridView = CType(sender, GridView)
        Dim linha As Integer = grid.SelectedRow.RowIndex
        If linha >= 0 Then
            txtIdAlterar.Text = grid.Rows(linha).Cells(1).Text
            txtRemoverId.Text = grid.Rows(linha).Cells(1).Text
            txtNomeAlterar.Text = grid.Rows(linha).Cells(2).Text.ToString()
            txtCpfAlterar.Text = grid.Rows(linha).Cells(3).Text
            txtdtNascimentoAlterar.Text = grid.Rows(linha).Cells(4).Text
        End If
        GetAssociacaoEmpresas()
    End Sub

    Private Sub GetAssociacaoEmpresas()
        Dim associados As New Controllers.AssociadosController
        Dim lstAssociacoes As List(Of Integer) = associados.GetAssociacaoEmpresas(txtIdAlterar.Text)
        For Each item In lstAssociacoes
            lstAssociarEmpAlt.Items.FindByValue(item).Selected = True
        Next
    End Sub

End Class