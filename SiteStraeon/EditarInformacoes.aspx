<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditarInformacoes.aspx.cs" Inherits="SiteStraeon.EditarInformacoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="user-profile-header">
        <h1>Editar Informações</h1>
    </div>

    <div class="profile-edit-card">

        <h1 class="profile-section-title">Sua foto</h1>
        <asp:Image ID="ImagemPerfilUsuario" CssClass="profile-preview" runat="server" />
        <asp:FileUpload ID="FileUploadImagem" CssClass="input-file" accept=".jpeg,.jpg,.png,.webp" runat="server" />

        <div class="button-row">
            <asp:Button ID="TrocarFotoPerfil" CssClass="profile-button-secondary" runat="server"
                Text="Trocar foto de perfil" OnClick="TrocarFotoPerfil_Click" />
            <asp:Button ID="RemoverFotoPerfil" CssClass="profile-button-danger" Visible="false"
                runat="server" Text="Remover foto" OnClick="RemoverFotoPerfil_Click" />
        </div>

        <h1 class="profile-section-title">Nome</h1>
        <asp:TextBox ID="TextboxNome" CssClass="profile-input" runat="server" placeholder="Seu nome"></asp:TextBox>
        <asp:Button ID="EditarNome" CssClass="profile-button" runat="server" Text="Mudar nome de usuário" OnClick="EditarNome_Click"/>

        <h1 class="profile-section-title">Senha</h1>
        <asp:TextBox ID="TextboxSenhaAtual" CssClass="profile-input" runat="server"
            placeholder="Senha atual" TextMode="Password"></asp:TextBox>

        <asp:TextBox ID="TextboxSenhaNova" CssClass="profile-input" runat="server"
            placeholder="Nova senha" TextMode="Password"></asp:TextBox>

        <asp:Button ID="TrocarSenha" CssClass="profile-button" runat="server"
            Text="Atualizar senha" OnClick="TrocarSenha_Click" />

        <asp:Button ID="ExcluirConta" CssClass="profile-button-danger" runat="server"
            Text="Excluir conta" OnClick="ExcluirConta_Click" />

        <br />
        <asp:Label ID="Alerta" CssClass="profile-alert" Text="" runat="server" Visible="false"></asp:Label>
    </div>

</asp:Content>
