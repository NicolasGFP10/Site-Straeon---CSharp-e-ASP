using Datapost.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteStraeon
{
    public partial class AtualizarConta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuEscritor"].ToString() == "True")
            {
                Response.Redirect("~/Default.aspx");
            }
            // Zerar sempre que carregar so por precaucao
            //ZerarCampoCPF();
        }

        protected void RegistrarCpfButton_Click(object sender, EventArgs e)
        {
            // Zerar o alerta sempre que apertar o botao
            // pro alerta anterior nao continuar mesmo sem dar erro
            Alerta.Text = "";

            if (ValidarCPF(CPF.Text))
            {
                // Registraremos o CPF no banco de dados
                string link = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + Server.MapPath("~/app_data/StraeonDB.accdb") + "; Persist Security Info = False";

                DAO dao = new DAO();

                dao.ConnectionString = link;
                dao.DataProviderName = DAO.ProviderName.OleDb;

                // Pegar o nome do usuario para inserir o CPF na DB
                string codigoDoUsuario = Session["usuCodigo"].ToString();

                // Segundo o GPT, no accdb o true = -1 e false = 0
                string sql = $"UPDATE Usuario SET usuEscritor = -1 WHERE usuCodigo = {codigoDoUsuario};";

                dao.Query(sql);

                Session["usuEscritor"] = "True";

                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Alerta.Text = "Digite um CPF válido.";
            }

            
        }

        protected bool ValidarCPF(string cpfUsuario)
        {
            cpfUsuario = cpfUsuario.Replace(".", "").Replace("-", "");

            // Checa o tamanho do texto que o usuario digitou
            // Também verifica se o usuário nao digitou um CPF com todos os números iguais
            if(cpfUsuario.Length != 11 || cpfUsuario.Distinct().Count()==1)
            {
                return false;
            }

            int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpfUsuario.Substring(0, 9);
            int soma = 0;

            for(int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * mult1[i];
            }

            int resto = soma % 11;

            // se o resto < 2, ent ele vai ser 0
            // senao ele vai ser 11-resto 
            resto = resto < 2 ? 0 : 11 - resto;

            tempCpf += resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * mult2[i];
            }

            resto = soma % 11;

            // se o resto < 2, ent ele vai ser 0
            // senao ele vai ser 11-resto 
            resto = resto < 2 ? 0 : 11 - resto;

            return cpfUsuario.EndsWith(resto.ToString());
        }
    }
}