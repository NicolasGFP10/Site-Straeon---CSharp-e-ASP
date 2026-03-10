<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SiteStraeon.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="user-profile-header">
        <h1>Login</h1>
    </div>

    <div class="form-card">
        <center><h4><asp:Label ID="Alerta" runat="server"></asp:Label></h4></center>

        <label for="TextboxEmail" class="form-label">Email</label>
        <asp:TextBox ID="TextboxEmail" runat="server" CssClass="form-input" TextMode="Email" placeholder="Seu email"></asp:TextBox>

        <label for="TextboxSenha" class="form-label">Senha</label>
        <asp:TextBox ID="TextboxSenha" runat="server" CssClass="form-input" TextMode="Password" placeholder="Sua senha"></asp:TextBox>

        <asp:Button ID="LoginButton" runat="server" CssClass="form-button" OnClick="LoginButton_Click" Text="Entrar" />
    </div>
</asp:Content>
