<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Publicar.aspx.cs" Inherits="SiteStraeon.Publicar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="user-profile-header">
        <h1>Publicar um livro</h1>
    </div>
     <div style=" margin: auto; width: 400px; margin-top: 2em;">
         <h3>Título</h3>
         <asp:TextBox ID="TextTitulo" runat="server" placeholder="Título do livro" CssClass="margin-bottom-20 texto-normal tamanho-fixo form-input"></asp:TextBox>

         <h3>Sinopse</h3>
         <asp:TextBox ID="TextSinopse" runat="server" CssClass="form-input margin-bottom-20 texto-grande tamanho-fixo" placeholder="Sinopse do livro" TextMode="MultiLine"></asp:TextBox>

         <h3>Autor</h3>
         <asp:TextBox ID="TextAutor" runat="server" CssClass="form-input margin-bottom-20 texto-normal tamanho-fixo" placeholder="Autor do livro"></asp:TextBox>

         <h3>Editora</h3>
         <asp:TextBox ID="TextEditora" runat="server" CssClass="form-input margin-bottom-20 texto-normal tamanho-fixo" placeholder="Editora do livro"></asp:TextBox>

         <h3>Idioma</h3>
         <asp:TextBox ID="TextIdioma" runat="server" CssClass="form-input margin-bottom-20 texto-normal tamanho-fixo" placeholder="Idioma que está o livro"></asp:TextBox>

         <h3>Ano do livro</h3>
         <asp:TextBox ID="TextAno" runat="server" CssClass="form-input margin-bottom-20 texto-normal tamanho-fixo" placeholder="Ano de publicação" TextMode="Number"></asp:TextBox>

         <h3>Capa</h3>
         <asp:FileUpload ID="ImgCapa" runat="server" CssClass="form-input margin-bottom-20" placeholder="Capa do livro"/>

         <h3>Arquivo (Somente formato PDF)</h3>
         <asp:FileUpload ID="pdfLivro" runat="server" CssClass="form-input margin-bottom-20" placeholder="PDF do livro"/>

         <br />
         <asp:Button ID="ButtonCadastrarLivro" runat="server" Text="Cadastrar" CssClass="margin-bottom-20" OnClick="ButtonCadastrarLivro_Click"/>

         <br />
         <asp:Label ID="Alerta" runat="server" Text="" ForeColor="Red"></asp:Label>
     </div>
</asp:Content>
