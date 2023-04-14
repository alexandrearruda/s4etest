Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http

Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)
        ' Web API configuration and services

        ' Web API routes
        config.MapHttpAttributeRoutes()

        config.Routes.MapHttpRoute(
            name:="DeafultApi",
            routeTemplate:="api/{controller}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )
        config.Routes.MapHttpRoute(
           name:="PostAssociados",
           routeTemplate:="api/{controller}/{nome}/{cpf}/{dtNascimento}",
           defaults:=New With {.nome = RouteParameter.Optional, .cpf = RouteParameter.Optional, .dtNascimento = RouteParameter.Optional}
       )
    End Sub
End Module
