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

            string Sql_Aluno = "select Nome_Aluno, id_cargo from Alunos where Email_Aluno='" + txt_email.Value.Trim(rem) +
                    "' and SenhaAluno='" + txt_pass.Value.Trim(rem) + "';";

            string Sql_Professor = "select * from Professores where Email_prof='" + txt_email.Value.Trim(rem) +
                    "' and SenhaProf='" + txt_pass.Value.Trim(rem) + "';";

            string Sql_Tutor = "select * from Tutores where Email_tutor='" + txt_email.Value.Trim(rem) +
                    "' and SenhaTutor='" + Convert.ToBase64String(Encoding.ASCII.GetBytes(txt_pass.Value.Trim(rem))) + "';";

            DataTable dt = Database.GetFromDBSqlSrv(Sql_Aluno);

            if (dt.Rows.Count == 1)
            {
                //Response.Write("<script>alert('ele foi')</script>");
                string alo = dt.Rows[0]["Nome_Aluno"].ToString();
                Session["Utilizador"] = dt.Rows[0]["Nome_Aluno"];
                string alo2 = dt.Rows[0]["id_cargo"].ToString();

                Session["cargo"] = dt.Rows[0]["id_cargo"];

                Response.Redirect("~/GestEmp.aspx");
            }
            else {

                dt = Database.GetFromDBSqlSrv(Sql_Professor);

                if(dt.Rows.Count == 1)
                {
                    Session["Utilizador"] = dt.Rows[0]["Nome_prof"];
                    Response.Redirect("~/GestEmp.aspx");
                }
                else
                {
                    dt = Database.GetFromDBSqlSrv(Sql_Tutor);

                    if (dt.Rows.Count == 1)
                    {
                        Session["Utilizador"] = dt.Rows[0]["Nome_tutor"];
                        Response.Redirect("~/GestEmp.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert(' erro ')</script>");
                    }
                }
            
       
            }


        }
    }
    
}