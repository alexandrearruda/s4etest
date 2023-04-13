Imports System.Net.Http
Imports Newtonsoft.Json

Namespace Classes

    'Public Async Task<List<Produto>> GetProdutosAsync()
    '    {
    '        Try
    '        {
    '            String url = "http://www.macwebapi.somee.com/api/produtos/";
    '            var response = await client.GetStringAsync(url);
    '            var produtos = JsonConvert.DeserializeObject < List < Produto >> (response);
    '            Return produtos;
    '        }
    '        Catch (Exception ex)
    '        {
    '            Throw ex;
    '        }
    '    }
    Public Class DataService
        Dim client As HttpClient = New HttpClient()

        Public Async Function GetAssociadosAsync() As Threading.Tasks.Task(Of Models.Associados)
            Dim urlAssociados As String = "https://localhost:44317/api/GetAssociados/"
            Dim response = Await client.GetStringAsync(urlAssociados)
            Dim associados = JsonConvert.DeserializeObject(response)
            Return associados
        End Function

    End Class
End Namespace