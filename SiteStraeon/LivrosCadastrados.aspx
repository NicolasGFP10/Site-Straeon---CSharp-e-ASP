<%@ page title="" language="C#" masterpagefile="~/Default.Master" autoeventwireup="true" codebehind="LivrosCadastrados.aspx.cs" inherits="SiteStraeon.LivrosCadastrados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="user-profile-header">
        <h1>Pesquisar o livro</h1>
    </div>

    <asp:Label ID="Alerta" runat="server" Text=""></asp:Label>

    <asp:Repeater ID="LivroRepeater" runat="server" OnItemCommand="LivroRepeater_ItemCommand">
        <headertemplate>
            <div class="livros-container">
        </headertemplate>

        <itemtemplate>
            <div class="livro-item">
                <img src='<%# ResolveUrl("~/app_img_livro/" + Eval("livCapa")) %>' alt="Capa do livro" class="livro-capa" />
                <h3><%# Eval("livTitulo") %></h3>
                <p><strong>Autor:</strong> <%# Eval("livAutor") %></p>
                <p><strong>Ano:</strong> <%# Eval("livAnoPublicacao") %></p>
                <p><strong>Data de cadastro:</strong> <%# Eval("livDataCadastro", "{0:dd/MM/yyyy}") %></p>
                <asp:Button
                    CssClass="buttonPesquisar margin-bottom-20"
                    ID="buttonEditarLivro"
                    runat="server"
                    Text="Editar Livro"
                    CommandName="EditarLivro"
                    CommandArgument='<%# Eval("livCodigo") %>' />

                <asp:Button
                    CssClass="buttonPesquisar margin-bottom-20"
                    ID="buttonExcluirLivro"
                    runat="server"
                    Text="Excluir Livro"
                    CommandName="ExcluirLivro"
                    CommandArgument='<%# Eval("livCodigo") %>'
                    Visible="true" />

                <asp:Button
                    CssClass="buttonPesquisar margin-bottom-20"
                    ID="buttonCancelar"
                    runat="server"
                    Text="Cancelar"
                    Visible="false"
                    CommandName="CancelarExclusao"
                    CommandArgument='<%# Eval("livCodigo") %>' />

                <asp:Button
                    CssClass="buttonPesquisar margin-bottom-20"
                    ID="buttonConfirmar"
                    runat="server"
                    Text="Confirmar"
                    Visible="false"
                    CommandName="ConfirmarExclusao"
                    CommandArgument='<%# Eval("livCodigo") %>' />

            </div>
        </itemtemplate>

        <footertemplate>
            </div>
        </footertemplate>
    </asp:Repeater>
</asp:Content>
