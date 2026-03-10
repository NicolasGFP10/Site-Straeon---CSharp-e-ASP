<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="AtualizarConta.aspx.cs" Inherits="SiteStraeon.AtualizarConta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="color2" style="border: solid; margin: auto; height: 500px; width: 400px; margin-top: 2em;">
        <div style="margin-left: 10px; margin-right: 10px; color: white; font-family: Arial">
            <h1>Ative sua conta para poder cadastrar livros no nosso site!</h1>
            <h3>É necessario ter uma conta ativada como escritor
                 para poder subir livros para o nosso site</h3>

            <h1>CPF:</h1>
            <div class="textboxDivRoundWhite">
                <asp:TextBox CssClass="pesquisaTextbox cpf" ID="CPF" runat="server" placeholder="Digite aqui seu CPF." MaxLength="14"></asp:TextBox>
            </div>
            <br />
            <br />
            <asp:Button CssClass="buttonWhite" ID="RegistrarCpfButton" OnClick="RegistrarCpfButton_Click" runat="server" Text="Ativar conta" />
            <br />
            <br />
            <asp:Label ID="Alerta" runat="server" Text="" BackColor="Red"></asp:Label>
        </div>
    </div>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const cpfInput = document.querySelector(".cpf");

            cpfInput.addEventListener("input", function () {
                let v = cpfInput.value.replace(/\D/g, ""); // só números

                if (v.length > 3) v = v.replace(/^(\d{3})(\d)/, "$1.$2");
                if (v.length > 6) v = v.replace(/^(\d{3})\.(\d{3})(\d)/, "$1.$2.$3");
                if (v.length > 9) v = v.replace(/^(\d{3})\.(\d{3})\.(\d{3})(\d)/, "$1.$2.$3-$4");

                cpfInput.value = v;
            });
        });
    </script>

</asp:Content>