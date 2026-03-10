using Datapost.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteStraeon
{
    public partial class Publicar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonCadastrarLivro_Click(object sender, EventArgs e)
        {
            string extensao = Path.GetExtension(pdfLivro.FileName).ToLower();

            if (
                string.IsNullOrEmpty(TextTitulo.Text.Trim()) ||
                string.IsNullOrEmpty(TextIdioma.Text.Trim()) ||
                string.IsNullOrEmpty(Path.GetFileName(pdfLivro.FileName)) ||
                extensao != ".pdf"
                )
            {

                Alerta.Text = "Preencha os campos obrigatórios corretamente!";

            }
            else 
            {

                string link = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + Server.MapPath("~/app_data/StraeonDB.accdb") + "; Persist Security Info = False";

                DAO dao = new DAO();

                dao.ConnectionString = link;
                dao.DataProviderName = DAO.ProviderName.OleDb;

                string imagem = "";

                if (!(string.IsNullOrEmpty(ImgCapa.FileName)))
                {
                    string caminho = Server.MapPath("~/app_img_livro/");
                    imagem = Path.GetFileName(ImgCapa.FileName);
                    ImgCapa.SaveAs(caminho + imagem);
                }

                string pdf = "";

                if (!string.IsNullOrEmpty(pdfLivro.FileName))
                {
                    string caminho = Server.MapPath("~/app_pdf_livro/");
                    pdf = Path.GetFileName(pdfLivro.FileName); 
                    pdfLivro.SaveAs(caminho + pdf);
                }

                string sql = "INSERT INTO Livro " +
                "(livTitulo, livSinopse, livAutor, livEditora, lividioma, livAnoPublicacao, livCapa, livArquivo, livDataCadastro, livAtivo, usuCodigo) " +
                "VALUES (" +
                $"'{TextTitulo.Text.Trim()}', " +
                $"'{TextSinopse.Text.Trim()}', " +
                $"'{TextAutor.Text.Trim()}', " +
                $"'{TextEditora.Text.Trim()}', " +
                $"'{TextIdioma.Text.Trim()}', " +
                $"'{TextAno.Text}', " +
                $"'{imagem}', " +
                $"'{pdf}', " +
                $"'{DateTime.Now:yyyy-MM-dd HH:mm:ss}', " +
                $"1, " +
                $"{Session["usuCodigo"]})"; 

                dao.Query(sql);

                ZerarTodosOsCampos();

                Alerta.Text = "Livro cadastrado com sucesso!";

                Response.Redirect("~/LivrosCadastrados.aspx");
            }
        }

        protected void ZerarTodosOsCampos()
        {
            TextTitulo.Text = "";
            TextSinopse.Text = "";
            TextAutor.Text = "";
            TextEditora.Text = "";
            TextIdioma.Text = "";
            TextAno.Text = "";
        }
    }
}