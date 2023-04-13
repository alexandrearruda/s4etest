Imports System.ServiceModel

Namespace Interfaces

    <ServiceContract(Namespace:="https://localhost:44317/api/", Name:="Api")>
    Public Interface IAssociados
        Function GetAssociados(id As Integer?) As IEnumerable(Of Models.Associados)
    End Interface
End Namespace