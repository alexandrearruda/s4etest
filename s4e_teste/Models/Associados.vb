Namespace Models
    Public Class Associados

        Property IdAssociado As Integer

        Property Nome As String

        Property Cpf As String

        Property DtNascimento As Date

        Property Empresas As New List(Of Models.Empresas)

    End Class
End Namespace