<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="LivrosSalvos.aspx.cs" Inherits="SiteStraeon.LivrosSalvos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="user-profile-header">
        <h1>Livros Salvos</h1>
    </div>

    <br />

    <asp:Repeater ID="LivroRepeater" runat="server" OnItemCommand="LivroRepeater_ItemCommand">
    <HeaderTemplate>
        <div class="livros-container">
    </HeaderTemplate>

    <ItemTemplate>
        <div class="livro-item">
            <img src='<%# ResolveUrl("~/app_img_livro/" + Eval("livCapa")) %>' alt="Capa do livro" class="livro-capa" />
            <h3><%# Eval("livTitulo") %></h3>
            <p><strong>Autor:</strong> <%# Eval("livAutor") %></p>
            <p><strong>Ano:</strong> <%# Eval("livAnoPublicacao") %></p>
            <p><strong>Data de cadastro:</strong> <%# Eval("livDataCadastro", "{0:dd/MM/yyyy}") %></p>
            <asp:Button
                CssClass="buttonPesquisar"
                ID="AcessarLivro"
                runat="server"
                Text="Ver Livro"
                CommandName="VerLivro"
                CommandArgument='<%# Eval("livCodigo") %>' />
        </div>
    </ItemTemplate>

    <FooterTemplate>
        </div>
    </FooterTemplate>
</asp:Repeater>
</asp:Content>
