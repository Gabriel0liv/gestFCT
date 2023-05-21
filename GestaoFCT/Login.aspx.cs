using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestaoFCT
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            char[] rem = { '\'', ' ' };

            string Sql_login = "select id, Nome, cargo from tbl_login where email='" + txt_email.Value.Trim(rem) +
        "' and pass ='" + txt_pass.Value.Trim(rem) + "';";

            // SenhaTutor='" + Convert.ToBase64String(Encoding.ASCII.GetBytes(txt_pass.Value.Trim(rem))) + "';";

            DataTable dt = Database.GetFromDBSqlSrv(Sql_login);

            if (dt.Rows.Count == 1)
            {
                //Obter o primeiro e ultimo nome
                string nomeCompleto = dt.Rows[0]["Nome"].ToString();
                string[] abreviado = nomeCompleto.Split(' '); // divide a string em palavras utilizando o espaço como separador
                string nome = abreviado[0]; // obtem a primeira palavra do array
                string apelido = abreviado[abreviado.Length - 1]; // obtem a última palavra do array

                Session["Utilizador"] = nome + " " + apelido;
                Session["codigo"] = dt.Rows[0]["id"];

                Session["cargo"] = dt.Rows[0]["cargo"];

                Response.Redirect("~/GestAluno.aspx");

       
            }


        }
    }
    
}