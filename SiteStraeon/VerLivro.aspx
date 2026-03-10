<%@ page title="" language="C#" masterpagefile="~/Default.Master" autoeventwireup="true" codebehind="VerLivro.aspx.cs" inherits="SiteStraeon.VerLivro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="user-profile-header">
        <h1>
            <asp:Label ID="TextTitulo" runat="server" Text=""></asp:Label>
        </h1>
    </div>

    <h3>
        <asp:Label ID="Alerta" runat="server" Text=""></asp:Label>
    </h3>

    <br />

    <asp:HyperLink ID="Voltar" runat="server" NavigateUrl="~/Pesquisar.aspx" Font-Names="arial" CssClass="buttonWhite margin-bottom-20"><i class="fa fa-arrow-left"></i>Voltar</asp:HyperLink>

    <br />
    <br />

    <table class="tableLivro">
        <tr>
            <td>
                <asp:Image ID="ImageCapa" CssClass="livro-capa livro-capa-visualizacao diplay-flex" runat="server" alt="Capa do Livro" />
            </td>
            <td class="top-align">
                <h3><strong>Sinopse: </strong></h3>
                <h3>
                    <asp:Label ID="TextSinopse" runat="server" Text=""></asp:Label>
                </h3>

                <h3><strong>Autor: </strong></h3>
                <h3>
                    <asp:Label ID="TextAutor" runat="server" Text=""></asp:Label>
                </h3>
            </td>
        </tr>
    </table>

    <h3>Editora: 
        <asp:Label ID="TextEditora" runat="server" Text=""></asp:Label>
    </h3>

    <h3><strong>Idioma: </strong>
        <asp:Label ID="TextIdioma" runat="server" Text=""></asp:Label>
    </h3>

    <h3><strong>Ano de Publicação do Livro: </strong>
        <asp:Label ID="TextAnoPublicacao" runat="server" Text=""></asp:Label>
    </h3>

    <h3><strong>Data de cadastro: </strong>
        <asp:Label ID="TextDataCadastro" runat="server" Text=""></asp:Label>
    </h3>

    <h3><strong>Publicado por: </strong>
        <asp:Label ID="TextUsuario" runat="server" Text=""></asp:Label>
    </h3>

    <br />

    <hr />

    <br />

    <asp:Button ID="buttonSalvarLivro" Visible="true" OnClick="buttonSalvarLivro_Click" CssClass="buttonPesquisar" runat="server" Text="Salvar livro" />

    <asp:Button ID="buttonRemoverSalvo" Visible="false" OnClick="buttonRemoverSalvo_Click" CssClass="buttonPesquisar" runat="server" Text="Remover livro salvo" />

    <br />
    <br />

    <iframe id="PDFViewer" runat="server" style="width: 100%; height: 900px"></iframe>

    <hr />

    <h2>Comentários</h2>

    <h3>
        <asp:Label ID="labelTexto" runat="server" Text="Comente o que você achou do livro" Visible="true"></asp:Label>
    </h3>

    <br />

    <asp:TextBox ID="TextBoxComentario" Visible="true" CssClass="form-input margin-bottom-20 texto-grande tamanho-fixo" runat="server" placeHolder="Seu comentário" TextMode="MultiLine"></asp:TextBox>

    <asp:Button ID="buttonComentario" Visible="true" OnClick="buttonComentario_Click" CssClass="buttonPesquisar margin-bottom-20" runat="server" Text="Comentar" />

    <asp:Repeater ID="ComentarioRepeater" runat="server">
        <headertemplate>
        </headertemplate>

        <itemtemplate>

            <div class="comentario">
                <div class="comentarioImagem">

                    <img src='<%# ResolveUrl("~/app_img_usuario/" + Eval("usuImagem")) %>' alt="imagem do usuario" class="foto-usuario" width="40px" height="40px" style="border-radius: 100px;" />

                </div>

                <div class="comentarioTitulo">
                    <h3>
                        <p><%# Eval("usuNome") %></p>
                    </h3>
                </div>
                <div class="comentarioConteudo">

                    <h4>
                        <p><%# Eval("comConteudo") %></p>
                    </h4>

                </div>
                <div class="comentarioBotao">

                    <asp:Button
                        ID="buttonExcluir"
                        CssClass="buttonPesquisar"
                        OnClick="buttonExcluir_Click"
                        Text="Excluir"
                        CommandArgument='<%# Eval("comCodigo") %>'
                        Visible='<%# Session["usuCodigo"] != null && Eval("usuCodigo").ToString() == Session["usuCodigo"].ToString() %>'
                        runat="server" />

                </div>
            </div>

        </itemtemplate>

        <footertemplate>
        </footertemplate>
    </asp:Repeater>

</asp:Content>
