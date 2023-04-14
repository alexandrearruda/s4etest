Namespace Models

    Public Class Empresas

        Property IdEmpresa As Integer

        Property NomeEmpresa As String

        Property Cnpj As String

        Property Associados As New List(Of Models.Associados)
    End Class
End Namespace