<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Pesquisar.aspx.cs" Inherits="SiteStraeon.Pesquisar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="user-profile-header">
        <h1>Pesquisar o livro</h1>
    </div>

    <div>
        <div class="pesquisaDiv">
            <i class="fa fa-search"></i>
            <asp:TextBox CssClass="pesquisaTextbox" width="400px" ID="Pesquisa" placeholder="Pesquisar" runat="server" TextMode="Search"></asp:TextBox>
        </div>

        <asp:Button ID="PesquisarButton" runat="server" CssClass="buttonPesquisar" OnClick="PesquisarButton_Click" Text="Pesquisar"/>

        <asp:Button ID="CancelarButton" runat="server" CssClass="buttonPesquisar" OnClick="CancelarButton_Click" Text="Limpar" Visible="false"/>
    </div>

    <br />
    <asp:Label ID="Alerta" runat="server" Text="" CssClass="margin-bottom-20"></asp:Label>

    <div class="ajustar">
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
    </div>
</asp:Content>
