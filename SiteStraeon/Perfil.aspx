<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="SiteStraeon.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="user-profile-header">
        <h1>Meu Perfil</h1>
    </div>

    <div class="user-profile-card">
        <asp:Image ID="imagemPerfilUsuario" runat="server" CssClass="user-profile-picture" />

        <div class="user-profile-info">
            <asp:Label ID="nomeDoUsuario" CssClass="user-profile-name" runat="server" Text=""></asp:Label>
            <asp:Label ID="dataCriacaoConta" CssClass="user-profile-date" runat="server" Text=""></asp:Label>
        </div>
    </div>

    <asp:Button ID="EditarInformacoes" CssClass="user-profile-button" runat="server"
        Text="Editar Informações" OnClick="EditarInformacoes_Click" />
    <hr />

    <div id="MeusLivros">
        <h1>Meus livros</h1>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

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
                        ID="buttonEditarLivro"
                        runat="server"
                        Text="Editar Livro"
                        CommandName="EditarLivro"
                        CommandArgument='<%# Eval("livCodigo") %>' />

                    <asp:Button
                        ID="buttonExcluirLivro"
                        runat="server"
                        Text="Excluir Livro"
                        CommandName="ExcluirLivro"
                        CommandArgument='<%# Eval("livCodigo") %>'
                        Visible="true" />

                    <asp:Button
                        ID="buttonCancelar"
                        runat="server"
                        Text="Cancelar"
                        Visible="false"
                        CommandName="CancelarExclusao"
                        CommandArgument='<%# Eval("livCodigo") %>' />

                    <asp:Button
                        ID="buttonConfirmar"
                        runat="server"
                        Text="Confirmar"
                        Visible="false"
                        CommandName="ConfirmarExclusao"
                        CommandArgument='<%# Eval("livCodigo") %>' />

                </div>
            </ItemTemplate>

            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>



</asp:Content>
