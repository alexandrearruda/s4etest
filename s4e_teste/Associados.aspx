<%@ Page Title="Associados" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Associados.aspx.vb" Inherits="s4e_teste.About" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="pnFiltrosAssociados" runat="server" CssClass="form">
        <table cellpadding="0" cellspacing="0" class="form-check-input" style="width: 100%">
            <tr>

                <td valign="bottom">
                    <span class="form-text">Consultar associados</span>
                    &nbsp<asp:Button ID="btnConsultarAssociados" Text="Pesquisar" class="form-text" runat="server" />
                    &nbsp &nbsp<span class="form-text">Consultar associados por id:</span>
                    <asp:TextBox ID="txtCosultarId" Width="35px" runat="server"></asp:TextBox>
                    &nbsp<asp:Button class="form-text" ID="btnConsultarAssociadosId" Text="Pesquisar" runat="server" />
                     &nbsp &nbsp<span class="form-text">Consultar associados por CPF:</span>
                    <asp:TextBox ID="txtCosultarCpf" Width="300px" runat="server"></asp:TextBox>
                    &nbsp<asp:Button class="form-text" ID="btnConsultarAssociadosCpf" Text="Pesquisar" runat="server" />
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
                    <span class="form-text">Inserir associado: </span>
                    <br />
                </td>
            </tr>
            <tr>
                <td valign="bottom">
                    <asp:Label class="form-text" Text="Nome:" ID="lblNome" runat="server"></asp:Label>
                    <asp:TextBox class="form-text" ID="txtNome" runat="server" Width="300px"></asp:TextBox>
                    <asp:Label class="form-text" Text="Cpf:" ID="lblCpf" runat="server"></asp:Label>
                    &nbsp &nbsp<asp:TextBox class="form-text" ID="txtCpf" runat="server"></asp:TextBox>
                    <asp:Label class="form-text" Text="Data Nascimento:" ID="lblDtNascimento" runat="server"></asp:Label>
                    <asp:TextBox ID="txtData" runat="server" class="form-text" MaxLength="10" Width="100px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtData_CalendarExtender" runat="server" TargetControlID="txtData" Format="dd/MM/yyyy"></cc1:CalendarExtender>
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
                    <span class="form-text">Alterar associado: </span>
                    <br />
                </td>
            </tr>
            <tr>
                <td valign="bottom">
                    <asp:Label class="form-text" Text="Id:" ID="lblIdAlterar" runat="server"></asp:Label>
                    <asp:TextBox class="form-text" ID="txtIdAlterar" runat="server" Width="30px"></asp:TextBox>
                    <asp:Label class="form-text" Text="Nome:" ID="lblNomeAlterar" runat="server"></asp:Label>
                    <asp:TextBox class="form-text" ID="txtNomeAlterar" runat="server" Width="300px"></asp:TextBox>
                    <asp:Label class="form-text" Text="Cpf:" ID="lblCpfAlterar" runat="server"></asp:Label>
                    &nbsp &nbsp<asp:TextBox class="form-text" ID="txtCpfAlterar" runat="server"></asp:TextBox>
                    <asp:Label class="form-text" Text="Data Nascimento:" ID="lbldtNascimentoAlterar" runat="server"></asp:Label>
                    <asp:TextBox ID="txtdtNascimentoAlterar" runat="server" class="form-text" MaxLength="10" Width="100px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdtNascimentoAlterar" Format="dd/MM/yyyy"></cc1:CalendarExtender>
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
                    <span class="form-text">Remover associado: </span>
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
    <asp:GridView ID="gvAssociados" runat="server" CssClass="form-text" ShowHeader="true" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="Vertical" Width="100%" AutoGenerateColumns="true" ShowFooter="false">

        <FooterStyle BackColor="Tan" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <SortedAscendingCellStyle BackColor="#FAFAE7" />
        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
        <SortedDescendingCellStyle BackColor="#E1DB9C" />
        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
        <EmptyDataTemplate>Nenhum associado cadastrado</EmptyDataTemplate>
        <RowStyle CssClass="form-text" />
      
    </asp:GridView>
</asp:Content>
