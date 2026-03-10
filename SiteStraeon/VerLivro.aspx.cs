using Datapost.DB;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace SiteStraeon
{
    public partial class VerLivro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string codigo = Request.QueryString["c"];

                if (!string.IsNullOrEmpty(codigo))
                {
                    string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/app_data/StraeonDB.accdb") + ";Persist Security Info=False";

                    string sql = $"SELECT livArquivo FROM Livro WHERE livCodigo = {codigo};";

                    DAO dao = new DAO();
                    dao.ConnectionString = conexao;
                    dao.DataProviderName = DAO.ProviderName.OleDb;

                    DataTable dt = (DataTable)dao.Query(sql);

                    if (dt.Rows.Count == 1)
                    {
                        string livArquivo = dt.Rows[0]["livArquivo"].ToString();
                        PDFViewer.Attributes["src"] = ResolveUrl("~/app_pdf_livro/" + livArquivo);
                    }
                    else
                    {
                        Alerta.Text = "Livro não encontrado.";
                        Response.Redirect("Pesquisar.aspx");
                    }
                }

            }

            ExibirBotaoSalvar();

            ExibirLivro();

            ExibirComentario();
            
        }

        public void ExibirBotaoSalvar()
        {

            if (Session["usuCodigo"] == null)
            {
                buttonRemoverSalvo.Visible = false;
                buttonSalvarLivro.Visible = false;
            }
            else
            {

                string codigo = Request.QueryString["c"];

                string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/app_data/StraeonDB.accdb") + ";Persist Security Info=False";

                string sql = $"SELECT livCodigo FROM LivroSalvo WHERE usuCodigo = {Session["usuCodigo"]} AND livCodigo = {codigo};";

                DAO dao = new DAO();
                dao.ConnectionString = conexao;
                dao.DataProviderName = DAO.ProviderName.OleDb;

                DataTable dt = (DataTable)dao.Query(sql);

                if (dt.Rows.Count == 1)
                {
                    buttonRemoverSalvo.Visible = true;
                    buttonSalvarLivro.Visible = false;
                }
                else
                {
                    buttonRemoverSalvo.Visible = false;
                    buttonSalvarLivro.Visible = true;
                }
            }
        }

        protected void buttonSalvarLivro_Click(object sender, EventArgs e)
        {

            string codigo = Request.QueryString["c"];

            string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/app_data/StraeonDB.accdb") + ";Persist Security Info=False";

            string sql = $"INSERT INTO LivroSalvo (livCodigo, usuCodigo) VALUES ({codigo}, {Session["usuCodigo"]});";

            DAO dao = new DAO();
            dao.ConnectionString = conexao;
            dao.DataProviderName = DAO.ProviderName.OleDb;

            dao.Query(sql);

            Response.Redirect("VerLivro.aspx?c=" + codigo);
        }

        protected void buttonRemoverSalvo_Click(object sender, EventArgs e)
        {
            string codigo = Request.QueryString["c"];

            string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/app_data/StraeonDB.accdb") + ";Persist Security Info=False";

            string sql = $"DELETE FROM LivroSalvo WHERE livCodigo = {codigo} AND usuCodigo = {Session["usuCodigo"]};";

            DAO dao = new DAO();
            dao.ConnectionString = conexao;
            dao.DataProviderName = DAO.ProviderName.OleDb;

            dao.Query(sql);

            Response.Redirect("VerLivro.aspx?c=" + codigo);
        }

        protected void ExibirLivro()
        {
            if (!IsPostBack)
            {
                try
                {
                    string codigo = Request.QueryString["c"];

                    if (codigo == null || codigo == "")
                    {
                        Response.Redirect("Pesquisar.aspx");
                    }

                    string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/app_data/StraeonDB.accdb") + ";Persist Security Info=False";

                    string sql = $"SELECT livTitulo, livSinopse, livAutor, livEditora, livIdioma, livAnoPublicacao, livCapa, livArquivo, livDataCadastro, livAtivo, usuCodigo FROM Livro WHERE livCodigo = {codigo};";

                    DAO db = new DAO();
                    db.DataProviderName = DAO.ProviderName.OleDb;
                    db.ConnectionString = conexao;

                    DataTable DT = new DataTable();
                    DT = (DataTable)db.Query(sql);

                    var row = DT.Rows[0];

                    bool livAtivo = Convert.ToBoolean(row["livAtivo"]);

                    if (livAtivo == false)
                    {
                        Response.Redirect("Pesquisar.aspx");
                    }

                    TextTitulo.Text = row["livTitulo"].ToString();

                    TextSinopse.Text = row["livSinopse"] == DBNull.Value ? "Não informado" : row["livSinopse"].ToString();
                    TextAutor.Text = row["livAutor"] == DBNull.Value ? "Não informado" : row["livAutor"].ToString();
                    TextEditora.Text = row["livEditora"] == DBNull.Value ? "Não informado" : row["livEditora"].ToString();
                    TextIdioma.Text = row["livIdioma"] == DBNull.Value ? "Não informado" : row["livIdioma"].ToString();

                    TextAnoPublicacao.Text = row["livAnoPublicacao"] == DBNull.Value ?
                                             "Não informado" :
                                             row["livAnoPublicacao"].ToString();

                    TextDataCadastro.Text = row["livDataCadastro"] == DBNull.Value ?
                                            "Não informado" :
                                            row["livDataCadastro"].ToString();

                    ImageCapa.ImageUrl = ResolveUrl("~/app_img_livro/" + row["livCapa"].ToString());

                    ExibirAutor(row["usuCodigo"].ToString());

                }
                catch (Exception ex)
                {

                    Alerta.Text = "Houve um pequeno problema por aqui, tente novamente mais tarde!";

                }
            }
        }

        protected void ExibirAutor(string codigo)
        {
            string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/app_data/StraeonDB.accdb") + ";Persist Security Info=False";

            string sql = $"SELECT usuNome FROM Usuario WHERE usuCodigo = {codigo}";

            DAO db = new DAO();
            db.DataProviderName = DAO.ProviderName.OleDb; // DataBase Acess
            db.ConnectionString = conexao;

            DataTable DT = new DataTable();
            DT = (DataTable)db.Query(sql);

            TextUsuario.Text = DT.Rows[0]["usuNome"].ToString();
        }

        protected void ExibirComentario()
        {
            if (!IsPostBack)
            {
                if (Session["usuCodigo"] == null)
                {
                    labelTexto.Visible = false;
                    TextBoxComentario.Visible = false;
                    buttonComentario.Visible = false;
                }

                string codigo = Request.QueryString["c"];

                string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/app_data/StraeonDB.accdb") + ";Persist Security Info=False";

                string sql =
                    "SELECT c.comConteudo, c.comCodigo, c.usuCodigo, u.usuNome, u.usuImagem " +
                    "FROM Comentario c " +
                    "INNER JOIN Usuario u ON c.usuCodigo = u.usuCodigo " +
                    "WHERE c.livCodigo = " + codigo + " AND u.usuAtivo = true;";

                DAO db = new DAO();
                db.ConnectionString = conexao;
                db.DataProviderName = DAO.ProviderName.OleDb;

                var dados = db.Query(sql);

                ComentarioRepeater.DataSource = dados;
                ComentarioRepeater.DataBind();
            }
        }

        protected void buttonExcluir_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string comCodigo = btn.CommandArgument;

            string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                             Server.MapPath("~/app_data/StraeonDB.accdb") + ";";

            string sql = $"DELETE FROM Comentario WHERE comCodigo = {comCodigo} AND usuCodigo = {Session["usuCodigo"]};";

            DAO db = new DAO();
            db.ConnectionString = conexao;
            db.DataProviderName = DAO.ProviderName.OleDb;
            db.Query(sql);

            Response.Redirect(Request.RawUrl);
        }

        protected void buttonComentario_Click(object sender, EventArgs e)
        {
            if (TextBoxComentario.Text.Trim() == "")
            {

                Alerta.Text = "Preencha todos os campos antes de enviar seu comentario!";

            }
            else
            {

                string codigo = Request.QueryString["c"];

                string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/app_data/StraeonDB.accdb") + ";Persist Security Info=False";

                string sql;

                sql = $"INSERT INTO Comentario (comConteudo, usuCodigo, livCodigo) VALUES ('{TextBoxComentario.Text.Trim()}', {Session["usuCodigo"]}, {codigo});";

                Datapost.DB.DAO db = new Datapost.DB.DAO();
                db.ConnectionString = conexao;
                db.DataProviderName = Datapost.DB.DAO.ProviderName.OleDb;

                db.Query(sql);

                TextBoxComentario.Text = null;

                Response.Redirect("VerLivro.aspx?c=" + codigo);

            }
        }
    }
}