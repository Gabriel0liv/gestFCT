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

            string Sql_login = "select * from tbl_login where email='" + txt_email.Value.Trim(rem) +
        "' and pass ='" + txt_pass.Value.Trim(rem) + "';";

            // SenhaTutor='" + Convert.ToBase64String(Encoding.ASCII.GetBytes(txt_pass.Value.Trim(rem))) + "';";

            DataTable dt = Database.GetFromDBSqlSrv(Sql_login);

            if (dt.Rows.Count == 1)
            {
                //obtem o cargo e ID do utilizador
                Session["cargo"] = dt.Rows[0]["cargo"];
                Session["codigo"] = dt.Rows[0]["id"];

                //Obter o primeiro e ultimo nome
                string nomeCompleto = dt.Rows[0]["Nome"].ToString();
                string[] abreviado = nomeCompleto.Split(' '); // divide a string em palavras utilizando o espaço como separador
                string nome = abreviado[0]; // obtem a primeira palavra do array
                string apelido = abreviado[abreviado.Length - 1]; // obtem a última palavra do array
                
                if (nome != apelido)
                    Session["Utilizador"] = nome + " " + apelido;
                else
                    Session["Utilizador"] = nome;

                if(Session["cargo"].ToString() == "4") // se for um aluno
                {
                    Session["nome"] = dt.Rows[0]["nome"];
                    Session["email"] = dt.Rows[0]["email"];
                    Session["curso"] = dt.Rows[0]["curso"];
                    Session["pass"] = dt.Rows[0]["pass"];
                    Response.Redirect("~/Sumarios.aspx"); //redireciona para pagina de sumários
                }
                if (Session["cargo"].ToString() == "3") // se for um tutor
                {
                    Session["nome"] = dt.Rows[0]["nome"];
                    Session["email"] = dt.Rows[0]["email"];
                    Session["entidade"] = dt.Rows[0]["entidade"];
                    Session["pass"] = dt.Rows[0]["pass"];
                    Response.Redirect("~/Tarefas.aspx"); // redireciona para pagina de tarefas
                }
                if (Session["cargo"].ToString() == "2") //Se for um professor
                {
                    Session["nome"] = dt.Rows[0]["nome"];
                    Session["email"] = dt.Rows[0]["email"];
                    Session["curso"] = dt.Rows[0]["curso"];
                    Session["pass"] = dt.Rows[0]["pass"];
                    Session["direcao"] = dt.Rows[0]["direcao"];
                    Response.Redirect("~/GestAluno.aspx"); // redireciona para pagina de alunos
                }
                if (Session["cargo"].ToString() == "1") // se for um administrador
                {
                    Session["nome"] = dt.Rows[0]["nome"];
                    Session["email"] = dt.Rows[0]["email"];
                    Session["pass"] = dt.Rows[0]["pass"];
                    Response.Redirect("~/GestFCT.aspx"); //redireciona para a pagina de administradores
                }




                

       
            }


        }
    }
    
}