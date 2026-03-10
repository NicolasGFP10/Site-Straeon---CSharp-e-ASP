using Datapost.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteStraeon
{
    public partial class Cadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CadastroButton_Click(object sender, EventArgs e)
        {
            // Se precisar ver o arquivo da DB, use esse site
            // https://mdb-viewer.vercel.app/

            // Zerar alerta pra erros anteriores nao permancerem.
            Alerta.Text = "";

            // Emite um alerta se algum dos campos estiverem vazios
            if (string.IsNullOrEmpty(TextboxNome.Text.Trim()) ||
                string.IsNullOrEmpty(TextboxEmail.Text.Trim()) ||
                string.IsNullOrEmpty(TextboxSenha.Text.Trim()) ||
                string.IsNullOrEmpty(TextboxConfirmarSenha.Text.Trim()))
            {

                Alerta.Text = "Preencha todos os campos corretamente.";
                return;
            }

            // Confirma se as dus senhas sao iguais.
            // Sem o trim mesmo pra evitar que o uuario meta um:
            // " senha " e "senha"
            if (TextboxSenha.Text != TextboxConfirmarSenha.Text)
            {
                Alerta.Text = "As duas senhas precisam ser iguais.";
                return;
            }

            string link = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + Server.MapPath("~/app_data/StraeonDB.accdb") + "; Persist Security Info = False";

            DAO dao = new DAO();
            dao.ConnectionString = link;
            dao.DataProviderName = DAO.ProviderName.OleDb;

            // Verificar se o email ja esta cadastrado
            string sql = "SELECT usuEmail FROM usuario WHERE usuEmail = '" + TextboxEmail.Text + "';";

            DataTable dt = (DataTable)dao.Query(sql);

            if (dt.Rows.Count >= 1)
            { 
                Alerta.Text = "Este email já está em uso!";
                ZerarTodosOsCampos();
                return;
            }

            string imagem = Server.MapPath("~/app_img_usuario/") + "default.jpg";
            string proximoID = PegarIdDoUltimoUsuario().ToString();


            // Tenho q testar mais isso ai, caso der MUITO pau, ai eh melhor so tirar
            if (FileUpload.HasFile && FileUpload.PostedFile.ContentLength > 0 && proximoID != "-1")
            {
                // Pega o ID do ultimo usuario, adiciona +1 e salva isso numa variavel
                string usuarioID = (PegarIdDoUltimoUsuario() + 1).ToString();
                string novaImagemNome = usuarioID + ".jpg";
                string caminhoNovaImagem = Server.MapPath("~/app_img_usuario/") + novaImagemNome;

                // Verificar tamanho máximo (5MB)
                if (FileUpload.PostedFile.ContentLength > 5 * 1024 * 1024)
                {
                    Alerta.Text = "Imagem muito grande. Tamanho máximo: 5MB.";
                    return;
                }

                try
                {
                    // Ler a imagem em um array de bytes primeiro
                    byte[] imageData = new byte[FileUpload.PostedFile.ContentLength];
                    FileUpload.PostedFile.InputStream.Read(imageData, 0, FileUpload.PostedFile.ContentLength);

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
                                bitmap.Save(caminhoNovaImagem, ImageFormat.Jpeg);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Alerta.Text = "Vish nao deu pra usar essa imagem nao.";
                    return;
                }
            }



            // ------------ Removi isso aqui pq eu botei o nome da imagem de foto pra ser o ID.jpg
            // ------------ pq ia dar pau nas imagem se varios usuario enviassem a mesma imagem com o mesmo nome
            // ------------ vou deixar aqui caso seja necessario retomar isto.

            //if (!(string.IsNullOrEmpty(FileUpload.FileName)))
            //{
            //    string caminho = Server.MapPath("~/app_img_usuario/");
            //    imagem = Path.GetFileName(FileUpload.FileName);
            //    FileUpload.SaveAs(caminho + imagem);
            //}

            // Vou colocar o CPF sempre como zero, pro usuario verificar dentro do site dps
            // Adicionei a coluna usuEscritor, eh so um boolean se o usuario for escritor
            // pra facilitar mais tarde
            sql = $"INSERT INTO Usuario(usuNome, usuEmail, usuSenha, usuDenunciado, usuDataCadastro, usuAtivo, usuImagem, usuCPF, usuEscritor) VALUES('{TextboxNome.Text.Trim()}', '{TextboxEmail.Text.Trim()}', '{TextboxSenha.Text.Trim()}', '{0}', '{DateTime.Now.ToString()}', '{1}', '{imagem}', '0', '{0}');";

            dao.Query(sql);

            ZerarTodosOsCampos();

            Response.Redirect("~/Default.aspx");
        }

        protected void ZerarTodosOsCampos()
        {
            TextboxNome.Text = "";
            TextboxEmail.Text = "";
            TextboxSenha.Text = "";
            TextboxConfirmarSenha.Text = "";
            
        }

        // Deixar essa parte em uma função separada
        // pra diminuir o tamanho do codigo na função do botão.
        protected int PegarIdDoUltimoUsuario()
        {
            string link = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + Server.MapPath("~/app_data/StraeonDB.accdb") + "; Persist Security Info = False";

            DAO dao = new DAO();

            dao.ConnectionString = link;
            dao.DataProviderName = DAO.ProviderName.OleDb;

            // Obter o ID do usuário recém-criado
            string sql = $"SELECT usuCodigo FROM Usuario ORDER BY usuCodigo DESC;";

            DataTable dt = (DataTable)dao.Query(sql);

            // Se der pau no SELECT acima,
            // Retorna -1, que nesse caso seria o return de erro.
            if (dt.Rows.Count == 0)
            {
                return -1;
            }

            return Convert.ToInt32(dt.Rows[0]["usuCodigo"]);
        }
    }
}