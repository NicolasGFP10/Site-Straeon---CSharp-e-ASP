using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteStraeon
{
    public partial class Default : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["usuCodigo"] == null)
            {
                buttonEditarInformacoes.Visible = false;
                buttonLogar.Visible = true;
                buttonCadastrar.Visible = true;
                buttonDeslogar.Visible = false;
                buttonLivrosSalvos.Visible = false;
                buttonAtualizarConta.Visible = false;
                buttonPublicar.Visible = false;
                buttonLivrosCadastrados.Visible = false;

                // buttonHome.Visible = true;
                // buttonPesquisar.Visible = true; Esses dois sempre ficam ativos

            }
            else
            {
                usuNome.Text = Session["usuNome"].ToString();

                if (Session["usuImagem"] == null)
                {

                    imgUsuario.ImageUrl = ResolveUrl("~/img/straeonIcon.png");

                } else
                {

                    imgUsuario.ImageUrl = ResolveUrl("~/app_img_usuario/" + Session["usuImagem"].ToString());

                }
                    

                buttonEditarInformacoes.Visible = true;
                buttonLogar.Visible = false;
                buttonCadastrar.Visible = false;
                buttonDeslogar.Visible = true;
                buttonLivrosSalvos.Visible = true;

                // Apenas mostre o 
                if(Session["usuEscritor"].ToString() == "False")
                {
                    buttonAtualizarConta.Visible = true;
                }
                else
                {
                    buttonPublicar.Visible = true;
                    buttonLivrosCadastrados.Visible = true;
                }


            }

        }

        protected void buttonDeslogar_Click(object sender, EventArgs e)
        {

            Session.Clear();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");

        }
    }
}