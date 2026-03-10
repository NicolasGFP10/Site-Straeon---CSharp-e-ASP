<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ExcluirConta.aspx.cs" Inherits="SiteStraeon.ExcluirConta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="delete-card">
        <h1 class="delete-title">Desativar Conta</h1>
        <p class="delete-text">
            Isto apenas coloca sua conta como desativada. Outros usuários não irão conseguir ver nem interagir com a sua conta. 
            Caso queira reativar sua conta, basta logar novamente.
       
        </p>

        <h2 class="delete-title" style="font-size: 18px;">Confirme sua senha atual</h2>
        <asp:TextBox ID="TextboxSenhaAtual" runat="server" CssClass="delete-input" placeholder="Sua senha atual" TextMode="Password"></asp:TextBox>

        <p class="delete-text">Digite <strong>"desativar conta"</strong> e depois clique no botão para confirmar.</p>
        <asp:TextBox ID="TextboxConfirmacao" CssClass="delete-input" runat="server" placeholder="Digite aqui"></asp:TextBox>

        <asp:Button ID="ConfirmarDesativar" runat="server" CssClass="delete-button" Text="Desativar Conta" OnClick="ConfirmarDesativar_Click" />

        <asp:Label ID="Alerta" CssClass="delete-alert" Text="" runat="server" Visible="false"></asp:Label>
    </div>
</asp:Content>
