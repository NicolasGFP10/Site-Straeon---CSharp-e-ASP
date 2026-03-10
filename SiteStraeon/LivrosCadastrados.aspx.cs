using System;
using System.Web.UI.WebControls;

namespace SiteStraeon
{
    public partial class LivrosCadastrados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usuCodigo"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                if (!IsPostBack) // ISSO é obrigatório!
                {
                    Exibir();
                }
            }
            catch (Exception ex)
            {
                Alerta.Text = "Estamos com problemas técnicos, tente novamente outra hora!";
                //Alerta.Text += "<br />Detalhes do erro: " + ex.Message;
            }
        }

        protected void Exibir(string pesquisar = null)
        {
            string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/app_data/StraeonDB.accdb") + ";Persist Security Info=False";

            string sql;

            if (pesquisar != null)
            {

                sql = $"SELECT livCodigo, livTitulo, livAutor, livAnoPublicacao, livCapa, livDataCadastro FROM Livro WHERE livAtivo = true AND usuCodigo = {Session["usuCodigo"]} ORDER BY livDataCadastro ASC;";

            }
            else
            {

                sql = $"SELECT livCodigo, livTitulo, livAutor, livAnoPublicacao, livCapa, livDataCadastro FROM Livro WHERE livAtivo = true AND usuCodigo = {Session["usuCodigo"]} ORDER BY livDataCadastro ASC;";

            }

            Datapost.DB.DAO db = new Datapost.DB.DAO();
            db.ConnectionString = conexao;
            db.DataProviderName = Datapost.DB.DAO.ProviderName.OleDb;

            var dados = db.Query(sql);

            LivroRepeater.DataSource = dados;
            LivroRepeater.DataBind();

        }

        protected void LivroRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EditarLivro")
            {
                string codigo = e.CommandArgument.ToString();
                Response.Redirect("EditarLivro.aspx?c=" + codigo);
            }
            else if (e.CommandName == "ExcluirLivro")
            {
                // Localiza os botões dentro do Repeater
                Button btnEditar = (Button)e.Item.FindControl("buttonEditarLivro");
                Button btnExcluir = (Button)e.Item.FindControl("buttonExcluirLivro");
                Button btnConfirmar = (Button)e.Item.FindControl("buttonConfirmar");
                Button btnCancelar = (Button)e.Item.FindControl("buttonCancelar");

                // Ajusta a visibilidade
                btnEditar.Visible = false;
                btnExcluir.Visible = false;
                btnConfirmar.Visible = true;
                btnCancelar.Visible = true;

            }
            else if (e.CommandName == "ConfirmarExclusao")
            {
                string codigo = e.CommandArgument.ToString();

                string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/app_data/StraeonDB.accdb") + ";Persist Security Info=False";

                string sql = $"UPDATE Livro SET livAtivo = 0 WHERE livCodigo = {codigo};";

                Datapost.DB.DAO db = new Datapost.DB.DAO();
                db.ConnectionString = conexao;
                db.DataProviderName = Datapost.DB.DAO.ProviderName.OleDb;

                db.Query(sql);

                Exibir();
            }
            else if (e.CommandName == "CancelarExclusao")
            {
                Exibir();
            }
        }
    }
}