using Datapost.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteStraeon
{
    public partial class ExcluirConta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuCodigo"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                TextboxSenhaAtual.Text = "";
                TextboxConfirmacao.Text = "";
            }
        }

        protected void ConfirmarDesativar_Click(object sender, EventArgs e)
        {
            if(TextboxSenhaAtual.Text.Trim() != Session["usuSenha"].ToString())
            {
                Alerta.Visible=true;
                Alerta.Text = "A senha digita precisa ser igual a senha atual.";
            }

            if (TextboxSenhaAtual.Text.ToLower() == "desativar conta")
            {
                Alerta.Visible = true;
                Alerta.Text = "Eh necessario digitar a confirmacao corretamente caso queira desativar a conta.";
            }

            string link = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + Server.MapPath("~/app_data/StraeonDB.accdb") + "; Persist Security Info = False";

            DAO dao = new DAO();
            dao.ConnectionString = link;
            dao.DataProviderName = DAO.ProviderName.OleDb;

            string sql = $"UPDATE Usuario SET usuAtivo = '{0}' WHERE usuCodigo = {Session["usuCodigo"].ToString()};";

            dao.Query(sql);

            Session.Clear();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }
    }
}