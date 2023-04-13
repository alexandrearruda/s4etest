Imports s4e_teste.Interfaces

Public Class About
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Private Sub btnConsultarAssociados_Click(sender As Object, e As EventArgs) Handles btnConsultarAssociados.Click
        RecuperarAssociados(False)
    End Sub
    Private Sub RecuperarAssociados(buscaId As Boolean)


    End Sub
End Class