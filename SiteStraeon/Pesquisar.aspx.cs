using Datapost.DB;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace SiteStraeon
{
    public partial class Pesquisar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Pesquisa.Text = null;
                    Exibir();
                } else
                {
                    CancelarButton.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Alerta.Text = "Estamos com problemas técnicos, tente novamente outra hora!";
                Alerta.Text += "<br />Detalhes do erro: " + ex.Message;
            }
        }


        protected void Exibir(string pesquisar = null)
        {
            string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/app_data/StraeonDB.accdb") + ";Persist Security Info=False";

            string sql;

            if (pesquisar == null)
            {

                sql = $"SELECT l.livCodigo, l.livTitulo, l.livAutor, l.livAnoPublicacao, l.livCapa, l.livDataCadastro, u.usuAtivo " +
                                             "FROM Livro l INNER JOIN Usuario u ON u.usuCodigo = l.usuCodigo WHERE livAtivo = true AND u.usuAtivo = true ORDER BY livDataCadastro ASC;";
            }
            else
            {
                sql = $"SELECT livCodigo, livTitulo, livAutor, livAnoPublicacao, livCapa, livDataCadastro FROM Livro WHERE livAtivo = true AND livTitulo LIKE '%{ Pesquisa.Text.Trim() }%' ORDER BY livDataCadastro;";
            }

            Datapost.DB.DAO db = new Datapost.DB.DAO();
            db.ConnectionString = conexao;
            db.DataProviderName = Datapost.DB.DAO.ProviderName.OleDb;

            var dados = db.Query(sql);

            LivroRepeater.DataSource = dados;
            LivroRepeater.DataBind();
        }

        protected void PesquisarButton_Click(object sender, EventArgs e)
        {
            if (Pesquisa.Text.Trim() == null)
            {
                Exibir();

            } else {

                string pesquisa = Pesquisa.Text.Trim();

                Exibir(pesquisa);

            }
        }

        protected void CancelarButton_Click(object sender, EventArgs e)
        {
            Pesquisa.Text = null;
            CancelarButton.Visible = false;
            Exibir();
        }

        protected void LivroRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerLivro")
            {
                string codigo = e.CommandArgument.ToString();
                Response.Redirect("VerLivro.aspx?c=" + codigo);
            }
        }

    }
}
