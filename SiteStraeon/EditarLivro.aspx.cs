using Datapost.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteStraeon
{
    public partial class EditarLivro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string codigo = Request.QueryString["c"];

                if (codigo == null || codigo == "")
                {
                    Response.Redirect("Default.aspx");
                }

                string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/app_data/StraeonDB.accdb") + ";Persist Security Info=False";

                string sql = $"SELECT livTitulo, livSinopse, livAutor, livEditora, livIdioma, livAnoPublicacao, livCapa, livArquivo FROM Livro WHERE livCodigo = {codigo} AND usuCodigo = {Session["usuCodigo"]};";

                DAO dao = new DAO();

                dao.ConnectionString = conexao;
                dao.DataProviderName = DAO.ProviderName.OleDb;

                DataTable dt = (DataTable)dao.Query(sql);

                if (dt.Rows.Count == 1)
                {

                    TextTitulo.Text = dt.Rows[0]["livTitulo"].ToString();
                    TextSinopse.Text = dt.Rows[0]["livSinopse"].ToString();
                    TextEditora.Text = dt.Rows[0]["livEditora"].ToString();
                    TextIdioma.Text = dt.Rows[0]["livIdioma"].ToString();
                    TextAno.Text = dt.Rows[0]["livAnoPublicacao"].ToString();
                    TextAutor.Text = dt.Rows[0]["livAutor"].ToString();

                }
                else
                {

                    Response.Redirect("Default.aspx");

                }

            }

        }

        protected void buttonAlterar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(TextTitulo.Text))
            {

                Alerta.Text = "Preencha o campo Título!";

            } 
            else
            {
                string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/app_data/StraeonDB.accdb") + ";Persist Security Info=False";

                string sql;

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

                if (pdf == "" && imagem == "")
                {

                    sql = $"UPDATE Livro SET " +
                             $"livTitulo = '{TextTitulo.Text.Trim()}', " +
                             $"livSinopse = '{TextSinopse.Text.Trim()}', " +
                             $"livAutor = '{TextAutor.Text.Trim()}', " +
                             $"livEditora = '{TextEditora.Text.Trim()}', " +
                             $"livIdioma = '{TextIdioma.Text.Trim()}', " +
                             $"livAnoPublicacao = '{TextAno.Text.Trim()}' " +
                             $"WHERE livCodigo = {Request.QueryString["c"]} AND usuCodigo = {Session["usuCodigo"]};";

                }
                else if (pdf != "" && imagem == "")
                {

                    sql = $"UPDATE Livro SET " +
                             $"livTitulo = '{TextTitulo.Text.Trim()}', " +
                             $"livSinopse = '{TextSinopse.Text.Trim()}', " +
                             $"livAutor = '{TextAutor.Text.Trim()}', " +
                             $"livEditora = '{TextEditora.Text.Trim()}', " +
                             $"livIdioma = '{TextIdioma.Text.Trim()}', " +
                             $"livAnoPublicacao = '{TextAno.Text.Trim()}', " +
                             $"livArquivo = '{pdf}' " +
                             $"WHERE livCodigo = {Request.QueryString["c"]} AND usuCodigo = {Session["usuCodigo"]};";

                }
                else if (pdf == "" && imagem != "")
                {

                    sql = $"UPDATE Livro SET " +
                             $"livTitulo = '{TextTitulo.Text.Trim()}', " +
                             $"livSinopse = '{TextSinopse.Text.Trim()}', " +
                             $"livAutor = '{TextAutor.Text.Trim()}', " +
                             $"livEditora = '{TextEditora.Text.Trim()}', " +
                             $"livIdioma = '{TextIdioma.Text.Trim()}', " +
                             $"livAnoPublicacao = '{TextAno.Text.Trim()}', " +
                             $"livCapa = '{imagem}' " + 
                             $"WHERE livCodigo = {Request.QueryString["c"]} AND usuCodigo = {Session["usuCodigo"]};";

                }
                else
                {

                    sql = $"UPDATE Livro SET " +
                             $"livTitulo = '{TextTitulo.Text.Trim()}', " +
                             $"livSinopse = '{TextSinopse.Text.Trim()}', " +
                             $"livAutor = '{TextAutor.Text.Trim()}', " +
                             $"livEditora = '{TextEditora.Text.Trim()}', " +
                             $"livIdioma = '{TextIdioma.Text.Trim()}', " +
                             $"livAnoPublicacao = '{TextAno.Text.Trim()}' " +
                             $"livCapa = '{imagem}', " +
                             $"livArquivo = '{pdf}' " +
                             $"WHERE livCodigo = {Request.QueryString["c"]} AND usuCodigo = {Session["usuCodigo"]};";

                }

                DAO dao = new DAO();

                dao.ConnectionString = conexao;
                dao.DataProviderName = DAO.ProviderName.OleDb;

                dao.Query(sql);

                Response.Redirect("LivrosCadastrados.aspx");
            }
        }
    }
}