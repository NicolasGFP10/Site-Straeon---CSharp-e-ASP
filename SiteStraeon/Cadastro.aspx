<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="SiteStraeon.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="user-profile-header">
        <h1>Cadastre-se no Straeon</h1>
    </div>

    <div class="register-card">

        <label for="TextboxNome" class="register-label">Nome</label>
        <asp:TextBox ID="TextboxNome" runat="server" CssClass="register-input" placeholder="Seu nome"></asp:TextBox>

        <label for="TextboxEmail" class="register-label">Email</label>
        <asp:TextBox ID="TextboxEmail" runat="server" CssClass="register-input" TextMode="Email" placeholder="Seu email"></asp:TextBox>

        <label for="TextboxSenha" class="register-label">Senha</label>
        <asp:TextBox ID="TextboxSenha" runat="server" CssClass="register-input" TextMode="Password" placeholder="Sua senha"></asp:TextBox>

        <label for="TextboxConfirmarSenha" class="register-label">Confirmar senha</label>
        <asp:TextBox ID="TextboxConfirmarSenha" runat="server" CssClass="register-input" TextMode="Password" placeholder="Confirme sua senha"></asp:TextBox>

        <label for="FileUpload" class="register-label">Foto de usuário</label>
        <asp:FileUpload ID="FileUpload" CssClass="register-file" accept=".jpeg,.jpg,.png,.webp" runat="server" />

        <asp:Button ID="CadastroButton" runat="server" CssClass="register-button" OnClick="CadastroButton_Click" Text="Cadastrar" />

        <asp:Label ID="Alerta" runat="server" CssClass="register-alert" Text="" Visible="false"></asp:Label>
    </div>
</asp:Content>
