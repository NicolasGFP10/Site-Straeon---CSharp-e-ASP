<%@ page title="" language="C#" masterpagefile="~/Default.Master" autoeventwireup="true" codebehind="EditarLivro.aspx.cs" inherits="SiteStraeon.EditarLivro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="user-profile-header">
        <h1>Editar Livro</h1>
    </div>

    <br />

    <asp:HyperLink ID="Voltar" runat="server" NavigateUrl="~/LivrosCadastrados.aspx" Font-Names="arial" CssClass="buttonWhite margin-bottom-20"><i class="fa fa-arrow-left"></i> Voltar</asp:HyperLink>

    <br />

    <h3>Título</h3>
    <asp:TextBox ID="TextTitulo" runat="server" placeholder="Título do livro" CssClass="margin-bottom-20"></asp:TextBox>

    <h3>Sinopse</h3>
    <asp:TextBox ID="TextSinopse" runat="server" CssClass="margin-bottom-20" placeholder="Sinopse do livro"></asp:TextBox>

    <h3>Autor</h3>
    <asp:TextBox ID="TextAutor" runat="server" CssClass="margin-bottom-20" placeholder="Autor do livro"></asp:TextBox>

    <h3>Editora</h3>
    <asp:TextBox ID="TextEditora" runat="server" CssClass="margin-bottom-20" placeholder="Editora do livro"></asp:TextBox>

    <h3>Idioma</h3>
    <asp:TextBox ID="TextIdioma" runat="server" CssClass="margin-bottom-20" placeholder="Idioma que está o livro"></asp:TextBox>

    <h3>Ano do livro</h3>
    <asp:TextBox ID="TextAno" runat="server" CssClass="margin-bottom-20" placeholder="Ano de publicação" TextMode="Number"></asp:TextBox>

    <h3>Capa</h3>
    <asp:FileUpload ID="ImgCapa" runat="server" CssClass="margin-bottom-20" placeholder="Capa do livro" />

    <h3>Arquivo (Somente formato PDF)</h3>
    <asp:FileUpload ID="pdfLivro" runat="server" CssClass="margin-bottom-20" placeholder="PDF do livro" />

    <asp:Button ID="buttonAlterar" OnClick="buttonAlterar_Click" runat="server" Text="Alterar" />
    
    <asp:Label ID="Alerta" runat="server" Text="" ForeColor="Red"></asp:Label>

</asp:Content>
