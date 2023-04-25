using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestaoFCT
{
    public partial class GestFCT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Utilizador"] == null)
            {
                //Redirect to login page.
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                //Redirect to home page
                NomeUser.InnerText = Session["Utilizador"].ToString();
            }

            if (rptItems.Items.Count == 0)
            {
                refresh();
            }


        }

        protected void refresh()
        {
            String linhasql = "select * from tabelas_FCT;";
            DataTable dt = Database.GetFromDBSqlSrv(linhasql);

            rptItems.DataSource = dt;
            rptItems.DataBind();
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx"); // redireciona para a página de login
        }



        protected void Atualizar()
        {

            string linhadesql = "select * from tabelas_fct where id_fct = " + labelCod.Text + ";";
            var sqlConn = new SqlConnection(FCTSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                txt_aluno.Text = r["nome_aluno"].ToString();
                ddl_professor.SelectedValue = r["id_professor"].ToString();
                ddl_entidade.SelectedValue = r["id_entidade"].ToString();
                ddl_entidade.SelectedValue = r["id_entidade"].ToString();
                ddl_curso.SelectedValue = r["id_curso"].ToString();
                ddl_tutor.SelectedValue = r["id_tutor"].ToString();
                txt_anoFCT.Value = r["ano_fct"].ToString();
                txt_numHora.Value = r["num_horas"].ToString();
            }
            r.Close();
            sqlConn.Close();
        }

        protected void spanFechar_Click(object sender, EventArgs e)
        {
            exampleModalForm.Visible = false;
        }

        protected void Fechar(object sender, EventArgs e)
        {
            exampleModalForm.Visible = false;

        }



        protected void Editar(object sender, EventArgs e)
        {
            operacao.Text = "2";
            labelCod.Text = HiddenField1.Value;

            if(labelCod.Text != "0")
            {
                string workConn = ConfigurationManager.ConnectionStrings["FCTConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(workConn))
                {
                    string query = "SELECT nome_aluno FROM tabelas_FCT WHERE id_fct = " + labelCod.Text + ";";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        txt_aluno.Text = dt.Rows[0]["nome_aluno"].ToString();
                    }

                    SqlCommand cmd = new SqlCommand("select id_curso, nome_curso from cursos;", con);
                    con.Open();
                    ddl_curso.DataTextField = "nome_curso";
                    ddl_curso.DataValueField = "id_curso";
                    ddl_curso.DataSource = cmd.ExecuteReader();
                    ddl_curso.DataBind();
                    con.Close();

                    SqlCommand cmd2 = new SqlCommand("select id_prof, nome_prof from professores;", con);
                    con.Open();
                    ddl_professor.DataTextField = "nome_prof";
                    ddl_professor.DataValueField = "id_prof";
                    ddl_professor.DataSource = cmd2.ExecuteReader();
                    ddl_professor.DataBind();
                    con.Close();

                    SqlCommand cmd3 = new SqlCommand("select id_entidade, nome_entidade from Entidades;", con);
                    con.Open();
                    ddl_entidade.DataTextField = "nome_entidade";
                    ddl_entidade.DataValueField = "id_entidade";
                    ddl_entidade.DataSource = cmd3.ExecuteReader();
                    ddl_entidade.DataBind();
                    con.Close();

                    SqlCommand cmd4 = new SqlCommand("select id_tutor, nome_tutor from tutores;", con);
                    con.Open();
                    ddl_tutor.DataTextField = "nome_tutor";
                    ddl_tutor.DataValueField = "id_tutor";
                    ddl_tutor.DataSource = cmd4.ExecuteReader();
                    ddl_tutor.DataBind();
                    con.Close();

                }

                Atualizar();
                exampleModalFormTitle.InnerText = "Editar FCT";
                btn_enviar.Text = "Editar FCT";
                exampleModalForm.Visible = true;
            }
            else
            {
                textoCancelar.InnerText = "Nenhum registo foi selecionado!";
                btnDeletar.Visible = false;
                exampleModal.Visible = true;

            }


        }

        protected void Eliminar(object sender, EventArgs e)
        {

            operacao.Text = "3";
            labelCod.Text = HiddenField1.Value;

            if(labelCod.Text != "0")
            {
                btnDeletar.Visible = true;
                string linhadesql = "select nome_tutor from tutores where id_tutor = " + labelCod.Text + ";";
                var sqlConn = new SqlConnection(FCTSQLData.ConnectionString);
                var com = new SqlCommand(linhadesql, sqlConn);
                sqlConn.Open();
                SqlDataReader r = com.ExecuteReader();
                r.Read();
                textoCancelar.InnerText = "Deseja eliminar o registo \"" + r["nome_tutor"] + "\"?";
                r.Close();
                sqlConn.Close();
            }
            else
            {
                textoCancelar.InnerText = "Nenhum registo foi selecionado!";
                btnDeletar.Visible = false;
            }


            exampleModal.Visible = true;


        }

        
        protected void Comandos(object sender, EventArgs e)
        {



            if (operacao.Text == "2")
            {
                //Response.Write("<script>alert('22222')</script>");

                String linhasql = "update tabelas_FCT set id_tutor = '" + ddl_tutor.SelectedValue + "', id_professor = '" + ddl_professor.SelectedValue + "', id_curso = '" + ddl_curso.SelectedValue + "', id_entidade = '" + ddl_entidade.SelectedValue + "', ano_fct = '" + txt_anoFCT.Value + "', num_horas = '" + txt_numHora.Value + "' where id_fct = " + labelCod.Text + ";";

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");
                //Response.Write("<script>alert('aaaaa')</script>");

                Database.NonQuerySqlSrv(linhasql);
                refresh();
                
            }

            if (operacao.Text == "3")
            {
                //Response.Write("<script>alert('33333')</script>");
                String linhasql = "delete from fichasFCT where id_fct = " + labelCod.Text + ";";
                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                Database.NonQuerySqlSrv(linhasql);
                refresh();
            }

            exampleModalForm.Visible = false;
            exampleModal.Visible = false;
        }
        
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            exampleModal.Visible = false;
        }

        protected void ddl_entidade_SelectedIndexChanged(object sender, EventArgs e)
        {

            using (SqlConnection sqlConn = new SqlConnection(FCTSQLData.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select id_tutor, nome_tutor from tutores where id_entidade = " + ddl_entidade.SelectedValue + ";", sqlConn);
                sqlConn.Open();
                ddl_tutor.DataTextField = "nome_tutor";
                ddl_tutor.DataValueField = "id_tutor";
                ddl_tutor.DataSource = cmd.ExecuteReader();
                ddl_tutor.DataBind();
                sqlConn.Close();
            }

        }
    }
}