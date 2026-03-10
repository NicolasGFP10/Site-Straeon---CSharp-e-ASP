using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteStraeon
{
    public partial class LivrosSalvos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["usuCodigo"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                if (!IsPostBack) // ISSO é obrigatório!
                {
                    Exibir();
                }
            }
            catch (Exception ex)
            {
                // Alerta.Text = "Estamos com problemas técnicos, tente novamente outra hora!";
                //Alerta.Text += "<br />Detalhes do erro: " + ex.Message;
            }
        }


        protected void Exibir(string pesquisar = null)
        {
            string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/app_data/StraeonDB.accdb") + ";Persist Security Info=False";

            string sql;

                sql = $"SELECT l.livCodigo, l.livTitulo, l.livAutor, l.livAnoPublicacao, l.livCapa, l.livDataCadastro FROM Livro l INNER JOIN LivroSalvo s ON l.livCodigo = s.livCodigo WHERE l.livAtivo = TRUE AND s.usuCodigo = {Session["usuCodigo"]} ORDER BY l.livDataCadastro ASC;";

            Datapost.DB.DAO db = new Datapost.DB.DAO();
            db.ConnectionString = conexao;
            db.DataProviderName = Datapost.DB.DAO.ProviderName.OleDb;

            var dados = db.Query(sql);

            LivroRepeater.DataSource = dados;
            LivroRepeater.DataBind();
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