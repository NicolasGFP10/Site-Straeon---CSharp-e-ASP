using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteStraeon
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Se o usuario nao tiver logado, manda ele pra pagina de login
            if (Session["usuCodigo"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (Session["usuEscritor"].ToString() == "False")
            {
                Label1.Visible = true;
                Label1.Text = "Voce nao possui livros.";
            }

            if (!IsPostBack) // ISSO é obrigatório!
            {
                Exibir();
            }

            imagemPerfilUsuario.ImageUrl = ResolveUrl("~/app_img_usuario/" + Session["usuImagem"].ToString());

            nomeDoUsuario.Text = Session["usuNome"].ToString();
            dataCriacaoConta.Text = "Conta criada em:" + Session["usuDataCadastro"].ToString();
        }

        protected void EditarInformacoes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EditarInformacoes.aspx");
        }

        protected void Exibir()
        {
            string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/app_data/StraeonDB.accdb") + ";Persist Security Info=False";

            string sql;

           
            sql = $"SELECT livCodigo, livTitulo, livAutor, livAnoPublicacao, livCapa, livDataCadastro FROM Livro WHERE livAtivo = true AND usuCodigo = { Session["usuCodigo"].ToString() } ORDER BY livDataCadastro;";

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