using Datapost.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteStraeon
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            // Se precisar ver o arquivo da DB, use esse site
            // https://mdb-viewer.vercel.app/

            // Emite um alerta se algum dos campos estiverem vazios
            if (string.IsNullOrEmpty(TextboxEmail.Text.Trim()) || string.IsNullOrEmpty(TextboxSenha.Text.Trim()))
            {
                Alerta.Text = "Preencha todos os campos corretamente";
            } 
            else
            {

                string link = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + Server.MapPath("~/app_data/StraeonDB.accdb") + "; Persist Security Info = False";

                DAO dao = new DAO();

                dao.ConnectionString = link;
                dao.DataProviderName = DAO.ProviderName.OleDb;

                // Verificar se o email ja esta cadastrado
                string sql = $"SELECT usuCodigo, usuEmail, usuSenha, usuNome, usuImagem, usuEscritor, usuDataCadastro, usuAtivo FROM usuario WHERE usuEmail = '{TextboxEmail.Text.Trim()}' AND usuSenha = '{TextboxSenha.Text.Trim()}';";

                DataTable dt = (DataTable)dao.Query(sql);

                if (dt.Rows.Count == 1)
                {

                    string usuCodigo = dt.Rows[0]["usuCodigo"].ToString();
                    string usuNome = dt.Rows[0]["usuNome"].ToString();
                    string usuEmail = dt.Rows[0]["usuEmail"].ToString();
                    string usuSenha = dt.Rows[0]["usuSenha"].ToString();
                    string usuImagem = dt.Rows[0]["usuImagem"].ToString();
                    string usuEscritor = dt.Rows[0]["usuEscritor"].ToString();
                    string usuDataCadastro = dt.Rows[0]["usuDataCadastro"].ToString();
                    string usuAtivo = dt.Rows[0]["usuAtivo"].ToString();

                    Session["usuCodigo"] = usuCodigo;
                    Session["usuNome"] = usuNome;
                    Session["usuEmail"] = usuEmail;
                    Session["usuSenha"] = usuSenha;
                    Session["usuEscritor"] = usuEscritor;
                    Session["usuDataCadastro"] = usuDataCadastro;

                    if(usuAtivo == "False")
                    {
                        sql = $"UPDATE Usuario SET usuAtivo = '{1}' WHERE usuCodigo = {Session["usuCodigo"].ToString()};";
                        dao.Query(sql);
                    }

                    if (usuImagem == null)
                    {

                        Session["usuImagem"] = null;

                    } else
                    {

                        Session["usuImagem"] = usuImagem;
                    
                    }
                        
                    Session["usuCPF"] = null;

                    System.Web.Security.FormsAuthentication.Initialize();
                    
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, Session["usuCodigo"].ToString(), DateTime.Now, DateTime.Now.AddMinutes(20), false, FormsAuthentication.FormsCookiePath);
                    
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));

                    Response.Redirect("Default.aspx");

                } 
                else
                {

                    Alerta.Text = "E-mail ou senha não encontrados";

                }
            }
        }
    }
}