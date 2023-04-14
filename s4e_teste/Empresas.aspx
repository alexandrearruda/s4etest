<%@ Page Title="Empresas" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Empresas.aspx.vb" Inherits="s4e_teste.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <asp:Panel ID="pnFiltrosempresas" runat="server" CssClass="form">
        <table cellpadding="0" cellspacing="0" class="form-check-input" style="width: 100%">
            <tr>

                <td valign="bottom">
                    <span class="form-text">Consultar empresas</span>
                    &nbsp<asp:Button ID="btnConsultarempresas" Text="Pesquisar" class="form-text" runat="server" />
                    &nbsp &nbsp<span class="form-text">Consultar empresas por id:</span>
                    <asp:TextBox ID="txtCosultarId" Width="35px" runat="server"></asp:TextBox>
                    <asp:Button class="form-text" ID="btnConsultarempresasId" Text="Pesquisar" runat="server" />
                     &nbsp &nbsp<span class="form-text">Consultar empresas por CNPJ:</span>
                    <asp:TextBox ID="txtCosultarCnpj" Width="300px" runat="server"></asp:TextBox>
                    &nbsp<asp:Button class="form-text" ID="btnConsultarEmpresasCnpj" Text="Pesquisar" runat="server" />
     
                    <br />
                </td>
            </tr>

        </table>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnInserir" runat="server" CssClass="form">
        <table cellpadding="0" cellspacing="0" class="form-check-input" style="width: 100%">
            <tr>
                <td>
                    <span class="form-text">Inserir empresa: </span>
                    <br />
                </td>
            </tr>
            <tr>
                <td valign="bottom">
                    <asp:Label class="form-text" Text="Nome:" ID="lblNome" runat="server"></asp:Label>
                    <asp:TextBox class="form-text" ID="txtNome" runat="server" Width="300px"></asp:TextBox>
                    <asp:Label class="form-text" Text="Cnpj:" ID="lblCnpj" runat="server"></asp:Label>
                    &nbsp &nbsp<asp:TextBox class="form-text" ID="txtCnpj" runat="server"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td valign="bottom">
                    <asp:Button ID="btnInserir" Text="Inserir" class="form-text" runat="server" />
                    <br />
                </td>
            </tr>
        </table>

    </asp:Panel>
    <asp:Panel ID="pnAlterar" runat="server" CssClass="form">
        <table cellpadding="0" cellspacing="0" class="form-check-input" style="width: 100%">
            <tr>
                <td>
                    <span class="form-text">Alterar empresa: </span>
                    <br />
                </td>
            </tr>
            <tr>
                <td valign="bottom">
                    <asp:Label class="form-text" Text="Id:" ID="lblIdAlterar" runat="server"></asp:Label>
                    <asp:TextBox class="form-text" ID="txtIdAlterar" runat="server" Width="30px"></asp:TextBox>
                    <asp:Label class="form-text" Text="Nome:" ID="lblNomeAlterar" runat="server"></asp:Label>
                    <asp:TextBox class="form-text" ID="txtNomeAlterar" runat="server" Width="300px"></asp:TextBox>
                    <asp:Label class="form-text" Text="Cnpj:" ID="lblCnpjAlterar" runat="server"></asp:Label>
                    &nbsp &nbsp<asp:TextBox class="form-text" ID="txtCnpjAlterar" runat="server"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td valign="bottom">
                    <asp:Button ID="btnAlterar" Text="Alterar" class="form-text" runat="server" />
                    <br />
                </td>
            </tr>
        </table>

    </asp:Panel>
    <asp:Panel ID="pnRemover" runat="server" CssClass="form">
        <table cellpadding="0" cellspacing="0" class="form-check-input" style="width: 100%">
            <tr>
                <td>
                    <span class="form-text">Remover empresa: </span>
                    <br />
                </td>
            </tr>
            <tr>
                <td valign="bottom">
                    <asp:Label class="form-text" Text="Id:" ID="lblRemoverId" runat="server"></asp:Label>
                    <asp:TextBox class="form-text" ID="txtRemoverId" runat="server" Width="30px"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td valign="bottom">
                    <asp:Button ID="btnRemover" Text="Remover" class="form-text" runat="server" />
                    <br />
                </td>
            </tr>
        </table>

    </asp:Panel>
     <asp:GridView ID="gvEmpresas" runat="server" CssClass="form-text" ShowHeader="true" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="Vertical" Width="100%" AutoGenerateColumns="true" ShowFooter="false">

        <FooterStyle BackColor="Tan" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <SortedAscendingCellStyle BackColor="#FAFAE7" />
        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
        <SortedDescendingCellStyle BackColor="#E1DB9C" />
        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
        <EmptyDataTemplate>Nenhuma empresa cadastrada</EmptyDataTemplate>
        <RowStyle CssClass="form-text" />
      
    </asp:GridView>
</asp:Content>
