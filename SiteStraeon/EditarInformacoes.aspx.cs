using Datapost.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteStraeon
{
    public partial class EditarInformacoes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuCodigo"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            // Se o usuario nao tiver nenhuma foto, entao nem mostra a opcao de remover foto
            if (Session["usuImagem"].ToString() == "default.jpg")
            {
                RemoverFotoPerfil.Visible = false;
            }
            else
            {
                RemoverFotoPerfil.Visible = true;
            }

            if (!IsPostBack)
            {
                TextboxNome.Text = Session["usuNome"].ToString();
            }

            ImagemPerfilUsuario.ImageUrl = ResolveUrl("~/app_img_usuario/" + Session["usuImagem"].ToString());
        }

        protected void TrocarSenha_Click(object sender, EventArgs e)
        {
            Alerta.Text = "";
            Alerta.Visible = false;

            if (string.IsNullOrEmpty(Session["usuSenha"].ToString()) || string.IsNullOrEmpty(TextboxSenhaNova.Text))
            {
                Alerta.Text = "Preencha todos os campos corretamente.";
                Alerta.Visible = true;
                return;
            }

            if (Session["usuSenha"].ToString() != TextboxSenhaAtual.Text.Trim())
            {
                Alerta.Text = "A senha digitada NÃO corresponde a senha atual.";
                Alerta.Visible = true;
                return;
            }

            string link = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + Server.MapPath("~/app_data/StraeonDB.accdb") + "; Persist Security Info = False";

            DAO dao = new DAO();
            dao.ConnectionString = link;
            dao.DataProviderName = DAO.ProviderName.OleDb;

            string sql = $"UPDATE Usuario SET usuSenha = '{ TextboxSenhaNova.Text.Trim() }' WHERE usuCodigo = { Session["usuCodigo"] };";

            dao.Query(sql);

            Session["usuSenha"] = TextboxSenhaNova.Text.Trim();

            TextboxSenhaNova.Text = "";
            TextboxSenhaAtual.Text = "";
        }

        protected void RemoverFotoPerfil_Click(object sender, EventArgs e)
        {
            Alerta.Text = "";
            Alerta.Visible = false;

            string link = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + Server.MapPath("~/app_data/StraeonDB.accdb") + "; Persist Security Info = False";

            DAO dao = new DAO();
            dao.ConnectionString = link;
            dao.DataProviderName = DAO.ProviderName.OleDb;

            //string img = Server.MapPath("~/app_img_usuario/" + "default.jpg");
            //string img = Server.MapPath("~/app_img_usuario/" + "default.jpg");
            string img = "default.jpg";

            string sql = $"UPDATE Usuario SET usuImagem = '{img}' WHERE usuCodigo = {Session["usuCodigo"]};";

            dao.Query(sql);

            Session["usuImagem"] = img;
            ImagemPerfilUsuario.ImageUrl = ResolveUrl("~/app_img_usuario/" + Session["usuImagem"].ToString());
        }

        protected void TrocarFotoPerfil_Click(object sender, EventArgs e)
        {
            Alerta.Text = "";
            Alerta.Visible = false;

            if (!FileUploadImagem.HasFile || !(FileUploadImagem.PostedFile.ContentLength > 0))
            {
                Alerta.Text = "Insira uma imagem antes de enviar.";
                Alerta.Visible = true;
                return;
            }

            // Tamanho maximo 5mb
            if (FileUploadImagem.PostedFile.ContentLength > 5 * 1024 * 1024)
            {
                Alerta.Text = "Imagem muito grande. Tamanho máximo: 5MB.";
                Alerta.Visible = true;
                return;
            }

            string caminho = Server.MapPath("~/app_img_usuario/");
            string img = Session["usuCodigo"].ToString() + ".jpg";

            try
            {
                // Ler a imagem em um array de bytes primeiro
                byte[] imageData = new byte[FileUploadImagem.PostedFile.ContentLength];
                FileUploadImagem.PostedFile.InputStream.Read(imageData, 0, FileUploadImagem.PostedFile.ContentLength);

                // Converter de bytes para Image
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    using (System.Drawing.Image imagemOriginal = System.Drawing.Image.FromStream(ms))
                    {
                        // Criar um novo bitmap
                        using (Bitmap bitmap = new Bitmap(imagemOriginal.Width, imagemOriginal.Height))
                        {
                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                g.DrawImage(imagemOriginal, 0, 0, imagemOriginal.Width, imagemOriginal.Height);
                            }

                            // Salvar como JPG
                            bitmap.Save(caminho+img, ImageFormat.Jpeg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Alerta.Text = $"Vish nao deu pra usar essa imagem nao.\nEx: {ex}";
                Alerta.Visible = true;
                return;
            }

            string link = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + Server.MapPath("~/app_data/StraeonDB.accdb") + "; Persist Security Info = False";

            DAO dao = new DAO();
            dao.ConnectionString = link;
            dao.DataProviderName = DAO.ProviderName.OleDb;

            string sql = $"UPDATE Usuario SET usuImagem = '{img}' WHERE usuCodigo = {Session["usuCodigo"]};";

            dao.Query(sql);

            Session["usuImagem"] = img;
            ImagemPerfilUsuario.ImageUrl = ResolveUrl("~/app_img_usuario/" + Session["usuImagem"].ToString());
            
        }

        protected void ExcluirConta_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ExcluirConta.aspx");
        }

        protected void EditarNome_Click(object sender, EventArgs e)
        {
            Alerta.Text = "";
            Alerta.Visible = false;

            if (string.IsNullOrEmpty(Session["usuNome"].ToString()))
            {
                Alerta.Visible = true;
                Alerta.Text = "O novo nome nao pode ser vazio.";
                return;
            }


            if (string.IsNullOrEmpty(TextboxNome.Text.Trim()))
            {
                Alerta.Visible = true;
                Alerta.Text = "O nome não pode ser vazio.";
                return;
            }

            if(TextboxNome.Text.Trim() == Session["usuNome"].ToString())
            {
                Alerta.Visible = true;
                Alerta.Text = "O novo nome precisa ser diferente do atual.";
                return;
            }

            string link = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + Server.MapPath("~/app_data/StraeonDB.accdb") + "; Persist Security Info = False";

            DAO dao = new DAO();
            dao.ConnectionString = link;
            dao.DataProviderName = DAO.ProviderName.OleDb;

            string sql = $"UPDATE Usuario SET usuNome = '{ TextboxNome.Text.Trim() }' WHERE usuCodigo = { Session["usuCodigo"].ToString() };";

            dao.Query(sql);

            Session["usuNome"] = TextboxNome.Text.Trim();

        }
    }
}