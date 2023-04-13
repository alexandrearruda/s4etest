Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Web.Http

Public Class AssociadosController
    Inherits ApiController

    Private associados As List(Of Models.Associados)

    Public Function GetAssociados(id As Integer) As List(Of Models.Associados)
        associados = Dal.Associados.GetAssociados(id)
        Return associados
    End Function


End Class
